using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;  

[System.Serializable]
public struct AstroidSpawnRate
   {
      public LootColor Color;
      public float BeginDist, EndDist;
      public int ClusterMin, ClusterMax;
      public float Purity;
      public float ClusterHealth, AsteroidHealth;
   }
public class AsteroidSpawnner : MonoBehaviour
{
   public Vector2 CellSize;
   public float SpawnChancePerCell;
   public float ClusterPartSurvivalChance;
   public float currentDist;

   public GameObject AsteroidClusterPrefab;

   private Transform playerTransform;

   public List<AstroidSpawnRate> AsteroidBands;

   public HashSet<Vector2Int> usedSections;

   // Start is called before the first frame update
   void Start()
   {
      usedSections = new HashSet<Vector2Int>();
      playerTransform = GameObject.Find("Ship").transform;
      TrySpawnAsteroidsAroundCell(Vector2Int.zero);
      savedPlayerCell = TileFromWorldPos(playerTransform.position.ToVector2());

      ShipFuel.OnOutOfFuel.AddListener(ClearAstroidField);
      UpgradesUIActions.OnResetGame.AddListener(() => TrySpawnAsteroidsAroundCell(Vector2Int.zero));
   }

   private Vector2Int savedPlayerCell;
   // Update is called once per frame
   void Update()
   {
      Vector2Int currentPlayerCell = TileFromWorldPos(playerTransform.position.ToVector2());
      if(currentPlayerCell != savedPlayerCell)
      {
         savedPlayerCell = currentPlayerCell;
         TrySpawnAsteroidsAroundCell(currentPlayerCell);
      }
   }

   private void TrySpawnAsteroidsAroundCell(Vector2Int currentPlayerCell)
   {
      for (int i = -4; i <= 4; ++i)
      {
         for (int j = -4; j <= 4; ++j)
         {
            Vector2Int targetSection = new Vector2Int(currentPlayerCell.x + i, currentPlayerCell.y + j);
            if(!usedSections.Contains(targetSection))
            {
               usedSections.Add(targetSection);
               if(Random.value < SpawnChancePerCell)
               {
                  SpawnAstroidForCell(targetSection);
               }
            }
         }
      }
   }

   private void SpawnAstroidForCell(Vector2Int targetSection)
   {
      Vector2 newAstroidLocation = new Vector2((targetSection.x * CellSize.x) + Random.Range(-CellSize.x / 2, CellSize.x / 2), (targetSection.y * CellSize.y) + Random.Range(-CellSize.y / 2, CellSize.y / 2));
      float DistFromSpawn = Vector2.Distance(Vector2.zero, newAstroidLocation);
      currentDist = DistFromSpawn;
      IEnumerable<AstroidSpawnRate> possableAsteroids = AsteroidBands.Where(asteroid => asteroid.BeginDist < DistFromSpawn && asteroid.EndDist > DistFromSpawn).ToList();
      if (!possableAsteroids.Any())
      {
         //Debug.LogError("No available asteroids?");
         return;
      }

      AstroidSpawnRate selected = possableAsteroids.ElementAt(Random.Range(0, possableAsteroids.Count()));

      GameObject newAsteroid = Instantiate(AsteroidClusterPrefab, newAstroidLocation, Quaternion.identity, this.transform);
      newAsteroid.GetComponent<AsteroidCluster>().CreateCluster(Random.Range(selected.ClusterMin, selected.ClusterMax), selected.Color, selected.Purity, ClusterPartSurvivalChance, selected.ClusterHealth, selected.AsteroidHealth);

   }

   private Vector2Int TileFromWorldPos(Vector2 worldPos)
   {
      return new Vector2Int((int)(worldPos.x / CellSize.x), (int)(worldPos.y / CellSize.y));
   }

   public void ClearAstroidField()
   {
      foreach(Transform child in this.transform)
      {
         Destroy(child.gameObject);
      }
      foreach(Transform loot in GameObject.Find("LootParent").transform)
      {
         Destroy(loot.gameObject);
      }
      usedSections = new HashSet<Vector2Int>();
   }

   public void SetClusterBreakupChance(float newChance)
   {
      ClusterPartSurvivalChance = newChance;
   }
   public void SetAstroidSpawnRate(float newRate)
   {
      SpawnChancePerCell = newRate;
   }
}

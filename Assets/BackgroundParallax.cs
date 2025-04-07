using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
   public float ParalaxSpeed;
   public int SpriteLayer;

   public Vector2Int TileBorder;
   public bool RotateSprites;

   [System.Serializable]
   public struct WeightedImage
   {
      public Sprite Prefab;
      public int Weight;
   }
   public List<WeightedImage> AvailableMapTiles;

   //A list of the sprites in the available tiles list where each sprite is repeated based on its weight. This will be used to select from the list.
   private List<Sprite> weightedimagesList;

   private Vector2 imageScale;
   private HashSet<Vector2Int> createdTiles;

   private Vector2 lastFrameTile;
   private Vector3 initalCameraOffset;

   private Transform cameraTransform;

   // Start is called before the first frame update
   void Start()
   {
      weightedimagesList = AvailableMapTiles.SelectMany(wi => Enumerable.Repeat(wi.Prefab, wi.Weight)).ToList();
      imageScale = AvailableMapTiles.First().Prefab.bounds.size.ToVector2();

      cameraTransform = Camera.main.gameObject.transform;

      initalCameraOffset = this.transform.position - cameraTransform.position;
      ResetBackground();

      ShipFuel.OnOutOfFuel.AddListener(ResetBackground);
   }

   public void ResetBackground()
   {
      foreach(Transform tile in this.transform)
      {
         Destroy(tile.gameObject);
      }

      createdTiles = new HashSet<Vector2Int>();
      lastFrameTile = Vector2.zero;
      TryCreateTilesAroundPos(Vector2Int.zero);
   }


   // Update is called once per frame
   void Update()
   {
      Vector3 targetPos = cameraTransform.position * ParalaxSpeed;
      this.transform.position = new Vector3(targetPos.x, targetPos.y, cameraTransform.position.z) + initalCameraOffset;
      Vector2Int currentTileLocation = TileFromWorldPos(cameraTransform.position - this.transform.position);
      if (currentTileLocation != lastFrameTile)
      {
         //we have crossed into a new tile, make sure everything has tiles
         TryCreateTilesAroundPos(currentTileLocation);
         lastFrameTile = currentTileLocation;
      }
   }


   private Vector2Int TileFromWorldPos(Vector2 worldPos)
   {
      return new Vector2Int((int)(worldPos.x / imageScale.x), (int)(worldPos.y / imageScale.y));
   }

   void TryCreateTilesAroundPos(Vector2Int pos)
   {
      for (int i = -TileBorder.x; i <= TileBorder.x; ++i)
      {
         for (int j = -TileBorder.y; j <= TileBorder.y; ++j)
         {
            Vector2Int targetTile = new Vector2Int(pos.x + i, pos.y + j);
            if (!createdTiles.Contains(targetTile))
            {
               CreateTile(PickSprite(), targetTile);
            }
         }
      }
   }

   GameObject CreateTile(Sprite image, Vector2Int gridPos)
   {
      GameObject go = new GameObject($"Tile ({gridPos.x}, {gridPos.y})");
      go.transform.position = this.transform.position + new Vector3(gridPos.x * imageScale.x, gridPos.y * imageScale.y, 0);
      go.transform.rotation = RotateSprites ?
         Quaternion.Euler(0,0, rotations[Random.Range(0,4)]):
         Quaternion.identity;
      go.transform.parent = this.transform;
      SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
      renderer.sprite = image;
      renderer.sortingOrder = SpriteLayer;
      

      createdTiles.Add(gridPos);

      return go;
   }
   private static readonly float[] rotations = { 0, 90, 180, 270 };
   private Sprite PickSprite()
   {
      return weightedimagesList[Random.Range(0, weightedimagesList.Count)];
   }

}

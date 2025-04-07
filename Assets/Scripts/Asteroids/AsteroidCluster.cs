using System.Collections.Generic;
using UnityEngine;

public class AsteroidCluster : MonoBehaviour
{
   public List<GameObject> AsteroidPrefabs;

   private float SurvivalChance;
   private List<GameObject> children;
   private List<Rigidbody2D> childrenBodies;

   private void Awake()
   {
      children = new List<GameObject>(); 
      childrenBodies = new List<Rigidbody2D>();
      //int number = Random.Range(MinInCluster, MaxInCluster);
      //CreateCluster(number, LootColor.Green, .5f);
   }

   public void CreateCluster(int number, LootColor color, float purity, float survivalChance, float health, float asteroidHelath)
   {
      this.SurvivalChance = survivalChance;
      this.GetComponent<AsteroidHealth>().SetHealth(health);
      for (int i = 0; i < number; ++i)
      {
         GameObject child = Instantiate(
            AsteroidPrefabs[Random.Range(0, AsteroidPrefabs.Count - 1)],
            new Vector2(Random.value - .5f, Random.value - .5f) + this.transform.position.ToVector2(),
            Quaternion.Euler(0, 0, Random.Range(0, 360)),
            this.transform);
         children.Add(child);
         childrenBodies.Add(child.GetComponent<Rigidbody2D>());

         //Disable health so that the damage message is only caught by the cluster
         //This feels like a bad way to do this but whatever
         child.GetComponent<AsteroidHealth>().Clustered = true;

         child.GetComponent<LootAstroid>().AsteroidColor = Random.value < purity ? color : LootColor.None;
         child.GetComponent<AsteroidHealth>().SetHealth(asteroidHelath);
      }
      this.GetComponent<Rigidbody2D>().mass *= number;
   }

   private void FixedUpdate()
   {
      foreach(Rigidbody2D child in childrenBodies)
      {
         Vector2 towords = this.transform.position - child.transform.position;
         child.AddForce(Vector2.Max(towords, towords.normalized));
      }
   }

   public void HitByShot(float damage)
   {
      foreach(GameObject child in children)
      {
         if(child.TryGetComponent(out AsteroidHealth hp))
         {
            hp.DamageEffect();
         }
      }
   }
   public void AsteroidDestroyed(bool _)
   {

      foreach(GameObject child in children)
      {
         if(Random.value > SurvivalChance)
         {
            if(child.TryGetComponent(out LootAstroid asteroid))
            {
               asteroid.SpawnSomeCrystals(1);
            }
            child.SendMessage("AsteroidDestroyed", false);
         }
         else
         {
            child.GetComponent<AsteroidHealth>().Clustered = false;
            child.transform.parent = this.transform.parent;
         }
      }
      Destroy(this.gameObject);
   }
}

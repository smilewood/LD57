using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootColor
{
   None,
   Green,
   Blue,
   Yellow,
   Orange,
   Red,
   Purple
}

public class LootAstroid : MonoBehaviour
{
   public GameObject LootPrefab;
   public int MinSpawn, MaxSpawn;
   private Transform lootParent;
   public LootColor AsteroidColor;
   private void Start()
   {
      lootParent = GameObject.Find("LootParent").transform;

      GetComponent<Animator>().SetTrigger(AsteroidColor.ToString());
   }

   public void AsteroidDestroyed(bool dropLoot)
   {
      //TODO: make this reflect the selected color
      if(LootPrefab != null && AsteroidColor != LootColor.None && dropLoot)
      {
         int number = Random.Range(UpgradeStatus.Instance.MinCrystalsPerAsteroid, UpgradeStatus.Instance.MaxCrystalsPerAsteroid);
         for(int i = 0; i < number; ++i)
         {
            GameObject loot = Instantiate(LootPrefab, this.transform.position, this.transform.rotation, lootParent);
            loot.GetComponent<Animator>().SetTrigger(AsteroidColor.ToString());
            loot.GetComponent<PickupLoot>().PickupType = AsteroidColor;
         }
      }
      Destroy(this.gameObject);
   }

   public void SpawnSomeCrystals(int number)
   {
      if (LootPrefab != null && AsteroidColor != LootColor.None)
      {
         for (int i = 0; i < number; ++i)
         {
            GameObject loot = Instantiate(LootPrefab, this.transform.position, this.transform.rotation, lootParent);
            loot.GetComponent<Animator>().SetTrigger(AsteroidColor.ToString());
            loot.GetComponent<PickupLoot>().PickupType = AsteroidColor;
         }
      }
   }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStatus
{
   public static UpgradeStatus Instance
   {
      get
      {
         if(_instance == null)
         {
            _instance = new UpgradeStatus();
         }
         return _instance;
      }
   }
   private static UpgradeStatus _instance;

   public struct ShotStats
   {
      public float Damage, Speed, Lifetime;
   }
   public ShotStats CurrentShotStats;

   public GameObject CurrentShotPrefab { get; set; }

   public float CurrentFireRate { get; set; }

   public float DailyFuel { get; set; }

   public int MinCrystalsPerAsteroid { get; set; }
   public int MaxCrystalsPerAsteroid { get; set; }
   public float ThrusterPower{ get; set; }
   public float FuelConsumptionRate{ get; set; }
   public float RotationSpeed{ get; set; }

}

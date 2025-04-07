using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static AsteroidSpawnner;

public class ResetGameEvent : UnityEvent
{
}
public class UpgradesUIActions : MonoBehaviour
{
   public GameObject InitialShotPrefab;
   public float InitialFireRate, InitialThrustPower, InitialFuelRate, InitialRotationSpeed;
   public GameObject ShipVaccum;
   public int InitailMinDrops, InitialMaxDrops;
   private AsteroidSpawnner asteroidSpawnner;

   public static ResetGameEvent OnResetGame
   {
      get
      {
         if (_onResetGame == null)
         {
            _onResetGame = new ResetGameEvent();
         }
         return _onResetGame;
      }
   }
   private static ResetGameEvent _onResetGame;

   private void Start()
   {
      SetShotPrefab(InitialShotPrefab);
      asteroidSpawnner = GameObject.Find("AsteroidSpawnner").GetComponent<AsteroidSpawnner>();
      SetFireRate(InitialFireRate);
      ChangeAsteroidMinDrops(InitailMinDrops);
      ChangeAsteroidMaxDrops(InitialMaxDrops);
      SetThrustPower(InitialThrustPower);
      SetFuelRate(InitialFuelRate);
      SetRotationSpeed(InitialRotationSpeed);
      this.gameObject.SetActive(false);
   }

   public void SetShotPrefab(GameObject newprefab)
   {
      UpgradeStatus.Instance.CurrentShotPrefab = newprefab;
      if(newprefab.TryGetComponent<BasicShot>(out var shot))
      {
         UpgradeStatus.Instance.CurrentShotStats = new UpgradeStatus.ShotStats { Damage = shot.ShotDamage, Lifetime = shot.ShotLifetime, Speed = shot.ShotSpeed };
      }
   }

   public void StartNewDay()
   {
      OnResetGame.Invoke();
   }

   public void AddAsteroidBand(GameObject bandHolder)
   {
      asteroidSpawnner.AsteroidBands.Add(bandHolder.GetComponent<AsteroidBandHolder>().AsteroidBand);
   }

   public void AddShotDamage(float delta)
   {
      UpgradeStatus.Instance.CurrentShotStats.Damage += delta;
   }
   public void AddShotSpeed(float delta)
   {
      UpgradeStatus.Instance.CurrentShotStats.Speed += delta;
   }
   public void SetFireRate(float newRate)
   {
      UpgradeStatus.Instance.CurrentFireRate = newRate;
   }

   public void AddFuel(float delta)
   {
      UpgradeStatus.Instance.DailyFuel += delta;
   }

   public void IncreasePullRange(float increase)
   {
      ShipVaccum.GetComponent<CircleCollider2D>().radius += increase;
   }

   public void IncreasePullPower(float increase)
   {
      ShipVaccum.GetComponent<PickupVaccum>().SuckForce += increase;
   }

   public void ChangeAsteroidMinDrops(int newMin)
   {
      UpgradeStatus.Instance.MinCrystalsPerAsteroid = newMin;
   }
   public void ChangeAsteroidMaxDrops(int newMax)
   {
      UpgradeStatus.Instance.MaxCrystalsPerAsteroid = newMax;
   }
   public void SetThrustPower(float newPower)
   {
      UpgradeStatus.Instance.ThrusterPower = newPower;
   }
   public void SetFuelRate(float rate)
   {
      UpgradeStatus.Instance.FuelConsumptionRate = rate;
   }
   public void SetRotationSpeed(float speed)
   {
      UpgradeStatus.Instance.RotationSpeed = speed;
   }
}

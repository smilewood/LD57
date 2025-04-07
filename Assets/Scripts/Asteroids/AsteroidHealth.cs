using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour
{
   public float StartingHealth;
   public GameObject EffectPrefab;

   public float currentHealth;

   public bool Clustered = false;
   private SpriteRenderer image;
   private Transform EffectParent;
   // Start is called before the first frame update
   void Start()
   {
      this.TryGetComponent(out image);
      EffectParent = GameObject.Find("AsteroidSpawnner").transform;
   }


   public void HitByShot(float damage)
   {
      if (!Clustered)
      {
         if (image != null)
         {
            StartCoroutine(FlashRed(.2f));
         }
         currentHealth -= damage;
         if (currentHealth <= 0)
         {
            SendMessage("AsteroidDestroyed", true);
         }
      }
   }

   public void AsteroidDestroyed(bool _)
   {
      if(EffectPrefab != null)
      {
         Instantiate(EffectPrefab, this.transform.position, Quaternion.identity, EffectParent);
      }
   }

   internal void SetHealth(float health)
   {
      this.currentHealth = health;
   }
   public void DamageEffect()
   {
      StartCoroutine(FlashRed(.2f));
   }
   public IEnumerator FlashRed(float time)
   {
      image.color = Color.red;
      float countup = 0;
      while(countup < time)
      {
         yield return 0;
         image.color = Color.Lerp(Color.red, Color.white, countup / time);
         countup += Time.deltaTime;
      }
      image.color = Color.white; 
   }
}

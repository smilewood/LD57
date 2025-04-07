using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BasicShot : MonoBehaviour
{
   public float ShotSpeed, ShotDamage, ShotLifetime;
   public GameObject ShotExplodeEffect;

   // Start is called before the first frame update
   void Start()
   {
      GetComponent<Rigidbody2D>().velocity += this.transform.up.ToVector2() * ShotSpeed;
      StartCoroutine(ShotTimeout(ShotLifetime));

      ShotSpeed = UpgradeStatus.Instance.CurrentShotStats.Speed;
      ShotDamage = UpgradeStatus.Instance.CurrentShotStats.Damage;
      ShotLifetime = UpgradeStatus.Instance.CurrentShotStats.Lifetime;
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      if (collision.gameObject.CompareTag("Asteroid"))
      {
         collision.gameObject.SendMessageUpwards("HitByShot", ShotDamage);
         Instantiate(ShotExplodeEffect, this.transform.position, Quaternion.identity, this.transform.parent);
         DestroyShot();
      }
   }

   private void DestroyShot()
   {
      StopAllCoroutines();
      Destroy(this.gameObject);
   }

   private IEnumerator ShotTimeout(float timer)
   {
      yield return new WaitForSeconds(timer);
      DestroyShot();
   }
}

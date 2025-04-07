using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipShootController : MonoBehaviour
{
   //Firerate in shots/sec
   public Transform ShotParent;


   private bool canShoot = true;
   private Rigidbody2D shipBody;
   // Start is called before the first frame update
   void Start()
   {
      shipBody = GetComponentInParent<Rigidbody2D>();
      
      ShipFuel.OnOutOfFuel.AddListener(() => { StopAllCoroutines(); canShoot = false; });
      UpgradesUIActions.OnResetGame.AddListener(() => canShoot = true);
   }

   // Update is called once per frame
   void Update()
   {
      if (canShoot && Input.GetButton("Fire1"))
      {
         GameObject newShot = Instantiate(UpgradeStatus.Instance.CurrentShotPrefab, this.transform.position, this.transform.rotation, ShotParent);
         newShot.GetComponent<Rigidbody2D>().velocity += shipBody.velocity;
         StartCoroutine(ShotCooldown(1 / UpgradeStatus.Instance.CurrentFireRate));
      }
   }


   private IEnumerator ShotCooldown(float delay)
   {
      canShoot = false;
      yield return new WaitForSeconds(delay);
      canShoot = true;
   }
}

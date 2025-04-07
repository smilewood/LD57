using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PickupLoot : MonoBehaviour
{
   public LootColor PickupType;
   private PickupVaccum activeVaccum;
   private Rigidbody2D pickupBody;


   private void Start()
   {
      pickupBody = GetComponent<Rigidbody2D>();
   }

   private void FixedUpdate()
   {
      if(activeVaccum != null)
      {
         pickupBody.AddForce((activeVaccum.transform.position - this.transform.position).normalized * activeVaccum.SuckForce);
      }
   }

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if(collision.gameObject.TryGetComponent(out PickupVaccum vaccum))
      {
         activeVaccum = vaccum;
      }
   }

   private void OnTriggerExit2D(Collider2D collision)
   {
      if (collision.gameObject.TryGetComponent(out PickupVaccum vaccum))
      {
         if(activeVaccum == vaccum)
         {
            activeVaccum = null;
         }
      }
   }
}

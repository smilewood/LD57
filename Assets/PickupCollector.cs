using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCollector : MonoBehaviour
{

   private void OnTriggerEnter2D(Collider2D collision)
   {
      if(collision.gameObject.TryGetComponent(out PickupLoot loot))
      {
         DailyResourceTracker.OnPickupCollected.Invoke(loot.PickupType);
         Destroy(collision.gameObject);
      }
   }

}

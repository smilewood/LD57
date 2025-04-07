using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompleteDayUI : MonoBehaviour
{
   public DailyResourceTracker dailyTracker;
   public static Dictionary<LootColor, int> TotalAvailableCrystal = Enum.GetValues(typeof(LootColor)).Cast<LootColor>().ToDictionary(c => c, c => 0);
   public RectTransform DailyReport;
   public GameObject ReportLinePrefab;

   public GameObject OutOfFuelMessage, DeliveryCompleteMessage;
   private void Start()
   {
      ShipFuel.OnOutOfFuel.AddListener(() => { this.gameObject.SetActive(true);  DayComplete(true); });
      this.gameObject.SetActive(false);
   }

   private void OnEnable()
   {
      //The day has ended when this is enabled, grab any loot from the daily tracker

      
   }

   public void DayComplete(bool outOfFuel)
   {
      foreach (Transform t in DailyReport)
      {
         Destroy(t.gameObject);
      }
      if (outOfFuel)
      {
         OutOfFuelMessage.SetActive(true);
         DeliveryCompleteMessage.SetActive(false);
      }
      else
      {
         OutOfFuelMessage.SetActive(false);
         DeliveryCompleteMessage.SetActive(true);


         foreach (LootColor c in dailyTracker.DailyPickups.Keys.OrderBy(k => (int)k))
         {
            TotalAvailableCrystal[c] += dailyTracker.DailyPickups[c];

            if (dailyTracker.DailyPickups[c] != 0 || TotalAvailableCrystal[c] != 0)
            {
               GameObject reportLine = Instantiate(ReportLinePrefab, DailyReport);
               reportLine.GetComponent<DailyReportLine>().UpdateLine(c, dailyTracker.DailyPickups[c], TotalAvailableCrystal[c]);
            }
         }

      }

      dailyTracker.ClearDailyPickupRecord();
   }

}

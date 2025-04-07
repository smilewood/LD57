using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DailyReportLine : MonoBehaviour
{
   public TMP_Text DayNumber, TotalNumber;
   public Animator crystalImage;

   internal void UpdateLine(LootColor color, int today, int total)
   {
      DayNumber.text = today.ToString();
      TotalNumber.text = total.ToString();
      crystalImage.SetTrigger(color.ToString());
   }
}

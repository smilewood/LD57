using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class PickupCollectedEvent : UnityEvent<LootColor> {}

public class DailyResourceTracker : MonoBehaviour
{
   public Dictionary<LootColor, int> DailyPickups;
   private Dictionary<LootColor, GameObject> lootUIDict;
   public GameObject LootUIPrefab;
   public Transform LootUIParent;

   public static PickupCollectedEvent OnPickupCollected
   {
      get
      {
         if (_onPickupCollected == null)
         {
            _onPickupCollected = new PickupCollectedEvent();
         }
         return _onPickupCollected;
      }
   }
   private static PickupCollectedEvent _onPickupCollected;

   // Start is called before the first frame update
   void Start()
   {
      DailyPickups = new Dictionary<LootColor, int>();
      lootUIDict = new Dictionary<LootColor, GameObject>();
      foreach (LootColor color in Enum.GetValues(typeof(LootColor)))
      {
         if(color == LootColor.None)
         {
            //dont bother setting up for none, there are not collectables for None but I needed it for an animation buffer
            continue;
         }
         DailyPickups.Add(color, 0);
         GameObject newUIElement = Instantiate(LootUIPrefab, LootUIParent);
         newUIElement.GetComponentInChildren<Animator>().SetTrigger(color.ToString());
         newUIElement.SetActive(false);
         lootUIDict.Add(color, newUIElement);
      }

      OnPickupCollected.AddListener(PickupColleted);
   }

   public void PickupColleted(LootColor color)
   {
      ++DailyPickups[color];
      UpdatePickupUI(color);
   }

   public void UpdatePickupUI(LootColor color)
   {
      if (DailyPickups[color] == 0)
      {
         lootUIDict[color].SetActive(false);
      }
      else
      {
         if (!lootUIDict[color].activeSelf)
         {
            lootUIDict[color].SetActive(true);
            lootUIDict[color].GetComponentInChildren<Animator>().SetTrigger(color.ToString());
         }
         lootUIDict[color].GetComponentInChildren<TMP_Text>().text = DailyPickups[color].ToString();
      }
   }

   public void ClearDailyPickupRecord()
   {
      foreach(LootColor c in Enum.GetValues(typeof(LootColor)))
      {
         if(c != LootColor.None)
         {
            DailyPickups[c] = 0;
            UpdatePickupUI(c);
         }
      }
   }


}

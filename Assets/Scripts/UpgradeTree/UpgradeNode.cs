using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UpgradeNode : MonoBehaviour, IPointerClickHandler
{
   public List<UpgradeTier> Upgrades;
   public int currentTier = 0;
   public TMP_Text DescriptionText;
   public Animator CrystalPicker;
   public TMP_Text CostText;

   private bool CanAfford = true;
   [System.Serializable]
   public struct UpgradeTier
   {
      public LootColor CostColor;
      public int CostAmmount;
      public UnityEvent TierUnlockEvent;
      [TextArea]
      public string UpgradeDescription;
   }

   private void OnEnable()
   {
      UpdateStuff();
   }

   public void OnPointerClick(PointerEventData eventData)
   {
      if(currentTier < Upgrades.Count && CanAfford)
      {
         //Check for actually having the resources somehow


         Upgrades[currentTier].TierUnlockEvent.Invoke();
         CompleteDayUI.TotalAvailableCrystal[Upgrades[currentTier].CostColor] -= Upgrades[currentTier].CostAmmount;
         ++currentTier;

         if(currentTier == Upgrades.Count)
         {
            //Done upgrading, hide the cost and description
            foreach(Transform t in this.transform)
            {
               if(!(t.gameObject.name == "ParentLine"))
               {
                  t.gameObject.SetActive(false);
               }
            }
         }
         else
         {
            UpdateStuff();
         }
      }

   }

   private void UpdateStuff()
   {
      DescriptionText.text = Upgrades[currentTier].UpgradeDescription;
      CrystalPicker.SetTrigger(Upgrades[currentTier].CostColor.ToString());
      CostText.text = Upgrades[currentTier].CostAmmount.ToString();
      CanAfford = CompleteDayUI.TotalAvailableCrystal[Upgrades[currentTier].CostColor] >= Upgrades[currentTier].CostAmmount;
      CostText.color = CanAfford ? Color.white : Color.red;


   }
}

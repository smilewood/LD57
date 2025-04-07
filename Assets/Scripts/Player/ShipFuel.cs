using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OutOfFuelEvent : UnityEvent {}

public class ShipFuel : MonoBehaviour
{
   public float StartingFuel;
   public float currentFuel;
   public RectTransform IndicatorBar;
   //in fuel/sec of thrust
   
   private float barStartWidth;
   public static OutOfFuelEvent OnOutOfFuel
   {
      get
      {
         if(_onOutOfFuel == null)
         {
            _onOutOfFuel = new OutOfFuelEvent();
         }
         return _onOutOfFuel;
      }
   }
   private static OutOfFuelEvent _onOutOfFuel;

   internal void ThrusterThrusting()
   {
      currentFuel -= UpgradeStatus.Instance.FuelConsumptionRate * Time.deltaTime;
      IndicatorBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barStartWidth * (currentFuel / StartingFuel));
   }

   // Start is called before the first frame update
   void Start()
   {
      UpgradesUIActions.OnResetGame.AddListener(ResetFuel);
      barStartWidth = IndicatorBar.rect.width;
      UpgradeStatus.Instance.DailyFuel = StartingFuel;
      ResetFuel();
   }

   private void ResetFuel()
   {
      StartingFuel = currentFuel = UpgradeStatus.Instance.DailyFuel;
      IndicatorBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barStartWidth * (currentFuel / StartingFuel));
   }

   // Update is called once per frame
   void Update()
   {
      if(currentFuel < 0)
      {
         //day is over, reset things
         GetComponent<Rigidbody2D>().velocity = Vector2.zero;
         OnOutOfFuel.Invoke();
         currentFuel = float.MaxValue;
      }
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ThrustDirection
{
   Forward, Backward, Left, Right
}

public class ShipThruster : MonoBehaviour
{
   public ThrustDirection Direction;
   public float ThrustPower;

   private Rigidbody2D shipBody;
   private ParticleSystem thrustParts;
   private ShipFuel fueltank;
   private bool thrustDisabled = false;

   // Start is called before the first frame update
   void Start()
   {
      shipBody = GetComponentInParent<Rigidbody2D>();
      thrustParts = GetComponentInChildren<ParticleSystem>();
      fueltank = GetComponentInParent<ShipFuel>();
      ShipFuel.OnOutOfFuel.AddListener(() => thrustDisabled = true);
      UpgradesUIActions.OnResetGame.AddListener(() => thrustDisabled = false);
   }
   // Update is called once per frame
   void Update()
   {
      if (thrustDisabled)
      {
         return;
      }
      bool thrusting = false;
      switch (Direction)
      {
         case ThrustDirection.Forward:
         {
            if (Input.GetButton("ThrustForward"))
            {
               thrusting = true;
            }
            break;
         }
         case ThrustDirection.Backward:
         {
            if (Input.GetButton("ThrustBackward"))
            {
               thrusting = true;
            }
            break;
         }
         case ThrustDirection.Left:
         {
            //This is backwards and it is stupid but I think it is a physics result
            if (Input.GetButton("ThrustRight"))
            {
               thrusting = true;
            }
            break;
         }
         case ThrustDirection.Right:
         {
            if (Input.GetButton("ThrustLeft"))
            {
               thrusting = true;
            }
            break;
         }
      }

      if(thrusting)
      {
         if (!thrustParts.isPlaying)
         {
            thrustParts.Play();
         }
         fueltank.ThrusterThrusting();
      }
      else
      {
         if (thrustParts.isPlaying)
         {
            thrustParts.Stop();
         }
      }
      
      shipBody.AddForceAtPosition(this.transform.up.ToVector2() * (thrusting ? UpgradeStatus.Instance.ThrusterPower : 0), this.transform.position.ToVector2());
   }
}

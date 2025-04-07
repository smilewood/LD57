using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   public Transform Target;
   public float TetherStrength;

   private Vector3 offset;

   // Start is called before the first frame update
   void Start()
   {
      offset = this.transform.position - Target.transform.position;
      UpgradesUIActions.OnResetGame.AddListener(() =>
      {
         //Force reset to the start (this only works because the ship starts at 0,0)
         this.transform.position = offset;
      });
   }


   // Update is called once per frame
   void Update()
   {
      Vector3 targetPos = Target.position + offset;
      Vector3 moveVector = targetPos - this.transform.position;
      this.transform.position = Vector3.Lerp(this.transform.position, targetPos, TetherStrength * moveVector.magnitude * Time.deltaTime);
   }
}

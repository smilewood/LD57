using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetShipOnNewDay : MonoBehaviour
{
   // Start is called before the first frame update
   void Start()
   {
      UpgradesUIActions.OnResetGame.AddListener(() => this.transform.position = Vector2.zero);
   }

}

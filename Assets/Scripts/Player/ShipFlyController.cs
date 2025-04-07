using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipFlyController : MonoBehaviour
{



   // Update is called once per frame
   void Update()
   {
      Vector3 mousePos = Input.mousePosition;

      Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
      mousePos.x -= objectPos.x;
      mousePos.y -= objectPos.y;

      float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

      transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), UpgradeStatus.Instance.RotationSpeed * Time.deltaTime);
   }
}

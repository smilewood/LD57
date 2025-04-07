using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Helpers
{
   public static Vector2 ToVector2(this Vector3 target)
   {
      return new Vector2(target.x, target.y);
   }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteInEditMode]
public class LinkedUpgradeNode : MonoBehaviour
{
   public Transform LinkedNode;
   
   // Start is called before the first frame update
   void Start()
   {
      //UILineRenderer connectingLine = GetComponentInChildren<UILineRenderer>();
      //connectingLine.points = new Vector2[] { Vector2.zero, LinkedNode.position - this.transform.position };
   }


   // Update is called once per frame
   void Update()
   {
#if UNITY_EDITOR
      if (!Application.isPlaying)
      {
         UILineRenderer connectingLine = GetComponentInChildren<UILineRenderer>();
         connectingLine.points = new Vector2[] { Vector2.zero, LinkedNode.position - this.transform.position };
         connectingLine.SetAllDirty();
      }
#endif
   }
}

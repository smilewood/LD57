using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateDistanceText : MonoBehaviour
{

   private TMP_Text text;
   public Transform target;
   // Start is called before the first frame update
   void Start()
   {
      text = this.GetComponent<TMP_Text>();
   }

   // Update is called once per frame
   void Update()
   {
      text.text = target.position.magnitude.ToString("0");
   }
}

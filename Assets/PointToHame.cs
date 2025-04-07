using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PointToHame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      float angle = Mathf.Atan2(this.transform.position.y, this.transform.position.x) * Mathf.Rad2Deg;
      this.transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }
}

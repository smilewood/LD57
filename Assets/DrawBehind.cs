using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBehind : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
      this.transform.SetParent(this.transform.parent.parent, true);
      this.transform.SetAsFirstSibling();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

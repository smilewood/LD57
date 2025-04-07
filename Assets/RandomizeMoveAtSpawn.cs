using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class RandomizeMoveAtSpawn : MonoBehaviour
{
   public float VelocityPower, SpinPower;
   // Start is called before the first frame update
   void Start()
   {
      Rigidbody2D rb = GetComponent<Rigidbody2D>();
      rb.velocity = new Vector2(Random.Range(-VelocityPower, VelocityPower), Random.Range(-VelocityPower, VelocityPower));
      rb.AddTorque(Random.Range(-SpinPower, SpinPower));
   }

}

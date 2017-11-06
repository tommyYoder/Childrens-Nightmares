using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTimmer : MonoBehaviour {


    public float timeDetroyed = 2f;

    void Start()
    {
        Destroy(gameObject, timeDetroyed);                 // Will destroy game object after timeDestroyed is true. 
    }
}

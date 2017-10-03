using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFacingBillboard : MonoBehaviour {

    public Camera m_Camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward, m_Camera.transform.rotation * Vector3.up);       // Updates camera's transform position with the vector 3 forward and up variables to allow sliders to be seen by the player even when the enemy is turning in the game level. 
            	
	}
}

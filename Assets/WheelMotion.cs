using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelMotion : MonoBehaviour {

    public WheelCollider wheel;
    private Vector3 wheelpos = new Vector3();
    private Quaternion rotation = new Quaternion();
    
	
	// Update is called once per frame
	void Update () {
        wheel.GetWorldPose(out wheelpos, out rotation); // gets the wheelposition and rotation based off the wheel colliders
        transform.position = wheelpos;
        transform.rotation = rotation;
	}
}

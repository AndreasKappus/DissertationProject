using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

    public Transform path; // this calls the Path.cs code
    public Vector3 centreMass;
    public Rigidbody car;

    [Header("Movement and Turning")]
    public bool isBraking = false;
    public float maxSteerAngle = 45f; // set the maximum steering angle to 45 for relatively authentic turning
    public float steerSpeed = 5f;
    public float maxMotorTorque = 40;
    public float maxBrakeTorque = 100;
    public float currentSpeed; // this can be used for the GUI application which could become thread from the GUI to get the current speed during runtime
    public float maxSpeed = 20; // adjust until suitable speed found
    public float reverseTorque = -20;

    [Header("Wheels")]
    public WheelCollider RR_wheel; // declaration for the wheel colliders, so they can be run and called whithin the simulation to set speeds, torque etc
    public WheelCollider RL_wheel;
    public WheelCollider FR_wheel;
    public WheelCollider FL_wheel;

    [Header("Sensors")]
    [HideInInspector] public float shortSensorLength = 1f;
    [HideInInspector] public Vector3 frontSensorPos = new Vector3(0.5f, 0.02f, 0f);
    [HideInInspector] public float frontSideSensor = 0.1f;

    [HideInInspector]public List<Transform> nodes;
    [HideInInspector] public int currentNode = 0;
    [HideInInspector] public bool avoiding = false;
    [HideInInspector] public float avoid = 0;

    private Controller controller;


    void Start () {
        GetComponent<Rigidbody>().centerOfMass = centreMass;

        Transform[] paths = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();
        
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i] != path.transform)
            {
                nodes.Add(paths[i]); // adds paths if the amount is not equal to the amount created in the simulation
            }
        }
        
        

    }

    void FixedUpdate() {

        this.GetComponent<Controller>().RunController();
          

    }
   
}

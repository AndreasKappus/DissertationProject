using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour {

    public Transform path; // this calls the Path.cs code
    public Vector3 centreMass;

    public Rigidbody car;

    [Header("Movement and Turning")]
    public bool isBraking = false;
    public float maxSteerAngle = 45f; // set the maximum steering angle to 45 for authentic-ish turning
    public float steerSpeed = 5f;
    public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float currentSpeed; // this can be used for the GUI application which could become thread from the GUI to get the current speed during runtime
    public float maxSpeed = 20; // adjust until suitable speed found

    [Header("Wheels")]
    public WheelCollider RR_wheel; // declaration for the wheel colliders, so they can be run and called whithin the simulation to set speeds, torque etc
    public WheelCollider RL_wheel;
    public WheelCollider FR_wheel;
    public WheelCollider FL_wheel;

    [Header("Sensors")]
    public float sideSensorLength = 2f;
    public float sensorLength = 4f; // Max distance for sensor
    public float shortSensorLength = 1f;
    public Vector3 frontSensorPos = new Vector3(0f, 0.02f, 0.5f);
    public Vector3 SideSensorPos = new Vector3(0f, 0.02f, 0f);
    public Vector3 rearSensorPos = new Vector3(0f, 0.02f, 0.5f);
    public float frontSideSensor = 0.1f;
    public float frontSensorAngle = 15f; // 15 degree angle



    public List<Transform> nodes;
    public int currentNode = 0;
    public bool avoiding = false;
    public float avoid = 0;

    private Actions actions;

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

    // Update is called once per frame
    void FixedUpdate() {
        setAction(new Accelerate(this));
        setAction(new Avoid(this));
        setAction(new Steer(this));
        setAction(new CheckWaypoint(this));
        //setAction(new NewKFilter(this));
        Time.timeScale = 0.5f;
        RaycastHit hit;
        Vector3 startpos = transform.position;
        startpos += transform.forward * frontSensorPos.z; // transform the z axis to match the car as it moves
        startpos += transform.up * frontSensorPos.y;
        avoid = 0; // avoid value to multiply by the steer angle, a simple method for collision avoidance
        avoiding = false;

        // front centre
        if (Physics.Raycast(startpos, transform.forward, out hit, shortSensorLength))
        {
            // Lable the certain colliders as "Obstacle" so collision avoidance would work in that environment
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red); // ensures that raycast is working by drawing a line from the car to the object
                avoiding = true;
                if (hit.normal.x < 0)
                {
                    avoid = -1;
                    
                }
                else
                {
                    
                    avoid = 1;
                }
            }
        }

        // Rear Sensor
        if (Physics.Raycast(startpos, -transform.forward, out hit, shortSensorLength))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                avoiding = true;
                if (hit.normal.x > 0)
                {
                    avoid = 1;
                }
                else
                {
                    avoid = -1;
                }
            }
        }

        // front right sensor

        startpos += transform.right * frontSideSensor;
        if (Physics.Raycast(startpos, transform.forward, out hit, shortSensorLength))
        {
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                avoiding = true;
                avoid -= 1f;
            }
        }

        // front left sensor
        startpos -= transform.right * frontSideSensor * 2; 
        if (Physics.Raycast(startpos, transform.forward, out hit, shortSensorLength))
        {
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                avoiding = true;
                avoid -= 1f;
            }
        }

        //if (avoiding) // avoiding is set to true when the raycast hits an object
        //{
        //    FL_wheel.steerAngle = maxSteerAngle * avoid;
        //    FR_wheel.steerAngle = maxSteerAngle * avoid;

        //}

    }

    public void setAction(Actions _action)
    {
        actions = _action;
        actions.doAction();

        //gameObject.name = "Action - " + actions.GetType().Name;
    }

}

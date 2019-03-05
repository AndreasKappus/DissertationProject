using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OGEngine : MonoBehaviour
{

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
    public float maxSpeed = 50; // adjust until suitable speed found

    [Header("Wheels")]
    public WheelCollider RR_wheel; // declaration for the wheel colliders, so they can be run and called whithin the simulation to set speeds, torque etc
    public WheelCollider RL_wheel;
    public WheelCollider FR_wheel;
    public WheelCollider FL_wheel;

    [Header("Sensors")]
    public float sideSensorLength = 2f;
    public float sensorLength = 4f; // Max distance for sensor
    public float shortSensorLength = 2f;
    public Vector3 frontSensorPos = new Vector3(0f, 0.02f, 0.5f);
    public Vector3 SideSensorPos = new Vector3(1.5f, 0.02f, 0f);
    public Vector3 rearSensorPos = new Vector3(0f, 0.02f, 0.5f);
    public float frontSideSensor = 0.1f;
    public float frontSensorAngle = 15f; // 15 degree angle



    private List<Transform> nodes;
    private int currentNode = 0;
    public bool avoiding = false;



    void Start()
    {

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

    void FixedUpdate() // calling all the relevant methods
    {
        Sensors();
        Steer();
        Drive();
        checkDistance();
        Brake();
        

    }

    private void Sensors()
    {
        RaycastHit hit;
        Vector3 startpos = transform.position;
        startpos += transform.forward * frontSensorPos.z; // transform the z axis to match the car as it moves
        startpos += transform.up * frontSensorPos.y;
        float avoid = 0; // avoid value to multiply by the steer angle, a simple method for collision avoidance
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
        if (Physics.Raycast(startpos, transform.forward, out hit, sensorLength))
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
        if (Physics.Raycast(startpos, transform.forward, out hit, sensorLength))
        {
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                avoiding = true;
                avoid += 1f;
            }
        }

        if (avoiding) // avoiding is set to true when the raycast hits an object
        {
            FL_wheel.steerAngle = maxSteerAngle * avoid;
            FR_wheel.steerAngle = maxSteerAngle * avoid;

        }

    }


    public void Steer()
    {
        if (avoiding) return;
        Vector3 relativePos = transform.InverseTransformPoint(nodes[currentNode].position); // pretty much makes the car turn towards the nodes in the path object
        float steer = (relativePos.x / relativePos.magnitude) * maxSteerAngle; // divides the x axis of the position by the length of the position, then multiply by the steer angle to turn

        FL_wheel.steerAngle = Mathf.Lerp(FL_wheel.steerAngle, steer, Time.deltaTime * steerSpeed); // added more realistic steering
        FR_wheel.steerAngle = Mathf.Lerp(FR_wheel.steerAngle, steer, Time.deltaTime * steerSpeed);

    }

    public void Drive()
    {
        currentSpeed = 2 * Mathf.PI * FL_wheel.radius * FL_wheel.rpm; // speed = 2 * 3.14 * radius of wheel * revs per minute

        if (currentSpeed < maxSpeed && !isBraking) // if car not braking and less than the maximum speed, speed up
        {
            FL_wheel.motorTorque = maxMotorTorque;
            FR_wheel.motorTorque = maxMotorTorque;
        }
        else // if car is braking, stop
        {
            FL_wheel.motorTorque = 0;
            FR_wheel.motorTorque = 0;
        }
    }

    public void checkDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1f) // checks the distance from the car to the current node's position
        {
            if (currentNode == nodes.Count - 1) // check if its on the last node, if so then, go back to start node, this enables the waypoint objects to be constantly looping around
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    public void Brake() // STOP DE CAR
    {
        if (isBraking)
        {
            RL_wheel.motorTorque = 0;
            RR_wheel.motorTorque = 0;

        }
        else
        {
            RL_wheel.brakeTorque = 0;
            RR_wheel.brakeTorque = 0;
        }
    }

    //KALMAN FILTER CODE

    // gets position and uses random numbers to replace the pos variable's values through a certain range, so the kalman filter can get the measurements .etc
    /*
    float sensorNoise()
    {
        float actual_x = car.position.x;
        float actual_z = car.position.z;

        // simulate gps sensor noise through random numbers. 
        float pos_x = Random.Range(actual_x - 10, actual_x + 10); // The range of the numbers are the actual position - or + 10.

        return pos_x;
    }
    
    List<float> kalmanUpdate(float mean1, float var1, float mean2, float var2)
    {
        float new_mean = (var2 * mean1 + var1 * mean2) / (var1 + var2);
        float new_var = 1 / (1 / var1 + 1 / var2);

        List<float> return_update = new List<float>();
        return_update.Add(new_mean);
        return_update.Add(new_var);

        print(return_update[0]);
        print(return_update[1]);
        return return_update;
    }

    List<float> kalmanPredict(float mean1, float var1, float mean2, float var2)
    {
        float new_mean = mean1 + mean2;
        float new_var = var1 + var2;

        List<float> return_predict = new List<float>();
        return_predict.Add(new_mean);
        return_predict.Add(new_var);

        print(return_predict[0]);
        print(return_predict[1]);
        return return_predict;
    }
    */
}

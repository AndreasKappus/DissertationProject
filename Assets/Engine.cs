using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour {

    public Transform path;
    public float maxSteerAngle = 45f;
    public WheelCollider wheelFL;
    public WheelCollider wheelFR;
    public WheelCollider wheelRL;
    public WheelCollider wheelRR;
    public float maxMotorTorque = 80f;
    public float maxBrakeTorque = 150f;
    public float currentSpeed;
    public float maxSpeed = 100f;
    public Vector3 centreMass;
    public bool isBraking = false;

    [Header("Sensors")]
    public float sensorLength = 3f;
    public Vector3 frontSensorPos = new Vector3(0f, 0.2f, 0.5f);

    private List<Transform> nodes;
    private int currentNode = 0;
    private bool avoiding = false;


    void Start ()
    {
        GetComponent<Rigidbody>().centerOfMass = centreMass;

        Transform[] paths = path.GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i = 0; i < paths.Length; i++)
        {
            if(paths[i] != path.transform)
            {
                nodes.Add(paths[i]);
            }
        }
	}
	

	void FixedUpdate ()
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
        startpos += transform.forward * frontSensorPos.z;
        startpos += transform.up * frontSensorPos.y;
        float avoid = 0;
        avoiding = false;

        if (avoid == 0)
        {
            if (Physics.Raycast(startpos, transform.forward, out hit, sensorLength))
            {
                if (!hit.collider.CompareTag("Terrain"))
                {
                    Debug.DrawLine(startpos, hit.point);
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
        }

    }


    private void Steer()
    {
        if (avoiding) return;
        Vector3 relativePos = transform.InverseTransformPoint(nodes[currentNode].position);
        float steer = (relativePos.x / relativePos.magnitude) * maxSteerAngle;
        wheelFL.steerAngle = steer;
        wheelFR.steerAngle = steer;
    }

    private void Drive()
    {
        currentSpeed = 2 * Mathf.PI * wheelFL.radius * wheelFL.rpm * 60 / 1000;

        if(currentSpeed < maxSpeed && !isBraking)
        {
            wheelFL.motorTorque = maxMotorTorque;
            wheelFR.motorTorque = maxMotorTorque;
        }
        else
        {
            wheelFL.motorTorque = 0;
            wheelFL.motorTorque = 0;
        }
    }

    private void checkDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1f) ;
        {
            if (currentNode == nodes.Count - 1) // check if its on the last node
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    private void Brake()
    {
        if(isBraking)
        {
            wheelRL.brakeTorque = maxBrakeTorque;
            wheelRR.brakeTorque = maxBrakeTorque;
        }
        else
        {
            wheelRL.brakeTorque = 0;
            wheelRR.brakeTorque = 0;
        }
    }
}

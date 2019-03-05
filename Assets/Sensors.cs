using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour {

    [Header("Sensors")]
    public float sideSensorLength = 2f;
    public float sensorLength = 4f; // Max distance for sensor
    public float shortSensorLength = 2f;
    public Vector3 frontSensorPos = new Vector3(1f, 0.02f, 0.5f);
    public Vector3 SideSensorPos = new Vector3(0f, 0f, 0f);
    public Vector3 rearSensorPos = new Vector3(0f, 0.02f, 0.5f);
    public float frontSideSensor = 0.1f;
    [HideInInspector]   public bool avoiding = false;
    [HideInInspector]   public float avoid = 0;
    [HideInInspector]   public RaycastHit hit;

    private void FixedUpdate()
    {
        SensorMethod();
       
    }

    public void SensorMethod()
    {
        
        Vector3 startpos = transform.position;
        startpos += transform.forward * frontSensorPos.z; // transform the z axis to match the car as it moves
        startpos += transform.up * frontSensorPos.y;
        //float avoid = 0; // avoid value to multiply by the steer angle, a simple method for collision avoidance
        avoiding = false;

        // front centre
        if (Physics.Raycast(startpos, transform.forward, out hit, shortSensorLength))
        {
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
            if (hit.collider)
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



    }
    
}

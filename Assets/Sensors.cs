using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensors : MonoBehaviour {

    [Header("Sensors")]
    public float sensorLength = 4f; // Max distance for sensor
    public Vector3 frontSensorPos = new Vector3(0f, 0.5f, 0.5f);
    public float frontSideSensor = 0.1f;
    public float frontSensorAngle = 15f; // 15 degree angle

    // Single sensor method
    public void SensorMethod()
    {
        Engine engine = GetComponent<Engine>();
        bool avoiding = engine.avoiding;

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
                if (hit.collider.CompareTag("Terrain"))
                {
                    Debug.DrawLine(startpos, hit.point, Color.red);
                    engine.Brake();
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
    // Multiple sensor method
    // Uncomment for multiple sensors ##NEEDS FIXING
    /*
    private void Sensors()
    {
        RaycastHit hit;
        Vector3 startpos = transform.position;
        startpos += transform.forward * frontSensorPos.z; // transform the z axis to match the car as it moves
        startpos += transform.up * frontSensorPos.y;
        float avoid = 0; // added an avoid multiplier to keep track if it's actually avoiding stuff
        avoiding = false;

        
        // front right sensor
        startpos += transform.right * frontSideSensor;
        if(Physics.Raycast(startpos, transform.forward, out hit, sensorLength))
        {
            //if (!hit.collider.CompareTag("Obstacle")) // Terrain or Obstacle
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                avoiding = true;
                avoid -= 1f;
            }
        }
        // front right angle sensor
        else if (Physics.Raycast(startpos, Quaternion.AngleAxis(frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if(!hit.collider.CompareTag("Obstacle"))
            {
            Debug.DrawLine(startpos, hit.point, Color.red);
                avoiding = true;
                avoid -= 0.5f;
            }

        }
            

            // front left sensor
        startpos -= transform.right * frontSideSensor * 2;
        if (Physics.Raycast(startpos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                avoiding = true;
                avoid -= 1f;
            }
        }
        // front left angle sensor
        else if (Physics.Raycast(startpos, Quaternion.AngleAxis(-frontSensorAngle, transform.up) * transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Obstacle"))
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                avoiding = true;
                avoid -= 0.5f;
            }
        }

        // front centre
        if (Physics.Raycast(startpos, transform.forward, out hit, sensorLength))
        {
            if (!hit.collider.CompareTag("Obstacle"))
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
        if (avoiding)
        {
            FL_wheel.steerAngle = maxSteerAngle * avoid;
            FR_wheel.steerAngle = maxSteerAngle * avoid;

        }

    }
    */
}

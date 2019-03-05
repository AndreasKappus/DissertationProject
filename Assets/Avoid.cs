using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : Actions {
    public Avoid(Vehicle _Vehicle) : base(_Vehicle)
    {
    }

    public override void doAction()
    {
        

        if (Vehicle.avoiding) // avoiding is set to true when the raycast hits an object
        {
            Vehicle.FL_wheel.steerAngle = Vehicle.maxSteerAngle * Vehicle.avoid;
            Vehicle.FR_wheel.steerAngle = Vehicle.maxSteerAngle * Vehicle.avoid;

        }
    }

    //public void avoid()
    //{
    //    RaycastHit hit;
    //    Vector3 startpos = transform.position;
    //    startpos += transform.forward * frontSensorPos.z; // transform the z axis to match the car as it moves
    //    startpos += transform.up * frontSensorPos.y;
    //    float avoid = 0; // avoid value to multiply by the steer angle, a simple method for collision avoidance
    //    avoiding = false;

    //    // front centre
    //    if (Physics.Raycast(startpos, transform.forward, out hit, shortSensorLength))
    //    {
    //        // Lable the certain colliders as "Obstacle" so collision avoidance would work in that environment
    //        if (hit.collider)
    //        {
    //            Debug.DrawLine(startpos, hit.point, Color.red); // ensures that raycast is working by drawing a line from the car to the object
    //            avoiding = true;
    //            if (hit.normal.x < 0)
    //            {
    //                avoid = -1;
    //            }
    //            else
    //            {
    //                avoid = 1;
    //            }
    //        }
    //    }

    //    // Rear Sensor
    //    if (Physics.Raycast(startpos, -transform.forward, out hit, shortSensorLength))
    //    {
    //        if (hit.collider.CompareTag("Obstacle"))
    //        {
    //            Debug.DrawLine(startpos, hit.point, Color.red);
    //            avoiding = true;
    //            if (hit.normal.x > 0)
    //            {
    //                avoid = 1;
    //            }
    //            else
    //            {
    //                avoid = -1;
    //            }
    //        }
    //    }

    //    // front right sensor

    //    startpos += transform.right * frontSideSensor;
    //    if (Physics.Raycast(startpos, transform.forward, out hit, sensorLength))
    //    {
    //        if (hit.collider)
    //        {
    //            Debug.DrawLine(startpos, hit.point, Color.red);
    //            avoiding = true;
    //            avoid -= 1f;
    //        }
    //    }

    //    // front left sensor
    //    startpos -= transform.right * frontSideSensor * 2;
    //    if (Physics.Raycast(startpos, transform.forward, out hit, sensorLength))
    //    {
    //        if (hit.collider)
    //        {
    //            Debug.DrawLine(startpos, hit.point, Color.red);
    //            avoiding = true;
    //            avoid += 1f;
    //        }
    //    }

    //    if (avoiding) // avoiding is set to true when the raycast hits an object
    //    {
    //        FL_wheel.steerAngle = maxSteerAngle * avoid;
    //        FR_wheel.steerAngle = maxSteerAngle * avoid;

    //    }
    //}
}

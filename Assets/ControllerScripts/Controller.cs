using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {


    public Vehicle vehicle;
    private Actions actions;
	// Use this for initialization
	public void RunController()
    {
        if (vehicle.isBraking)
        {
            setAction(new Brake(this));
        }
        if(vehicle.car.velocity == Vector3.zero)
        {
            setAction(new Reverse(this));
        }
        else
        {
            setAction(new Accelerate(this));
            setAction(new Avoid(this));
            setAction(new Steer(this));
            setAction(new CheckWaypoint(this));
            Sensors();
            
        }
        
    }

    public void setAction(Actions _action)
    {
        actions = _action;


    }

    public void Sensors()
    {
        RaycastHit hit;
        Vector3 startpos = transform.position;
        Vector3 leftpos = transform.position;
        Vector3 rightpos = transform.position;
        startpos += transform.forward * vehicle.frontSensorPos.z; // transform the z axis to match the car as it moves
        leftpos += transform.forward * vehicle.frontSensorPos.z;
        rightpos += transform.forward * vehicle.frontSensorPos.z;

        startpos += transform.up * vehicle.frontSensorPos.y;
        leftpos += transform.up * vehicle.frontSensorPos.y;
        rightpos += transform.up * vehicle.frontSensorPos.y;


        vehicle.avoid = 0; // avoid value to multiply by the steer angle, a simple method for collision avoidance
        vehicle.avoiding = false;


        // front centre
        if (Physics.Raycast(startpos, transform.forward, out hit, vehicle.shortSensorLength))
        {
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red); // ensures that raycast is working by drawing a line from the car to the object
                vehicle.avoiding = true;
                if (hit.normal.x < 0)
                {
                    vehicle.avoid = -1;
                    //currentSpeed--;
                }
                else
                {

                    vehicle.avoid = 1;
                }
            }
        }

        //// Rear Sensor
        if (Physics.Raycast(startpos, -transform.forward, out hit, vehicle.shortSensorLength))
        {
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                vehicle.avoiding = true;
                if (hit.normal.x > 0)
                {
                    vehicle.avoid = 1;
                }
                else
                {
                    vehicle.avoid = -1;
                }
            }
        }

        // front right sensor

        startpos += transform.right * vehicle.frontSideSensor;
        if (Physics.Raycast(startpos, transform.forward, out hit, vehicle.shortSensorLength))
        {
            if (hit.collider)
            {
                Debug.DrawLine(rightpos, hit.point, Color.red);
                vehicle.avoiding = true;
                if (hit.normal.x > 0)
                {
                    vehicle.avoid += 1f;
                }
                else
                {
                    vehicle.avoid -= 1f;
                }



            }
        }

        // front left sensor
        startpos -= transform.right * vehicle.frontSideSensor * 2;
        if (Physics.Raycast(startpos, transform.forward, out hit, vehicle.shortSensorLength))
        {
            Debug.DrawLine(leftpos, hit.point, Color.red);
            vehicle.avoiding = true;
            if (hit.collider)
            {
                if (hit.normal.x < 0)
                {
                    vehicle.avoid += 1f;
                }
                else
                {
                    vehicle.avoid -= 1f;
                }

            }
        }

        //if (avoiding) // avoiding is set to true when the raycast hits an object
        //{
        //    FL_wheel.steerAngle = maxSteerAngle * avoid;
        //    FR_wheel.steerAngle = maxSteerAngle * avoid;

        //}
    }
}

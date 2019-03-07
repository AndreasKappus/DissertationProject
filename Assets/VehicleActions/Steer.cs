using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : Actions {

    private SensorManager sensors;

    public Steer(Vehicle _Vehicle) : base(_Vehicle)
    {
    }


    public override void doAction()
    {
        
        if (Vehicle.avoiding) // avoiding is set to true when the raycast hits an object
        {
            Vehicle.FL_wheel.steerAngle = Vehicle.maxSteerAngle * Vehicle.avoid;
            Vehicle.FR_wheel.steerAngle = Vehicle.maxSteerAngle * Vehicle.avoid;
            

        }
        else
        {
            Vector3 relativePos = Vehicle.transform.InverseTransformPoint(Vehicle.nodes[Vehicle.currentNode].position); // pretty much makes the car turn towards the nodes in the path object
            float steer = (relativePos.x / relativePos.magnitude) * Vehicle.maxSteerAngle; // divides the x axis of the position by the length of the position, then multiply by the steer angle to turn

            Vehicle.FL_wheel.steerAngle = Mathf.Lerp(Vehicle.FL_wheel.steerAngle, steer, Time.deltaTime * Vehicle.steerSpeed); // added more realistic steering
            Vehicle.FR_wheel.steerAngle = Mathf.Lerp(Vehicle.FR_wheel.steerAngle, steer, Time.deltaTime * Vehicle.steerSpeed);
        }
        
    }

    //public void steer()
    //{
    //    if (avoiding) return;
    //    Vector3 relativePos = transform.InverseTransformPoint(nodes[currentNode].position); // pretty much makes the car turn towards the nodes in the path object
    //    float steer = (relativePos.x / relativePos.magnitude) * maxSteerAngle; // divides the x axis of the position by the length of the position, then multiply by the steer angle to turn

    //    FL_wheel.steerAngle = Mathf.Lerp(FL_wheel.steerAngle, steer, Time.deltaTime * steerSpeed); // added more realistic steering
    //    FR_wheel.steerAngle = Mathf.Lerp(FR_wheel.steerAngle, steer, Time.deltaTime * steerSpeed);
    //}
}

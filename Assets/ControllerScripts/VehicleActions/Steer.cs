using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steer : Actions {


    public Steer(Controller _controller) : base(_controller)
    {
    }


    public override void execute()
    {
        
        if (controller.vehicle.avoiding) // avoiding is set to true when the raycast hits an object
        {

            controller.vehicle.FL_wheel.steerAngle = controller.vehicle.maxSteerAngle * controller.vehicle.avoid;
            controller.vehicle.FR_wheel.steerAngle = controller.vehicle.maxSteerAngle * controller.vehicle.avoid;
            

        }
        else
        {
            Vector3 relativePos = controller.vehicle.transform.InverseTransformPoint(controller.vehicle.nodes[controller.vehicle.currentNode].position); // pretty much makes the car turn towards the nodes in the path object
            float steer = (relativePos.x / relativePos.magnitude) * controller.vehicle.maxSteerAngle; // divides the x axis of the position by the length of the position, then multiply by the steer angle to turn

            controller.vehicle.FL_wheel.steerAngle = Mathf.Lerp(controller.vehicle.FL_wheel.steerAngle, steer, Time.deltaTime * controller.vehicle.steerSpeed); // added more realistic steering
            controller.vehicle.FR_wheel.steerAngle = Mathf.Lerp(controller.vehicle.FR_wheel.steerAngle, steer, Time.deltaTime * controller.vehicle.steerSpeed);
        }
        
    }

    
}

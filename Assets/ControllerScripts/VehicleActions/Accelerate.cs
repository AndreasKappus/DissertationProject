using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : Actions {
    public Accelerate(Controller _controller) : base(_controller)
    {
    }

   

    public override void execute()
    {
        
            controller.vehicle.currentSpeed = 2 * Mathf.PI * controller.vehicle.FL_wheel.radius * controller.vehicle.FL_wheel.rpm; // speed = 2 * 3.14 * radius of wheel * revs per minute
            controller.vehicle.FL_wheel.motorTorque = controller.vehicle.maxMotorTorque;
            controller.vehicle.FR_wheel.motorTorque = controller.vehicle.maxMotorTorque;
            controller.vehicle.FL_wheel.brakeTorque = 0;
            controller.vehicle.FR_wheel.brakeTorque = 0;

       
        
    }
}

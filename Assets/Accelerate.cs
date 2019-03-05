using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerate : Actions {
    public Accelerate(Vehicle _Vehicle) : base(_Vehicle)
    {
    }

    //public void accelerate()
    //{
    //    currentSpeed = 2 * Mathf.PI * FL_wheel.radius * FL_wheel.rpm; // speed = 2 * 3.14 * radius of wheel * revs per minute
    //    FL_wheel.motorTorque = maxMotorTorque;
    //    FR_wheel.motorTorque = maxMotorTorque;
    //}

    public override void doAction()
    {
        if (!Vehicle.isBraking)
        {
            Vehicle.currentSpeed = 2 * Mathf.PI * Vehicle.FL_wheel.radius * Vehicle.FL_wheel.rpm; // speed = 2 * 3.14 * radius of wheel * revs per minute
            Vehicle.FL_wheel.motorTorque = Vehicle.maxMotorTorque;
            Vehicle.FR_wheel.motorTorque = Vehicle.maxMotorTorque;
        }
        else if(!Vehicle.isBraking && Vehicle.avoiding)
        {
            Vehicle.currentSpeed = -10; // speed = 2 * 3.14 * radius of wheel * revs per minute
            Vehicle.FL_wheel.motorTorque = -10;
            Vehicle.FR_wheel.motorTorque = -10;
        }
        else
        {
            Vehicle.RL_wheel.motorTorque = 0;
            Vehicle.RR_wheel.motorTorque = 0;
        }
        
    }
}

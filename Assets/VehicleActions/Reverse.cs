using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : Actions {
    public Reverse(Vehicle _Vehicle) : base(_Vehicle)
    {
    }

    public override void doAction()
    {
        if (Vehicle.car.velocity == Vector3.zero)
        {
            Vehicle.car.position -= Vehicle.car.transform.forward;
            Vehicle.FL_wheel.motorTorque = Vehicle.reverseTorque;
            Vehicle.FR_wheel.motorTorque = Vehicle.reverseTorque;
        }
      
    }

    void onCollision(Collision collision)
    {
        Vehicle.car.position -= Vehicle.car.transform.forward;
        Vehicle.FL_wheel.motorTorque -= Vehicle.reverseTorque;
        Vehicle.FR_wheel.motorTorque -= Vehicle.reverseTorque;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverse : Actions {
    public Reverse(Controller _controller) : base(_controller)
    {
    }

    public override void execute()
    {
       
            controller.vehicle.car.position += Vector3.back * Time.deltaTime;
            controller.vehicle.FL_wheel.motorTorque = controller.vehicle.reverseTorque;
            controller.vehicle.FR_wheel.motorTorque = controller.vehicle.reverseTorque;
        
      
    }

  
    
}

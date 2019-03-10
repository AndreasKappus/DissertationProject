using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avoid : Actions {
    public Avoid(Controller _controller) : base(_controller)
    {
    }

    public override void execute()
    {

        controller.vehicle.FL_wheel.steerAngle = controller.vehicle.maxSteerAngle * controller.vehicle.avoid;
        controller.vehicle.FR_wheel.steerAngle = controller.vehicle.maxSteerAngle * controller.vehicle.avoid;

        
    }

  
}

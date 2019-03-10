using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brake : Actions {
    public Brake(Controller _controller) : base(_controller)
    {
    }

    public override void execute()
    {
        controller.vehicle.RL_wheel.motorTorque = 0;
        controller.vehicle.RR_wheel.motorTorque = 0;
    }

  
}

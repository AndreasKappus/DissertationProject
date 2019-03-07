using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brake : Actions {
    public Brake(Vehicle _Vehicle) : base(_Vehicle)
    {
    }

    public override void doAction()
    {
        Vehicle.RL_wheel.motorTorque = 0;
        Vehicle.RR_wheel.motorTorque = 0;
    }

    //public void brake()
    //{
        
    //        RL_wheel.motorTorque = 0;
    //        RR_wheel.motorTorque = 0;

        
    //}
}

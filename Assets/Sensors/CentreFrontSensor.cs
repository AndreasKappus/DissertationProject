using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentreFrontSensor : Sensors
{
    public CentreFrontSensor(SensorManager _sensorManager) : base(_sensorManager)
    {
    }

    public override void setSensors()
    {

        Vector3 startpos = transform.position; // to apply transformations dynamically while agent is in motion
        RaycastHit hit;
        if(Physics.Raycast(startpos, transform.forward, out hit, sensorManager.shortSensorLength))
        {
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                sensorManager.avoiding = true;
                if (hit.normal.x < 0)
                {
                    sensorManager.avoid = -1;
                }
                else
                {
                    sensorManager.avoid = 1;
                }
            }
        }
      
        
    }
}

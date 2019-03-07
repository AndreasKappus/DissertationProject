using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontLeftSensor : Sensors
{
    public FrontLeftSensor(SensorManager _sensorManager) : base(_sensorManager)
    {
    }

    public override void setSensors()
    {
        Vector3 startpos = transform.forward + sensorManager.front_left_sensor_pos;
        RaycastHit hit;

        if (Physics.Raycast(startpos, transform.forward, out hit, sensorManager.shortSensorLength))
        {
            if (hit.collider)
            {
                Debug.DrawLine(startpos, hit.point, Color.red);
                sensorManager.avoiding = true;
                sensorManager.avoid += 1f;
            }

        }
    }
}

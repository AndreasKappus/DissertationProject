using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Sensors : MonoBehaviour {

    public abstract void setSensors();

    protected SensorManager sensorManager;
    protected Vehicle Vehicle;

    /*public Sensors(SensorManager _sensorManager)
    {
        this.sensorManager = _sensorManager;
        setSensors();
    }*/
    public Sensors(SensorManager _sensorManager)
    {
        this.sensorManager = _sensorManager;
        setSensors();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorManager : MonoBehaviour {

    [Header("Sensors")]
    public float sideSensorLength = 2f;
    public float sensorLength = 4f; // Max distance for sensor
    public float shortSensorLength = 1f;
    public Vector3 frontSensorPos = new Vector3(0f, 0.02f, 0.5f);
    public Vector3 front_right_sensor_pos = new Vector3(0f, 0.02f, 0.1f);
    public Vector3 front_left_sensor_pos = new Vector3(0f, 0.02f, -0.1f);
    public Vector3 SideSensorPos = new Vector3(0f, 0.02f, 0f);
    public Vector3 rearSensorPos = new Vector3(0f, 0.02f, 0.5f);
    public float frontSideSensor = 0.1f;
    public float frontSensorAngle = 15f; // 15 degree angle
    public bool avoiding = false;
    public float avoid = 0;
    protected Sensors sensors;
    
    void Start () {
       
	}
	
	
	void FixedUpdate() {

        execute(new FrontLeftSensor(this));
        execute(new CentreFrontSensor(this));
        execute(new FrontRightSensor(this));
	}

    public bool execute(Sensors _sensors)
    {
        sensors = _sensors;
        sensors.setSensors();
        return true;
        //gameObject.name = "Action - " + actions.GetType().Name;
    }


}

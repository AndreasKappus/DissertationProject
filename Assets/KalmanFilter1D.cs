using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KalmanFilter1D : MonoBehaviour {

    public Text location_x;
    public Text location_y;

    // public List<float> velocity_measurements = new List<float>();
    public float noise = 10;
    public Rigidbody car;

    // Update is called once per frame
    void FixedUpdate() {
        
        float mu = 0; // mean uncertainty
        float motion = car.velocity.magnitude;
        float sensor_measurements_x = sensorNoise(car.position.x); // get values from sensor noise method to get the "noisy" measurements
        float measurement_sig = 4; // measurement uncertainty ( experiment with the "uncertainty" values)
        float motion_sig = 2; // motion uncertainty
        float sig = 1000; // initial uncertainty
        float sensor_measurements_y = sensorNoise(car.position.z);
        noise -= 0.001f;

        location_x.text = kalmanUpdate(mu, sig, sensor_measurements_x, measurement_sig).ToString();
        location_y.text = kalmanUpdate(mu, sig, sensor_measurements_y, measurement_sig).ToString();


    }

    // gets position and uses random numbers to replace the pos variable's values through a certain range, so the kalman filter can get the measurements .etc
    public float sensorNoise(float pos)
    {
        // TODO: 
        //create variable for the range, loop it and subtract 0.001 through each iteration to get a lower range for a more accurate reading or do the subtraction in update

        

        // simulate gps sensor noise through random numbers. 
        pos = Random.Range(pos - noise, pos + noise); // The range of the numbers are the actual position - or + 10.

        return pos;
    }

    public float kalmanUpdate(float mean1, float var1, float mean2, float var2)
    {
        float new_mean = (var2 * mean1 + var1 * mean2) / (var1 + var2);
        float new_var = 1 / (1 / var1 + 1 / var2);

        List<float> return_update = new List<float>();
        return_update.Add(new_mean);
        return_update.Add(new_var);

        print(return_update[0]);
        print(return_update[1]);
        return new_mean;
    }

    public float kalmanPredict(float mean1, float var1, float mean2, float var2)
    {
        float new_mean = mean1 + mean2;
        float new_var = var1 + var2;

        List<float> return_predict = new List<float>();
        return_predict.Add(new_mean);
        return_predict.Add(new_var);

        print(return_predict[0]);
        print(return_predict[1]);


        return new_mean;
    }

    
}

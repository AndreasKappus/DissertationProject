using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewKFilter : Actions {

    // public List<float> velocity_measurements = new List<float>();
    public float noise = 10;

    public NewKFilter(Vehicle _Vehicle) : base(_Vehicle)
    {
    }

    

   

    // gets position and uses random numbers to replace the pos variable's values through a certain range, so the kalman filter can get the measurements .etc
    public float sensorNoise(float pos)
    {
        // TODO: 
        //create variable for the range, loop it and subtract 0.001 through each iteration to get a lower range for a more accurate reading



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

        
        return new_mean;
    }

    public float kalmanPredict(float mean1, float var1, float mean2, float var2)
    {
        float new_mean = mean1 + mean2;
        float new_var = var1 + var2;

        List<float> return_predict = new List<float>();
        return_predict.Add(new_mean);
        return_predict.Add(new_var);

        


        return new_mean;
    }

    public override void doAction()
    {
        float mu = 0; // mean uncertainty
        float motion = Vehicle.car.velocity.magnitude;
        float sensor_measurements_x = sensorNoise(Vehicle.car.position.x); // get values from sensor noise method to get the "noisy" measurements
        float measurement_sig = 4; // measurement uncertainty ( experiment with the "uncertainty" values)
        float motion_sig = 2; // motion uncertainty
        float sig = 1000; // initial uncertainty

        Debug.Log(kalmanUpdate(mu, sig, sensor_measurements_x, measurement_sig));
        Debug.Log(kalmanPredict(mu, sig, motion, motion_sig));
        kalmanUpdate(mu, sig, sensor_measurements_x, measurement_sig);
        kalmanPredict(mu, sig, motion, motion_sig);

        
    }
}

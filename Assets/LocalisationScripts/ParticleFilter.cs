using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticleFilter : MonoBehaviour {

    public Transform t_landmarks;
    public Rigidbody car;
    // for obstacle positions 2d list or array
    // [[X, Y],[X, Y]]
    //float orientation = Random.value * 2.0f * Mathf.PI; // orientation would be the vehicle bearing in degrees or radians
    float orientation = 0f; // orientation would be the vehicle bearing in degrees or radians
    float forward_noise = 0.0f;
    float turn_noise = 0.0f;
    float sensor_noise = 0.0f;
    
    public List<Transform> landmark_list;
    public float[][] landmark_position;

	void Start () {
        Transform[] landmarks = t_landmarks.GetComponentsInChildren<Transform>();
        landmark_list = new List<Transform>();

        for(int i = 0; i < landmarks.Length; i++)
        {
            if(landmarks[i] != t_landmarks.transform)
            {
                landmark_list.Add(landmarks[i]);
                
            }
        }
    }

    // Update is called once per frame
    void Update () {

        generate_particles();
	}


    public void get_landmarks()
    {
 
    }

    // set noise values for experimentation
    //public void set_noise(float new_forward_noise, float new_turn_noise, float new_sensor_noise)
    //{
    //    this.forward_noise = new_forward_noise;
    //    this.turn_noise = new_turn_noise;
    //    this.sensor_noise = new_sensor_noise;
    //}

    //public Vector3 set_noise(float new_forward_noise, float new_turn_noise, float new_sensor_noise)
    //{
    //    Vector3 noise = new Vector3();
    //    this.forward_noise = new_forward_noise;
    //    this.turn_noise = new_turn_noise;
    //    this.sensor_noise = new_sensor_noise;
    //    noise = set_noise(this.forward_noise, this.turn_noise, this.sensor_noise);
    //    return set_noise(new_forward_noise, new_turn_noise, new_sensor_noise);
    //}

    public float getMovement(float turn, float drive)
    {
        //Vector3 turning = car.position;
        float move = 0.0f;
        //turn = Mathf.Atan2(turning.x, turning.y) * Mathf.Rad2Deg;
        turn = Mathf.Atan2(car.transform.right.z, car.transform.right.x) * Mathf.Rad2Deg;
        drive = car.velocity.magnitude;


        return move;

    }
    public float getVelocity(float velocity)
    {
        return velocity;
    }
    
    public float getBearing(float bearing)
    {

        return bearing;
    }


    public List<float> sense()
    {
        List<float> z = new List<float>();
        float x = car.position.x;
        float y = car.position.y;

        for (int i = 0; i < landmark_list.Count; i++)
        {
            float dist = Mathf.Sqrt((x - landmark_list[i].position.x) * Mathf.Exp(2) + (y - landmark_list[i].position.y) * Mathf.Exp(2));
            dist += Random.Range(0.0f, sensor_noise);
            z.Add(dist);
        }
        return z;
        // MAKE SURE THIS IS CORRECT
    }

    public float gaussian(float mu, float sigma, float x)
    {
        // calculate probability of x for a 1-dimensional gaussian with mean (mu) and variance (sigma)
        return Mathf.Exp(-(mu - x) * Mathf.Exp(2) / 2.0f / Mathf.Sqrt(2.0f * Mathf.PI * (sigma * (Mathf.Exp(2)))));
    }

    public float measurement_probability(List<float> measurement)
    {
        float probability = 0.0f;
        float x = car.position.x;
        float y = car.position.y;


        for(int i = 0; i < landmark_list.Count; i++)
        {
            float distance = Mathf.Sqrt((x - landmark_list[i].position.x) * Mathf.Exp(2) + (car.position.y - landmark_list[i].position.y) * Mathf.Exp(2));
        }

        return probability;
    }
    
    public void generate_particles()
    {
        int n = 100;
        List<float> w = new List<float>();
        List<Vector3> p = new List<Vector3>();
        float beta = 0.0f;
        List<float> z = sense();


        //for (int i = 0; i < n; i++)
        //{
        //    p.add(set_noise(0.05f, 0.05f, 0.05f));

        //}

        for (int i = 0; i < n; i++)
        {
            w.Add(measurement_probability(z));
        }

        //int index = Random.seed * n;
        int index = Random.Range(0, n);
        float max_w = w.Max();

        for (int i = 0; i < n; i++)
        {
            //beta += Random.seed * 2.0f * max_w;
            beta += Random.Range(0, max_w);
            while (beta > w[index])
            {
                beta -= index;
                index = (index + 1) % n;

            }
            p.Add(p[index]);
        }
        Debug.Log(p);
    }
    
}

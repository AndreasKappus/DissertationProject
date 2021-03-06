﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LandmarkBasedLocalisation : MonoBehaviour {

    public GameObject particles;
    public List<GameObject> particle_list;

    public Transform t_landmarks;
    public Rigidbody car;
    public List<Transform> landmark_list;

    void Start() {
        Transform[] landmarks = GetComponentsInChildren<Transform>();
        landmark_list = new List<Transform>();
        particle_list = new List<GameObject>();

        for (int i = 0; i < landmarks.Length; i++)
        {
            if (landmarks[i] != t_landmarks.transform)
            {
                landmark_list.Add(landmarks[i]);

            }
        }

    }

    void FixedUpdate() {

        generateParticles();
    }





    public void generateParticles()
    {
        float particle_pos_x = 0f;
        float particle_pos_z = 0f;
        int num_particles = 10;
        GameObject g_particle;

        for (int i = 0; i < num_particles; i++)
        {
            Vector3 position = new Vector3(particle_pos_x, 5f, particle_pos_z);
            

            for (int j = 0; j < landmark_list.Count; j++)
            {
                particle_pos_x = Random.Range(car.position.x, landmark_list[j].position.x);
                particle_pos_z = Random.Range(car.position.z, landmark_list[j].position.z);



            }
            //particle_list.Add((GameObject)Instantiate(particles, position, Quaternion.identity));
            g_particle = Instantiate(particles, position, Quaternion.identity);
            //particle_list.OrderBy(distance => Vector3.Distance(car.position, particles.transform.position));
            Destroy(g_particle, 1.5f);

        }

    }
    
}


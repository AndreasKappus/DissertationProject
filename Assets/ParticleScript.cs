using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{

	private List<GameObject> particle_list = new List<GameObject>();

    private void OnDrawGizmos()
    {
        GameObject[] particles = GetComponentsInChildren<GameObject>();
        particle_list = new List<GameObject>();

        for (int i = 0; i < particles.Length; i++)
        {
            particle_list.Add(particles[i]);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Landmarks : MonoBehaviour {


    private List<Transform> landmark_list = new List<Transform>();


    private void OnDrawGizmosSelected()
    {
        Transform[] landmarks = GetComponentsInChildren<Transform>();
        landmark_list = new List<Transform>();

        for(int i = 0; i < landmarks.Length; i++)
        {
            landmark_list.Add(landmarks[i]);
        }

        for(int i = 0; i < landmark_list.Count; i++)
        {
            Vector3 current_landmark = landmarks[i].position;
            Vector3 prev_landmark = Vector3.zero;

            if(i > 0)
            {
                prev_landmark = landmarks[i - 1].position;
            }
            else if(i == 0 && landmark_list.Count > 1)
            {
                prev_landmark = landmark_list[landmark_list.Count - 1].position;
            }
            Gizmos.DrawLine(prev_landmark, current_landmark);
            
        }
    }
}

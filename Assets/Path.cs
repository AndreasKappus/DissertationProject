using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public Color linecolour;
    private List<Transform> nodes = new List<Transform>();

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = linecolour;
        Transform[] paths = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i = 0; i <paths.Length; i++)
        {
            if(paths[i] != transform)
            {
                nodes.Add(paths[i]);
            }
        }

        for(int i = 0; i < nodes.Count; i++)
        {
            Vector3 currentNode = nodes[i].position;
            Vector3 prevNode = Vector3.zero;

            if (i > 0) // check if there are any nodes remaining
            {
                prevNode = nodes[i - 1].position;
            }
            else if (i == 0 && nodes.Count > 1) // the starting node, as there is no previous node
            {
                prevNode = nodes[nodes.Count - 1].position;
            }
            Gizmos.DrawLine(prevNode, currentNode); // doesnt work so change this
        }
    }
}

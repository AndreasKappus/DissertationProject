using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWaypoint : Actions {
    public CheckWaypoint(Vehicle _Vehicle) : base(_Vehicle)
    {
    }

    public override void doAction()
    {
        if(Vector3.Distance(Vehicle.transform.position, Vehicle.nodes[Vehicle.currentNode].position) < 1f) // checks the distance from the car to the current node's position
        {
            if (Vehicle.currentNode == Vehicle.nodes.Count - 1) // check if its on the last node, if so then, go back to start node, this enables the waypoint objects to be constantly looping around
            {
                Vehicle.currentNode = 0;
            }
            else
            {
                Vehicle.currentNode++;
            }
        }
    }

    //public void checkDistance()
    //{
    //    if (Vector3.Distance(transform.position, nodes[currentNode].position) < 1f) // checks the distance from the car to the current node's position
    //    {
    //        if (currentNode == nodes.Count - 1) // check if its on the last node, if so then, go back to start node, this enables the waypoint objects to be constantly looping around
    //        {
    //            currentNode = 0;
    //        }
    //        else
    //        {
    //            currentNode++;
    //        }
    //    }
    //}
}

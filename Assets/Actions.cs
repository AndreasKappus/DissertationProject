using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions {

    public abstract void doAction();

    protected Vehicle Vehicle;

    public Actions(Vehicle _Vehicle)
    {
        this.Vehicle = _Vehicle;
        doAction();
    }

}

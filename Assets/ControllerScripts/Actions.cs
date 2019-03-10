using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actions {

    public abstract void execute();

    protected Controller controller;

    public Actions(Controller _controller)
    {
        this.controller = _controller;
        execute();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public abstract class CameraViewManager : MonoBehaviour {

    public Button btn_default_view;
    public Button btn_overhead_view;
    public Button btn_left_view;
    public Button btn_right_view;

    public Camera default_view;
    public Camera overhead_view;
    public Camera left_view;
    public Camera right_view;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(defaultViewSwitch);
        GetComponent<Button>().onClick.AddListener(overheadViewSwitch);
        GetComponent<Button>().onClick.AddListener(leftViewSwitch);
        GetComponent<Button>().onClick.AddListener(rightViewSwitch);
    }

    void defaultViewSwitch()
    {
        default_view.enabled = true;
        overhead_view.enabled = false;
        left_view.enabled = false;
        right_view.enabled = false;
    }

    void overheadViewSwitch()
    {
        default_view.enabled = false;
        overhead_view.enabled = true;
        left_view.enabled = false;
        right_view.enabled = false;
    }

    void leftViewSwitch()
    {
        default_view.enabled = false;
        overhead_view.enabled = false;
        left_view.enabled = true;
        right_view.enabled = false;
    }

    void rightViewSwitch()
    {
        default_view.enabled = false;
        overhead_view.enabled = false;
        left_view.enabled = false;
        right_view.enabled = false;
    }
}

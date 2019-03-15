using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeftView : MonoBehaviour {

    public Button btn_left_view;
    public Camera default_view;
    public Camera overhead_view;
    public Camera left_view;
    public Camera right_view;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(leftViewSwitch);
    }

    void leftViewSwitch()
    {
        default_view.enabled = false;
        overhead_view.enabled = false;
        left_view.enabled = true;
        right_view.enabled = false;
    }
}

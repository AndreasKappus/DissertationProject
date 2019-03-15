using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefaultCameraView : MonoBehaviour {

    public Button btn_default_view;
    public Camera default_view;
    public Camera overhead_view;
    public Camera left_view;
    public Camera right_view;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(defaultViewSwitch);
    }

    void defaultViewSwitch()
    {
        default_view.enabled = true;
        overhead_view.enabled = false;
        left_view.enabled = false;
        right_view.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverheadView : MonoBehaviour {

    public Button btn_overhead_view;
    public Camera default_view;
    public Camera overhead_view;
    public Camera left_view;
    public Camera right_view;

    public void Start()
    {
        GetComponent<Button>().onClick.AddListener(overheadViewSwitch);
    }

    void overheadViewSwitch()
    {
        default_view.enabled = false;
        overhead_view.enabled = true;
        left_view.enabled = false;
        right_view.enabled = false;
    }
}

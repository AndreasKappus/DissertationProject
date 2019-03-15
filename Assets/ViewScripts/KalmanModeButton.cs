using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KalmanModeButton : MonoBehaviour {

    public RawImage landmark_localisation;
    public Camera particle_overhead;
    public Button kalman_filter_btn_press;
    public Text location_x;
    public Text location_y;
    public Text x_location_lable;
    public Text y_location_lable;

    void Start () {
        GetComponent<Button>().onClick.AddListener(kalmanFilterMode);
	}
	
    void kalmanFilterMode()
    {
        landmark_localisation.enabled = false;
        particle_overhead.enabled = false;
        location_x.enabled = true;
        location_y.enabled = true;
        x_location_lable.enabled = true;
        y_location_lable.enabled = true;
    }
	
}

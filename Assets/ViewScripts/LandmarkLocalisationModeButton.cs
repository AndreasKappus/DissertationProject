using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LandmarkLocalisationModeButton : MonoBehaviour {

    public RawImage landmark_localisation;
    public Camera particle_overhead;
    public Button landmark_localisation_btn_press;
    public Text location_x;
    public Text location_y;
    public Text x_location_lable;
    public Text y_location_lable;

    void Start () {
        GetComponent<Button>().onClick.AddListener(landmarkLocalisationMode);
	}
	
	void landmarkLocalisationMode()
    {
        landmark_localisation.enabled = true;
        particle_overhead.enabled = true;
        location_x.enabled = false;
        location_y.enabled = false;
        x_location_lable.enabled = false;
        y_location_lable.enabled = false;
    }
}

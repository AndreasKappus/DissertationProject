using UnityEngine;
using UnityEngine.UI;

public class ChangeTimeScale : MonoBehaviour {

    public Slider timeAdjust;

	void Start () {
        timeAdjust = GetComponent<Slider>();
	}
	
	void Update () {
        Time.timeScale = timeAdjust.value;
	}
}

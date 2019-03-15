using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour {

    public Button pause_button;
	
	void Start () {
        GetComponent<Button>().onClick.AddListener(Pause);
        
	}

    private void Pause()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0) ? 1f : 0f;
    }
}

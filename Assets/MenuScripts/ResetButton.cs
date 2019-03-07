using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {

    public Button reset_button;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(resetSimulation);
	}

    private void resetSimulation()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

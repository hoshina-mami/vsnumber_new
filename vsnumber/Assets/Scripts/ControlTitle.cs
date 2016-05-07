using UnityEngine;
using System.Collections;

public class ControlTitle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // VS Play選択画面へとぶ
    public void LoadMainVS () {
        Application.LoadLevel("MainVS");
    }
}

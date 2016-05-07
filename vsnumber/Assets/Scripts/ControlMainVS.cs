using UnityEngine;
using System.Collections;

public class ControlMainVS : MonoBehaviour {

    //private
    private GameObject MyBtnGenerator;
    private BtnGenerator BtnGenerator;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        MyBtnGenerator = GameObject.Find("MyBtnGenerator");
        BtnGenerator   = MyBtnGenerator.GetComponent<BtnGenerator> ();

        BtnGenerator.GanerateBtns();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

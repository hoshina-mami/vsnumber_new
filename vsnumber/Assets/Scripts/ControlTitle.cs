using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlTitle : MonoBehaviour {

	//private
	private GameObject Btn_vs;
	private GameObject Btn_single;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        Btn_vs          = GameObject.Find("Btn_vs");
        Btn_single      = GameObject.Find("Btn_single");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// SingleModeボタンを選択
	public void tapSigleMode () {

		//GetComponent<AudioSource>().Play();

		hideObjects ();

		Invoke("LoadInGameSingle",  0.9f);

	}

    // SingleMode選択画面へとぶ
    public void LoadInGameSingle () {
        //Application.LoadLevel("InGameSingle");
    }


    //画面遷移のさいにオブジェクトを消す
	void hideObjects () {

		//makes object's alpha 0
		Btn_vs.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		
	}
}

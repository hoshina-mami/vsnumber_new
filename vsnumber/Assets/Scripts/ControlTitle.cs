using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlTitle : MonoBehaviour {

	//private
	private GameObject TitleText;
	private GameObject Btn_vs;
	private GameObject Btn_single;
	private GameObject Btn_record;
	private GameObject Btn_setting;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        TitleText       = GameObject.Find("TitleText");
        Btn_vs          = GameObject.Find("Btn_vs");
        Btn_single      = GameObject.Find("Btn_single");
        Btn_record      = GameObject.Find("Btn_record");
        Btn_setting     = GameObject.Find("Btn_setting");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// SingleModeボタンを選択
	public void tapSigleMode () {

		//GetComponent<AudioSource>().Play();

		hideObjects ();

		Invoke("LoadInGameSingle",  0.6f);

	}

    // SingleMode選択画面へとぶ
    public void LoadInGameSingle () {
        //Application.LoadLevel("InGameSingle");
    }


    //画面遷移のさいにオブジェクトを消す
	void hideObjects () {

		//makes object's alpha 0
		TitleText.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_vs.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_single.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_record.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_setting.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		
	}
}

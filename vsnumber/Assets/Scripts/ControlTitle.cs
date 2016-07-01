using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlTitle : MonoBehaviour {

	//private
	private GameObject TitleText;
	private GameObject Btn_vs;
	private GameObject Btn_single;
	//private GameObject Btn_record;
	private GameObject Btn_setting;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        TitleText       = GameObject.Find("TitleText");
        Btn_vs          = GameObject.Find("Btn_vs");
        Btn_single      = GameObject.Find("Btn_single");
        //Btn_record      = GameObject.Find("Btn_record");
        Btn_setting     = GameObject.Find("Btn_setting");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// メニューボタンを選択
	public void tapButton (string target) {

		//GetComponent<AudioSource>().Play();

		hideObjects ();

		if (target == "InGameSingle") {
			Invoke("LoadInGameSingle",  0.4f);
		} else if (target == "InGameVs") {
			Invoke("LoadInGameVs",  0.4f);
		}
		

	}

    // SingleMode選択画面へとぶ
    public void LoadInGameSingle () {
    	SceneManager.LoadScene("InGameSingle");
    }

	// VsMode選択画面へとぶ
    public void LoadInGameVs () {
    	SceneManager.LoadScene("InGameVs");
    }


    //画面遷移のさいにオブジェクトを消す
	void hideObjects () {

		//makes object's alpha 0
		TitleText.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_vs.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_single.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		//Btn_record.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_setting.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		
	}
}

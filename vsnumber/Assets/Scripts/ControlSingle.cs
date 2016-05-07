using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlSingle : MonoBehaviour {

	//private
	private GameObject GameUi;
	private GameObject Btn_return;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

        GameUi       = GameObject.Find("GameUi");
        Btn_return         = GameObject.Find("Btn_return");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 戻るボタンを選択
	public void tapReturnButton () {

		//GetComponent<AudioSource>().Play();

		GameUi.GetComponent<uTools.uTweenAlpha> ().enabled = true;

		Invoke("LoadTitle",  0.4f);

	}

    // タイトル画面へとぶ
    public void LoadTitle () {
        Application.LoadLevel("Title");
    }
}

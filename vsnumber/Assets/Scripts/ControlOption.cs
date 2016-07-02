using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlOption : MonoBehaviour {

	//private
	private GameObject GameUi;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

		GameUi           = GameObject.Find("GameUi");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 戻るボタンを選択
	public void tapReturnButton () {

		//GetComponent<AudioSource>().Play();

		HideContents();

		Invoke("LoadTitle",  0.4f);

	}

	// タイトル画面へとぶ
    public void LoadTitle () {
        SceneManager.LoadScene("Title");
    }


    //コンテンツを隠す
	void HideContents () {
		GameUi.GetComponent<uTools.uTweenAlpha> ().enabled = true;
	}
}

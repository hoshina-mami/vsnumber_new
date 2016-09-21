﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlOption : MonoBehaviour {

	public Sprite background1;
	public Sprite background2;
	public Sprite background3;
	public Sprite background4;
	public Sprite background5;

	//private
	private GameObject GameUi;
	private GameObject Background;
	private GameObject Text_bestTime;
	private GameObject Text_bestName;
	private GameObject Toggle1;
	private GameObject Toggle2;

    private AudioSource se_ok;
    private AudioSource se_cancel;
    private AudioSource se_countdown;

	private int BestMinCount;
	private int BestSecCount;
	private float BestDecCount;

	private string BestMinText;
	private string BestSecText;
	private string BestDecText;

	private string BestName;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

		GameUi           = GameObject.Find("GameUi");
		Background       = GameObject.Find("Background");
		Text_bestTime    = GameObject.Find("Text_bestTime");
        Text_bestName    = GameObject.Find("Text_bestName");
        Toggle1          = GameObject.Find("Toggle1");
        Toggle2          = GameObject.Find("Toggle2");

		BestMinCount = PlayerPrefs.GetInt("BestMin");
		BestSecCount = PlayerPrefs.GetInt("BestSec");
		BestDecCount = PlayerPrefs.GetFloat("BestDec");
		if(PlayerPrefs.GetString("BestName") == "") {
			BestName = "AAA";
		} else {
			BestName = PlayerPrefs.GetString("BestName");
		}

		AudioSource[] audioSources = GetComponents<AudioSource>();
		se_ok = audioSources[0];
		se_cancel = audioSources[1];
		se_countdown = audioSources[2];

		//音量のON/OFF
		if (PlayerPrefs.GetInt("SoundFlg") != 0) {
			AudioListener.volume = 0;
			Toggle1.GetComponent<Toggle>().isOn = false;
			Toggle2.GetComponent<Toggle>().isOn = true;
		} else {
			AudioListener.volume = 0.7f;
			Toggle1.GetComponent<Toggle>().isOn = true;
			Toggle2.GetComponent<Toggle>().isOn = false;
		}

		changeBackground();

		showBestRecord();
	
	}
	
	// Update is called once per frame
	void Update () {
		// プラットフォームがアンドロイドかチェック
		if (Application.platform == RuntimePlatform.Android)
		{
		    // エスケープキー取得
		    if (Input.GetKey(KeyCode.Escape))
		    {
		        tapReturnButton();
		        return;
		    }
		}
	}

	// 戻るボタンを選択
	public void tapReturnButton () {

		se_cancel.PlayOneShot(se_cancel.clip);

		HideContents();

		Invoke("LoadTitle",  0.4f);

	}

	// タイトル画面へとぶ
    public void LoadTitle () {
        SceneManager.LoadScene("Title");
    }

    //サウンド切り替え
    public void switchSound () {
    	if (Toggle1.GetComponent<Toggle>().isOn) {
    		PlayerPrefs.SetInt("SoundFlg", 0);
    		AudioListener.volume = 0.7f;
		} else {
			PlayerPrefs.SetInt("SoundFlg", 1);
			AudioListener.volume = 0;
		}
    }


    //コンテンツを隠す
	void HideContents () {
		GameUi.GetComponent<uTools.uTweenAlpha> ().enabled = true;
	}

	//背景画像変更
    void changeBackground () {
    	switch (PlayerPrefs.GetInt("BackNum")) {
    		case 1:
    			Background.GetComponent<Image> ().sprite = background1;
    			break;
    		case 2:
    			Background.GetComponent<Image> ().sprite = background2;
    			break;
    		case 3:
    			Background.GetComponent<Image> ().sprite = background3;
    			break;
    		case 4:
    			Background.GetComponent<Image> ().sprite = background4;
    			break;
    		case 5:
    			Background.GetComponent<Image> ().sprite = background5;
    			break;
    		default:
    			Background.GetComponent<Image> ().sprite = background1;
    			break;
    	}
    }

    //記録のリセット
    public void deleteRecord () {
    	se_ok.PlayOneShot(se_ok.clip);
    	PlayerPrefs.SetInt("isFirstPlay", 0);
    	PlayerPrefs.SetString("BestName", "");
        PlayerPrefs.SetInt("BestMin", 0);
		PlayerPrefs.SetInt("BestSec", 0);
		PlayerPrefs.SetFloat("BestDec", 0);
		Text_bestTime.GetComponent<Text> ().text = "00:00:00";
		Text_bestName.GetComponent<Text> ().text = "by AAA";
    }

    //最高記録の表示
    void showBestRecord () {
    	if (BestMinCount < 10) {
			BestMinText = string.Format ("0{0}", BestMinCount.ToString ());
		} else {
			BestMinText = string.Format ("{0}", BestMinCount.ToString ());
		}
		if (BestSecCount < 10) {
			BestSecText = string.Format ("0{0}", BestSecCount.ToString ());
		} else {
			BestSecText = string.Format ("{0}", BestSecCount.ToString ());
		}
		if (BestDecCount >= 0 && BestDecCount < 9.9) {
			BestDecText = string.Format ("0{0}", BestDecCount.ToString ("f0"));
		} else if (BestDecCount < 99.9) {
			BestDecText = string.Format ("{0}", BestDecCount.ToString ("f0"));
		}

		Text_bestTime.GetComponent<Text> ().text = BestMinText +":"+ BestSecText +":"+ BestDecText;
		Text_bestName.GetComponent<Text> ().text = "by " + BestName;
    }
}

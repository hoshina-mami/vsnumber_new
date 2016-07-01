using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlResult : MonoBehaviour {

	//private
	private GameObject GameUi;
	private GameObject Text_currentTime;
	private GameObject Text_bestTime;
	private GameObject Text_bestName;
	private GameObject Text_newTime;
	private GameObject Popup;

	private int BestMinCount;
	private int BestSecCount;
	private float BestDecCount;
	private int CurrentMinCount;
	private int CurrentSecCount;
	private float CurrentDecCount;

	private string MinText;
	private string SecText;
	private string DecText;

	private string BestName;

	//public
	public InputField inputField;


	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

        GameUi           = GameObject.Find("GameUi");
        Text_currentTime = GameObject.Find("Text_currentTime");
        Text_bestTime    = GameObject.Find("Text_bestTime");
        Text_bestName    = GameObject.Find("Text_bestName");
        Text_newTime     = GameObject.Find("Text_newTime");
        Popup            = GameObject.Find("Popup");

        BestMinCount = PlayerPrefs.GetInt("BestMin");
		BestSecCount = PlayerPrefs.GetInt("BestSec");
		BestDecCount = PlayerPrefs.GetFloat("BestDec");

        CurrentMinCount = PlayerPrefs.GetInt("CurrentMin");
		CurrentSecCount = PlayerPrefs.GetInt("CurrentSec");
		CurrentDecCount = PlayerPrefs.GetFloat("CurrentDec");

		if(PlayerPrefs.GetString("BestName") == "") {
			BestName = "AAA";
		} else {
			BestName = PlayerPrefs.GetString("BestName");
		}

		//今回の記録を表示
		showCurrentRecord();

		//最高記録と比較・表示
		showBestRecord();
	
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
	

    // リトライボタンを選択
	public void tapRetryButton () {

		HideContents();

		Invoke("LoadSingle",  0.4f);

	}
	// 再度singleモードを表示
    public void LoadSingle () {
        SceneManager.LoadScene("InGameSingle");
    }


    // 入力情報保存
    public void saveNewRecord () {
        PlayerPrefs.SetString("LastName", inputField.text);

        PlayerPrefs.SetInt("BestMin", CurrentMinCount);
		PlayerPrefs.SetInt("BestSec", CurrentSecCount);
		PlayerPrefs.SetFloat("BestDec", CurrentDecCount);

		//ポップアップを閉じる
		Text_bestTime.GetComponent<Text> ().text = MinText +":"+ SecText +":"+ DecText;
		Text_bestName.GetComponent<Text> ().text = "by " + inputField.text;
		Popup.SetActive(false);
    }


    //今回の記録を表示
    void showCurrentRecord () {

    	if (CurrentMinCount < 10) {
			MinText = string.Format ("0{0}", CurrentMinCount.ToString ());
		} else {
			MinText = string.Format ("{0}", CurrentMinCount.ToString ());
		}
		if (CurrentSecCount < 10) {
			SecText = string.Format ("0{0}", CurrentSecCount.ToString ());
		} else {
			SecText = string.Format ("{0}", CurrentSecCount.ToString ());
		}
		if (CurrentDecCount >= 0 && CurrentDecCount < 9.9) {
			DecText = string.Format ("0{0}", CurrentDecCount.ToString ("f0"));
		} else if (CurrentDecCount < 99.9) {
			DecText = string.Format ("{0}", CurrentDecCount.ToString ("f0"));
		}

		Text_currentTime.GetComponent<Text> ().text = MinText +":"+ SecText +":"+ DecText;

    }

    //これまでの最高記録を表示
    void showBestRecord () {

    	if (BestMinCount < 10) {
			MinText = string.Format ("0{0}", BestMinCount.ToString ());
		} else {
			MinText = string.Format ("{0}", BestMinCount.ToString ());
		}
		if (BestSecCount < 10) {
			SecText = string.Format ("0{0}", BestSecCount.ToString ());
		} else {
			SecText = string.Format ("{0}", BestSecCount.ToString ());
		}
		if (BestDecCount >= 0 && BestDecCount < 9.9) {
			DecText = string.Format ("0{0}", BestDecCount.ToString ("f0"));
		} else if (BestDecCount < 99.9) {
			DecText = string.Format ("{0}", BestDecCount.ToString ("f0"));
		}

		Text_bestTime.GetComponent<Text> ().text = MinText +":"+ SecText +":"+ DecText;
		Text_bestName.GetComponent<Text> ().text = "by " + BestName;

		checkBestRecord();

    }

    //最高記録と比較
    void checkBestRecord () {
    	if (MinText!="00" &&  SecText!="00" && DecText!="00") {
    		//分の比較
	    	if (BestMinCount > CurrentMinCount) {
	    		//小さければ必ずOK
	    		showInputPopup();
	    	} else if (BestMinCount == CurrentMinCount) {
	    	 	//秒の比較
	    	 	if (BestSecCount > CurrentSecCount){
	    	 		//小さければ必ずOK
	    	 		showInputPopup();
				} else if (BestSecCount == CurrentSecCount) {
					if (BestDecCount > CurrentDecCount){
						showInputPopup();
					}
				}
	    	}
    	} else {
    		//記録がなければ登録
    		showInputPopup();
    	}
    }

    //NewRecorポップアップを表示
    void showInputPopup () {
    	if (PlayerPrefs.GetString("LastName") != "") {
    		inputField.text = PlayerPrefs.GetString("LastName");
    	}
    	Text_newTime.GetComponent<Text> ().text = MinText +":"+ SecText +":"+ DecText;
    	Popup.GetComponent<uTools.uTweenPosition> ().enabled = true;
    }



    //コンテンツを隠す
	void HideContents () {
		GameUi.GetComponent<uTools.uTweenAlpha> ().enabled = true;
	}
}

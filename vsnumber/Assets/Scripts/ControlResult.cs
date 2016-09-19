using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlResult : MonoBehaviour {

	public Sprite background1;
	public Sprite background2;
	public Sprite background3;
	public Sprite background4;
	public Sprite background5;

	//private
	private GameObject GameUi;
	private GameObject Btn_retry;
	private GameObject Text_record;
	private GameObject Text_currentTime;
	private GameObject Text_bestRecord;
	private GameObject Text_bestTime;
	private GameObject Text_bestName;
	private GameObject Text_newTime;
	private GameObject Popup;
	private GameObject Background;

	private int BestMinCount;
	private int BestSecCount;
	private float BestDecCount;
	private int CurrentMinCount;
	private int CurrentSecCount;
	private float CurrentDecCount;

	private string MinText;
	private string SecText;
	private string DecText;
	private string BestMinText;
	private string BestSecText;
	private string BestDecText;

	private string BestName;

	//public
	public InputField inputField;


	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

        GameUi           = GameObject.Find("GameUi");
        Btn_retry        = GameObject.Find("Btn_retry");
        Text_record      = GameObject.Find("Text_record");
        Text_currentTime = GameObject.Find("Text_currentTime");
        Text_bestRecord  = GameObject.Find("Text_bestRecord");
        Text_bestTime    = GameObject.Find("Text_bestTime");
        Text_bestName    = GameObject.Find("Text_bestName");
        Text_newTime     = GameObject.Find("Text_newTime");
        Popup            = GameObject.Find("Popup");
        Background       = GameObject.Find("Background");

        BestMinCount = PlayerPrefs.GetInt("BestMin");
		BestSecCount = PlayerPrefs.GetInt("BestSec");
		BestDecCount = PlayerPrefs.GetFloat("BestDec");

        CurrentMinCount = PlayerPrefs.GetInt("CurrentMin");
		CurrentSecCount = PlayerPrefs.GetInt("CurrentSec");
		CurrentDecCount = PlayerPrefs.GetFloat("CurrentDec");

		changeBackground();

		if(PlayerPrefs.GetString("BestName") == "") {
			BestName = "AAA";
		} else {
			BestName = PlayerPrefs.GetString("BestName");
		}

		//今回の記録を表示
		Invoke("showCurrentRecord",  0.2f);
		//showCurrentRecord();

		//最高記録と比較・表示
		Invoke("showBestRecord",  1.2f);
		//showBestRecord();
	
	}
	
	// Update is called once per frame
	void Update () {
			// エスケープキー取得
		    if (Input.GetKey(KeyCode.Escape))
		    {
		        tapReturnButton();
		        return;
		    }
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

		Invoke("LoadGame",  0.4f);

	}
	// 再度ゲームを表示
    public void LoadGame () {
    	if (PlayerPrefs.GetInt("playMode") == 1) {
			SceneManager.LoadScene("InGameSingle");
		} else {
			SceneManager.LoadScene("InGameVs");
		}
        
    }


    // 入力情報保存
    public void saveNewRecord () {
        PlayerPrefs.SetString("LastName", inputField.text);
        PlayerPrefs.SetString("BestName", inputField.text);

        PlayerPrefs.SetInt("BestMin", CurrentMinCount);
		PlayerPrefs.SetInt("BestSec", CurrentSecCount);
		PlayerPrefs.SetFloat("BestDec", CurrentDecCount);

		//ポップアップを閉じる
		Text_bestTime.GetComponent<Text> ().text = MinText +":"+ SecText +":"+ DecText;
		Text_bestName.GetComponent<Text> ().text = "by " + inputField.text;
		Popup.SetActive(false);
		showContinueBtn();

		if (PlayerPrefs.GetInt("isFirstPlay") == 0) {
			PlayerPrefs.SetInt("isFirstPlay", 1);
		}
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
		Text_record.GetComponent<uTools.uTweenPosition> ().enabled = true;

    }

    //これまでの最高記録を表示
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

		checkBestRecord();

		Text_bestTime.GetComponent<Text> ().text = BestMinText +":"+ BestSecText +":"+ BestDecText;
		Text_bestName.GetComponent<Text> ().text = "by " + BestName;
		Text_bestRecord.GetComponent<uTools.uTweenPosition> ().enabled = true;
    }

    //最高記録と比較
    void checkBestRecord () {
    	if (PlayerPrefs.GetInt("isFirstPlay") != 0) {
    		//分の比
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
					} else {
						showContinueBtn();
					}
				} else {
					showContinueBtn();
				}
	    	} else {
	    		showContinueBtn();
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


    //continueボタンを表示
    void showContinueBtn () {
    	Btn_retry.GetComponent<uTools.uTweenPosition> ().enabled = true;
    }



    //コンテンツを隠す
	void HideContents () {
		GameUi.GetComponent<uTools.uTweenAlpha> ().enabled = true;
	}
}

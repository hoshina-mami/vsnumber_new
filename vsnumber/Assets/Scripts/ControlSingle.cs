using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlSingle : MonoBehaviour {

	public Sprite background1;
	public Sprite background2;
	public Sprite background3;
	public Sprite background4;
	public Sprite background5;

	public ParticleSystem tapEffect;//タップエフェクト
    public Camera _camera;// カメラの座標

	//private
	private Vector3 pos;
	private GameObject GameUi;
	private GameObject Btn_return;
	private GameObject Btn_start;
	private GameObject Content;
	private GameObject Content2;
	private GameObject Box_Timer;
	private GameObject Text_bestRecord;
	private GameObject Text_bestTime;
	private GameObject Text_bestName;
	private GameObject Text_countDown;
	private GameObject Text_complete;
	private GameObject Background;
	private Text Text_countDown_text;
	private Timer Timer;
	//private GameObject _AdBannerObserver;

	private AudioSource se_complete;
    private AudioSource se_ok;
    private AudioSource se_btn1;
    private AudioSource se_countdown;
    private AudioSource se_cancel;

	private int CurrentNum;//現在の数を保存する変数
	private int CountDownNum;//ゲーム開始時のカウントダウン
	private int BestMinCount;
	private int BestSecCount;
	private float BestDecCount;

	private string MinText;
	private string SecText;
	private string DecText;

	private string BestName;

	private bool isPlayed;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

        GameUi          = GameObject.Find("GameUi");
        Btn_return      = GameObject.Find("Btn_return");
        Btn_start       = GameObject.Find("Btn_start");
        Content         = GameObject.Find("Content");
        Content2        = GameObject.Find("Content2");
        Box_Timer       = GameObject.Find("Box_Timer");
        Text_bestRecord = GameObject.Find("Text_bestRecord");
        Text_bestTime   = GameObject.Find("Text_bestTime");
        Text_bestName   = GameObject.Find("Text_bestName");
        Text_countDown  = GameObject.Find("Text_countDown");
        Text_complete   = GameObject.Find("Text_complete");
        Background      = GameObject.Find("Background");
        Text_countDown_text = Text_countDown.GetComponent<Text> ();
        Timer           = Box_Timer.GetComponent<Timer>();

        //_AdBannerObserver  = GameObject.Find("_AdBannerObserver");

        AudioSource[] audioSources = GetComponents<AudioSource>();
		se_complete = audioSources[0];
		se_ok = audioSources[1];
		se_btn1 = audioSources[2];
		se_countdown = audioSources[3];
		se_cancel = audioSources[4];

		//音量のON/OFF
		if (PlayerPrefs.GetInt("SoundFlg") != 0) {
			AudioListener.volume = 0;
		} else {
			AudioListener.volume = 0.7f;
		}

        changeBackground();

        CurrentNum = 1;
        CountDownNum = 3;

        Box_Timer.SetActive(false);
        //_AdBannerObserver.SetActive(false);

        BestMinCount = PlayerPrefs.GetInt("BestMin");
		BestSecCount = PlayerPrefs.GetInt("BestSec");
		BestDecCount = PlayerPrefs.GetFloat("BestDec");

		if(PlayerPrefs.GetString("BestName") == "") {
			BestName = "AAA";
		} else {
			BestName = PlayerPrefs.GetString("BestName");
		}

        showBestRecord();

        isPlayed = false;
	
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

    }





    //------------------------------------------------------
    // InGame Control
    //------------------------------------------------------

    // startボタン選択でゲームスタート
    public void startGame () {
    	//se_ok.PlayOneShot(se_ok.clip);

    	Btn_start.GetComponent<uTools.uTweenAlpha> ().enabled = true;
    	Text_bestRecord.GetComponent<uTools.uTweenAlpha> ().enabled = true;
    	Text_countDown.SetActive (true);

    	//カウントダウン
    	Invoke("countDownNumber",  0.5f);
    	Invoke("countDownNumber",  1.5f);
    	Invoke("countDownNumber",  2.5f);

    	//タップスタート
    	Invoke("startTapNumber",  3.5f);
    	
    }


    /*
	 * 次に押すべきボタンナンバーを返す
	 * @return {int} CurrentNum
	 */
	public int GetCurrentNum() {
		return CurrentNum;
	}


	/*
	 * ボタンナンバーを更新
	 */
	public void addCurrentNum() {
		 CurrentNum++;

		// マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
        pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
        tapEffect.transform.position = pos;
        tapEffect.Emit(1);

		 if (CurrentNum == 16) {
		 	//クリア表示
		 	se_complete.PlayOneShot(se_complete.clip);
		 	Timer.setStartFlg(false);
		 	Text_complete.GetComponent<uTools.uTweenPosition> ().enabled = true;

		 	//結果を一時保存
		 	saveCurrentTime();

		 	PlayerPrefs.SetInt("playMode", 1);

		 	//結果画面へ飛ばす
		 	isPlayed = true;
		 	Invoke("HideContents",  1.0f);
		 	Invoke("LoadResult",  1.5f);
		 } else {
		 	se_btn1.PlayOneShot(se_btn1.clip);
		 }
	}


	//ゲーム中のボタンを押した時の処理
    public void tapNumberBtn () {
    	//Debug.Log("tap");
    }


    /*
	 * ゲーム内容の開始
	 */
	void startTapNumber () {

		Text_countDown_text.text  = "";

		//ボタンをアクティブにする
    	Content.SetActive (true);
    	Content2.SetActive (false);

    	//タイマーをスタートする
    	//Timer.setStartFlg(true);
    	Box_Timer.SetActive(true);
	}



    //ゲーム開始時のカウントダウン
    void countDownNumber () {
    	se_countdown.PlayOneShot(se_countdown.clip);

		Text_countDown_text.text  = CountDownNum.ToString();

		CountDownNum = CountDownNum - 1;

	}


	//今回のタイムを一時保存
	void saveCurrentTime () {
		PlayerPrefs.SetInt("CurrentMin", Timer.getCurrentMin());
		PlayerPrefs.SetInt("CurrentSec", Timer.getCurrentSec());
		PlayerPrefs.SetFloat("CurrentDec", Timer.getCurrentDec());
	}


	//コンテンツを隠す
	void HideContents () {
		Btn_start.SetActive(false);
		Text_bestRecord.SetActive(false);
		GameUi.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Content2.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		if (!isPlayed) {
			Content.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		}
	}


	// 結果画面へとぶ
    void LoadResult () {
        SceneManager.LoadScene("Result");
    }

    //------------------------------------------------------
}

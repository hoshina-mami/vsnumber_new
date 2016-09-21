using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlVs : MonoBehaviour {

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
	private GameObject Content3;
	private GameObject Content4;
	private GameObject TitleText;
	private GameObject Text_bestRecord;
	private GameObject Box_Timer;
	private GameObject Text_countDown;
	private GameObject Text_complete;
	private GameObject finish1;
	private GameObject finish2;
	private GameObject Text_result1;
	private GameObject Text_result2;
	private GameObject Background;
	private GameObject Win1;
	private GameObject Win2;
	private GameObject Text_Win1;
	private GameObject Text_Win2;
	private Text Text_countDown_text;
	private Timer Timer;
	private BtnGeneratorVs1 BtnGeneratorVs1;
	private BtnGeneratorVs2 BtnGeneratorVs2;

	private AudioSource se_complete;
    private AudioSource se_btn1;
    private AudioSource se_btn2;
    private AudioSource se_countdown;
    private AudioSource se_cancel;

	private int CurrentNum1;//現在の数を保存する変数
	private int CurrentNum2;//現在の数を保存する変数
	private int CountDownNum;//ゲーム開始時のカウントダウン

	private int WinPlayer;

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
        Content3        = GameObject.Find("Content3");
        Content4        = GameObject.Find("Content4");
        TitleText       = GameObject.Find("TitleText");
        Text_bestRecord = GameObject.Find("Text_bestRecord");
        Box_Timer       = GameObject.Find("Box_Timer");
        Text_countDown  = GameObject.Find("Text_countDown");
        Text_complete   = GameObject.Find("Text_complete");
        finish1         = GameObject.Find("finish1");
        finish2         = GameObject.Find("finish2");
        Text_result1    = GameObject.Find("Text_result1");
        Text_result2    = GameObject.Find("Text_result2");
        Background      = GameObject.Find("Background");
        Win1            = GameObject.Find("Win1");
        Win2            = GameObject.Find("Win2");
        Text_Win1       = GameObject.Find("Text_Win1");
        Text_Win2       = GameObject.Find("Text_Win2");
        Text_countDown_text = Text_countDown.GetComponent<Text> ();
        Timer           = Box_Timer.GetComponent<Timer>();
        BtnGeneratorVs1 = GameObject.Find("BtnGenerator").GetComponent<BtnGeneratorVs1>();
        BtnGeneratorVs2 = GameObject.Find("BtnGenerator2").GetComponent<BtnGeneratorVs2>();

        Box_Timer.SetActive(false);
        finish1.SetActive(false);
        finish2.SetActive(false);
        Text_Win1.GetComponent<Text> ().text = PlayerPrefs.GetInt("Win1").ToString();
        Text_Win2.GetComponent<Text> ().text = PlayerPrefs.GetInt("Win2").ToString();
        Win1.SetActive(false);
        Win2.SetActive(false);

        CurrentNum1 = 1;
        CurrentNum2 = 1;
        CountDownNum = 3;

        isPlayed = false;

        changeBackground();

        AudioSource[] audioSources = GetComponents<AudioSource>();
		se_complete = audioSources[0];
		se_btn1 = audioSources[1];
		se_btn2 = audioSources[2];
		se_countdown = audioSources[3];
		se_cancel = audioSources[4];

		//音量のON/OFF
		if (PlayerPrefs.GetInt("SoundFlg") != 0) {
			AudioListener.volume = 0;
		} else {
			AudioListener.volume = 0.7f;
		}
	
	}
	


	// 戻るボタンを選択
	public void tapReturnButton () {

		HideContents();

		Invoke("LoadTitle",  0.4f);

		se_cancel.PlayOneShot(se_cancel.clip);

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



    //------------------------------------------------------
    // InGame Control
    //------------------------------------------------------

    // startボタン選択でゲームスタート
    public void startGame () {
    	Btn_start.SetActive (false);
    	Btn_return.SetActive (false);
    	Text_bestRecord.SetActive (false);
    	TitleText.SetActive (false);
    	Text_countDown.SetActive (true);

    	BtnGeneratorVs1.GanerateBtns();
    	BtnGeneratorVs2.GanerateBtns();

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
	public int GetCurrentNum1() {
		return CurrentNum1;
	}


	/*
	 * ボタンナンバーを更新
	 */
	public void addCurrentNum1() {
		 CurrentNum1++;

		 // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
        pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
        tapEffect.transform.position = pos;
        tapEffect.Emit(1);

		 if (CurrentNum1 == 16) {
		 	//クリア表示
		 	se_complete.PlayOneShot(se_complete.clip);
		 	Timer.setStartFlg(false);
		 	finish1.SetActive(true);
        	finish2.SetActive(true);
        	Text_result1.GetComponent<Text> ().text = "WIN!";

		 	//結果を一時保存
		 	saveCurrentTime();

		 	PlayerPrefs.SetInt("playMode", 2);

		 	WinPlayer = 1;
		 	Win1.SetActive(true);
		 	Win2.SetActive(true);
		 	Invoke("UpdateWinCount",  1f);

		 	isPlayed = true;
	
		 } else {
		 	se_btn1.PlayOneShot(se_btn1.clip);
		 }
	}


	// 勝利数の更新
    void UpdateWinCount () {
    	if (WinPlayer == 1) {
    		PlayerPrefs.GetInt("Win1");
    		PlayerPrefs.SetInt("Win1", PlayerPrefs.GetInt("Win1") + 1);
    		Text_Win1.GetComponent<Text> ().text = PlayerPrefs.GetInt("Win1").ToString();
    	} else {
    		PlayerPrefs.GetInt("Win2");
    		PlayerPrefs.SetInt("Win2", PlayerPrefs.GetInt("Win2") + 1);
    		Text_Win2.GetComponent<Text> ().text = PlayerPrefs.GetInt("Win2").ToString();
    	}
    }


	/*
	 * 次に押すべきボタンナンバーを返す
	 * @return {int} CurrentNum
	 */
	public int GetCurrentNum2() {
		return CurrentNum2;
	}


	/*
	 * ボタンナンバーを更新
	 */
	public void addCurrentNum2() {
		 CurrentNum2++;

		 // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
        pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
        tapEffect.transform.position = pos;
        tapEffect.Emit(1);

		 if (CurrentNum2 == 16) {
		 	//クリア表示
		 	se_complete.PlayOneShot(se_complete.clip);
		 	Timer.setStartFlg(false);
		 	finish1.SetActive(true);
        	finish2.SetActive(true);
        	Text_result2.GetComponent<Text> ().text = "WIN!";

		 	//結果を一時保存
		 	saveCurrentTime();

		 	WinPlayer = 2;
		 	Win1.SetActive(true);
		 	Win2.SetActive(true);
		 	Invoke("UpdateWinCount",  1f);

		 	isPlayed = true;
		 } else {
		 	se_btn2.PlayOneShot(se_btn2.clip);
		 }
	}

	public void endGame () {
    	//結果画面へ飛ばす
	 	Invoke("HideContents",  0.3f);
	 	Invoke("LoadResult",  0.7f);
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
    	Content3.SetActive (true);
    	Content4.SetActive (false);

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
		GameUi.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Content4.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Content2.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		if (!isPlayed) {
			Content.GetComponent<uTools.uTweenAlpha> ().enabled = true;
			Content3.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		}
	}


	// 結果画面へとぶ
    void LoadResult () {
        SceneManager.LoadScene("Result");
    }
}

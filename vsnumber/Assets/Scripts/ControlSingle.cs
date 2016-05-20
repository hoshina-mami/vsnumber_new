using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlSingle : MonoBehaviour {

	//private
	private GameObject GameUi;
	private GameObject Btn_return;
	private GameObject Btn_start;
	private GameObject Content;
	private GameObject Content2;
	private GameObject Text_bestRecord;
	private GameObject Text_countDown;
	private Text Text_countDown_text;

	private int CurrentNum;//現在の数を保存する変数
	private int CountDownNum;//ゲーム開始時のカウントダウン

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

        GameUi          = GameObject.Find("GameUi");
        Btn_return      = GameObject.Find("Btn_return");
        Btn_start       = GameObject.Find("Btn_start");
        Content         = GameObject.Find("Content");
        Content2        = GameObject.Find("Content2");
        Text_bestRecord = GameObject.Find("Text_bestRecord");
        Text_countDown  = GameObject.Find("Text_countDown");
        Text_countDown_text = Text_countDown.GetComponent<Text> ();

        CurrentNum = 1;
        CountDownNum = 3;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// 戻るボタンを選択
	public void tapReturnButton () {

		//GetComponent<AudioSource>().Play();

		GameUi.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Content.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Content2.GetComponent<uTools.uTweenAlpha> ().enabled = true;

		Invoke("LoadTitle",  0.4f);

	}

    // タイトル画面へとぶ
    public void LoadTitle () {
        Application.LoadLevel("Title");
    }





    //------------------------------------------------------
    // InGame Control
    //------------------------------------------------------

    // startボタン選択でゲームスタート
    public void startGame () {
    	Btn_start.SetActive (false);
    	Text_bestRecord.SetActive (false);
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

		 if (CurrentNum == 16) {
		 	//クリア表示
		 	Text_countDown_text.text  = "complete!";
		 }
	}


	//ゲーム中のボタンを押した時の処理
    public void tapNumberBtn () {
    	Debug.Log("tap");
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
	}





    //ゲーム開始時のカウントダウン
    void countDownNumber () {

		Text_countDown_text.text  = CountDownNum.ToString();

		CountDownNum = CountDownNum - 1;

	}







    //------------------------------------------------------
}

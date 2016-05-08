using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ControlSingle : MonoBehaviour {

	//private
	private GameObject GameUi;
	private GameObject Btn_return;
	private GameObject Btn_start;
	private GameObject Content;

	private int CurrentNum;//現在の数を保存する変数

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

        GameUi       = GameObject.Find("GameUi");
        Btn_return   = GameObject.Find("Btn_return");
        Btn_start    = GameObject.Find("Btn_start");
        Content      = GameObject.Find("Content");

        CurrentNum = 1;
	
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





    //------------------------------------------------------
    // InGame Control
    //------------------------------------------------------

    // startボタン選択でゲームスタート
    public void startGame () {
    	Btn_start.SetActive (false);

    	//ボタンをアクティブにする
    	Content.SetActive (true);

    	//タイマーをスタートする
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
Debug.Log("clear");
		 }
	}


	//ゲーム中のボタンを押した時の処理
    public void tapNumberBtn () {
    	Debug.Log("tap");
    }







    //------------------------------------------------------
}

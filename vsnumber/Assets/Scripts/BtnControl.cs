using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnControl : MonoBehaviour {

	//private
	private ControlSingle ControlSingle;
	private Text btnText;
	private int btnNum;
	private int _currentBtnNum;

	// Use this for initialization
	void Start () {

		ControlSingle = GameObject.Find("Controller").GetComponent<ControlSingle>();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/*
     * ボタンを押した時の処理
     */
    public void tapNumBtn () {

        //押したボタンの番号を取得
        btnText = GetComponentInChildren<Text>();
        btnNum = int.Parse(btnText.text);

        //次に押すべきボタンの番号を取得
        _currentBtnNum = ControlSingle.GetCurrentNum();

        if (btnNum == _currentBtnNum) {
        	//ボタン番号を更新
        	ControlSingle.addCurrentNum();
        	//ボタンの表示を変更
        	GetComponent<Button>().interactable = false;
        	GetComponent<uTools.uTweenColor> ().enabled = true;
        }   
    }
}

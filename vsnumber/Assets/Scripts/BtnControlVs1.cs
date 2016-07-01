using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnControlVs1 : MonoBehaviour {

	//private
	private ControlVs ControlVs;
	private Text btnText;
	private int btnNum;
	private int _currentBtnNum;

	// Use this for initialization
	void Start () {

		ControlVs = GameObject.Find("Controller").GetComponent<ControlVs>();
	
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
        _currentBtnNum = ControlVs.GetCurrentNum1();

        if (btnNum == _currentBtnNum) {
        	//ボタン番号を更新
        	ControlVs.addCurrentNum1();
        	//ボタンの表示を変更
        	GetComponent<Button>().interactable = false;
        	GetComponent<uTools.uTweenColor> ().enabled = true;
        }   
    }
}

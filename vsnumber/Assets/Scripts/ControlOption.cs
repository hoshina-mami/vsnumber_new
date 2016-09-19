using UnityEngine;
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

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

		GameUi           = GameObject.Find("GameUi");
		Background       = GameObject.Find("Background");

		changeBackground();
	
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
}

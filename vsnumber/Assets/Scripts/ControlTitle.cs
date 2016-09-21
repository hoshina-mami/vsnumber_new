using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class ControlTitle : MonoBehaviour {

	//public
	public GameObject bgm;//生成したいプレハブ
	public Sprite background1;
	public Sprite background2;
	public Sprite background3;
	public Sprite background4;
	public Sprite background5;

	//private
	private GameObject cloneBgm;
	private GameObject TitleText;
	private GameObject Btn_vs;
	private GameObject Btn_single;
	//private GameObject Btn_record;
	private GameObject Btn_setting;
	private GameObject Background;
	private int _BackNum;

	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;

        TitleText       = GameObject.Find("TitleText");
        Btn_vs          = GameObject.Find("Btn_vs");
        Btn_single      = GameObject.Find("Btn_single");
        //Btn_record      = GameObject.Find("Btn_record");
        Btn_setting     = GameObject.Find("Btn_setting");
        Background      = GameObject.Find("Background");

        PlayerPrefs.SetInt("Win1", 0);
        PlayerPrefs.SetInt("Win2", 0);

        //音量のON/OFF
		if (PlayerPrefs.GetInt("SoundFlg") != 0) {
			AudioListener.volume = 0;
		} else {
			AudioListener.volume = 0.7f;
		}

        //BGMを流す
		if (GameObject.Find("BGM(Clone)") == null) {
			cloneBgm = (GameObject)Instantiate(bgm);
		}

        //背景を切り替える
        setBackground();
	
	}
	
	// Update is called once per frame
	void Update () {
		// プラットフォームがアンドロイドかチェック
		if (Application.platform == RuntimePlatform.Android)
		{
		    // エスケープキー取得
		    if (Input.GetKey(KeyCode.Escape))
		    {
		        // アプリケーション終了
		        Application.Quit();
		        return;
		    }
		}
	}


	// メニューボタンを選択
	public void tapButton (string target) {

		GetComponent<AudioSource>().Play();

		hideObjects ();

		if (target == "InGameSingle") {
			Invoke("LoadInGameSingle",  0.4f);
		} else if (target == "InGameVs") {
			Invoke("LoadInGameVs",  0.4f);
		} else if (target == "Option") {
			Invoke("LoadOption",  0.4f);
		}
		

	}

    // SingleMode選択画面へとぶ
    public void LoadInGameSingle () {
    	SceneManager.LoadScene("InGameSingle");
    }

	// VsMode選択画面へとぶ
    public void LoadInGameVs () {
    	SceneManager.LoadScene("InGameVs");
    }

    // Option画面へとぶ
    public void LoadOption () {
    	SceneManager.LoadScene("Option");
    }


    //背景画像変更
    void setBackground () {
    	int randomNum = Random.Range(1, 6);
    	PlayerPrefs.SetInt("BackNum", randomNum);
    	_BackNum = PlayerPrefs.GetInt("BackNum");
    	switch (_BackNum) {
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


    //画面遷移のさいにオブジェクトを消す
	void hideObjects () {

		//makes object's alpha 0
		TitleText.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_vs.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_single.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		//Btn_record.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		Btn_setting.GetComponent<uTools.uTweenAlpha> ().enabled = true;
		
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnGenerator : MonoBehaviour {

	//public
	public GameObject BtnBox;//ボタンを横に並べる用のプレハブ
	public GameObject Btn_inGame;//ボタン用のプレハブ

	//private
	private GameObject Content;
	private GameObject cloneBox;
	private GameObject cloneBtn;
	private Vector3 newScale;
	private Vector3 newPosition;
	private Text cloneBtnNum;
	//private int _HighScoreStageNum;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

		Content = GameObject.Find("Content");

		newScale.x = 1.2f;
		newScale.y = 1.2f;
		newScale.z = 1.2f;

		GanerateBtns();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    // ボタンの生成をスタート
    public void GanerateBtns () {
        Debug.Log("ok");

        for (int i = 0; i < 3; i++) {

			cloneBox = (GameObject)Instantiate(BtnBox);
			cloneBox.transform.SetParent(Content.transform, true );
			cloneBox.transform.localScale = newScale;

			for (int j = 1; j < 6; j++) {

				cloneBtn = (GameObject)Instantiate(Btn_inGame);
				cloneBtn.transform.SetParent(cloneBox.transform, true );
				cloneBtn.transform.localScale = newScale;

				int thisStageNum = j + i * 5;
				cloneBtnNum = cloneBtn.GetComponentInChildren<Text>();
				cloneBtnNum.text = thisStageNum.ToString();

				//ボタンのアクティブ・非アクティブを設定
				//if (thisStageNum == 1 || thisStageNum <= (_HighScoreStageNum + 1)) {
				//	cloneBtn.GetComponent<Button>().interactable = true;
				//}

			}

		}
    }
}

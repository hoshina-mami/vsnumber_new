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
	private int[] deck = new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
	//private int _HighScoreStageNum;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;

		Content = GameObject.Find("Content");

		newScale.x = 1.2f;
		newScale.y = 1.2f;
		newScale.z = 1.2f;

        for (int i = 0; i < deck.Length; i++) {
            int temp = deck[i];
            int randomIndex = Random.Range(0, deck.Length);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }
		GanerateBtns();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    // ボタンの生成をスタート
    public void GanerateBtns () {

        for (int i = 0; i < 3; i++) {

			cloneBox = (GameObject)Instantiate(BtnBox);
			cloneBox.transform.SetParent(Content.transform, true );
			cloneBox.transform.localScale = newScale;

			for (int j = 0; j < 5; j++) {

				cloneBtn = (GameObject)Instantiate(Btn_inGame);
				cloneBtn.transform.SetParent(cloneBox.transform, true );
				cloneBtn.transform.localScale = newScale;

				//数字を設定
				int thisStageNum = j + i * 5;
				cloneBtnNum = cloneBtn.GetComponentInChildren<Text>();
				cloneBtnNum.text = deck[thisStageNum].ToString();

				//ボタンを非アクティブにしておく
				//cloneBtn.GetComponent<Button>().interactable = false;

			}

		}

		Content.SetActive (false);
    }
}

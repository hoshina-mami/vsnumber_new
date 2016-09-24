using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BtnGeneratorVs2 : MonoBehaviour {

	//public
	public GameObject BtnBox;//ボタンを横に並べる用のプレハブ
	public GameObject Btn_inGame;//ボタン用のプレハブ
	public GameObject Content3;
	public GameObject Content4;

	//private
	private GameObject cloneBox;
	private GameObject cloneBtn;
	private Vector3 newScale;
	private Vector3 newPosition;
	private Quaternion newRotation;
	private Text cloneBtnNum;
	private int[] deck = new int[] {1,2,3,4,5,6,7,8,9,10,11,12,13,14,15};
	//private int _HighScoreStageNum;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		
		newScale.x = 1.9f;
		newScale.y = 1.9f;
		newScale.z = 0f;
		newPosition.z = 0f;
		newRotation.z = 180;

        for (int i = 0; i < deck.Length; i++) {
            int temp = deck[i];
            int randomIndex = Random.Range(0, deck.Length);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }	
	}


    // ボタンの生成をスタート
    public void GanerateBtns () {

        for (int i = 0; i < 3; i++) {

			cloneBox = (GameObject)Instantiate(BtnBox);
			cloneBox.transform.SetParent(Content3.transform, true );
			cloneBox.transform.localScale = newScale;
			cloneBox.transform.localPosition = newPosition;

			for (int j = 0; j < 5; j++) {

				cloneBtn = (GameObject)Instantiate(Btn_inGame);
				cloneBtn.transform.SetParent(cloneBox.transform, true );
				cloneBtn.transform.localScale = newScale;
				cloneBtn.transform.localPosition = newPosition;
				cloneBtn.transform.rotation = newRotation;

				//数字を設定
				int thisStageNum = j + i * 5;
				cloneBtnNum = cloneBtn.GetComponentInChildren<Text>();
				cloneBtnNum.text = deck[thisStageNum].ToString();

				//ボタンを非アクティブにしておく
				//cloneBtn.GetComponent<Button>().interactable = false;

			}

		}

		Content3.SetActive (false);


		for (int k = 0; k < 3; k++) {

			cloneBox = (GameObject)Instantiate(BtnBox);
			cloneBox.transform.SetParent(Content4.transform, true );
			cloneBox.transform.localScale = newScale;
			cloneBox.transform.localPosition = newPosition;

			for (int l = 0; l < 5; l++) {

				cloneBtn = (GameObject)Instantiate(Btn_inGame);
				cloneBtn.transform.SetParent(cloneBox.transform, true );
				cloneBtn.transform.localScale = newScale;
				cloneBtn.transform.localPosition = newPosition;


				//ボタンを非アクティブにしておく
				//cloneBtn.GetComponent<Button>().interactable = false;

			}

		}
    }
}

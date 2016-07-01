using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
	
	public float timeCount = 0;
    public float DecCount;
 
	public int MinCount;
	public int SecCount;

	private Text Min;
	private Text Sec;
	private Text Decimal;
	private bool isStarted;

	// Use this for initialization
	void Start () {
		Min = GameObject.Find ("Min").GetComponent<Text> ();
		Sec = GameObject.Find ("Sec").GetComponent<Text> ();
		Decimal = GameObject.Find ("Decimal").GetComponent<Text> ();

		isStarted = true;
	}

	// Update is called once per frame
	void Update () {


		if (isStarted) {
	
			timeCount += 1.0f * Time.deltaTime;
			DecCount = timeCount * 100;
	 
			if (timeCount >= 0.98) {
				timeCount = 0;
				SecCount = SecCount + 1;
			} else if (SecCount >= 60) {
				SecCount = 0;
				MinCount = MinCount + 1;
			}
	 
			if (MinCount < 10) {
				Min.text = string.Format ("0{0}", MinCount.ToString ());
			} else {
				Min.text = string.Format ("{0}", MinCount.ToString ());
			}
			if (SecCount < 10) {
				Sec.text = string.Format ("0{0}", SecCount.ToString ());
			} else {
				Sec.text = string.Format ("{0}", SecCount.ToString ());
			}
			if (DecCount >= 0 && DecCount < 9.9) {
				Decimal.text = string.Format ("0{0}", DecCount.ToString ("f0"));
			} else if (DecCount < 99.9) {
				Decimal.text = string.Format ("{0}", DecCount.ToString ("f0"));
			}
		}
	}


	/*
	 * タイマーのフラグをセットする
	 */
	public void setStartFlg (bool flg) {
        isStarted = flg;
    }


    /*
	 * 今回のタイムを返す
	 */
	public int getCurrentMin () {
        return MinCount;
    }
    public int getCurrentSec () {
        return SecCount;
    }
    public float getCurrentDec () {
        return DecCount;
    }
}

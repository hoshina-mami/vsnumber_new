using UnityEngine;
using System.Collections;

public class BackgroundControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Sceneを遷移してもオブジェクトが消えないようにする
        DontDestroyOnLoad(this);
	}

}

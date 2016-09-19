using UnityEngine;
using System.Collections;

public class TapEffect : MonoBehaviour {

    public ParticleSystem tapEffect;//タップエフェクト
    public Camera _camera;// カメラの座標

    private Vector3 pos;


    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
            pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            tapEffect.transform.position = pos;
            tapEffect.Emit(1);
        }
    }
}

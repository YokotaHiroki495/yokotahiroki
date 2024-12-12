using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraScript : MonoBehaviour
{

    //プレイヤーを取得
    [SerializeField]private GameObject player;

    Player playerScript;

    //加速時のエフェクト
    [SerializeField] GameObject DashEffect;

    float yVelocity = 5.0f;
    [SerializeField] private float NomalFov = 75f;       //通常時のカメラFOV
    [SerializeField] private float AccelFov  = 100f;    //加速時のカメラFOV
    [SerializeField] private float DashCameraRetunTime = 2; //加速時に切り替わったカメラを元の位置に戻すまでの時間
    private float smoothTime = 1000;

    float StartFov;
    float NowFov;
    Camera Camera = null;

    //プレイヤーとの距離
    [SerializeField]private float offsetZ;
    float offsetZ_Save = 0f;

    //target(Player)の位置を取得するため
    [SerializeField] private Transform target;

    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<Player>();


        //カメラとプレイヤーの距離を求める(プレイヤーの左右にも追従)
        StartFov = 75f;
        offsetZ_Save = offsetZ;


        //カメラの位置を設定
        Camera = Camera.main;

         

}

    
    void Update()
    {
        //オブジェクト座標を変数に格納
        Vector3 pos = transform.position;
        pos.z = target.position.z + offsetZ;
        transform.position = pos;

       

       
        //カメラのFOVがStartFovより大きいなら
        if(Camera.fieldOfView > StartFov)
        {
            //FOVを少しずつ小さくする
            Camera.fieldOfView -= 1f;
          
            //加速時のエフェクトを表示
            DashEffect.SetActive(true);
        }

        //カメラの位置の比較をして
        //元の位置より小さかったら
        if(offsetZ < offsetZ_Save)
        {
            //大きくする
            offsetZ += 1f;
           
            
        }
        //FOVが80以下なら
        if(Camera.fieldOfView < 80)
        {
            //ダッシュのエフェクトを非表示
            DashEffect.SetActive(false);
        }

    }


    //コルーチン
    public IEnumerator CameraMove()
    {
        Camera.main.fieldOfView = Mathf.SmoothDamp(AccelFov, NomalFov, ref yVelocity, smoothTime);

        offsetZ -= 5;

        
        yield return new WaitForSeconds(DashCameraRetunTime);

        playerScript.DashNow = false;






    }

   
}

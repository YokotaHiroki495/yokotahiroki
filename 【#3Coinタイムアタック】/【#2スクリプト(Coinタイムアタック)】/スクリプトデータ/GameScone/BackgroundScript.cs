using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    //プレイヤーを取得
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;

    //プレイヤーとの距離
    //private Vector3 offset;

    [SerializeField]float offsetZ;
    
  
    void Start()
    {
        //カメラとプレイヤーの距離を求める
       // offset.z = transform.position.z - player.transform.position.z;

    }


    void Update()
    {

        //新しいトランスフォームの値を代入
        //transform.position = player.transform.position + offset;
        Vector3 pos = transform.position;
        pos.z = target.position.z + offsetZ;
        transform.position = pos;
    }
}

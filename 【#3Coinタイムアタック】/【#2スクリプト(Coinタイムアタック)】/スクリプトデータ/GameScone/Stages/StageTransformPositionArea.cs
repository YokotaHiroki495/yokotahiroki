using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTransformPositionArea : MonoBehaviour
{
    //ステージを移動させるScript

    //追従する対象
    [SerializeField] private Transform target;

    //対象との距離
    [SerializeField] private float offsetX;




    // Update is called once per frame
    void Update()
    {
        
        //オブジェクト座標を変数に格納
        Vector3 pos = transform.position * Time.deltaTime;

        //動作環境で変動しないようにTime.deltaTimeをかけて公平性を保つ
        pos.z = target.position.z + offsetX　;
        transform.position = pos;


    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.CompareTag("StartArea"))
        {
            Destroy(other.gameObject);
        }
    }



}

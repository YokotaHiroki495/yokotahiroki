using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Playerに追従するカメラのScript

    //Playerの位置を取得
    [SerializeField] Transform player;

    //Playerの距離を取得
    [SerializeField] float CameraOffsetX;
    [SerializeField] float CameraOffsetY;
    [SerializeField] float CameraOffsetZ;

    [SerializeField] float LimitMinZ;

    void Start()
    {
        
    }

    
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x = player.position.x + CameraOffsetX;
       
        pos.z = player.position.z + CameraOffsetZ;

        pos.y = player.position.y + CameraOffsetY; 


        //Y座標を取得して、一定以下には下がらないようにする
        if (pos.y <= LimitMinZ)
        {
            //pos.y = LimitMinZ;
        }
        

        transform.position = pos;
        
    }
}

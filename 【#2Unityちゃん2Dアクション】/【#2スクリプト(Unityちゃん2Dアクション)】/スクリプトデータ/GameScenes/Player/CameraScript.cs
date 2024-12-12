using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //Player�ɒǏ]����J������Script

    //Player�̈ʒu���擾
    [SerializeField] Transform player;

    //Player�̋������擾
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


        //Y���W���擾���āA���ȉ��ɂ͉�����Ȃ��悤�ɂ���
        if (pos.y <= LimitMinZ)
        {
            //pos.y = LimitMinZ;
        }
        

        transform.position = pos;
        
    }
}

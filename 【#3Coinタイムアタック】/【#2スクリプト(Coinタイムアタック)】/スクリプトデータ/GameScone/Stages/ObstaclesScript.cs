using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    //��Q���Ɋւ���Script



    private void OnTriggerEnter(Collider other)
    {
        //�v���C���[�ɓ���������
        if (other.gameObject.tag == "Player")
        {
            //0.1�b��Ɏ��s
            //Invoke("BK", 0.1f);
            //renderer.enabled = false;
        }


        if (other.gameObject.tag == "DestroyArea")
        {
            //renderer.enabled = true;  //SetActive���Ɠ����蔻��������邩��true�ɂł��Ȃ�

        }
    }


  
}

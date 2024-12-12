using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScript : MonoBehaviour
{
    //�v���C���[���擾
    [SerializeField] private GameObject player;
    [SerializeField] private Transform target;

    //�v���C���[�Ƃ̋���
    //private Vector3 offset;

    [SerializeField]float offsetZ;
    
  
    void Start()
    {
        //�J�����ƃv���C���[�̋��������߂�
       // offset.z = transform.position.z - player.transform.position.z;

    }


    void Update()
    {

        //�V�����g�����X�t�H�[���̒l����
        //transform.position = player.transform.position + offset;
        Vector3 pos = transform.position;
        pos.z = target.position.z + offsetZ;
        transform.position = pos;
    }
}

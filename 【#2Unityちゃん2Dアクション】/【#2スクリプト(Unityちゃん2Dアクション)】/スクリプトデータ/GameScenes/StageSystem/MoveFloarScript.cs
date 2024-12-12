using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloarScript : MonoBehaviour
{
    //BossStage�̓�����

    //X���@Y���@�ǂ���ɓ��������̔���
    [SerializeField] bool moveXdir;

    // �������钷��
    [SerializeField] private float _length;

    //���W�ʒu
    [SerializeField] float xPos;
    [SerializeField] float yPos;


    void Update()
    {
        // ���������l�����Ԃ���v�Z
        var value = Mathf.PingPong(Time.time, _length);

        // y���W�����������ď㉺�^��������
        //transform.localPosition = new Vector2(xPos, value);


        if (moveXdir)
        {
            // y���W�����������ď㉺�^��������
            transform.localPosition = new Vector2(value, yPos);

        }
        else if (!moveXdir)
        {
            // y���W�����������ď㉺�^��������
            transform.localPosition = new Vector2(xPos, value);

        }
    }
}


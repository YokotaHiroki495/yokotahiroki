using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    //BattleRoyaleStage�̏���Script
    //�����Ă��鏰�����蔲�����悤�ɂ���


    PlatformEffector2D platFormEffector2d;


    bool SlipThrough = false; //���̂��蔲������

    void Start()
    {

        platFormEffector2d = GetComponent<PlatformEffector2D>();

    }

    void Update()
    {
        //���̂܂܂��ƁA�������삵�Ȃ��ƘA���ł��蔲���Ă��܂�
        
        if (Input.GetKey(KeyCode.Space))
        {
            //platFormEffector2d.rotationalOffset = 0f;
        }
    }


   
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Player���������Ɉړ����悤�Ƃ����
        if (Input.GetAxisRaw("Vertical") <0)
        {
            //�����蔻���180�x���]������
            platFormEffector2d.rotationalOffset = 180;
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        platFormEffector2d.rotationalOffset = 0f;
        
    }


}

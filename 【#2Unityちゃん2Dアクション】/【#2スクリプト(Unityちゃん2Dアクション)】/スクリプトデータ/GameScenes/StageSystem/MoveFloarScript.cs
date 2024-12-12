using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFloarScript : MonoBehaviour
{
    //BossStageの動く床

    //X軸　Y軸　どちらに動かすかの判定
    [SerializeField] bool moveXdir;

    // 往復する長さ
    [SerializeField] private float _length;

    //座標位置
    [SerializeField] float xPos;
    [SerializeField] float yPos;


    void Update()
    {
        // 往復した値を時間から計算
        var value = Mathf.PingPong(Time.time, _length);

        // y座標を往復させて上下運動させる
        //transform.localPosition = new Vector2(xPos, value);


        if (moveXdir)
        {
            // y座標を往復させて上下運動させる
            transform.localPosition = new Vector2(value, yPos);

        }
        else if (!moveXdir)
        {
            // y座標を往復させて上下運動させる
            transform.localPosition = new Vector2(xPos, value);

        }
    }
}


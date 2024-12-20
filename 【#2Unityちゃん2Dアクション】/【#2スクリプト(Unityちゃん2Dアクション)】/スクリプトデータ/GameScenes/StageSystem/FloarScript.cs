using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    //BattleRoyaleStageの床のScript
    //浮いている床をすり抜けれるようにする


    PlatformEffector2D platFormEffector2d;


    bool SlipThrough = false; //床のすり抜け判定

    void Start()
    {

        platFormEffector2d = GetComponent<PlatformEffector2D>();

    }

    void Update()
    {
        //このままだと、何も操作しないと連続ですり抜けてしまう
        
        if (Input.GetKey(KeyCode.Space))
        {
            //platFormEffector2d.rotationalOffset = 0f;
        }
    }


   
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Playerが下方向に移動しようとすると
        if (Input.GetAxisRaw("Vertical") <0)
        {
            //当たり判定を180度反転させる
            platFormEffector2d.rotationalOffset = 180;
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        platFormEffector2d.rotationalOffset = 0f;
        
    }


}

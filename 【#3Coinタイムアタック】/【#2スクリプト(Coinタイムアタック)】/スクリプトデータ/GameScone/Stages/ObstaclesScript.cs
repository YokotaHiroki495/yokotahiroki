using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesScript : MonoBehaviour
{
    //障害物に関するScript



    private void OnTriggerEnter(Collider other)
    {
        //プレイヤーに当たったら
        if (other.gameObject.tag == "Player")
        {
            //0.1秒後に実行
            //Invoke("BK", 0.1f);
            //renderer.enabled = false;
        }


        if (other.gameObject.tag == "DestroyArea")
        {
            //renderer.enabled = true;  //SetActiveだと当たり判定も消えるからtrueにできない

        }
    }


  
}

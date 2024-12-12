using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MugicManagerScript : MonoBehaviour
{

    //タイトルシーン用BGMのScript
    //自分自身が読み込まれているか判定
    static bool isLoad = false;

    
    void Update()
    {

        //ゲームシーンになったら
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            //BGMを消す
            Destroy(this.gameObject);

        }
        else
        {
            //それ以外(ゲームシーン以外)の時は
            //消さずに流す
            DontDestroyOnLoad(this.gameObject);
        }

    }
}

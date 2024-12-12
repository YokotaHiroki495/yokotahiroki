using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHPBarScript : MonoBehaviour
{
    int maxHP; //ここにプレイヤのplayerHpの値を入れる

    int crrentHp; //ここにプレイヤーの現在のHPを入れる

    public Slider playerHpSlider;

    PlayerScript playerScript;

    int enat;
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        //enemyattackScript = GetComponent<EnemyAttackScript>();
     

        //バーを最大に
        playerHpSlider.value = 1;

        //プレイヤーの初期体力を最大HPにする
        maxHP = playerScript.playeryHp;//crrentHp;

        //最大体力を現在の体力に入れる
        crrentHp = maxHP;
    }

    void Update()
    {
        PlayerHPDamege();
    }



    public  void PlayerHPDamege()
    {

        //Playerが持っているplayerHpを常に参照して、体力バーに反映させる
        crrentHp = playerScript.playeryHp;

        //スライダーにmaxHPから現在の体力に反映させる
        playerHpSlider.value = (float)crrentHp / (float)maxHP;
        


    }
}

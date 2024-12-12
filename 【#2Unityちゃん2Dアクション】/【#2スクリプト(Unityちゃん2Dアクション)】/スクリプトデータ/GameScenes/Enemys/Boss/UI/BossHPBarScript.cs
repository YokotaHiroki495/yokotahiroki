using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBarScript : MonoBehaviour
{
    int bossMaxHP; //ここにボスのplayerHpの値を入れる

    int bossCrrentHp; //ここにボスの現在のHPを入れる

    public Slider bossHpSlider; // ボスの体力バー


    BossController bossScript;

    void Start()
    {
        bossScript = GameObject.Find("BossEnemy").GetComponent<BossController>();

        //バーを最大に
        bossHpSlider.value = 1;

        //プレイヤーの初期体力を最大HPにする
        bossMaxHP = bossScript.enemyHp;
        
        //bossMaxHP = bossHp; //crrentHp;



        //最大体力を現在の体力に入れる
        bossCrrentHp = bossMaxHP;
    }

    public void BossHPDamege()
    {
        //Bossが持っているenemyHpを常に参照して、体力バーに反映させる
        bossCrrentHp = bossScript.enemyHp;

        //スライダーにmaxHPから現在の体力に反映させる
        bossHpSlider.value = (float)bossCrrentHp / (float)bossMaxHP;


    }
}

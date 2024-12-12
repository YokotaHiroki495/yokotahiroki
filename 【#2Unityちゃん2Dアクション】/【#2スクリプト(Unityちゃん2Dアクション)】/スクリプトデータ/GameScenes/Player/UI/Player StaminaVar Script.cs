using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStaminaVarScript : MonoBehaviour
{
    float maxSta; //ここにプレイヤのplayerStaminaの値を入れる

    float crrentSta; //ここにプレイヤーの現在のStaminaを入れる

    public Slider playerHpSlider;

    PlayerScript playerScript;

    //[SerializeField] EnemyAttackScript enemyattackScript;

    int enat;
    void Start()
    {
        playerScript = GameObject.Find("Player").GetComponent<PlayerScript>();
        //enemyattackScript = GetComponent<EnemyAttackScript>();


        

        //バーを最大に
        playerHpSlider.value = 1;
        
        //プレイヤーの初期スタミナを最大量にする
        maxSta = playerScript.nowPlayerAttackStamina;

        //最大スタミナを現在のスタミナに入れる
        crrentSta = maxSta;

    }

    void Update()
    {
        PlayerStaminaDown();
        
    }



    void PlayerStaminaDown()
    {

        //Playerが持っているplayerHpを常に参照して、体力バーに反映させる
        crrentSta = playerScript.nowPlayerAttackStamina;

        //スライダーにmaxHPから現在の体力に反映させる
        playerHpSlider.value = (float)crrentSta / (float)maxSta;



    }
}

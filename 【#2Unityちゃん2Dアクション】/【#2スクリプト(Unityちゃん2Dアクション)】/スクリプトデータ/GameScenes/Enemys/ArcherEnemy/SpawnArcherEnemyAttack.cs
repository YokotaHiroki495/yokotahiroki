using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArcherEnemyAttack : MonoBehaviour
{
    //通常Enemyの攻撃用Script

    [SerializeField] public int enemyAttackDamage;

    PlayerScript playerScrpt;

    PlayerHPBarScript playerHPBarscript;




    void Start()
    {
        Invoke("HitBoxDestroy", 3.0f);


        playerHPBarscript = GameObject.Find("HPBar").GetComponent<PlayerHPBarScript>();
        playerScrpt = GameObject.Find("Player").GetComponent<PlayerScript>();
    }

   
    void HitBoxDestroy()
    {
        Destroy(gameObject);

    }

    //プレイヤーに当たったら
    


    //プレイヤーの攻撃に当たったら
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack") || collision.gameObject.CompareTag("Stage"))
        {
            HitBoxDestroy();
        }

        if (collision.gameObject.CompareTag("Player"))
        {   
            HitBoxDestroy();
            //PlayerがisHit(攻撃があたっているか)状態か調べる記述を書く

            if (playerScrpt.isHit == false )
            {
                playerScrpt.playeryHp -= enemyAttackDamage;
                playerScrpt.DamageEffect();
                playerHPBarscript.PlayerHPDamege();

            }
           


        }
        
    }
}

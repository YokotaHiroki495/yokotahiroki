using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireAttackScript : MonoBehaviour
{

    //Boss通常攻撃用Script
    GameObject target;

    Vector3 targetPos;

    PlayerScript playerScrpt;
    PlayerHPBarScript playerHPBarscript;

    BossFireAttackPool bossFireAttackpool;


    [SerializeField] public int enemyAttackDamage;

    bool getTargetPos;



    void OnEnable()
    {
        getTargetPos = true;
        
    }



    void Start()
    {

        //targetをPlayerに設定
        target = GameObject.Find("Player");


        //Playerに関するScriptを取得
        playerScrpt = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerHPBarscript = GameObject.Find("HPBar").GetComponent<PlayerHPBarScript>();
 
        //Bossの攻撃用のPoolを取得
        bossFireAttackpool = GameObject.Find("BossEnemy").GetComponent<BossFireAttackPool>();

        
    }

   


    void Update()
    {

        if (getTargetPos == true)
        {

            //プレイヤーの位置を取得する
            targetPos = target.transform.position - transform.position;

            getTargetPos = false;
        }


        transform.Translate(targetPos.normalized * 3 * Time.deltaTime);
    }

    void HitBoxDestroy()
    {
        bossFireAttackpool.DelBossFire(gameObject);
    }

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

            if (playerScrpt.isHit == false)
            {
                playerScrpt.playeryHp -= enemyAttackDamage;
                playerScrpt.DamageEffect();
                playerHPBarscript.PlayerHPDamege();

            }



        }

    }
   
}

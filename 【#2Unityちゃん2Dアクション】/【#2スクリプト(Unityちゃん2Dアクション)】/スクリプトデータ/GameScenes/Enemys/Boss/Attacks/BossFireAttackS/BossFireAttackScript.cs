using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFireAttackScript : MonoBehaviour
{

    //Boss�ʏ�U���pScript
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

        //target��Player�ɐݒ�
        target = GameObject.Find("Player");


        //Player�Ɋւ���Script���擾
        playerScrpt = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerHPBarscript = GameObject.Find("HPBar").GetComponent<PlayerHPBarScript>();
 
        //Boss�̍U���p��Pool���擾
        bossFireAttackpool = GameObject.Find("BossEnemy").GetComponent<BossFireAttackPool>();

        
    }

   


    void Update()
    {

        if (getTargetPos == true)
        {

            //�v���C���[�̈ʒu���擾����
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
            //Player��isHit(�U�����������Ă��邩)��Ԃ����ׂ�L�q������

            if (playerScrpt.isHit == false)
            {
                playerScrpt.playeryHp -= enemyAttackDamage;
                playerScrpt.DamageEffect();
                playerHPBarscript.PlayerHPDamege();

            }



        }

    }
   
}

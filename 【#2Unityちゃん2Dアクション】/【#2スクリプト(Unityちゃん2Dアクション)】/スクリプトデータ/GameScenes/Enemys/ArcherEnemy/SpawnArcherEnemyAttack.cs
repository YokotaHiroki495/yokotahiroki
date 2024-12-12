using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArcherEnemyAttack : MonoBehaviour
{
    //�ʏ�Enemy�̍U���pScript

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

    //�v���C���[�ɓ���������
    


    //�v���C���[�̍U���ɓ���������
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

            if (playerScrpt.isHit == false )
            {
                playerScrpt.playeryHp -= enemyAttackDamage;
                playerScrpt.DamageEffect();
                playerHPBarscript.PlayerHPDamege();

            }
           


        }
        
    }
}

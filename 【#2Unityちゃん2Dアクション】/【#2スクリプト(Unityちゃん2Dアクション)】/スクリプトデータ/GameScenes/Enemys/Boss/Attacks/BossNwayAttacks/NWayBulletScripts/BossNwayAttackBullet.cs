using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNwayAttackBullet: MonoBehaviour
{

    //BowwN-Way�U���̒e���̂�Script
    GameObject target;

    Vector3 targetPos;

    PlayerScript playerScrpt;
    PlayerHPBarScript playerHPBarscript;

    BossNwayAttackPool boss3wayattackpool;

    [SerializeField] public int enemyAttackDamage;

    BossNWayBulletPool boss3WayBulletPool;
    bool getTargetPos;
    void OnEnable()
    {
        //transform.position = new Vector2(0, 0);
        getTargetPos = true;

    }
    void Start()
    {
        //�v���C���[���^�[�Q�b�g�Ƃ��Đݒ肵��
        target = GameObject.Find("Player");
        //�e�����̈ʒu�܂Ŕ�΂�
        targetPos = target.transform.position - transform.position;

        playerScrpt = GameObject.Find("Player").GetComponent<PlayerScript>();
        playerHPBarscript = GameObject.Find("HPBar").GetComponent<PlayerHPBarScript>();
        boss3wayattackpool = GameObject.Find("BossEnemy").GetComponent<BossNwayAttackPool>();

        //boss3WayBulletPool = GameObject.Find("N-Way").GetComponent<Boss3WayBulletPool>();
        boss3WayBulletPool = GameObject.Find("BossEnemy").GetComponent<BossNWayBulletPool>();

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

        //boss3WayBulletPool.Del3WayBullet(gameObject);
        boss3wayattackpool.Del3Way(gameObject);
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

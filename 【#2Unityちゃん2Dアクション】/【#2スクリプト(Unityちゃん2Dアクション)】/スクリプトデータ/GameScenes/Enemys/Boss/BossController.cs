using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{
    [Header("�̗͊֘A")]
    [SerializeField] public int enemyHp;

    [Space]
    [Header("�ړ��֘A")]
    [SerializeField] float enemyMoveSpeed; //�ړ��X�s�[�h

    [Space]
    [Header("�U���֘A")]
    [SerializeField] GameObject enemyAttackPrefab;  //�U���v���t�@�u
    [SerializeField] float enemyAttackCoolTime;     //�U���̃N�[���^�C��

    [SerializeField] float bulletspeed;             //�e��
    [SerializeField] SpriteRenderer enemyAttackRebderer;             //�U����SpriteRnderer
    [SerializeField] float asenAttackdis;           //�U���ʒu

    //�U���̃I�u�W�F�N�g�v�[��
    [SerializeField] BossFireAttackPool bossFireAttackpool; //�P���U���̒e�I�u�W�F�N�g
    [SerializeField] BossNwayAttackPool bossNwayattackpool; //3Way�U���̒e�I�u�W�F�N�g
    [SerializeField] int wayNumberlimit;                    //NWay�U���̋����̏��
    GameObject createBoss3NayPos;                           //3Way�U���̐����ʒu��3Way�������ˈʒu

    bool attackChance;

    int wayNumber;
 

    float nowAttackCoolTime;                        //���݂̃N�[���^�C���̌o�ߎ���

    [Space]
    [Header("Boss�T�C�Y")]
    [SerializeField] int enemyXsize;
    [SerializeField] int enemyYsize;

    [Space]
    [Header("��e����")]
    [SerializeField] float enemyknockback;  //�m�b�N�o�b�N�̐���
    Rigidbody2D ribd2d = null;
    MeshRenderer meshRend = null;
    SpriteRenderer spritRend = null;
    bool MovedireRight = false;  //�E�ɓ����Ă��邩�ǂ���
    BossHPBarScript bossHpBarScript;

    //�����蔻��
    BoxCollider2D boxcol2d;     //�����𔻒肷�铖���蔻��
    CapsuleCollider2D capcol2d; //�G�{�̂̓����蔻��
    CircleCollider2D cico2d;    //�U���𔻒肷��(�_���[�W���󂯂�)�����蔻��


    //[SerializeField] 
    GameObject target; //�v���C���[��F��


    //�G�t�F�N�g�֘A
    [Space]
    [Header("�G�t�F�N�g�֌W")]
    GameObject effect;
    [SerializeField] GameObject effectPrefab;


    Collider2D enemyCollier2D;          //���g(Boss)�̓����蔻��

    //�v���C���[�ƈʒu�̍�
    public Vector2 playerPos;

    //Sound�֌W

    [Space]
    [Header("Sound�֌W")]
    AudioSource audioSource;
    [SerializeField] AudioClip destroySE;
    [SerializeField] AudioClip damageSE;

    //�X�R�A����x�������Z����p��bool�^
    bool scoreTrue = false;


    GameManager gameManager;


    //�{�X�̍��W������
    Vector3 myBossPos;


    void Awake()
    {

        //���O�Ɏ擾���Ă���
        ribd2d = GetComponent<Rigidbody2D>();
        meshRend = GetComponent<MeshRenderer>();
        spritRend = GetComponent<SpriteRenderer>();
        enemyCollier2D = gameObject.GetComponent<Collider2D>();   
    }
    void Start()
    {
        //�X�N���v�g���擾
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossHpBarScript = GameObject.Find("BOSSHPBar").GetComponent<BossHPBarScript>();
        bossNwayattackpool = GameObject.Find("BossEnemy").GetComponent<BossNwayAttackPool>();
        bossFireAttackpool = GameObject.Find("BossEnemy").GetComponent<BossFireAttackPool>();

        //�R���|�[�l���g(Collider��)���擾
        audioSource = GetComponent<AudioSource>();
        boxcol2d = gameObject.GetComponent<BoxCollider2D>();
        capcol2d = gameObject.GetComponent<CapsuleCollider2D>();
        cico2d = gameObject.GetComponent<CircleCollider2D>();

        target = GameObject.Find("Player");
    }


    void Update()
    {
        Vector3 scale = transform.localScale;
        transform.localScale = scale;

        //���g�̈ʒu���擾
        Transform myTransform = this.transform;
        myBossPos = myTransform.position;

        if (spritRend.isVisible == true)
        {
            EnemyHp();
            EnemyMove();
            EnemyAttack();

            //�ړ������ɍ��킹�ăA�j���[�V�����𔽓]������
            if (playerPos.x >= 0.1)
            {
                //�E����
                //Object���܂Ƃ߂Ĕ��]������
                transform.localScale = new Vector3(-enemyXsize, enemyYsize, 1);
            }
            else if (playerPos.x <= -0.1)
            {
                //������
                //Object���܂Ƃ߂Ĕ��]������
                transform.localScale = new Vector3(enemyXsize, enemyYsize, 1);
            }
        }
        else if (spritRend.isVisible == false)
        {
            ribd2d.Sleep();
        }
    }

    void EnemyHp()
    {
        //HP��0�ɂȂ�����
        if (enemyHp == 0)
        {
            //isTrigger��false(�����蔻������蔲����)�ɂ���
            enemyCollier2D.isTrigger = true;

            //Collider�̓����蔻�������
            boxcol2d.enabled = false;
            capcol2d.enabled = false;

            if (!scoreTrue)
            {
                scoreTrue = true;
                gameManager.PlusScore();
            }

            if (playerPos.x < 0)
            {
                LeftFlomAttack();
            }
            // �E�����ɂ���
            if (playerPos.x > 0)
            {
                RightFlomAttack();
            }
            Destroy(gameObject, 0.5f);

        }
    }



    void EnemyMove()
    {
        //�v���C���[�̈ʒu�Ǝ����̈ʒu���r���č����m�F����
        playerPos = target.transform.position - transform.position;

        //target(�v���C���[)��ǂ������Ă��鏈��
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemyMoveSpeed * Time.deltaTime);
    }


    void EnemyAttack()
    {
        nowAttackCoolTime += Time.deltaTime;
        if (enemyHp >= 1)
        {
            if (nowAttackCoolTime >= enemyAttackCoolTime)
            {
                // ������
                if (playerPos.x < 0)
                {
                    attackChance = true;
                    CreateFireAttack();
                    Create3WayAttack();   

                }
                nowAttackCoolTime = 0.0f;
            }
        }
    }

    void CreateFireAttack()
    {
        if (attackChance == true)
        { 
            
            // �U���I�u�W�F�N�g���擾
            GameObject createBossFirePos = bossFireAttackpool.GetBossFire();

            // �U�������ʒu�̍��W�A�e(�{�X)�̈ʒu���擾���Ĕ��f������
            createBossFirePos.transform.position = new Vector2(myBossPos.x + 1, myBossPos.y + 1);

            attackChance = false;
        } 
    }


    void Create3WayAttack()
    {
        // �e���w��̐���������
        for (wayNumber = 0; wayNumber < wayNumberlimit; wayNumber++)
        {
            createBoss3NayPos = bossNwayattackpool.Get3Way();

            //�����̍��W�A�e(�{�X)�̈ʒu���擾���Ĕ��f������
            createBoss3NayPos.transform.position = new Vector2(myBossPos.x -1, myBossPos.y - 1);

            // �e�̊p�x��15�x�ÂŔ��ˁ@�@�e���������Ă��Ή��\��
            createBoss3NayPos.transform.rotation = Quaternion.Euler(0, 0, 7.5f - (7.5f * wayNumberlimit) + (15 * wayNumber));
        }
    }

   

    public void LeftFlomAttack()
    {
        // �U�����󂯂���U�����ꂽ�����Ƃ͔��΂̕����ɔ��
        ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.right * enemyknockback, (ForceMode2D)ForceMode.Acceleration);
    }

    public void RightFlomAttack()
    {
        // �U�����󂯂���U�����ꂽ�����Ƃ͔��΂̕����ɔ��
        ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.left * enemyknockback, (ForceMode2D)ForceMode.Acceleration);
    }



}

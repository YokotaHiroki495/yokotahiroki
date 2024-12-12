using UnityEngine;

public class SwordEnemy : MonoBehaviour
{
    //SwordEnemy�pScript
    [Header("�̗͊֘A")]
    [SerializeField, Min(1)] public int enemyHp;
    int nowSwordEnemyHp;

    [Header("�ړ��֘A")]
    [SerializeField] float enemyMoveSpeed; // �ړ��X�s�[�h

    [Header("�U���֘A")]
    [SerializeField] GameObject enemyAttackPrefab;  // �U���v���t�@�u
    [SerializeField] float enemyAttackCoolTime;     // �U���̃N�[���^�C��
    [SerializeField] float spawnAttackPosition;      // �U���ʒu
    [SerializeField] float enemyknockback;          // �m�b�N�o�b�N�̐���


    float nowAttackCoolTime;                        // �U���̃N�[���^�C���̌o�ߎ���

    //���g(SwordEnemy)�̈ʒu
    Vector2 swordEnemyPos;

    // �v���C���[�ƈʒu�̍�
    Vector2 playerPos;

    //�����蔻��̐ݒ�
    Rigidbody2D ribd2d = null;
    SpriteRenderer spritRend = null;
    Collider2D enemyCollier2D;
    CapsuleCollider2D capcol2d;
    GameObject player; // �v���C���[��F��

    // �G�t�F�N�g�֘A
    GameObject effect;
    [SerializeField]GameObject effectPrefab;    

    //Sound�֌W
    AudioSource audioSource;
    [SerializeField] AudioClip destroySE;
    [SerializeField] AudioClip damageSE;

    // �X�R�A����x�������Z����p��bool�^
    bool scoreTrue = false;

    //�h���b�v�A�C�e���֘A
    [Header("�h���b�v�A�C�e��")]
    [SerializeField] GameObject dropHPItemPrefab;       //HP�񕜃A�C�e��
    [SerializeField] GameObject dropStaminaItemPrefab;  //�X�^�~�i�񕜃A�C�e��
    int itemDropProbability;                            //�A�C�e���h���b�v�m��

    //�X�R�A�����p�ɃA�N�Z�X���邽��
    GameManager gameManager;

    // ObjectPool�ŃG�l�~�[���Ăт��������������
    EnemyObjectPool enemyManager;

    void Awake()
    {
        // �����蔻�蓙���擾
        ribd2d = GetComponent<Rigidbody2D>();
        spritRend = GetComponent<SpriteRenderer>();
        enemyCollier2D = gameObject.GetComponent<Collider2D>();
        capcol2d = gameObject.GetComponent<CapsuleCollider2D>();
    }

    void OnEnable()
    {
        //���g�̗̑͂�ݒ�
        nowSwordEnemyHp = enemyHp;
        
        //�g���K�[�����������
        enemyCollier2D.isTrigger = false;

        //�R���|�[�l���g��L����
        capcol2d.enabled = true;
    }
    void Start()
    {
        //�e��X�N���v�g���擾
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyObjectPool>();
        player = GameObject.Find("Player");    
    }

    void Update()
    {
        //�G�̑傫�����w��
        Vector3 scale = transform.localScale;
        transform.localScale = scale;

      
            EnemyHp();
            EnemyMove();
            EnemyAttack();

            //�ړ������ɍ��킹�ăA�j���[�V�����𔽓]������
            if (playerPos.x >= swordEnemyPos.x)
            {
                //�E����
                //���������f�t�H���g�Ȃ̂ł��܂Ƃ߂Ĕ��]������
                transform.localScale = new Vector3( -1, 1, 1);
            }
            else if (playerPos.x <= swordEnemyPos.x)
            {
                //������
                transform.localScale = new Vector3( 1, 1, 1);
            }

       
    }

    void EnemyHp()
    {
        // HP��0�ɂȂ�����
        if (nowSwordEnemyHp == 0)
        {
            audioSource.PlayOneShot(destroySE);

            //isTrigger��false(�����蔻������蔲����)�ɂ���
            enemyCollier2D.isTrigger = true;
            capcol2d.enabled = false;

            if (scoreTrue == false)
            {
                scoreTrue = true;
                gameManager.PlusScore();
                itemDropProbability = Random.Range(1, 10);

                //�A�C�e���𐶐�����
                Instantiate(dropHPItemPrefab, transform.position, Quaternion.identity);

                scoreTrue = false;
            }









            if (playerPos.x < 0)
            {
                LeftFlomAttack();
            }
              
            //�E�����ɂ���
            if (playerPos.x > 0)
            {
                RightFlomAttack();
            }
            
            enemyManager.DelEnemy(gameObject);
        }
    }

    void EnemyMove()
    {
        //�v���C���[�̈ʒu�Ǝ����̈ʒu���r���č����m�F����
        playerPos = player.transform.position - transform.position;

        //target(�v���C���[)��ǂ������Ă��鏈��
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
    }


    void EnemyAttack()
    {
        if (playerPos.x < 3)
        {
            nowAttackCoolTime += Time.deltaTime;
            if (enemyHp >= 1)
            {
                if (nowAttackCoolTime >= enemyAttackCoolTime)
                {
                    //�������ɂ���
                    if (playerPos.x < 0)
                    {
                        GameObject Bullet = Instantiate(enemyAttackPrefab, transform.position + Vector3.left * spawnAttackPosition, Quaternion.identity);
                        Bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        Bullet.GetComponent<SpriteRenderer>().flipX = false;
                    }

                    //�E�����ɂ���
                    if (playerPos.x > 0)
                    {
                        GameObject Bullet = Instantiate(enemyAttackPrefab, transform.position + Vector3.right * spawnAttackPosition, Quaternion.identity);
                        Bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        Bullet.GetComponent<SpriteRenderer>().flipX = true;
                    }

                    nowAttackCoolTime = 0.0f;
                }
            }
        }
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack")) //�������̂ق�������������
        {
            nowSwordEnemyHp -= 1;

            effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            //playerpos���Q�Ƃ��āAplayer�̕����𔻒肷��B
            //�������ɂ���
            if (playerPos.x < 0)
            {
                LeftFlomAttack();
            }

            //�E�����ɂ���
            if (playerPos.x > 0)
            {
                RightFlomAttack();
            }

            Destroy(effect, 0.5f);
        }
    }

    //��������U�����ꂽ��E���ɐ�����΂�
    void LeftFlomAttack()
    {    
        ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.right * enemyknockback, (ForceMode2D)ForceMode.Acceleration);
    }

    //�E������U�����ꂽ�獶���ɐ�����΂�
    void RightFlomAttack()
    {
        ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.left * enemyknockback, (ForceMode2D)ForceMode.Acceleration);
    }
}

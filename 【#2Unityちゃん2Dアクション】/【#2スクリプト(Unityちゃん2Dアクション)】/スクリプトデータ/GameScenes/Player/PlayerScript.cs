using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("�̗͊֘A")]
    [SerializeField] public int playeryHp;
    int MaxplayerHp;

    [Header("�ړ��֘A")]
    [SerializeField] float playerspeed;
    [SerializeField] float jumpForce;

    float axisPlusLimit =0.2f;
    float axisMinusLimit = -0.2f;


    [Header("�U��")]
    [SerializeField] GameObject bulletPrefab;   //�U���v���t�@�u
    [SerializeField] float attackdis;           //�U���ʒu
    [SerializeField] public  float nowPlayerAttackStamina;
    float maxPlayerAttackStamina;
    [SerializeField] float recvoryStumina; //�񕜗�

    //�����Ă������
    public Vector3 dir;


    // �_���[�W���󂯂����̓_�ŏ���
    [SerializeField] float flashInterval;    //�_�ł̊Ԋu
    [SerializeField] int loopCount; //�_�ł̃��[�v�J�E���g

    public bool isHit; //�����������ǂ����̃t���O


    //�ڒn����
    [Header("�ݒu����")]
    [SerializeField] bool isGrounded;

    [Header("�G�t�F�N�g�֘A")]
    GameObject effect;
    [SerializeField] GameObject effectPrefab;

    [Header("�T�E���h�֘A")]
    [SerializeField] AudioClip attackSE;

    AudioSource audioSource;


    //--------Animator�֘A-----------------
    static int hashSpeed = Animator.StringToHash("Speed");
    static int hashFallSpeed = Animator.StringToHash("FallSpeed");
    static int hashGroundDistance = Animator.StringToHash("GroundDistance");
    static int hashIsCrouch = Animator.StringToHash("IsCrouch");


    // �U�����[�V����
    private int hashAttack1 = Animator.StringToHash("Attack1");


    [Header("�A�j���[�V�����֘A")]
    [SerializeField] private float characterHeightOffset = 0.2f;
    [SerializeField] LayerMask groundMask;

    [SerializeField, HideInInspector] Animator animator;
    [SerializeField, HideInInspector] SpriteRenderer spriteRenderer;
    [SerializeField, HideInInspector] Rigidbody2D ribd2d;


    //�v���C���[�������Ă邩�̔���
    bool playerIsLive;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ribd2d = GetComponent<Rigidbody2D>();
       
    }
    void Start()
    {
        //�ő�̗͂ɐݒ肵���̗͂�����
        MaxplayerHp = playeryHp; 

        playerIsLive = true;

        dir = Vector3.right;

        maxPlayerAttackStamina = nowPlayerAttackStamina;

        audioSource = GetComponent<AudioSource>();


    }


    void Update()
    {
        PlayeryHp();
        PlayerStumina();
        Move();
        Attack();

    }


    void PlayeryHp()
    {
        
        if (playeryHp <= 0)
        {
            playerIsLive = false;
            effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

        }

        if (playeryHp > MaxplayerHp)
        {
            playeryHp = MaxplayerHp;

        }

    }


    void PlayerStumina()
    {
        if (nowPlayerAttackStamina < maxPlayerAttackStamina)
        {
            nowPlayerAttackStamina += recvoryStumina * Time.deltaTime;
        }

    }
    void Move()
    {


        //-------------------------------------
        //Unity���������A�Z�b�g���Q�l
        if (playerIsLive)
        {

            float axis = Input.GetAxis("Horizontal");       //�����ړ�
            bool isDown = Input.GetAxisRaw("Vertical") < 0; //�����ړ�

            Vector2 velocity = ribd2d.velocity;
            if (Input.GetButtonDown("Jump") && isGrounded || Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                velocity.y = jumpForce;
            }

            if (axis != 0)
            {
                spriteRenderer.flipX = axis < 0;
                velocity.x = axis * playerspeed;    //����X�s�[�h��������������킹��

            }

            if (axis < axisPlusLimit && axis > axisMinusLimit)      //�ړ��̓��͂�+-0.2�ȓ���������L�����̉��ړ���0�ɂ���
            {
                velocity.x = 0;
            }

            //�L�����̍��E�ړ�
            if (axis > axisPlusLimit)
            {
                dir = Vector3.right;
            }
            if (axis < axisMinusLimit)
            {
                dir = Vector3.left;
            }


            ribd2d.velocity = velocity;

            animator.SetFloat(hashFallSpeed, ribd2d.velocity.y);
            animator.SetFloat(hashSpeed, Mathf.Abs(axis));
            animator.SetBool(hashIsCrouch, isDown);


            //Player���牺�����Ƀ��C���΂��āA
            var distanceFromGround = Physics2D.Raycast(transform.position, Vector3.down, 1, groundMask);
            //�Ⴊ�w�肵�����C���[�ɓ���������v�Z���āA���[�V����������
            animator.SetFloat(hashGroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - characterHeightOffset);


        }


        //-------------------------------------------
    }
    void Attack()
    {
        //�v���C���[�������Ă���
        if (playerIsLive)
        {
            //�X�^�~�i���c���Ă�����
            if (nowPlayerAttackStamina > 0) 
            {

                //�R���g���[���A�L�[�{�[�h���ɔF��
                if (Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Space))
                {
                    //SE���Đ�
                    audioSource.PlayOneShot(attackSE);

                    //�X�^�~�i������
                    nowPlayerAttackStamina -= 10;

                    //�U���A�j���[�V�������Đ�
                    animator.SetTrigger(hashAttack1);

                    //�U���I�u�W�F�N�g�𐶐�
                    GameObject Bullet = Instantiate(bulletPrefab, transform.position + dir * attackdis, Quaternion.identity);
                    Bullet.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);    //�U���̑傫�����w��


                }
            }



        }
    }

    public void DamageEffect()
    {

        effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
    }



    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stage") || collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Stage") || collision.gameObject.CompareTag("Floor"))
        {
            isGrounded = false;
        }

    }


   
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyAttack"))
        {
            StartCoroutine(Hit());
            return;

        }

        if (collision.gameObject.CompareTag("StageObject"))
        {


            return;

        }



    }


    IEnumerator Hit()
    {
        //�����蔻���ύX
        isHit = true;

        //�_�Ń��[�v
        for (int i = 0; i < loopCount; i ++)
        {
           
            
            yield return new WaitForSeconds(flashInterval);

            //�����_���[(�����蔻���)���I�t
            spriteRenderer.enabled = false;
           
            yield return new WaitForSeconds(flashInterval);

            //�����_���[(�����蔻���)�I��
            spriteRenderer.enabled = true;

        }

        isHit = false;
    }





}

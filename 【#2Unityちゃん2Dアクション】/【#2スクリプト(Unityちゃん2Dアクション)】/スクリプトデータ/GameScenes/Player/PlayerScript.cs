using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("体力関連")]
    [SerializeField] public int playeryHp;
    int MaxplayerHp;

    [Header("移動関連")]
    [SerializeField] float playerspeed;
    [SerializeField] float jumpForce;

    float axisPlusLimit =0.2f;
    float axisMinusLimit = -0.2f;


    [Header("攻撃")]
    [SerializeField] GameObject bulletPrefab;   //攻撃プレファブ
    [SerializeField] float attackdis;           //攻撃位置
    [SerializeField] public  float nowPlayerAttackStamina;
    float maxPlayerAttackStamina;
    [SerializeField] float recvoryStumina; //回復量

    //向いている方向
    public Vector3 dir;


    // ダメージを受けた時の点滅処理
    [SerializeField] float flashInterval;    //点滅の間隔
    [SerializeField] int loopCount; //点滅のループカウント

    public bool isHit; //当たったかどうかのフラグ


    //接地判定
    [Header("設置判定")]
    [SerializeField] bool isGrounded;

    [Header("エフェクト関連")]
    GameObject effect;
    [SerializeField] GameObject effectPrefab;

    [Header("サウンド関連")]
    [SerializeField] AudioClip attackSE;

    AudioSource audioSource;


    //--------Animator関連-----------------
    static int hashSpeed = Animator.StringToHash("Speed");
    static int hashFallSpeed = Animator.StringToHash("FallSpeed");
    static int hashGroundDistance = Animator.StringToHash("GroundDistance");
    static int hashIsCrouch = Animator.StringToHash("IsCrouch");


    // 攻撃モーション
    private int hashAttack1 = Animator.StringToHash("Attack1");


    [Header("アニメーション関連")]
    [SerializeField] private float characterHeightOffset = 0.2f;
    [SerializeField] LayerMask groundMask;

    [SerializeField, HideInInspector] Animator animator;
    [SerializeField, HideInInspector] SpriteRenderer spriteRenderer;
    [SerializeField, HideInInspector] Rigidbody2D ribd2d;


    //プレイヤーが生きてるかの判定
    bool playerIsLive;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ribd2d = GetComponent<Rigidbody2D>();
       
    }
    void Start()
    {
        //最大体力に設定した体力を入れる
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
        //Unityちゃん公式アセットを参考
        if (playerIsLive)
        {

            float axis = Input.GetAxis("Horizontal");       //水平移動
            bool isDown = Input.GetAxisRaw("Vertical") < 0; //垂直移動

            Vector2 velocity = ribd2d.velocity;
            if (Input.GetButtonDown("Jump") && isGrounded || Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
            {
                velocity.y = jumpForce;
            }

            if (axis != 0)
            {
                spriteRenderer.flipX = axis < 0;
                velocity.x = axis * playerspeed;    //走るスピードを方向をかけ合わせる

            }

            if (axis < axisPlusLimit && axis > axisMinusLimit)      //移動の入力が+-0.2以内だったらキャラの横移動を0にする
            {
                velocity.x = 0;
            }

            //キャラの左右移動
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


            //Playerから下方向にレイを飛ばして、
            var distanceFromGround = Physics2D.Raycast(transform.position, Vector3.down, 1, groundMask);
            //例が指定したレイヤーに当たったら計算して、モーションをする
            animator.SetFloat(hashGroundDistance, distanceFromGround.distance == 0 ? 99 : distanceFromGround.distance - characterHeightOffset);


        }


        //-------------------------------------------
    }
    void Attack()
    {
        //プレイヤーが生きていて
        if (playerIsLive)
        {
            //スタミナが残っていたら
            if (nowPlayerAttackStamina > 0) 
            {

                //コントローラ、キーボード共に認識
                if (Input.GetButtonDown("Attack") || Input.GetKeyDown(KeyCode.Space))
                {
                    //SEを再生
                    audioSource.PlayOneShot(attackSE);

                    //スタミナを消費
                    nowPlayerAttackStamina -= 10;

                    //攻撃アニメーションを再生
                    animator.SetTrigger(hashAttack1);

                    //攻撃オブジェクトを生成
                    GameObject Bullet = Instantiate(bulletPrefab, transform.position + dir * attackdis, Quaternion.identity);
                    Bullet.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);    //攻撃の大きさを指定


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
        //当たり判定を変更
        isHit = true;

        //点滅ループ
        for (int i = 0; i < loopCount; i ++)
        {
           
            
            yield return new WaitForSeconds(flashInterval);

            //レンダラー(当たり判定も)をオフ
            spriteRenderer.enabled = false;
           
            yield return new WaitForSeconds(flashInterval);

            //レンダラー(当たり判定も)オン
            spriteRenderer.enabled = true;

        }

        isHit = false;
    }





}

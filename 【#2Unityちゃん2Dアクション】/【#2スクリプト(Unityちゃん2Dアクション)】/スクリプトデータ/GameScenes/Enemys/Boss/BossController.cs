using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BossController : MonoBehaviour
{
    [Header("体力関連")]
    [SerializeField] public int enemyHp;

    [Space]
    [Header("移動関連")]
    [SerializeField] float enemyMoveSpeed; //移動スピード

    [Space]
    [Header("攻撃関連")]
    [SerializeField] GameObject enemyAttackPrefab;  //攻撃プレファブ
    [SerializeField] float enemyAttackCoolTime;     //攻撃のクールタイム

    [SerializeField] float bulletspeed;             //弾速
    [SerializeField] SpriteRenderer enemyAttackRebderer;             //攻撃のSpriteRnderer
    [SerializeField] float asenAttackdis;           //攻撃位置

    //攻撃のオブジェクトプール
    [SerializeField] BossFireAttackPool bossFireAttackpool; //単発攻撃の弾オブジェクト
    [SerializeField] BossNwayAttackPool bossNwayattackpool; //3Way攻撃の弾オブジェクト
    [SerializeField] int wayNumberlimit;                    //NWay攻撃の球数の上限
    GameObject createBoss3NayPos;                           //3Way攻撃の生成位置＆3Way同時発射位置

    bool attackChance;

    int wayNumber;
 

    float nowAttackCoolTime;                        //現在のクールタイムの経過時間

    [Space]
    [Header("Bossサイズ")]
    [SerializeField] int enemyXsize;
    [SerializeField] int enemyYsize;

    [Space]
    [Header("被弾処理")]
    [SerializeField] float enemyknockback;  //ノックバックの勢い
    Rigidbody2D ribd2d = null;
    MeshRenderer meshRend = null;
    SpriteRenderer spritRend = null;
    bool MovedireRight = false;  //右に動いているかどうか
    BossHPBarScript bossHpBarScript;

    //当たり判定
    BoxCollider2D boxcol2d;     //足元を判定する当たり判定
    CapsuleCollider2D capcol2d; //敵本体の当たり判定
    CircleCollider2D cico2d;    //攻撃を判定する(ダメージを受ける)当たり判定


    //[SerializeField] 
    GameObject target; //プレイヤーを認識


    //エフェクト関連
    [Space]
    [Header("エフェクト関係")]
    GameObject effect;
    [SerializeField] GameObject effectPrefab;


    Collider2D enemyCollier2D;          //自身(Boss)の当たり判定

    //プレイヤーと位置の差
    public Vector2 playerPos;

    //Sound関係

    [Space]
    [Header("Sound関係")]
    AudioSource audioSource;
    [SerializeField] AudioClip destroySE;
    [SerializeField] AudioClip damageSE;

    //スコアを一度だけ加算する用のbool型
    bool scoreTrue = false;


    GameManager gameManager;


    //ボスの座標を入れる
    Vector3 myBossPos;


    void Awake()
    {

        //事前に取得しておく
        ribd2d = GetComponent<Rigidbody2D>();
        meshRend = GetComponent<MeshRenderer>();
        spritRend = GetComponent<SpriteRenderer>();
        enemyCollier2D = gameObject.GetComponent<Collider2D>();   
    }
    void Start()
    {
        //スクリプトを取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        bossHpBarScript = GameObject.Find("BOSSHPBar").GetComponent<BossHPBarScript>();
        bossNwayattackpool = GameObject.Find("BossEnemy").GetComponent<BossNwayAttackPool>();
        bossFireAttackpool = GameObject.Find("BossEnemy").GetComponent<BossFireAttackPool>();

        //コンポーネント(Collider等)を取得
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

        //自身の位置を取得
        Transform myTransform = this.transform;
        myBossPos = myTransform.position;

        if (spritRend.isVisible == true)
        {
            EnemyHp();
            EnemyMove();
            EnemyAttack();

            //移動方向に合わせてアニメーションを反転させる
            if (playerPos.x >= 0.1)
            {
                //右向き
                //Objectをまとめて反転させる
                transform.localScale = new Vector3(-enemyXsize, enemyYsize, 1);
            }
            else if (playerPos.x <= -0.1)
            {
                //左向き
                //Objectをまとめて反転させる
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
        //HPが0になったら
        if (enemyHp == 0)
        {
            //isTriggerをfalse(当たり判定をすり抜ける)にする
            enemyCollier2D.isTrigger = true;

            //Colliderの当たり判定を消す
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
            // 右方向にいる
            if (playerPos.x > 0)
            {
                RightFlomAttack();
            }
            Destroy(gameObject, 0.5f);

        }
    }



    void EnemyMove()
    {
        //プレイヤーの位置と自分の位置を比較して差を確認する
        playerPos = target.transform.position - transform.position;

        //target(プレイヤー)を追いかけてくる処理
        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, enemyMoveSpeed * Time.deltaTime);
    }


    void EnemyAttack()
    {
        nowAttackCoolTime += Time.deltaTime;
        if (enemyHp >= 1)
        {
            if (nowAttackCoolTime >= enemyAttackCoolTime)
            {
                // 左方向
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
            
            // 攻撃オブジェクトを取得
            GameObject createBossFirePos = bossFireAttackpool.GetBossFire();

            // 攻撃生成位置の座標、親(ボス)の位置を取得して反映させる
            createBossFirePos.transform.position = new Vector2(myBossPos.x + 1, myBossPos.y + 1);

            attackChance = false;
        } 
    }


    void Create3WayAttack()
    {
        // 弾を指定の数だけ生成
        for (wayNumber = 0; wayNumber < wayNumberlimit; wayNumber++)
        {
            createBoss3NayPos = bossNwayattackpool.Get3Way();

            //ここの座標、親(ボス)の位置を取得して反映させる
            createBoss3NayPos.transform.position = new Vector2(myBossPos.x -1, myBossPos.y - 1);

            // 弾の角度を15度づつで発射　　弾数が増えても対応可能に
            createBoss3NayPos.transform.rotation = Quaternion.Euler(0, 0, 7.5f - (7.5f * wayNumberlimit) + (15 * wayNumber));
        }
    }

   

    public void LeftFlomAttack()
    {
        // 攻撃を受けたら攻撃された方向とは反対の方向に飛ぶ
        ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.right * enemyknockback, (ForceMode2D)ForceMode.Acceleration);
    }

    public void RightFlomAttack()
    {
        // 攻撃を受けたら攻撃された方向とは反対の方向に飛ぶ
        ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.left * enemyknockback, (ForceMode2D)ForceMode.Acceleration);
    }



}

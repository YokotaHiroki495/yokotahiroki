using UnityEngine;

public class ArcherEnemy : MonoBehaviour
{
    //ArcherEnemy用Script
    [Header("体力関連")]
    [SerializeField, Min(1)] public int enemyHp;
    int nowArcherEnemyHp;

    [Header("移動関連")]
    [SerializeField] float enemyMoveSpeed; //移動スピード

    [Header("攻撃関連")]
    [SerializeField] GameObject enemyAttackPrefab;  // 攻撃プレファブ
    [SerializeField] float enemyAttackCoolTime;     // 攻撃のクールタイム
    [SerializeField] float spawnAttackPosition;     // 攻撃位置
    [SerializeField] float enemyknockback;          //ノックバックの勢い
    [SerializeField] float bulletspeed;             // 弾速
    
    private float nowAttackCoolTime;                //攻撃のクールタイムの経過時間

    //自身(ArcherEnemy)の位置
    Vector2 archerEnemyPos;

    //プレイヤーと位置の差
    Vector2 playerPos;

    //当たり判定の設定
    Rigidbody2D ribd2d = null;
    SpriteRenderer spritRend = null;
    Collider2D enemyCollier2D;
    CapsuleCollider2D capcol2d;
    GameObject player; //プレイヤーを認識

    //エフェクト関連
    GameObject effect;
    [SerializeField]GameObject effectPrefab;    //エフェクトプレファブ設定

    //Sound関連
    AudioSource audioSource;
    [SerializeField] AudioClip destroySE;
    [SerializeField] AudioClip damageSE;

    //スコアを一度だけ加算する用のbool型
    bool scoreTrue = false;

    //ドロップアイテム関連
    [Header("ドロップアイテム")]
    [SerializeField] GameObject dropHPItemPrefab;       //HP回復アイテム
    [SerializeField] GameObject dropStaminaItemPrefab;  //スタミナ回復アイテム
    int itemDropProbability;                            //アイテムドロップ確率
    
    //スコア増減用にアクセスするため
    GameManager gameManager;

    //ObjectPoolでエネミーを呼びだしたり消すため
    EnemyObjectPool enemyManager;

    void Awake()
    {
        // 当たり判定等を取得
        ribd2d = GetComponent<Rigidbody2D>();
        spritRend = GetComponent<SpriteRenderer>();
        enemyCollier2D = gameObject.GetComponent<Collider2D>();
        capcol2d = gameObject.GetComponent<CapsuleCollider2D>(); 
    }

    void OnEnable()
    {
        //敵の体力を設定
        nowArcherEnemyHp = enemyHp;

        //トリガー判定を消して
        enemyCollier2D.isTrigger = false;

        //コンポーネントを有効化
        capcol2d.enabled = true;
    }
    void Start()
    {
        //各種スクリプトを取得
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyObjectPool>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        //敵の大きさを指定
        Vector3 scale = transform.localScale;
        transform.localScale = scale;

       
            EnemyHp();
            EnemyMove();
            EnemyAttack();

            //移動方向に合わせてアニメーションを反転させる
            if (playerPos.x >= archerEnemyPos.x)
            {
                //右向き
                //左向きがデフォルトなのでをまとめて反転させる
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (playerPos.x <= archerEnemyPos.x)
            {
                //左向き
                transform.localScale = new Vector3(1, 1, 1);
            }

        

       

    }

    void EnemyHp()
    {
   
        //HPが0になったら
        if (nowArcherEnemyHp == 0)
        {
            audioSource.PlayOneShot(destroySE);

            //isTriggerをtrue(当たり判定をすり抜ける)にする
            enemyCollier2D.isTrigger = true;
            capcol2d.enabled = false;

            if (scoreTrue　==false)
            {
                scoreTrue = true;
                gameManager.PlusScore();
                itemDropProbability = Random.Range(1, 10);


                //アイテムを生成する
                if (itemDropProbability == 1)
                {
                    Instantiate(dropHPItemPrefab, transform.position, Quaternion.identity);

                }

                if (itemDropProbability == 2)
                {
                     Instantiate(dropStaminaItemPrefab, transform.position, Quaternion.identity);

                }
                

                scoreTrue = false;

            }

            

            if (playerPos.x < 0) 
            { 
                LeftFlomAttack();
    
            }

            //右方向にいる
            if (playerPos.x > 0)    
            {
                RightFlomAttack();
            }

            enemyManager.DelEnemy(gameObject);

        }

    }



    void EnemyMove()
    {

        //プレイヤーの位置と自分の位置を比較して差を確認する
        playerPos = player.transform.position - transform.position;

        //target(プレイヤー)を追いかけてくる処理
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemyMoveSpeed * Time.deltaTime);
    }


    void EnemyAttack()
    {
        nowAttackCoolTime += Time.deltaTime;
        if (enemyHp >= 1)
        {
            if (nowAttackCoolTime >= enemyAttackCoolTime)
            {
                if (playerPos.x < archerEnemyPos.x)
                {
                    GameObject Bullet = Instantiate(enemyAttackPrefab, transform.position + Vector3.left * spawnAttackPosition, Quaternion.identity);
                    Bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * bulletspeed;
                    Bullet.GetComponent<SpriteRenderer>().flipX = false;
                }
                //右方向にいる
                if (playerPos.x > archerEnemyPos.x)
                {
                    GameObject Bullet = Instantiate(enemyAttackPrefab, transform.position + Vector3.right * spawnAttackPosition, Quaternion.identity);
                    Bullet.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletspeed;
                    Bullet.GetComponent<SpriteRenderer>().flipX = true;
                }

                nowAttackCoolTime = 0.0f;
            }
        }
    }




        void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.CompareTag("Attack")) //こっちのほうが処理が速い
            {


                nowArcherEnemyHp -= 1;

                effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                audioSource.PlayOneShot(damageSE);

                //playerposを参照して、playerの方向を判定する。
                //左方向にいる
                if (playerPos.x < 0)
                {
                //左から攻撃された場合
                    LeftFlomAttack();

                }
                //右方向にいる
                if (playerPos.x > 0)
                {
                //右から攻撃された場合
                RightFlomAttack();
                }

                Destroy(effect, 0.5f);

            }

        }


        void LeftFlomAttack()
        {

            ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.right * enemyknockback, (ForceMode2D)ForceMode.Acceleration);

        }

        void RightFlomAttack()
        {
            ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.left * enemyknockback, (ForceMode2D)ForceMode.Acceleration);

        }

    

}

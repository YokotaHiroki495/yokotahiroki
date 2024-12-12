using UnityEngine;

public class SwordEnemy : MonoBehaviour
{
    //SwordEnemy用Script
    [Header("体力関連")]
    [SerializeField, Min(1)] public int enemyHp;
    int nowSwordEnemyHp;

    [Header("移動関連")]
    [SerializeField] float enemyMoveSpeed; // 移動スピード

    [Header("攻撃関連")]
    [SerializeField] GameObject enemyAttackPrefab;  // 攻撃プレファブ
    [SerializeField] float enemyAttackCoolTime;     // 攻撃のクールタイム
    [SerializeField] float spawnAttackPosition;      // 攻撃位置
    [SerializeField] float enemyknockback;          // ノックバックの勢い


    float nowAttackCoolTime;                        // 攻撃のクールタイムの経過時間

    //自身(SwordEnemy)の位置
    Vector2 swordEnemyPos;

    // プレイヤーと位置の差
    Vector2 playerPos;

    //当たり判定の設定
    Rigidbody2D ribd2d = null;
    SpriteRenderer spritRend = null;
    Collider2D enemyCollier2D;
    CapsuleCollider2D capcol2d;
    GameObject player; // プレイヤーを認識

    // エフェクト関連
    GameObject effect;
    [SerializeField]GameObject effectPrefab;    

    //Sound関係
    AudioSource audioSource;
    [SerializeField] AudioClip destroySE;
    [SerializeField] AudioClip damageSE;

    // スコアを一度だけ加算する用のbool型
    bool scoreTrue = false;

    //ドロップアイテム関連
    [Header("ドロップアイテム")]
    [SerializeField] GameObject dropHPItemPrefab;       //HP回復アイテム
    [SerializeField] GameObject dropStaminaItemPrefab;  //スタミナ回復アイテム
    int itemDropProbability;                            //アイテムドロップ確率

    //スコア増減用にアクセスするため
    GameManager gameManager;

    // ObjectPoolでエネミーを呼びだしたり消すため
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
        //自身の体力を設定
        nowSwordEnemyHp = enemyHp;
        
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
            if (playerPos.x >= swordEnemyPos.x)
            {
                //右向き
                //左向きがデフォルトなのでをまとめて反転させる
                transform.localScale = new Vector3( -1, 1, 1);
            }
            else if (playerPos.x <= swordEnemyPos.x)
            {
                //左向き
                transform.localScale = new Vector3( 1, 1, 1);
            }

       
    }

    void EnemyHp()
    {
        // HPが0になったら
        if (nowSwordEnemyHp == 0)
        {
            audioSource.PlayOneShot(destroySE);

            //isTriggerをfalse(当たり判定をすり抜ける)にする
            enemyCollier2D.isTrigger = true;
            capcol2d.enabled = false;

            if (scoreTrue == false)
            {
                scoreTrue = true;
                gameManager.PlusScore();
                itemDropProbability = Random.Range(1, 10);

                //アイテムを生成する
                Instantiate(dropHPItemPrefab, transform.position, Quaternion.identity);

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
        if (playerPos.x < 3)
        {
            nowAttackCoolTime += Time.deltaTime;
            if (enemyHp >= 1)
            {
                if (nowAttackCoolTime >= enemyAttackCoolTime)
                {
                    //左方向にいる
                    if (playerPos.x < 0)
                    {
                        GameObject Bullet = Instantiate(enemyAttackPrefab, transform.position + Vector3.left * spawnAttackPosition, Quaternion.identity);
                        Bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        Bullet.GetComponent<SpriteRenderer>().flipX = false;
                    }

                    //右方向にいる
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
        if (collision.gameObject.CompareTag("Attack")) //こっちのほうが処理が速い
        {
            nowSwordEnemyHp -= 1;

            effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);

            //playerposを参照して、playerの方向を判定する。
            //左方向にいる
            if (playerPos.x < 0)
            {
                LeftFlomAttack();
            }

            //右方向にいる
            if (playerPos.x > 0)
            {
                RightFlomAttack();
            }

            Destroy(effect, 0.5f);
        }
    }

    //左側から攻撃されたら右側に吹っ飛ばす
    void LeftFlomAttack()
    {    
        ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.right * enemyknockback, (ForceMode2D)ForceMode.Acceleration);
    }

    //右側から攻撃されたら左側に吹っ飛ばす
    void RightFlomAttack()
    {
        ribd2d.AddForce(Vector2.up * enemyknockback + Vector2.left * enemyknockback, (ForceMode2D)ForceMode.Acceleration);
    }
}

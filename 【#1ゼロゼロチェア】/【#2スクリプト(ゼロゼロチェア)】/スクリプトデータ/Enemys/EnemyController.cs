using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// 必ず指定したコンポーネントを持っていなければならない
[RequireComponent(typeof(NavMeshAgent))]    
[RequireComponent(typeof(CapsuleCollider))]    
[RequireComponent(typeof(AudioSource))]   
[RequireComponent(typeof(Animator))]    
public class EnemyController : MonoBehaviour
{
    
    static readonly int MOVE_ANIM = Animator.StringToHash("EnemyMove");
    static readonly int ATTACK_ANIM = Animator.StringToHash("EnemyAttack");
    static readonly int DMAGE_ANIM = Animator.StringToHash("EnemyDamage");
    static readonly int DEAD_ANIM = Animator.StringToHash("EnemyDead");

    [SerializeField] NavMeshAgent _agent;           // ナビメッシュ
    [SerializeField] Collider _collider;            // 自身の当たり判定
    [SerializeField] AudioSource _audioSource;      // オーディオソース
    [SerializeField] Animator _anim;                // アニメーター
    [SerializeField] AudioClip _damageVoice;
    [SerializeField] AudioClip _deathVoice;
    [SerializeField] AudioClip _attackSE;

    [SerializeField] float _periodOfCircularMotion;    // 円運動時の周期

    int _hp;                                    // 体力
    public int _damage;                         // 攻撃力    

    bool _isMove;                               // 自身が移動できるかどうか
    bool _moveTurnDirReset = false;             // 円運動時の回転方向のリセットするかどうか
    bool _isLive;                               // 自身が生きているかどうか

    EnemyAttackArea _attackArea;                // 攻撃範囲   
    bool isAttacking = false;


    // 移動のバリエーション
    enum MoveVariations
    {
        straight,
        circularMotion
    }
    MoveVariations _moveVariations; // 移動のバリエーション

    // 円運動時の回転方向
    enum TurnDir            // 内部でしか使わないならintで1 : -1でも良いかも
    {
        _clockWise = 1,
        _counterClockWise = -1
    }
    TurnDir _turnDir;      // 回転方向

    // このスクリプトがオブジェクトについた瞬間に実行される
    void Reset()
    {
        // Playerを追いかけるためのNavMech       
        _agent = GetComponent<NavMeshAgent>();
        // Enemyの当たり判定
        _collider = GetComponent<CapsuleCollider>();
        // AudioSourceを取得
        _audioSource = GetComponent<AudioSource>();
        // Animatorを取得
        _anim = GetComponent<Animator>();              
    }

    void OnEnable()
    {
        // 再表示時に実行
        _isLive = true;
        _isMove = true;

        _hp = GameManager.instance._enemyHP;
        _damage = GameManager.instance._enemyDamage;
        _collider.enabled = true;

        // 移動方法の選択
        // _moveVariationsにランダムに0.0～1.0の値を入れる
        _moveVariations = Random.value < 0.5f ? MoveVariations.straight : MoveVariations.circularMotion;

        // 値が0.5以下だったらstraight 0.5以上だったらcircularMotionを_moveVariationsに代入する
        if (Random.value < 0.5f)
        {
            _moveVariations = MoveVariations.straight;
            _agent.stoppingDistance = 0;
        }
        else
        {
            _moveVariations = MoveVariations.circularMotion;
            _agent.stoppingDistance = 3;
        }
    }

    void Start()
    {
        // 自身の攻撃エリアを取得     
        _attackArea = GetComponentInChildren<EnemyAttackArea>();
    }

    void Update()
    {
        Move();
         Attack();
    }

    void Move()
    {

        // プレイヤーを追い続けるように移動
        if (Player.instance && _isLive && _isMove)
        {
            // 移動アニメーション
            _anim.SetBool(MOVE_ANIM, true);
            // target(Player)を追ってくるように設定
            _agent.destination = Player.instance.transform.position;


            // もし移動パターンがstraightだったら
            if (_moveVariations == MoveVariations.straight)
            {
                // 毎フレーム取得し続けるのはいらない
                _agent.stoppingDistance = 0;

            }
            // もし移動パターンがcircularMotionだったら
            else if (_moveVariations == MoveVariations.circularMotion)
            {
                _agent.stoppingDistance = 3;
                //自身とプレイヤーの位置を計算し
                // プレイヤーの方向と距離を取得して
                Vector3 playerTarget = transform.position - Player.instance.transform.position;

                // ベクトルの内積を計算する
                float dotProduct = Vector3.Dot(Player.instance.transform.forward, playerTarget);

                // 自身がプレイヤーの正面にいるかどうか
                // 計算を基に自身がプレイヤーの正面にいるか判定
                // dotProductが正の場合は正面
                // dotProductが負の場合は背面
                // dotProductが0の場合は真横にいる
                bool targetFront = dotProduct > 0;


                // 自身がターゲットの正面位いたら
                if (targetFront)
                {
                    // プレイヤーの正面に行くたびに円運動の方向を変える処理
                    if (!_moveTurnDirReset)
                    {
                        // Random.value = 0.0～1.0の間でランダムで返される
                        _turnDir = Random.value < 0.5f ? TurnDir._clockWise : TurnDir._counterClockWise;

                        // 処理をリセット
                        _moveTurnDirReset = true;

                    }

                    // プレイヤーへの移動を止めて
                    _agent.isStopped = true;
                    // 円運動の移動速度を計算
                    float rotationSpeed = 360f / _periodOfCircularMotion * Time.deltaTime;
                    // 移動方向を設定
                    rotationSpeed *= (int)_turnDir;// == TurnDir._clockWise ? 1 : -1;
                    // プレイヤーを中心に円運動を開始
                    transform.RotateAround(Player.instance.transform.position, Vector3.up, rotationSpeed);

                }
                else
                {
                    _moveTurnDirReset = false;
                    _agent.isStopped = false;
                    _agent.stoppingDistance = 0;
                }
            }
        }
    }

void Attack()
{


        if (_attackArea._enemyAttackAreaInPlayer && !isAttacking)
        {
            isAttacking = true;
            // 動きを止めて
            _isMove = false;
            // 攻撃アニメーションを再生
            _anim.SetBool(ATTACK_ANIM, true);

        }
        else
        {
            // 攻撃アニメーションを停止
            _anim.SetBool(ATTACK_ANIM, false);
            // 動かして
            _isMove = true;

            isAttacking = false;
        }

    }



void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            // Playerの攻撃力を取得して
            int playerBulletDamage = GameManager.instance._playerDamage;
            // 現在の体力から引く
            _hp -= playerBulletDamage;

            // DamageVoiseを再生
            if (_hp >= 1)
            {
                _audioSource.PlayOneShot(_damageVoice);

            }
            // 被弾アニメーション
            _anim.SetTrigger(DMAGE_ANIM);
            EnemyHP();
            _anim.SetBool(MOVE_ANIM, true);
        }
    }
    void EnemyHP()
    {
        if (_hp <= 0 && _isLive)
        {
            _collider.enabled = false;
            // 死亡アニメーション
            _anim.SetTrigger(DEAD_ANIM);

            // 自身を死んだことにして
            _isLive = false;
            _isMove = false;

            _agent.isStopped = true;

            // 死亡時のSEを再生
            _audioSource.PlayOneShot(_deathVoice);

            // EnemyDeadを2f後に実行させる
            Invoke("EnemyDead", 2f);


        }
    }
    void EnemyDead()
    {
        // GameManagerのスコアを加算する
        GameManager.instance._score++;
        // 自身が死んだらPoolに返す
        EnemyPool.instance.pool.Release(gameObject);

    }
}

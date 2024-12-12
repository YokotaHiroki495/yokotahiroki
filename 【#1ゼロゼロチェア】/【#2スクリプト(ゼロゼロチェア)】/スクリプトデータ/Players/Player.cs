using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : PlayerSingleton<Player>
{
    // アニメーション
    static readonly int RECOIL_ANIM = Animator.StringToHash("Attack");
   
    [Header("体力")]
    [SerializeField] int _maxHP;    // 最大HP
    [SerializeField] Slider _hpSlider; // 体力スライダー

    [Header("攻撃")]
    [SerializeField] float _spawnAttackPosition;     // 弾生成位置
    [SerializeField] ParticleSystem _muzzlueFlash;   // 発射時のエフェクト
    [SerializeField] float _recoilAngle;             // 傾ける角度
    [SerializeField] float _recoilDuration;         // 傾けてる間隔

    // 移動用のRigidbody
    [SerializeField] Rigidbody _rigidbody;

    [Header("ダメージ処理")]
    [SerializeField] float _blinkIntarvel;    // 点滅の間隔
    [SerializeField] int _blinkCount;         // 点滅のループカウント
    [SerializeField] Collider _childCollider;// 自身の攻撃の当たり判定

    [Header("レイキャスト")]
    [SerializeField] LineRenderer _lineRenderer;

    [Header("ギミックの情報")]
    [SerializeField] GameObject _damageWallObject;

    [Header("サウンド関係")]
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _shotSE;      // 攻撃時のSE
    [SerializeField] AudioClip _damageSE;    // 被ダメージ時のSE

    float _hp;                // 現在のHP
    SkinnedMeshRenderer _skinnedmeshrendrer;  // 点滅させるMeshRenderer 
    GameObject _grandChild;                   // 自身の子オブジェクト
    Plane _plane = new Plane();               // Rayを感知する平面
    Vector3 _lookPoint;                       // プレイヤーの向き                 
    float _damageWallAttackPower;             // ダメージウォールの攻撃力 

    // プレイヤーが行動可能かのbool
    public bool _isAction ;


    void Start()
    {
        // 子オブジェクトの情報を取得する
        _grandChild = transform.Find("ContractKiller/ContractKiller").gameObject;
        _skinnedmeshrendrer = _grandChild.GetComponent<SkinnedMeshRenderer>();
        
        // 設定した体力を今の体力に代入
        _hp = _maxHP;

        if (_hpSlider != null) 
        {
            // スライダーを最大に
            _hpSlider.value = 1;
        }
        // プレイヤーの行動可能状態として設定
         _isAction = !GameManager.instance._timeStop;

        // GamaManager経由で壁のダメージを取得    
        _damageWallAttackPower = _damageWallObject.GetComponent<DamageWall>()._wallDamage;




    }

    void Update()
    {
        if (_isAction)
        {
            GunAiming();
            GunFire();    
        }
    }


    void GunAiming()
    {
        // マウスからRayを飛ばす
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // 平面の設定
        _plane.SetNormalAndPosition(Vector3.up, transform.localPosition);

        // もしRayが平面上に当たったら
        if (_plane.Raycast(ray, out float distance))
        {
            // 当たった場所を取得して
            _lookPoint = ray.GetPoint(distance);

            // 取得した場所に向かせる
            transform.LookAt(_lookPoint);
            
        }

        // レイヤーマスクの当たるオブジェクトの設定
        int layerMask = LayerMask.GetMask("Enemy", "Obstacle", "Wall");

        // プレイヤーからLineRendererを出す
        //始点、向き、　レイキャストヒット、レイの長さ、レイヤーマスク
        if (Physics.Raycast(transform.position, transform.forward, out var hit, distance, layerMask))  
        {
            // もしプレイヤーの攻撃に当たったら無視する 
            _lineRenderer.SetPosition(1,  transform.InverseTransformPoint(hit.point)); 
        }
        else
        {
            //　障害物に当たらない場合　マウスの位置までLineRendererを伸ばす
            _lineRenderer.SetPosition(1,transform.InverseTransformPoint(_lookPoint));
        }
    }

    // 攻撃処理
    void GunFire()
    {
        // 左クリックしたら
        if (Input.GetMouseButtonDown(0))
        {

            GameObject playerBullet;            // 弾のオブジェクト
            Vector3 bulletMoveDirection;        //銃弾の移動方向
            const float inpulseBulletPower = 5; // 銃弾の移動パワー
            const float impulsePower = 3;       // 移動時の力
            bool firstFire = true;

            // BulletObjectをPoolから持ってくる
            playerBullet = BulletPool.Get();
            
            // 銃弾の移動方向をプレイヤーの正面に設定
            bulletMoveDirection = transform.forward;

            // BulletObjectの生成位置と進行方向を設定
            playerBullet.transform.SetPositionAndRotation(transform.position + transform.forward * _spawnAttackPosition, Quaternion.identity/*Quaternion.Euler(_bulletMoveDirection)*/);

            playerBullet.transform.localEulerAngles = new Vector3(90f, transform.eulerAngles.y, 0f);

            // 初めてpoolから取り出したら
            if (firstFire)
            {
                // BulletObjectの飛ばす方向を設定して飛ばす
                playerBullet.GetComponent<Rigidbody>().velocity = bulletMoveDirection * inpulseBulletPower;       // poolから取り出したらGetComponentしなおしてる
                // 以降は最初に設定した力をそのまま使う
                firstFire = false;

            }

            // プレイヤーの反動の挙動を実行    
            StartCoroutine(PlayerRecoil());
            
            
            // Player自身を発射方向とは反対方向に飛ばす
            _rigidbody.AddForce(-transform.forward * impulsePower, ForceMode.Impulse);

            // マズルフラッシュを再生
            _muzzlueFlash.Play();

            // 射撃SEを再生
            _audioSource.PlayOneShot(_shotSE);
        }
    }

    // 攻撃の反動での傾き
    IEnumerator PlayerRecoil()
    {
        // 初期角度を保存
        Quaternion playerRotation = transform.localRotation;

        // 傾ける角度を計算
        Quaternion recoilRotation = playerRotation * Quaternion.Euler(-90, 0, 0);       

        float elapsedTime = 0;

        // 徐々に傾ける
        while (elapsedTime < _recoilDuration)
        {
            transform.localRotation = Quaternion.Slerp(playerRotation, recoilRotation, elapsedTime / _recoilDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最終的な角度(設定した角度)を計算
        transform.localRotation = recoilRotation;

        // 傾いた状態での静止時間
        yield return new WaitForSeconds(0f);

        elapsedTime = 0f;

        // 徐々に元に戻す
        while (elapsedTime < _recoilDuration)
        {
            transform.localRotation = Quaternion.Slerp(recoilRotation, playerRotation, elapsedTime / _recoilDuration); // x軸のみ傾けてる ローカルエンゲルスの方が良いかも
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 最終的な角度(元の角度)を設定
        transform.localRotation = playerRotation;
    }


    void OnTriggerEnter(Collider other)
    {
        // ダメージ判定
        // 敵の攻撃に当たったら
        if (other.CompareTag("EnemyAttackObject"))
        {
            // 被ダメージSEを再生して
            _audioSource.PlayOneShot(_damageSE);

            // ダメージ処理を実行
            PlayerHPDamage();
        }

        // ダメージウォールに接触したら
        if (other.gameObject.TryGetComponent(out DamageWall damageWall))
        {
            // ダメージ状態だったら
            if (damageWall.damageOn)
            {
                // 被弾処理を実行 
                PlayerHPDamage();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // オブジェクト(Collider)に当たった際の物理挙動の設定
        Vector3 normal = collision.contacts[0].normal;
        Vector3 direction = _rigidbody.velocity;
        Vector3 reflectedDirection = Vector3.Reflect(direction, normal);
        _rigidbody.velocity = reflectedDirection;
    }
    void PlayerHPDamage()
    {
        // 自身の体力から敵の攻撃力を引く
        _hp -= GameManager.instance._enemyDamage;


        // 被弾処理を実行
        StartCoroutine(TakeDamage());
        // 体力バーの計算
        _hpSlider.value = (float)_hp / (float)_maxHP;

        // 体力が0になったら
        if (_hp <= 0)
        {
            // GameManagerのMoveGameOverを実行
            GameManager.instance.MoveGameOver();
        }
    }

    
   
    // 攻撃を受けたら
    IEnumerator TakeDamage()
    {
        // 当たり判定を無効状態に
       // _collider.enabled = false;
        _childCollider.enabled = false;
        // プレイヤーが被弾したことを点滅で表示
        for (int i = _blinkCount; i > 0; i--)    // 消して表示してで1セット
        {
            // SkinnedMeshRendererを非表示と表示を繰り返す
            _skinnedmeshrendrer.enabled = !_skinnedmeshrendrer.enabled;
            yield return new WaitForSeconds(_blinkIntarvel);
        }
        // 当たり判定を有効にして
        _skinnedmeshrendrer.enabled = true;
        _childCollider.enabled = true;
    }
}

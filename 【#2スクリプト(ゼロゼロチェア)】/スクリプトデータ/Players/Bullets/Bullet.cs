using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    [SerializeField]BulletData _bulletData; // 弾の情報

    float _returnPoolDuration;              // 弾をプールに返す時間
    float _validityTime;                  // 弾の有効時間

    // 弾の反射の計算
    Vector3 _velocity; // 弾の速度ベクトル
    Rigidbody _rb;      // 自身のRigidbody


    void Awake()
    {
        // 弾の持続時間を取得
        _returnPoolDuration = _bulletData._returnTime;
    }

    void OnEnable()
    {
        // 自分自身をアクティブにする
        this.gameObject.SetActive(true);   
    }

    void Start()
    {
        // 自身のRigidbodyを取得
        _rb = GetComponent<Rigidbody>();
     
    }

    void Update()
    {
        // rigidbodyの速度ベクトル(velocity)を取得
        _velocity = _rb.velocity;

        // 弾の有効時間を計算
        _validityTime += Time.deltaTime;

       

        // 弾の有効時間がプールに返す時間よりも長く存在していたら
        if (_validityTime >= _returnPoolDuration)        
        {
            // プールに返す
            BulletPool.Release(gameObject);
            // 弾の有効時間を0にする
            _validityTime = 0;

        }
        
    }



    void OnCollisionEnter(Collision collision)
    {

        Vector3 _normal;    // 衝突時の法線ベクトル

        switch (collision.gameObject.tag)
        {
            case "Wall":
            case "DamageWall":
            case "Obstacle":

                // 衝突の法線ベクトルを取得
                ContactPoint contant = collision.GetContact(0);
                _normal = contant.normal;

                // Reflectをつかって反射を刺せる
                Vector3 result = Vector3.Reflect(_velocity, _normal);
                // 反射後のベクトルに速度を設定
                _rb.velocity = result;
                // directionの更新
                _velocity = _rb.velocity;
                // オブジェクトの向きを進行方向に変更
                 transform.forward = _rb.velocity.normalized;
                // オブジェクトの向きをX軸に対して90度回転
                transform.Rotate(90, 0, 0);
                break;

            case "Enemy":
            case "Trap":
                BulletPool.instance.pool.Release(gameObject);
                break;
        }
    }
}

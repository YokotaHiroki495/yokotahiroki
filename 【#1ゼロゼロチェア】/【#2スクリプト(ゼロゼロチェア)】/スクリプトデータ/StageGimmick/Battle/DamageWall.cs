using System.Collections;
using UnityEngine;

public class DamageWall : MonoBehaviour
{

    [SerializeField] Renderer _renderer;    // 自身のレンダラー
    [SerializeField] Gradient _colorGradient;    // 色のグラデーション
                                                 
    [Tooltip("攻撃持続時間")]
    [SerializeField] float _damageTime;          // 攻撃持続時間
    [Tooltip("攻撃待機時間")]
    [SerializeField] float _damageStandbyTime;   // 攻撃待機時間

    // 点滅の周期
    [SerializeField] float _initialBlinkDuration = 1.0f;   // 初期の点滅周期
    [SerializeField] float _acceleration = 0.9f;           // 点滅速度の加速率

    public float _wallDamage { get; private set; } // 壁の攻撃力
    public bool damageOn { get; private set; } = false;  // 攻撃判定のプロパティ

    float _currentBlinkDuration;                        // 点滅する間隔
    // サウンド関係
    AudioSource _audioSource;



    void Start()
    {
        // AudioSourceを取得
        _audioSource = GetComponent<AudioSource>();
       
        // 点滅処理を実行
        StartCoroutine(SmoothBlink());

    }

    IEnumerator SmoothBlink()
    {        
        // ダメージSE再生終了
        _audioSource.Stop();

        // 初期の点滅時間を代入
        _currentBlinkDuration = _initialBlinkDuration;
       
        // 点滅の終了時間を設定
        float endTime = Time.time + _damageStandbyTime;

        while (Time.time < endTime) //　終わりの時間に満たしていない場合
        {
            // 設定した時間になるまで点滅を繰り返す
            for (float t = 0; t <= 1; t += Time.deltaTime / (_currentBlinkDuration))
            {
                _renderer.material.color = _colorGradient.Evaluate(t);
                yield return null;
            }

            // 点滅速度を速くする
            _currentBlinkDuration *= _acceleration;
        }
        // 終わりの時間達したら
        StartCoroutine(RedColor());
    }

    IEnumerator RedColor()
    {
        // 赤色に設定
        _renderer.material.color = Color.red;

       // プレイヤーがダメージを受けるようにbool型をtrueに
        damageOn = true;

        // ダメージSEを再生
        _audioSource.Play();

        // 設定した時間になったら
        yield return new WaitForSeconds(_damageTime);
        
        // bool型をfalseにして
        damageOn = false;
        // ダメージSEを停止
        _audioSource.Stop();

        // 点滅処理を実行
        StartCoroutine(SmoothBlink());
    }
}

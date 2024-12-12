using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour    
{
    // シングルトンとして設定
     static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<GameManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(GameManager).ToString());
                    _instance = singletonObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    [System.Serializable] // インスペクター上で編集できるように設定

    class SceneTransition
    {
        public string sceneName;        // シーン名
        public Color fadeColor;         // フェードカラー
        [Min(0)] public float fadeDamp; // フェードする時間
    }
    // List型でSceneTransitionを表示および編集を可能にする
    [SerializeField] List<SceneTransition> _sceneTransitions = new List<SceneTransition>();

    void OnValidate()
    {
        // SceneTransitionリスト内の要素tに対してループを実行
        foreach (var t in _sceneTransitions)
        {
            // tのfadeColorのアルファ値(a)が1未満かどうかチェック
            if (t.fadeColor.a < 1)
            {
                // 現在のfadeColorを一時的にコピー
                Color fadeColor = t.fadeColor;
                // アルファ値を1に設定　色が完全に不透明になるようにする
                fadeColor.a = 1;
                // 変更されたfadeColorを元のtに戻す
                t.fadeColor = fadeColor;
            }
        }
    }
  
    // スコアを表示するためのテキスト
    [SerializeField] TextMeshProUGUI _timeText;
    // スコア表示用のテキスト
    [SerializeField] TMP_Text _scoreText;
    // 制限時間
    [SerializeField] float _timeCount;
    // プレイヤーのBulletの情報を管理
    [SerializeField] BulletData _bulletData;
    // EnemyDataの情報を一括管理
    [SerializeField] EnemyData _enemyData;

    // スコアを入れる変数
    public int _score;
    // タイムを止めるための判定 
    public bool _timeStop;
    // チェックポイントのポジション
    public Vector3 _checkpointPosition;
    // 現在のステージを入れる変数
    public string _sceneName;
    // プレイヤーの攻撃力
    public int _playerDamage;
    // 敵の体力
    public int _enemyHP;
    // 敵の攻撃力
    public int _enemyDamage;

    void Awake()
    {
        _playerDamage = _bulletData._damage;

        _enemyHP = _enemyData._hp;
        _enemyDamage = _enemyData._damage;

        // 現在のステージを取得する
        _sceneName = SceneManager.GetActiveScene().name;    // ここで確保する必要はないのかも　シーン名を分割して保存するなら誰かが代表してもってても良い

        switch (_sceneName)     
        {

            case "Tutorial":
                _timeStop = false;
                break;

            case "Escape":
                _timeStop = true;
                // 初期チェックポイントの設定
                _checkpointPosition = transform.position;
                break;

            case "Battle":
                _timeStop = false;
                // スコアを表示する
                _scoreText.text = $"倒した敵の数:{ _score}";
                break;

         
        }
    }

    void Update()
    {
        if (_scoreText != null)
        {
            // スコアをテキストに表示する
            _scoreText.text = $"倒した敵の数:{ _score}";
        }
        if (_timeText != null)
        {
            TimeCount();
        }
        
    }

    void TimeCount()
    {
        if (_timeStop == false)
        {
            // 制限時間を適宜引いてく
            _timeCount -= Time.deltaTime;

            // テキストの時間を随時更新       
            _timeText.text = $"Time:{_timeCount:n1}";   // :n1 小数点第一位まで表示する

            // 制限時間が0になったら
            if (_timeCount <= 0)
            {
                switch (_sceneName)
                {
                    case "Battle":
                        MoveGameClear();
                        break;

                    case "Escape":
                        MoveGameOver();
                        break;
                }
            }
        }
    }

    public void MoveGameClear()
    {
        // カウントを止める
        _timeStop = true;
        Initiate.Fade("GameClear", Color.black, 2.0f);        
    }

    public void MoveGameOver()
    {
        // カウントを止める
        _timeStop = true;
        Initiate.Fade("GameOver", Color.black, 2.0f);
    }
}


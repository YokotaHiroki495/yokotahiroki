using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    [Header("説明テキスト")]
    [SerializeField] TextMeshProUGUI[] _explanationText;

    [Header("矢印UI")]   
    [Space]
    [SerializeField] SpriteRenderer[] _arrowRenderers;

    [Tooltip("地面の矢印案内")]
    [SerializeField] GameObject _secondAreaArrowObject;


    [SerializeField] float _blinkDuration;  // 点滅時間
    [SerializeField] int _blinkCount;
    [SerializeField] float _blinkInterval;  // 点滅間隔

    [Tooltip("ゴールの矢印")]
    [SerializeField] SpriteRenderer _goalArrow;

    [Tooltip("ゴールの当たり判定")]
    [SerializeField] GameObject _goalArea;

    [SerializeField] SpriteRenderer _tutorialTarget;

    [Header("敵の管理")]
    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] Vector3 _firstAreaEnemiesPos;
    [SerializeField] Vector3[] _secondAreaEnemiesPos;
    [SerializeField] string _tagToCheck = "Enemy";
    [SerializeField] float _checkInterval;
    [SerializeField] GameObject _enemySpawner;

    [Header("ギミック管理")]
    [SerializeField] GameObject _blockWalls;

    [Header("体力ゲージ")]
    [SerializeField] Slider _playerHPSlider;


    SpriteRenderer _secondAreaArrowRenderer;
    SpriteRenderer _goalArrowRenderer;
    SpriteRenderer _targetRender;
    EnemyController _enemyController;
    GameObject _createdEnemyPool;
    GameObject[] _createdEnemies;
    bool _enemyIsLive;

    void Start()
    {
        // 説明テキストを非表示に
        _explanationText[0].enabled = true;
        _explanationText[1].enabled = false;
        _explanationText[2].enabled = false;

        // 体力バーを非表示に
        _playerHPSlider.enabled = false;
     
        // 各種データを取得
        _secondAreaArrowRenderer = _secondAreaArrowObject.GetComponent<SpriteRenderer>();
        _targetRender = _tutorialTarget.GetComponent<SpriteRenderer>();
        _goalArrowRenderer = _goalArrow.GetComponent<SpriteRenderer>();
        _enemyController = _enemyPrefab.GetComponent<EnemyController>();


        // ゴールの当たり判定を非表示に
        _goalArea.SetActive(false);

        // エリアを区切る壁を非表示に
        _blockWalls.SetActive(false);
        
        // 敵が生きているかどうかの判定をfalseに
        _enemyIsLive = false;

        // 各種説明イラストを点滅させるコルーチンを実行
        StartCoroutine(DirectionArrow());
        StartCoroutine(StartDirection());

        //　初回Tagチェック
        _createdEnemies = GameObject.FindGameObjectsWithTag("Enemy");　 
        StartCoroutine(CheckEnemyTag());
    }

    // Firstエリアに入った時の処理
    public void FirstTutorial()
    {
        // 必要なテキストを表示して
        _explanationText[0].enabled = false;
        _explanationText[1].enabled = true;

        // 障害物を表示して
        _blockWalls.SetActive(true); 
        
        //CreateWall();

        // 敵をプールから呼び出す
        _createdEnemyPool = EnemyPool.instance.pool.Get();
        // 敵を指定位置に生成
        _createdEnemyPool.transform.position = _firstAreaEnemiesPos;
        // 敵の生成時の方向を指定(入口側を向くように)
        _createdEnemyPool.transform.rotation = Quaternion.Euler(0, 180, 0);// トランスフォーム　空のオブジェクト　firstEnemy 座標を firstEnemy.position 向きをfirstEnemy.rotation 

        // .setAndRotationで位置と角度を一括で取得
        // ここで敵の数を取得。　敵が死んだら、敵が死んだ瞬間に取得した数から敵の死んだ敵の数を引く　


    }

    // Secondエリアに入った時の処理
    public void SecondTutorial()
    {
        // 必要なテキストを表示して
        _explanationText[1].enabled = false;
        _explanationText[2].enabled = true;
        // 体力バーを表示する
        _playerHPSlider.enabled = true;

        // 障害物から表示して
        _blockWalls.SetActive(true);
        _goalArea.SetActive(false);
        // 敵の生成位置を指定  
        for (int i = 0; i < _secondAreaEnemiesPos.Length; i++)
        {
            // 敵を生成
            _createdEnemyPool = EnemyPool.instance.pool.Get();

            // 配列の中の生成位置を参照して生成      
            _createdEnemyPool.transform.position = _secondAreaEnemiesPos[i]; 
            _createdEnemyPool.transform.rotation = Quaternion.Euler(0, 180, 0);
            // .setAndRotationで位置と角度を一括で取得

        }

        // ここで敵の数を取得。　敵が死んだら、敵が死んだ瞬間に取得した数から敵の死んだ敵の数を引く　
    }

    // 点滅表示用
    IEnumerator DirectionArrow()
    {
       
        for (int i = _blinkCount; i > 0; i--)    // 消して表示してで1セット
        {
            // SkinnedMeshRendererを非表示と表示を繰り返す
            // ここでインターバル毎にtrueとfalseを入れ替える
            _secondAreaArrowRenderer.enabled = !_secondAreaArrowRenderer.enabled;
            _goalArrowRenderer.enabled = !_goalArrowRenderer.enabled;



            //_arrowRenderers[0].enabled = !_arrowRenderers[0].enabled;
            //_arrowRenderers[1].enabled = !_arrowRenderers[1].enabled;
            //_arrowRenderers[2].enabled = !_arrowRenderers[2].enabled;
            //_arrowRenderers[3].enabled = !_arrowRenderers[3].enabled;

            yield return new WaitForSeconds(_blinkInterval);
        }
        
        // 比較用に時間が点滅時間よりも長くなったらfalseにする
        _secondAreaArrowRenderer.enabled = false;
        _goalArrowRenderer.enabled = false;
    }

    // クリックされるまで表示する
    IEnumerator StartDirection()
    {
        //　クリック前の処理
        _targetRender.enabled = true;

        for (int i = _blinkCount; i > 0; i--)    // 消して表示してで1セット
        {
            // SkinnedMeshRendererを非表示と表示を繰り返す
            // ここでインターバル毎にtrueとfalseを入れ替える

            _arrowRenderers[0].enabled = !_arrowRenderers[0].enabled;
            _arrowRenderers[1].enabled = !_arrowRenderers[1].enabled;
            _arrowRenderers[2].enabled = !_arrowRenderers[2].enabled;
            _arrowRenderers[3].enabled = !_arrowRenderers[3].enabled;

            yield return new WaitForSeconds(_blinkInterval);
        }

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0)); //クリックするまで待機

        _targetRender.enabled = false;

        _arrowRenderers[0].gameObject.SetActive(false);
        _arrowRenderers[1].gameObject.SetActive(false);
        _arrowRenderers[2].gameObject.SetActive(false);
        _arrowRenderers[3].gameObject.SetActive(false);
    }

    IEnumerator CheckEnemyTag()
    {
        while (true)
        {
            _createdEnemies = GameObject.FindGameObjectsWithTag("Enemy");  //  Tagを持つオブジェクトを再取得
            // 取得するのはやめる
            // 死んだタイミングで_createdEnemies.Lengthの中身を－1する
            if (_createdEnemies.Length == 0)
            {
                _goalArea.SetActive(true);
                StartCoroutine(DirectionArrow());

                _blockWalls.SetActive(false);
            }
            yield return new WaitForSeconds(_checkInterval);  
        }
    }

   

   
}

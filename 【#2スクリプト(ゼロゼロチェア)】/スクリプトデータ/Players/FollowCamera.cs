using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;


public class FollowCamera : MonoBehaviour
{
    // Playerの位置を取得
    [SerializeField] Transform _playerTransform;    // プレイヤーの初期位置
    [SerializeField] Transform _cameraTransform;    // カメラの初期位置

    // ゲーム開始時にゴールを明示するためカメラの動き
    [SerializeField] PlayableDirector _cameraDirector;
    
    // ゲーム画面のUI
    [SerializeField]Canvas _playerUI;
    // ポーズ画面のUI
    [SerializeField]Canvas _pauseUI;
    // Escapeステージの説明テキスト
    [SerializeField] Canvas stageExplanation;

    // プレイヤーのラインレンダラー
    [SerializeField] LineRenderer playerRenderer;

    // カメラとプレイヤーの初期位置の差を保存する変数
    Vector3 _initialOffset;
    // リトライの際のシーン名
    public static string retryScene;

    void Start()
    {
        
        // カメラとプレイヤーの初期位置の差を計算して保存
        _initialOffset = _cameraTransform.position - _playerTransform.position;

        // ポーズUIを非表示に
        _pauseUI.enabled = false;

        if (_cameraDirector != null)
        {
            // mainCameraを動かすTimeLineを再生 
            _cameraDirector.Play();
            // TimeLineが終わった際の処理を実行するために設定       
            _cameraDirector.stopped += OnPlayableDirectorStopped;        
            // テキストを点滅させるコルーチンを実行       
           // StartCoroutine(DirectionText());
        }

      
    }

    void Update()
    {
        // ステージ名が"Escape"だったら
        if (GameManager.instance._sceneName == "Escape")
        {
            // Timelineが再生中かチェック
            if (_cameraDirector.state == PlayState.Playing)
            {
                // 各種、ゲームUIを非表示に          
                playerRenderer.enabled = false;
                _playerUI.enabled = false;
                Cursor.visible = false;
                // ステージ説明テキストを表示         
                stageExplanation.enabled = true;

            }
        }


        // カメラの位置をオブジェクトの位置に対して初期のを維持するように更新
        Vector3 newPosition = _playerTransform.position + _initialOffset;
        newPosition.y = _cameraTransform.position.y;            // Y座標のみ固定する
        _cameraTransform.position = newPosition;


        if (_cameraDirector == null || _cameraDirector.state != PlayState.Playing)
        {
            DisplayPauseUI();
        }
        //DisplayPauseUI();
    }

    void OnPlayableDirectorStopped(PlayableDirector director)
    {
        // カメラが元の位置に戻ったら
        if (director == _cameraDirector)
        {
            // ステージ説明テキストを非表示にし、
            stageExplanation.enabled = false;
            // ゲームUIを表示
            playerRenderer.enabled = true;
            _playerUI.enabled = true;
            Cursor.visible = true;
            Player.instance._isAction = true;
            GameManager.instance._timeStop = false;
        }
    }

    void DisplayPauseUI()
    {
        if (Input.GetKeyDown(KeyCode.P)|| Input.GetMouseButtonDown(1))
        {
            // キーを押すたびにCanvasの有効/無効を切り替える
            _pauseUI.enabled = !_pauseUI.enabled;
            
            // PauseUIが表示されていたら
            if (_pauseUI.enabled)
            {
                GameManager.instance._timeStop = true;
                // 時間の流れを0にする
                Time.timeScale = 0f;
                // Playerが持っている_playerIsActionを切り替え
                Player.instance._isAction = false;
            }
            else
            {
                GameManager.instance._timeStop = false;
                Time.timeScale = 1f;
                Player.instance._isAction = true;
            }
        }
    }

    public void Continue()
    {
        _pauseUI.enabled = false;
        GameManager.instance._timeStop = false;
        Time.timeScale = 1f;
        Player.instance._isAction = true;
    }

    public void Restart()
    {
        _pauseUI.enabled = false;
        GameManager.instance._timeStop = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(GameManager.instance._sceneName);
    }

    public void StageSelect()
    {
        _pauseUI.enabled = false;
        GameManager.instance._timeStop = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("StageSelect");
    }

    public　void MoveTitle()
    {
        _pauseUI.enabled = false;
        GameManager.instance._timeStop = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }
}

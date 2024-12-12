
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    

    // シーン移動関連
    // シーン移動時のフェードのカラー
    [SerializeField]Color fadeColor;

    // シー移動の時間
    [SerializeField]float fadeSpeed;


    //スコア関連
    //クリア用スコア
    [SerializeField] int clearScoreNumLimit;

    //スコア変数
    public static int score;
    [SerializeField]int minScoreLimit;

    //スコア表示用Text
    public TextMeshProUGUI scoreText;   

    //シーンの名前を保存する
    string sceneName;


    //音関連
    //ボス撃破ボイス
    [SerializeField] AudioClip bossDestroyVoice;
    AudioSource audioSource;

    PlayerScript playerObject;
    float gmPlayerHPNumber;

    void Start()
    {
        playerObject = GameObject.Find("Player").GetComponent<PlayerScript>();
        audioSource = GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;
        
        //●スコア
        //スコアを0にする
        score = clearScoreNumLimit;

        //スコアをテキストに表示
        scoreText.text = "Enemy:" + score;   
    }
       
    void Update()
    {
        //プレイヤーの体力をゲームマネージャーで保存する
        gmPlayerHPNumber = playerObject.playeryHp;
        GMPlayerHP();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#else

         Application.Quit();

#endif

        }

    }

        public void GMPlayerHP()
    {
        //プレイヤーのHPが0以下になったら
        if (gmPlayerHPNumber <= 0)
        {
            //リザルトシーンに移動
            Invoke("MoveResultScene", 2.0f);
        }
    }

    public void PlusScore()
    {
        //敵の残り数を減らす
        score--;

        //スコアをテキストに表示
        scoreText.text = "Enemy:" + score;

        if (score <= minScoreLimit)
        {
            score = minScoreLimit;

        }

        switch (sceneName)
        {
            case "BattleRoyaleScene":
                if (score == minScoreLimit)
                {
                    //シーンを移動させる
                    Invoke("MoveBossStageScene", 3.0f);
                }
            break;

            case "BossScene":
                if (score == 0)
                {
                    //ボス撃破SEを鳴らして
                    audioSource.PlayOneShot(bossDestroyVoice);

                    //シーンを移動させる
                    Invoke("MoveResultScene", 5.0f);
                }
                break;
        }
    }

    void MoveBossStageScene()
    {
        Initiate.Fade("BossScene", fadeColor, fadeSpeed);

    }
    
    void MoveResultScene()
    {
        Initiate.Fade("ResultScene", fadeColor, fadeSpeed);
    }
        
}

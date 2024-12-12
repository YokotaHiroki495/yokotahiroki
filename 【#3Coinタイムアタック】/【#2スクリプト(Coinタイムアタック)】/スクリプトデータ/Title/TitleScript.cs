using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


//TitleSceneのスクリプト
public class TitleScript: MonoBehaviour
{
    //タイトル画面用Script
    //リザルトのスコアを持ってきて表示する

    //Score変数を保存するための配列を生成
    public float [] TitleHighScore;

    //ランキング用スコア
    [SerializeField] GameObject TitleFirstScore;
    [SerializeField] GameObject TitleSecondScore;
    [SerializeField] GameObject TitleTherdScore;

    //TextMeshを表示する用の変数
    private TextMeshProUGUI TitleFirstText;
    private TextMeshProUGUI TitleSecondText;
    private TextMeshProUGUI TitleTherdText;

    //リザルト画面のScriptを取得させる
    public ResultScript resultscropt;


    //シーン移動する際の演出
    //ロードするシーン名前
    [SerializeField] private string SceneName;
    //フェードの色
    [SerializeField] private Color FadeColor;
    //フェードの速度
    [SerializeField] private float FadeSpeed;
 


    void Start()
    {
        //タイトル用の配列の中身を作成
        TitleHighScore = new float[3];



        //タイトル表示時に保存されているハイスコアをタイトルようの配列に入れる
        TitleHighScore[0] = PlayerPrefs.GetFloat("HIGHSCORETIME0");
        TitleHighScore[1] = PlayerPrefs.GetFloat("HIGHSCORETIME1");
        TitleHighScore[2] = PlayerPrefs.GetFloat("HIGHSCORETIME2");



        //テキストの障子オブジェクトを取得
        TitleFirstText = TitleFirstScore.GetComponent<TextMeshProUGUI>();
        TitleSecondText = TitleSecondScore.GetComponent<TextMeshProUGUI>();
        TitleTherdText = TitleTherdScore.GetComponent<TextMeshProUGUI>();


        //取得した値をタイトルに表示
        TitleFirstText.text = "1st:" + TitleHighScore[0].ToString("n1");
        TitleSecondText.text = "2nd:" + TitleHighScore[1].ToString("n1");
        TitleTherdText.text = "3rd:" + TitleHighScore[2].ToString("n1");

    }

    // Update is called once per frame
    void Update()
    {
        //もしEscapaキーが押されたら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Quiteを実行
            Quite();
        }
        
    }

    //ボタン(スタートボタン)を押されたら
    public void OnClick()
    {
        //フェードを使ってGameSceneに移動
        Initiate.Fade("Tutorial", Color.black, 1.0f);
    }



    //ゲーム修了
    void Quite()
    {
        //# プリプロセス(前処理)　プラットフォーム依存コンパイル
#if UNITY_EDITOR //ゲームコードから Unity エディターのスクリプトを呼び出すための #define ディレクティブ
        //エディターの再生モードの終了
        UnityEditor.EditorApplication.isPlaying = false; //true = 実行　false = 終了
#else
        //アプリケーションの終了
        Application.Quit(); //exeに出力した際にはこの部分がないとゲームが終了しない
#endif

    }
}

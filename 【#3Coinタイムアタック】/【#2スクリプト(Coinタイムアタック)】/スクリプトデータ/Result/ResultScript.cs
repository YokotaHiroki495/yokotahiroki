using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    //リザルト画面でのスコアの表示用Script

    //-----スコア保存用-----
    public static float TimeScoreNum;


    //ランキング保存用配列
    public static float[] HighScoreTime;


    //-----スコア表示用-----
    public GameObject FirstTimeScore;
    public GameObject SecondTimeScore;
    public GameObject TherdTimeScore;

    public TextMeshProUGUI FirstTimeText;
    public TextMeshProUGUI SecondTimeText;
    public TextMeshProUGUI TherdTimeText;

    //今回のスコア表示用
    public GameObject NowTimeScore;
    TextMeshProUGUI NowTimeScoreText;


    void Start()
    {
        //PlayerPrefs.DeleteKey("HIGHSCORETIME0");
        //PlayerPrefs.DeleteKey("HIGHSCORETIME1");
        //PlayerPrefs.DeleteKey("HIGHSCORETIME2");

        //GameManagerが持っているスコアを代入
        TimeScoreNum = GameManager.ClearTimeCount;
  
        //時間を比較して、ランキングを作る処理
        TimeRanking();


        //テキストに表示する処理
        TimeTextDisplay();

        


    }


    private void TimeRanking()
    {

        //3位まで表示する配列を作成
        HighScoreTime = new float[3];

        //配列のロード
        HighScoreTime[0] = PlayerPrefs.GetFloat("HIGHSCORETIME0");
        HighScoreTime[1] = PlayerPrefs.GetFloat("HIGHSCORETIME1");
        HighScoreTime[2] = PlayerPrefs.GetFloat("HIGHSCORETIME2");


        //-------------------------------------------------------------------------------------------------------------------------------色々修正

        //比較
        //配列を参照して、タイムが少なかったら中身を更新する。もしくは、0が入っていたら更新
        //if (HighScoreTime[0] > TimeScoreNum || HighScoreTime[0] == 0)
        //{
        //    //配列の数字を一つずらして
        //    HighScoreTime[2] = HighScoreTime[1];
        //    HighScoreTime[1] = HighScoreTime[0];

        //    //1位を更新
        //    HighScoreTime[0] = TimeScoreNum;


        //    //スコアをfloat型で取得
        //    PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
        //    //スコアを保存
        //    PlayerPrefs.Save();
        //}
        ////元々配列に入っている値以下で、なおかつ1位の値よりも数値が大きい場合。もしくは値に0が入っている場合。
        //else if (HighScoreTime[1] > TimeScoreNum && TimeScoreNum > HighScoreTime[0] || HighScoreTime[1] == 0)
        //{
        //    HighScoreTime[2] = HighScoreTime[1];
        //    HighScoreTime[1] = TimeScoreNum;

        //    PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
        //    PlayerPrefs.Save();
        //}
        //else if (HighScoreTime[2] > TimeScoreNum && TimeScoreNum > HighScoreTime[1] || HighScoreTime[2] == 0)
        //{
        //    HighScoreTime[2] = TimeScoreNum;

        //    PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);
        //    PlayerPrefs.Save();

        //}

        //比較ver2
        //最初は必ず1位になるように
        if (HighScoreTime[0] == 0)
        {
            HighScoreTime[0] = TimeScoreNum;

            PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
            PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
            PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);
            
            PlayerPrefs.Save();
        }
        else if (TimeScoreNum < HighScoreTime[0])
        {
            HighScoreTime[2] = HighScoreTime[1];
            HighScoreTime[1] = HighScoreTime[0];
            HighScoreTime[0] = TimeScoreNum;

            PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
            PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
            PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);
            
            PlayerPrefs.Save();
        }
        else if (TimeScoreNum < HighScoreTime[1])
        {
            HighScoreTime[2] = HighScoreTime[1];
            HighScoreTime[1] = TimeScoreNum;

            PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
            PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
            PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);

            PlayerPrefs.Save();
        }
        else if (TimeScoreNum < HighScoreTime[2])
        {
            HighScoreTime[2] = TimeScoreNum;

            PlayerPrefs.SetFloat("HIGHSCORETIME0", HighScoreTime[0]);
            PlayerPrefs.SetFloat("HIGHSCORETIME1", HighScoreTime[1]);
            PlayerPrefs.SetFloat("HIGHSCORETIME2", HighScoreTime[2]);

            PlayerPrefs.Save();
        }


    }

    //ランキング表示
    private void TimeTextDisplay()
    {


        //今回のスコアの表示
        NowTimeScoreText = NowTimeScore.GetComponent<TextMeshProUGUI>();
        NowTimeScoreText.text = "NowTime:" + TimeScoreNum.ToString("n1");



        //1位表示
        FirstTimeText = FirstTimeScore.GetComponent<TextMeshProUGUI>();
        FirstTimeText.text = "1st:" + HighScoreTime[0].ToString("n1");

        //2位表示
        SecondTimeText = SecondTimeScore.GetComponent<TextMeshProUGUI>();
        SecondTimeText.text = "2nd:" + HighScoreTime[1].ToString("n1");

        //3位表示
        TherdTimeText = TherdTimeScore.GetComponent<TextMeshProUGUI>();
        TherdTimeText.text = "3rd:" + HighScoreTime[2].ToString("n1");


    }


    //リトライボタン処理
    public void Retry()
    {
        //フェードを使ってGameSceneに移動
        Initiate.Fade("GameScene", Color.black, 1.0f);
    }

    //タイトルボタン処理
    public void Title()
    {
        //フェードを使ってTitleに移動
        Initiate.Fade("Title", Color.black, 1.0f);

    }
}

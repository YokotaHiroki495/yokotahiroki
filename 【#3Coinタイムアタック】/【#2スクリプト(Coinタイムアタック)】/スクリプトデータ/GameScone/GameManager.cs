using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    //音関連
    public AudioSource audioSource;

    //スコア関連
    public static int Coin;
    public TextMeshProUGUI scoreText;

    //加速アイテム
    private static int GmanaHubItem;

    //クリアコイン数
    [SerializeField] private int ClearCoinNum;


    //時間
    [SerializeField] private float TimeCount;

    //クリア時間
    public static float ClearTimeCount;

    public TextMeshProUGUI TimeText;


    //プレイヤーから受け取ったコインをマイナスにするための変数たち
    //スクリプトを受け取る
    public Player playerSC;

    //Playerスクリプトの変数の受け取り用
    int PlayerSCMinusItemNum;


    
    void Start()
    {
        //コインの枚数を０枚にリセットする
        Coin = 0;

        //コインの枚数をテキストに表示
        scoreText.text = "Coin:" + Coin;


        //時間を小数点第一まで表示ToString n1により、小数点以下1桁まで表示する
        TimeText.text = "Time:" + TimeCount.ToString("n1");


        //プレイヤースクリプトについている変数MinusItemNumを代入
        PlayerSCMinusItemNum = playerSC.MinusItemNum;
       

    }

    void Update()
    {
        //時間管理
        TimeKeeper();


    }


    //時間管理(カウントダウン)
    public void TimeKeeper()
    {
        //unscaledDeltaTimeにより、TimeScaleが変化しても一定のスピードでカウントが進む
        TimeCount += Time.unscaledDeltaTime;
        //テキストの時間を随時、更新。
        TimeText.text = "Time:" + TimeCount.ToString("n1");
    }

    //スコア(コイン)の加算
    public void PlusScore()
    {
        //コインを加算
        Coin++;
        scoreText.text = "Coin:" + Coin;

        //プレイヤーのアイテム数参照
        GmanaHubItem = Player.HubCoin;

        //入手したコインがクリア枚数取ったら
        if (GmanaHubItem == ClearCoinNum)
        {
            
            //シーン移動にかかる時間は加算されないのでクリア枚数取った時点のタイムで保存される

            //シーン移動させる
            ClearGame();

        }

    }

    //スコア(コイン)減算
    public void MinusScore()
    {
      
        //プレイヤーのスクリプトからコインのマイナス分を取得してその分を減らす
        Coin -= PlayerSCMinusItemNum;
        scoreText.text = "Coin:" + Coin;

        //スコアが0いかになったら
        if (Coin < 0)
        {
            //スコアを0で止める
            Coin = 0;
            scoreText.text = "Coin:" + Coin;
        }

    }

    //ゲームをクリアしたら
    public void ClearGame()
    {

        //ゴールした際のタイムをClearTimeCountに代入
        ClearTimeCount = TimeCount;

        //画面をフェードしてシーン移行
        Initiate.Fade("Result", Color.black, 1.0f);

    }


}

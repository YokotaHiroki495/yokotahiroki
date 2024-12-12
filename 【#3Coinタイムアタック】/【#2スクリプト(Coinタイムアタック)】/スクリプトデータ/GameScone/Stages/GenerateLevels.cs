using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevels : MonoBehaviour
{
    //ステージ生成に関するScript

    //床オブジェクトを格納する配列
    public GameObject[] level;


    public GameObject[] coinArea;
    public GameObject[] obstaclesArea;
    
    //生成するオブジェクトのZ軸の位置
    [SerializeField]public int zPos;

    //床を生成する条件
    public bool creatingLevel = false;

    //生成する床の番号
    public int lvlNum;
    public int coinAreaNum;
    public int obstacleAreaNum;


    //生成する床の最小ナンバー
    [SerializeField] private int reateMinNunber;

    //生成する際の最小ランダムナンバー
    [SerializeField] private int rundomMinNumber;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject yuka;
    [SerializeField] private TextMesh counter;

    //Areaの生成数
    [SerializeField] int forward;
    //CoinAreaの生成位置
    [SerializeField] int zPositionPuls;
    //CoinAreaに対してのObstacleAreaの生成位置の調整
    [SerializeField] int zPositionSpace;


    //最低生成数
    [SerializeField]int currentZ;

    //どのタイミングで床を作るのか
    int createfield;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = reateMinNunber; i <= forward; i++)
        {
            CreatStage();
        }

    }

    // Update is called once per frame
    void Update()
    {




        //床のポジションを取得
        Vector3 yukaPos = yuka.transform.position;

        //プレイヤーの位置を取得
        Vector3 playerPos = player.transform.position;

        //距離を取得
        float dis = Vector3.Distance(yukaPos, playerPos);

        //counter.text = Convert.ToString(dis);

        //int createfield = (int)(transform.position.z / 10  /*createcooltime*/); //値をconstで
        createfield = (int)(playerPos.z);

      

    }

    public void CreatStage()
    {
        //床をランダムで選択
        //lvlNum = Random.Range(rundomMinNumber, level.Length); //ここで生成するステージに制限をつけて障害物ステージが連続で生成されないようにする

        //インスタンスで選択した床を生成
        //Instantiate(level[lvlNum], new Vector3(0, 0, zPos), Quaternion.identity);

        //コインエリアと障害物エリアをそれぞれランダムで配列に入れる
        coinAreaNum = Random.Range(rundomMinNumber, coinArea.Length);
        obstacleAreaNum = Random.Range(rundomMinNumber, obstaclesArea.Length);

        //それぞれを配列内でランダムに生成
        Instantiate(coinArea[coinAreaNum], new Vector3(0, 0, zPos), Quaternion.identity);
        Instantiate(obstaclesArea[obstacleAreaNum], new Vector3(0, 0, zPos + zPositionSpace), Quaternion.identity);

        //障害物エリアをコインエリアと交互に生成されるように位置を調整
        zPos += zPositionPuls;
        creatingLevel = false;
    }


}

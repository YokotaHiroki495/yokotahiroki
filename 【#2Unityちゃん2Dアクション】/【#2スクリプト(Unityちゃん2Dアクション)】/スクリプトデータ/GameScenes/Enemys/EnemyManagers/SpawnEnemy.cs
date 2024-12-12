using UnityEngine;



public class SpawnEnemy : MonoBehaviour
{


    //オブジェクトプール
    [SerializeField] EnemyObjectPool enemyManager;
    

    // 敵の生成位置関連
    // 敵のスポーン位置用の変数
    Vector2 randomrange;

    //敵の生成位置の設定
    
    //X座標の最小ポジと最大ポジ
    [SerializeField] float spawnXmin, spawnXmax;

    //Y座標の最小ポジと最大ポジ
    [SerializeField] float spawnYmin, spawnYmax;

    // 敵のスポーン関連
    // ステージが始まってから何秒後に敵を生成し始めるか
    [SerializeField] float start;

    //敵の生成のインターバル時間
    [SerializeField] float interval;


    void Start()
    {
      
        //定期的に敵をスポーンさせる
        InvokeRepeating("CreateEnemy", start, interval);


        //スコア(残り敵数)が0になったら
        if (GameManager.score == 0)
        {
            //敵のスポーンを停止させ
            CancelInvoke();
            
            //残った敵を消す
            enemyManager.DelEnemy(gameObject);

        }
    }



    void Update()
    {
        //敵の生成位置の
        randomrange = new Vector2(Random.Range(spawnXmin, spawnXmax), Random.Range(spawnYmin, spawnYmax));

    }


    //敵の生成　Pool(EnemyManager)にアクセス
    void CreateEnemy()
    {

        //敵をオブジェクトプールから取り出す
        GameObject enemy = enemyManager.GetEnemy();

        //敵を指定した範囲の中でランダムに生成
        enemy.transform.position = randomrange;

    }

    
}


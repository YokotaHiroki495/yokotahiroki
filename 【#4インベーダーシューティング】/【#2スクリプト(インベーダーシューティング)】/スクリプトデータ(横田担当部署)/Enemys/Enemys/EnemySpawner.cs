using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{

    //------敵関連-----

    //敵を変数に入れる
    //[SerializeField]に変更する
    [SerializeField] GameObject NewEnemyPrefab;
    [SerializeField] GameObject NewEnemy2Prefab;
    [SerializeField] GameObject NewEnemy3Prefab;

    //ステージが始まって敵を生成するまでの時間
    [SerializeField] float start = 0.0f;

    //敵生成のインターバル
    //二種類用意する
    [SerializeField] float interval = 0.08f;
    [SerializeField] float intarval2 = 0.8f;


    [Header("シーン移動関係")]
    //ボスを倒した後シーンを移動するまでの時間
    [SerializeField] float sceneMoveTime;
    
    //シーン移動時にフェードにかかる時間
    [SerializeField] float sceneFadeTime;
    
    
    GameObject[] BS;


    //名前を変える
    /// <summary>
    //TimeScript  Timer  
    /// </summary>
    /// 
    //制限時間用の変数
    TimeScript timeLimit;

    void Start()
    {


        
        timeLimit = FindObjectOfType<TimeScript>();

       


        // 現在のシーンを保存
        switch (SceneManager.GetActiveScene().name)
        {

            case "EnemyScene":
            case "BossEnemyScene":
               

                //残り時間が1秒でもあれば敵を生成
                if (timeLimit.time >= 1)
                {
                    Debug.Log("敵生成");

                    StartCoroutine(Spawn(NewEnemyPrefab, start, interval));
                    StartCoroutine(Spawn(NewEnemy2Prefab, start, intarval2));
                    StartCoroutine(Spawn(NewEnemy3Prefab, start, intarval2));

                }

                break;
        }


        Debug.Log(SceneManager.GetActiveScene().name);




    }

    //例by小島先生　コルーチンを使う場合

    //引数(生成物、　時間、　インターバル)
    IEnumerator Spawn(GameObject enemy, float start, float interval)
    {
        //生成するまでの時間
        yield return new WaitForSeconds(start);

        //無限ループ
        while (true)
        {
            //
            if (Time.timeScale > 0)
            {
                //敵を生成する
                Instantiate(enemy, new Vector3(-10f + 20 * Random.value, 8, 2f), Quaternion.Euler(-90, 0, 0));
            }


            //インターバルの時間待つ
            //if文の中に入れると無限ループになる
            yield return new WaitForSeconds(interval);


        }



        yield return null; ;
    }

    public void Update()
    {


        //残り時間が0秒になれば敵の生成を止める
        if (timeLimit.time == 0)
        {
          
            StopAllCoroutines();


        }
    

        //Updateの中でシーンを読み込まない
        //
        if ((SceneManager.GetActiveScene().name == "BossEnemyScene"))
        {
            
            //アルゴリズムとロジック
            BS = GameObject.FindGameObjectsWithTag("Boss");

            //Debug.Log("ボスの数" + BS.Length);
            if (BS.Length == 0)
            {

                //コルーチンを全て止める
                StopAllCoroutines();

                Invoke("BossDestroySceneMove", sceneMoveTime);

            }
        }

       
    }



    void BossDestroySceneMove()
    {
        //プラグイン　画面をフェードイン　フェードアウトさせる
        //(移動先のシーン名、フェード時の画面のカラー、フェードする時間)
        Initiate.Fade("GameClearScene", Color.black, sceneFadeTime);
    }







}
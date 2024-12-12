using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BossEnemy : MonoBehaviour
{

    //[SerializeField]public のような書き方はしない
    //どっちかだけ
    //publicを消す


    [SerializeField]int BossHp = 10000;
    [SerializeField]float bossMoveSpeed = 1f;

    //BossController BC;

    public GameObject BossexplosionPrefab;
    public GameObject ExplosionPrefab;



    void Start()
    {

        //BC = GetComponent<BossController>();
    }

    void Update()
    {
        // プレイヤー側に向かって移動し続ける
        transform.Translate(0, 0, -bossMoveSpeed * Time.deltaTime);

        // コライダーとリジッドノディで当たり判定をとるなら
        // movepositionでとった方がよい　←　優先度は低くい、時間があったら


        // コメントを書く場合は//の後にスペースを入れる
        // //の後にスペースを入れないのは一時的にコメントアウトしているプログラムの時
        
    }



    // 下記処理を　メソッド化して呼び出した方が良いと思う
    //void OnTriggerEnter(Collider other) => Damege(other.gameObject.tag, "bulletPrefab");
    //void OnTriggerStay(Collider other) => Damege(other.gameObject.tag, "Beam");
    ////引数を渡してDamegeメソッドを実行する

    //// 例
    //void Damege(string targetTag, string hitTag)
    //{
    //    if (targetTag == hitTag)
    //    {
    //        BossHp--;
    //        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

    //        if (BossHp <= 0)
    //        {
    //            GameObject obj = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity) as GameObject;
    //            obj.transform.localScale = new Vector3(50, 50, 50);
    //            Destroy(gameObject);


    //        }

    //    }

    //}
    // -----------------------------------------------------------------------------


    void OnTriggerEnter(Collider other) => Damege(other, "bulletPrefab");
    void OnTriggerStay(Collider other) => Damege(other, "Beam");


    void Damege(Collider other, string hitTag)
    {
        if (other.gameObject.tag == hitTag)
        {
            BossHp--;
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

            if (BossHp <= 0)
            {
                // Boss撃破用爆発エフェクトを読み込んで
                GameObject obj = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity) as GameObject;

                //指定した位置でエフェクトを表示
                obj.transform.localScale = new Vector3(50, 50, 50);

                Invoke("MoveClearScene", 2);

                //自分を破壊
                Destroy(gameObject);

            }

        }

    }


    void MoveClearScene()
    {
        Debug.Log("クリアシーン移動");
        //プラグイン　画面をフェードイン　フェードアウトさせる
        //(移動先のシーン名、フェード時の画面のカラー、フェードする時間)
        Initiate.Fade("GameClear", Color.black, 1.0f);

    }













    //void OnTriggerEnter(Collider col)
    //{
    //    // オントリガーエンター
    //    // コライダーとリジッドボディでとっている



    //    //プレイヤーの攻撃が当たったら
    //    if (col.gameObject.tag == "bulletPrefab")
    //    {
    //        // 体力を1減らす
    //        // マジックナンバーはアウト
    //        // この書き方なら
    //        BossHp -= 1;

    //        // -1ならこれで良い
    //        //BossHp--;

    //        //爆発エフェクトを再生
    //        //オブジェクトプールのほうが良かったが、最初の作品なのでギリ良し
    //        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);

    //        //体力が0になったら
    //        if (BossHp <= 0)
    //        {
    //            // 爆発エフェクトを読み込んで
    //            //エフェクトの表示座標をここに書いとく
    //            //GameObject obj = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity) as GameObject;

    //            //// 指定した位置でエフェクトを表示
    //            ////  
    //            //obj.transform.localScale = new Vector3(50, 50, 50);


    //            // 上記をまとめて書くとこうなる
    //            Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity).transform.localScale = new Vector3(50, 50, 50);

    //            //自分を破壊
    //            Destroy(gameObject);

    //        }
    //    }


    //}




    //// otherを colにするとか
    //void OnTriggerStay(Collider other)
    //{
    //    //レーザー攻撃に当たったら
    //    if (other.gameObject.tag == "Beam")
    //    {

    //        // 中身をメソッド化



    //        BossHp -= 1;

    //        // -1ならこれで良い
    //        //BossHp--;

    //        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    //        //Debug.Log(BossHp);

    //        //体力が0になったら
    //        if (BossHp <= 0)
    //        {
    //            //Boss撃破用爆発エフェクトを読み込んで
    //            GameObject obj = Instantiate(BossexplosionPrefab, transform.position, Quaternion.identity) as GameObject;

    //            //指定した位置でエフェクトを表示
    //            obj.transform.localScale = new Vector3(50, 50, 50);

    //            //自分を破壊
    //            Destroy(gameObject);

    //        }
    //    }
    //}





}

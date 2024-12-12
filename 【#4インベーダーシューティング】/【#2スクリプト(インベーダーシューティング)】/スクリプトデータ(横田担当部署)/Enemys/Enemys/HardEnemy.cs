using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy : MonoBehaviour
{
    //敵の体力
    [SerializeField]int EnemyHp = 3;


    //-------------敵の移動関連-------------
    //下への移動スピード
    [SerializeField] float Speed = 3;
    
    //動きのランダム生成
    int moverand;

    //左右の移動スピード
    public float XSpeed = 5f;

    //左右の移動判定
    bool XPlusMove = true;//X方向に＋移動か？
    //移動一のランダム設定
    int Posrand;


    //爆発エフェクト
    public GameObject explosionPrefab;

    //ドロップアイテム関連
    public GameObject CureBombPrefab;

    //お試し
    public GameObject ItemPrefab;
    int itemrand;

    //音関連
    public AudioClip Sound1;
    AudioSource audioSource;


    void Start()
    {
        //動きをランダム生成
        moverand = Random.Range(1, 3);

        //ランダムな生成位置
        Posrand = Random.Range(1, 4);

        //音関係
        audioSource = GetComponent<AudioSource>();

        
    }


    void Update()
    {
        //transform取得
        Transform Ene2Tra = this.transform;

        //ワールド座標を基準に座標を取得
        Vector3 WorldPos = Ene2Tra.position;

        //手前に向かって移動
        if (moverand == 1)
        {
            //位置パターン1のとき
            if (Posrand == 1)
            {
                //もし、生成位置がX10よりも大きかったら
                if (WorldPos.x > 10)
                {
                    //マイナス(左)方向に移動する
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                //もし、生成位置がX-10寄りも小さかったら
                else if (WorldPos.x < -10)
                {
                    //＋方向(右)に移動する
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }
            //位置パターン2のとき
            if (Posrand == 2)
            {
                if (WorldPos.x > 5)
                {
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                else if (WorldPos.x < -5)
                {
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }
            //位置パターン3のとき
            if (Posrand == 3)
            {
                if (WorldPos.x > 1)
                {
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                else if (WorldPos.x < -1)
                {
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }

            //下に向かって移動
            WorldPos.y -= Speed * Time.deltaTime;

        }
        //ジグザグ移動
        if (moverand == 2)
        {
            //下に向かって移動
            WorldPos.y -= Speed * Time.deltaTime;

            //方向判定がtrue(本当)なら
            if (XPlusMove)
            {
                //そのまま＋方向に移動する
                WorldPos.x += XSpeed * Time.deltaTime;

                //もし一定以上に移動したら
                if (WorldPos.x >= 10)
                {
                    //＋方向に進んでいる判定をfalse(嘘)にする
                    XPlusMove = false;
                }
            }
            else//もし方向判定がfalseになっていたら
            {
                //マイナス方向に移動する
                WorldPos.x -= XSpeed * Time.deltaTime;

                //マイナスに進んで判定外に出たら
                if (WorldPos.x <= -10)
                {
                    //方向判定をtrue(本当)にする
                    XPlusMove = true;
                }
            }
        }
        //ワールド座標を基準として座標を変更
        Ene2Tra.position = WorldPos;

    }



    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "bulletPrefab")
        {
            //自分の体力を1減らして
            EnemyHp -= 1;

            //Debug.Log(EnemyHp);
            //もし体力が0になったら
            if (EnemyHp <= 0)
            {

              
                //このオブジェクトを破壊する
                Destroy(gameObject);

                //爆発エフェクトを表示して、エフェクトを1秒後に削除する
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);


                //ボム回復アイテムのランダム生成
                itemrand = Random.Range(1, 31);
                if (itemrand == 1)
                {
                    Instantiate(ItemPrefab, transform.position, Quaternion.identity);

                }
            }
        }
        if (col.gameObject.tag == "Beam")
        {
            //このオブジェクトを破壊する
            Destroy(gameObject);

            //爆発エフェクトを表示して、エフェクトを1秒後に削除する
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);



            //ボム回復アイテムのランダム生成
            itemrand = Random.Range(1, 1001);
            if (itemrand == 1)
            {
                Instantiate(ItemPrefab, transform.position, Quaternion.identity);

            }

        }



    } 
}



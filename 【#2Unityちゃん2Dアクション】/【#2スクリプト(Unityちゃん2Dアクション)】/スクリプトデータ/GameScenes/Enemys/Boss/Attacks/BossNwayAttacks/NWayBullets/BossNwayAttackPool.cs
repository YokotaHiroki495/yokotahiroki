using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class BossNwayAttackPool : MonoBehaviour
{
    //BossN-Way攻撃用ObjectPool

    //弾のプレファブ
    [SerializeField] GameObject boss3wayAttackPregfab;

    //オブジェクトプール
    ObjectPool<GameObject> pool;

    void Start()
    {
        pool = new ObjectPool<GameObject>
            (
                CreateBoss3WayPoolObject,  //第1引数　プール内にオブジェクトがない場合、生成する
                OnTakeFromPool,         //第2引数　プール内にプールされていて非表示になっているオブジェクトを表示状態にする
                OnReturnedToPool,       //第3引数　オブジェクトをプール内に戻す
                DestroyBoss3WayPoolObject, //第4引数　オブジェクトをプールに戻せなかったとき(最大数を超えたとき)オブジェクトを削除する
                true,                   //第5引数　すでプールにあるオブジェクトを追加した場合に例外とするか
                                        //trueにしておくとオブジェクトがプール内に戻るときに自動で実行される
                5,          //最初に作られるオブジェクト数
                10           //最大数　この値以上生成されたら、破壊する
            );
    }

    //ObjectPool コンストラクタ1つ目の引数の関数
    //プールに空きが無い時に新たに生成する処理
    //objectPool.Get()の時呼ばれる
    GameObject CreateBoss3WayPoolObject()
    {
        GameObject createdBoss3way = Instantiate(boss3wayAttackPregfab);

        return createdBoss3way;

    }


    //ObjectPool コンストラクタ2つ目の引数の関数
    //プールに空きがあった時の処理
    //objectPool.Get()の時呼ばれる
    void OnTakeFromPool(GameObject target)
    {
        target.SetActive(true);

    }


    //ObjectPool コンストラクタ3つ目の引数の関数
    //プールに返却する時の処理
    void OnReturnedToPool(GameObject target)
    {
        target.SetActive(false);
    }


    //ObjectPool コンストラクタ4つ目の引数の関数
    //指定した数より多くなったら自動で削除する
    void DestroyBoss3WayPoolObject(GameObject target)
    {
        Destroy(target);
    }


    //エネミーを呼び出す処理
    public GameObject Get3Way()
    {
        GameObject created3Way = pool.Get();
        return created3Way;
    }

    //呼び出してからエネミーを削除する
    public void Del3Way(GameObject created3Way)
    {
        pool.Release(created3Way);

    }
}


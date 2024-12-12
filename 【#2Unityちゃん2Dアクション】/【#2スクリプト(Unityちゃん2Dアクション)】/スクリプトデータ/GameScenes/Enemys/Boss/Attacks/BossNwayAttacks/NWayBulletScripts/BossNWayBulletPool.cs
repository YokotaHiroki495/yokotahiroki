using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class BossNWayBulletPool : MonoBehaviour
{
    //BossN-Way攻撃の弾自体のObjectPool

    //弾のプレファブ
    [SerializeField] GameObject boss3wayBulletPregfab;

    //オブジェクトプール
    ObjectPool<GameObject> boss3WayBulletPool;

    void Start()
    {

       
        boss3WayBulletPool = new ObjectPool<GameObject>
            (
                Create3WayBulletPoolObject,  //第1引数　プール内にオブジェクトがない場合、生成する
                OnTakeFromPool,         //第2引数　プール内にプールされていて非表示になっているオブジェクトを表示状態にする
                OnReturnedToPool,       //第3引数　オブジェクトをプール内に戻す
                Destroy3WayBulletPoolObject, //第4引数　オブジェクトをプールに戻せなかったとき(最大数を超えたとき)オブジェクトを削除する
                true,                   //第5引数　すでプールにあるオブジェクトを追加した場合に例外とするか
                                        //trueにしておくとオブジェクトがプール内に戻るときに自動で実行される
                5,          //最初に作られるオブジェクト数
                10           //最大数　この値以上生成されたら、破壊する
            );
    }

    //ObjectPool コンストラクタ1つ目の引数の関数
    //プールに空きが無い時に新たに生成する処理
    //objectPool.Get()の時呼ばれる
    GameObject Create3WayBulletPoolObject()
    {
        Debug.Log("Create3WayBulletPoolObject");
        GameObject created3wayBullet = Instantiate(boss3wayBulletPregfab);

        return created3wayBullet;

    }


    //ObjectPool コンストラクタ2つ目の引数の関数
    //プールに空きがあった時の処理
    //objectPool.Get()の時呼ばれる
    void OnTakeFromPool(GameObject target)
    {
        Debug.Log("OnTakeFromPool");
        target.SetActive(true);

    }


    //ObjectPool コンストラクタ3つ目の引数の関数
    //プールに返却する時の処理
    void OnReturnedToPool(GameObject target)
    {
        Debug.Log("OnReturnedToPool");
        target.SetActive(false);
    }


    //ObjectPool コンストラクタ4つ目の引数の関数
    //指定した数より多くなったら自動で削除する
    void Destroy3WayBulletPoolObject(GameObject target)
    {
        Debug.Log("Destroy3WayBulletPoolObject");
        Destroy(target);
    }


    //呼び出す処理
    public GameObject Get3WayBullet()
    {
        Debug.Log("Get3WayBullet");

        GameObject created3WayBullet = boss3WayBulletPool.Get();          //オブジェクトがインスタンスされていなエラー
        return created3WayBullet;
    }

    //呼び出してからエネミーを削除する
    public void Del3WayBullet(GameObject created3WayBullet)
    {
        Debug.Log("Del3WayBullet");
        boss3WayBulletPool.Release(created3WayBullet);

    }

}


using UnityEngine;
using UnityEngine.Pool;

public abstract class GameObjectPool<T> : SingltonGameObjectPool<GameObjectPool<T>>
{
    //GameObject用のObjectPool
    [SerializeField] GameObject _taget;

    [SerializeField] int _defaultCapacity = 5;

    [SerializeField] int _maxSize = 10;

    IObjectPool<GameObject> _pool;

    public IObjectPool<GameObject> pool
    {
        get
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<GameObject>
                    (
                        GameObjectCreatePoolObject,         // 第1引数　プール内にオブジェクトがない場合、生成する
                        OnTakeFromPool,                     // 第2引数　プール内にプールされていて非表示になっているオブジェクトを表示状態にする
                        OnReturnedToPool,                   // 第3引数　オブジェクトをプール内に戻す
                        GameObjectDestroyPoolObject,        // 第4引数　オブジェクトをプールに戻せなかったとき(最大数を超えたとき)オブジェクトを削除する
                        true,                               // 第5引数　すでプールにあるオブジェクトを追加した場合に例外とするか
                                                            // trueにしておくとオブジェクトがプール内に戻るときに自動で実行される
                        _defaultCapacity,                   // 最初に作られるオブジェクト数
                        _maxSize                            // 最大数　この値以上生成されたら、破壊する
                    );
            }
            return _pool;
        }
    }

    // ObjectPool コンストラクタ1つ目の引数の関数
    // プールに空きが無い時に新たに生成する処理
    // ObjectPool.Get()の時呼ばれる
    GameObject GameObjectCreatePoolObject()
    {
        return Instantiate(_taget);
    }

    // ObjectPool コンストラクタ2つ目の引数の関数
    // プールに空きがあった時の処理
    // objectPool.Get()の時呼ばれる
    void OnTakeFromPool(GameObject target)
    {
        target.SetActive(true);
    }

    // ObjectPool コンストラクタ3つ目の引数の関数
    // プールに返却する時の処理
    void OnReturnedToPool(GameObject target)
    {
        target.SetActive(false);
    }

    // ObjectPool コンストラクタ4つ目の引数の関数
    // 指定した数より多くなったら自動で削除する
    void GameObjectDestroyPoolObject(GameObject target)
    {
        Destroy(target);
    }

    // 敵を呼び出す処理
    public static GameObject Get() => instance.pool.Get();

    // 呼び出した敵を削除する
    public static void Release(GameObject target) => instance.pool.Release(target);
}

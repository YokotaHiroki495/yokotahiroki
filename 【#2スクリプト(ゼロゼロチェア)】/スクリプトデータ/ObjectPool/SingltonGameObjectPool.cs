using UnityEngine;

public class SingltonGameObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    // シングルトンパターン
    // ジェネリック型 T ジェネリックでMonoBehaviourを継承する任意の型 T を使用できる

    // Tのインスタンスを保持 
    static T _instance;

    // シングルトンにアクセスするため
    public static T instance
    {
        get
        {
            // nullだったら　シーン内のTのオブジェクトを探す
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();

                if (_instance == null)
                {
                    Debug.Log($"{typeof(T)}がないよ");
                }
            }

            return _instance;
        }

    }
}

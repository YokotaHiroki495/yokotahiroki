using UnityEngine;

public abstract class PlayerSingleton<T>: MonoBehaviour where T : MonoBehaviour
{
    static T _instance;

    public static T instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<T>();
                if (_instance == null)
                {
                    Debug.Log($"{typeof(T)}が見つかりません");
                }

            }
            return _instance;
        }
    }
}

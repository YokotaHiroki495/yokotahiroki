using UnityEngine;

public class SingltonEnemyPool<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _singltonPool;

    public static T singltonPool
    { 
    
        get
        {
            if(_singltonPool == null)
            {
                _singltonPool = FindFirstObjectByType<T>();

                if (_singltonPool == null)
                {
                    Debug.Log($"{typeof(T)}がないよ");
                }
            }

            return _singltonPool;
        }
            
    }



}

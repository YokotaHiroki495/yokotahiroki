using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 敵の生成の時間管理
    [SerializeField] float _enemyCreateStartTime;    // 敵生成開始時間
    [SerializeField] float _enemyCreateIntarvalTime; // 敵生成のクールタイム

    // 敵の生成範囲の管理
    [SerializeField] float _spawnMinX, _spawnMaxX;    // X軸での生成範囲
    [SerializeField] float _spawnMinZ, _spawnMaxZ;    // Z軸での生成範囲
    [SerializeField] float _detectionRadius;          //他のオブジェクトの感知半径   
    [SerializeField]　LayerMask _enemyLayer;          //検出対象
    
    void Start()
    {
        // 一定時間ごとに敵を生成するメソッドを実行する
         InvokeRepeating("CreateEnemy", _enemyCreateStartTime, _enemyCreateIntarvalTime);
    }


    void CreateEnemy()
    {
        while (true)
        {
            // 敵の生成範囲を基にランダムにポジションを決定
            Vector3 randomPosition = new Vector3(Random.Range(_spawnMinX, _spawnMaxX), 0, Random.Range(_spawnMinZ, _spawnMaxZ));

            // Playerを中心に範囲を指定してLayerMaskで敵の生成位置を管理する
            if (Physics.CheckSphere(randomPosition, _detectionRadius, _enemyLayer))
            {
                // 一定距離内に敵がいたら生成しない
                break;
            }
            else
            {
                // 一定距離に敵がいなかったら敵を生成
                GameObject enemy = EnemyPool.instance.pool.Get();
                enemy.transform.position = randomPosition;
                break;
            }
        }
        
    }
}

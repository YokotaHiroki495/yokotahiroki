using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    // プレイヤーがエリアに入っているかどうかを示すクラス
    public bool _enemyAttackAreaInPlayer{ get; private set; }  // プロパティに設定

    // 自身のエリアに入ってきたら
    void OnTriggerEnter(Collider other)
    {
        // プレイヤーの当たり判定だったら
        if (other.gameObject.CompareTag("HitJudgement"))
        {
            // bool型をtrueに
            _enemyAttackAreaInPlayer = true;
        }
    }

    // 自身のエリアから出たら
    void OnTriggerExit(Collider other)
    {
        // プレイヤーの当たり判定だったら
        if (other.gameObject.CompareTag("HitJudgement"))
        {
            // bool型をfalseに
            _enemyAttackAreaInPlayer = false;
        }
    }
}

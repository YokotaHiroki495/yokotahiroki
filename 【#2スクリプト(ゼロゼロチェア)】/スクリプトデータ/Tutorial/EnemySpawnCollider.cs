using UnityEngine;

public class EnemySpawnCollider : MonoBehaviour
{
    // 実行するイベント
    [SerializeField]UnityEngine.Events.UnityEvent _triggerEvent;

    void OnTriggerEnter(Collider other)
    {
        // 当たった相手がプレイヤーだったら
        if (other.gameObject.CompareTag("Player"))
        {
            // 当たった相手がプレイヤーだったら
            GetComponent<Collider>().enabled = false;

            // _triggerEventがnullでは無かったら_triggerEventを実行させる
            _triggerEvent?.Invoke();
        }
    }
}
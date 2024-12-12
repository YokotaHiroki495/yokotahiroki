using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // 当たり判定に接触したら
        // 接触した相手のタグがPlayerなら
        if (other.CompareTag("Player"))
        {
            // プレイヤーにかかっている慣性を0にする
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;

            // GameManagerが持っているcheckpointPositionにPlayerを移動させる
            other.transform.position = GameManager.instance._checkpointPosition;

        }
    }
}

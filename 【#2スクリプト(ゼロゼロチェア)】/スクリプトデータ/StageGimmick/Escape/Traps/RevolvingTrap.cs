using UnityEngine;

public class RevolvingTrap : MonoBehaviour
{
    [Header("回転速度")]
    [SerializeField] float _rotateY;

    [Header("吹っ飛ばす力")]
    [SerializeField] float _impulse;

    // 回転が時計回りかどうか
    [SerializeField] bool _clockWise = true;

    void Update()
    {
        // trueだったら
        if (_clockWise)
        {
            // 時計回り
            transform.Rotate(0, _rotateY * 10 * Time.deltaTime, 0);
        }
        // falseだったら
        else
        {
            // 反時計回り
            transform.Rotate(0, -_rotateY * 10 * Time.deltaTime, 0);
        }
    }




    //void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        // プレイヤーと自身の位置を計算してベクトルを取得
    //        Vector3 direction = collision.transform.position - transform.position;

    //        // 水平方向(xとz軸)のみに力を加える
    //        direction.y = 0;

    //        // 自身に触れたオブジェクトのRigidbodyを取得して相手を飛ばす
    //        Rigidbody rb=  collision.GetComponent<Rigidbody>().AddForce(direction.normalized * _impulse, ForceMode.Impulse);
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("名前：" + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            // プレイヤーと自身の位置を計算してベクトルを取得
            Vector3 direction = other.transform.position - transform.position;

            // 水平方向(xとz軸)のみに力を加える
            direction.y = 0;

            // 自身に触れたオブジェクトのRigidbodyを取得して相手を飛ばす
            other.GetComponent<Rigidbody>().AddForce(direction.normalized * _impulse, ForceMode.Impulse);
        }
    }
}

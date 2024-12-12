using UnityEngine;

public class MoveBlock : MonoBehaviour
{

    [SerializeField] float _speed;      // 往復の速さ
    [SerializeField] float _swingWidth; // 往復の幅
    float _blockPosZ;

   
    void Start()
    {
        _blockPosZ = transform.position.z;
    }

    void Update()
    {
        // Sin関数で往復移動を実施
        // speed変数をかけて速度を調整
        // 現状：1秒で1周(1往復)
        Vector3 pos = transform.position;
        pos.z = _blockPosZ + Mathf.Sin(Mathf.PI * Time.time * _speed) * _swingWidth;
        transform.position = pos;
    }
}

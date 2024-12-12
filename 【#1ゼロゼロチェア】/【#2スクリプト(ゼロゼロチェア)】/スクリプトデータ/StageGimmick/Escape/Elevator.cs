using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float _moveSpeed;      // ドアの閉まる速さ
    [SerializeField] AudioClip _elevatorSE; // ドアの閉まるSE

    AudioSource _audioSource;               // AudioSource


    [System.Serializable]

    public class ChildTarget 
    {
        // ドアの座標
        public Transform _childTrandform;
        // 初期のドアの座標
        public Vector3 _startPosition;
        // 最後のどらの座標
        public Vector3 _targetPosition;
    }

    public List<ChildTarget> _childTargets; //子オブジェクトと目標位置のリスト

    void Start()
    {
        // AudioSource　を取得
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        // プレイヤーがエレベーター内に入ったら
        if (other.CompareTag("Player"))
        {
            // ドアが閉まるSEを再生して
            _audioSource.PlayOneShot(_elevatorSE);
            // コルーチンを実行
            StartCoroutine(MoveDoor());

            // GameManagerのMoveGameClear()を実行
            GameManager.instance.MoveGameClear();
        }
       
    }

    IEnumerator MoveDoor()
    {
        // tを1に初期化
        float t = 1;

        // _childTargetsをchiTargetに代入
        foreach (ChildTarget chiTarget in _childTargets)
        {
            // _startPositionに今のPositionを代入
            chiTarget._startPosition = chiTarget._childTrandform.position;
        }


        // t が0.001より大きい間ループを続ける
        while (t > 0.001f)
        {
            // tを毎フレーム減少させる
            t *= _moveSpeed * Time.deltaTime;

            foreach (ChildTarget childTarget in _childTargets)
            {
                // それぞれの座標の更新
                childTarget._childTrandform.position = Vector3.Lerp(
                    childTarget._targetPosition,        // ゴール
                    childTarget._startPosition,         // スタート
                    t);                                 // 自分がどこにいるのか
            }
            yield return null;
        }
    }
}

using System.Collections;
using TMPro;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // 表示させるテキスト
    [SerializeField] TextMeshProUGUI _checkText;    
    [SerializeField]int _blinkCount;        // 何回点滅させるか
    // テキストを表示させる回数
    [SerializeField] float _blinkInterval;   // 点滅する間隔

    void Start()
    {
        // テキストを非表示に
        _checkText.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // 当たり判定に接触したら
        // 接触した相手のタグがPlayerなら
        if (other.CompareTag("Player"))
        {
            // ここで当たり判定をいったん消す
            Collider myCollier = GetComponent<BoxCollider>();
            myCollier.enabled = false;

            // テキストを点滅させるコルーチンを実行
            StartCoroutine(BlinkText());

            // checkpointPositionを接触したCheckPointの位置に更新する
            GameManager.instance._checkpointPosition = transform.position;   
        }       
    }

    IEnumerator BlinkText()
    {      
        for (int i = 0; i < _blinkCount * 2; i++)
        {
            // ここでインターバル毎にtrueとfalseを入れ替える(点滅させる)
            _checkText.enabled = !_checkText.enabled;
            yield return new WaitForSeconds(_blinkInterval);
           
            // 時間をインターバル分、加算
            //i +=  _blinkCount;
        }

        // 比較用に時間が点滅時間よりも長くなったら
        // falseにする
        _checkText.enabled = false;

        // チェックポイント自体を消す
        Destroy(gameObject);
    }
}
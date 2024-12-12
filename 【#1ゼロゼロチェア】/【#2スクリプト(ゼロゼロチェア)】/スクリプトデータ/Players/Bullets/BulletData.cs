using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BulletData", menuName = "ScriptableObject/BulletDataScriptableObject")]
public class BulletData : ScriptableObject, ISerializationCallbackReceiver
{
    // ISerializationCallbackReceiver シリアライズやデシリアライズ時にコールバックを受信するようにする

    [SerializeField] int _bulletDamage;     // 攻撃力のマザーデータ
    [NonSerialized] public int _damage;     // 他のスクリプトから参照する用

    [SerializeField] int _returnDurationTime;   // 存在している時間 
    [NonSerialized] public float _returnTime;   // 他のスクリプトから参照する用

    // オブジェクトをシリアライズする前にコールバックを受信するために実装
    public void OnBeforeSerialize() { }

    // Awakeが呼ばれる前に呼ばれる
    public void OnAfterDeserialize()
    {
        //　マザーデータを受け渡せるように設定
        _damage = _bulletDamage;
        _returnTime = _returnDurationTime;
       
    }


}

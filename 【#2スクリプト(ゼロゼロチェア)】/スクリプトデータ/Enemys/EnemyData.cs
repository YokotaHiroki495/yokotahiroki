using System;
using UnityEngine;

[CreateAssetMenu(fileName ="EnemyData",menuName = "ScriptableObject/EnemyDataScriptableObject")]
public class EnemyData : ScriptableObject, ISerializationCallbackReceiver
{
    // ISerializationCallbackReceiver シリアライズやデシリアライズ時にコールバックを受信するようにする
    [SerializeField] int _motherHP;     // 体力のマザーデータ
    [NonSerialized] public int _hp;     // 他のスクリプトから参照する用

    [SerializeField] int _motherDamage; // 攻撃力のマザーデータ
    [NonSerialized] public int _damage; // 他のスクリプトから参照する用

    public void OnBeforeSerialize() { }

    // Awakeが呼ばれる前に呼ばれる
    public void OnAfterDeserialize()
    {
        //　マザーデータを受け渡せるように設定
        _hp = _motherHP;
        _damage = _motherDamage;
    }
}

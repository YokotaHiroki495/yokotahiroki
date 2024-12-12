using UnityEngine;

public class SpawnSwordEnemyAttack : MonoBehaviour
{
    // 敵の攻撃を生成するスクリプト

    // 敵の攻撃力
    [SerializeField, Header("敵の攻撃力")] int enemyAttackDamage;

    //攻撃判定の残留時間
    [SerializeField] float enemyAttackHitBoxTimeLimit;

    // プレイヤーのスクリプト
    PlayerScript playerObject;

    // プレイヤーの体力用変数
    PlayerHPBarScript playerHPBarOnject;

    void Start()
    {
        // 生成された後、すぐ自身を消す
        Invoke("HitBoxDestroy", enemyAttackHitBoxTimeLimit);

        //プレイヤーの体力バーとプレイヤーのスクリプトを取得
        playerHPBarOnject = GetComponent<PlayerHPBarScript>();
        playerObject = GetComponent<PlayerScript>();
    }

    void HitBoxDestroy()
    {
        // 自身(敵の攻撃)を削除
        Destroy(gameObject);
    }
    
    // プレイヤーの攻撃に当たったら
    void OnTriggerEnter2D(Collider2D collision)
    {
        // タグを取得する
        switch (collision.gameObject.tag)
        {
            // タグが下記の二つだったら
            case "Attack":
            case "Stage":

                // HitBoxDestroy()を呼び出す
                HitBoxDestroy();
                break;

            // タグが下記のだったら
            case "Player":

                //HitBoxDestroy()を呼び出す
                HitBoxDestroy(); 

                // プレイヤーの当たり判定を消して
                if (playerObject.isHit == false)
                {
                    // プレイヤーの体力を減らす
                    playerObject.playeryHp -= enemyAttackDamage;

                    // プレイヤーのダメージエフェクトを表示
                    playerObject.DamageEffect();

                    // プレイヤーの体力バーを減らす
                    playerHPBarOnject.PlayerHPDamege();

                }
                break;
        }
    }
}

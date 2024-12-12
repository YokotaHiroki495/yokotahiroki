using UnityEngine;

public class BossWeakPointScript: MonoBehaviour
{

    
    BossController bossScript;
    BossHPBarScript bossHpBarScript;
    
    
    
    //エフェクト関係
    GameObject effect;
    [SerializeField] GameObject effectPrefab;

    //---サウンド関係
    AudioSource audioSource;
    [SerializeField] AudioClip destroySE;
    [SerializeField] AudioClip damageSE;


    void Start()
    {
        bossHpBarScript = GameObject.Find("BOSSHPBar").GetComponent<BossHPBarScript>();
    }


    void Update()
    {
        bossScript = GameObject.Find("BossEnemy").GetComponent<BossController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Attack")) //こっちのほうが処理が速い
        {


            if (collision.gameObject.CompareTag("Attack")) //こっちのほうが処理が速い
            {
                bossScript.enemyHp -= 1;
                bossHpBarScript.BossHPDamege();



                effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                //audioSource.PlayOneShot(damageSE);


                //playerposを参照して、playerの方向を判定する。
                //左方向にいる
                if (bossScript.playerPos.x < 0)
                {
                    bossScript.LeftFlomAttack();

                }
                //右方向にいる
                if (bossScript.playerPos.x > 0)
                {
                    bossScript.RightFlomAttack();
                }

                Destroy(effect, 0.5f);

            }
        }
    }
}

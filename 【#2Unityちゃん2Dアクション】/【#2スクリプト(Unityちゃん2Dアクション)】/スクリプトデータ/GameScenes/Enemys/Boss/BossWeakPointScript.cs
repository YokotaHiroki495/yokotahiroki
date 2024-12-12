using UnityEngine;

public class BossWeakPointScript: MonoBehaviour
{

    
    BossController bossScript;
    BossHPBarScript bossHpBarScript;
    
    
    
    //�G�t�F�N�g�֌W
    GameObject effect;
    [SerializeField] GameObject effectPrefab;

    //---�T�E���h�֌W
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
        if (collision.gameObject.CompareTag("Attack")) //�������̂ق�������������
        {


            if (collision.gameObject.CompareTag("Attack")) //�������̂ق�������������
            {
                bossScript.enemyHp -= 1;
                bossHpBarScript.BossHPDamege();



                effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
                //audioSource.PlayOneShot(damageSE);


                //playerpos���Q�Ƃ��āAplayer�̕����𔻒肷��B
                //�������ɂ���
                if (bossScript.playerPos.x < 0)
                {
                    bossScript.LeftFlomAttack();

                }
                //�E�����ɂ���
                if (bossScript.playerPos.x > 0)
                {
                    bossScript.RightFlomAttack();
                }

                Destroy(effect, 0.5f);

            }
        }
    }
}

using UnityEngine;

public class SpawnSwordEnemyAttack : MonoBehaviour
{
    // �G�̍U���𐶐�����X�N���v�g

    // �G�̍U����
    [SerializeField, Header("�G�̍U����")] int enemyAttackDamage;

    //�U������̎c������
    [SerializeField] float enemyAttackHitBoxTimeLimit;

    // �v���C���[�̃X�N���v�g
    PlayerScript playerObject;

    // �v���C���[�̗̑͗p�ϐ�
    PlayerHPBarScript playerHPBarOnject;

    void Start()
    {
        // �������ꂽ��A�������g������
        Invoke("HitBoxDestroy", enemyAttackHitBoxTimeLimit);

        //�v���C���[�̗̑̓o�[�ƃv���C���[�̃X�N���v�g���擾
        playerHPBarOnject = GetComponent<PlayerHPBarScript>();
        playerObject = GetComponent<PlayerScript>();
    }

    void HitBoxDestroy()
    {
        // ���g(�G�̍U��)���폜
        Destroy(gameObject);
    }
    
    // �v���C���[�̍U���ɓ���������
    void OnTriggerEnter2D(Collider2D collision)
    {
        // �^�O���擾����
        switch (collision.gameObject.tag)
        {
            // �^�O�����L�̓��������
            case "Attack":
            case "Stage":

                // HitBoxDestroy()���Ăяo��
                HitBoxDestroy();
                break;

            // �^�O�����L�̂�������
            case "Player":

                //HitBoxDestroy()���Ăяo��
                HitBoxDestroy(); 

                // �v���C���[�̓����蔻���������
                if (playerObject.isHit == false)
                {
                    // �v���C���[�̗̑͂����炷
                    playerObject.playeryHp -= enemyAttackDamage;

                    // �v���C���[�̃_���[�W�G�t�F�N�g��\��
                    playerObject.DamageEffect();

                    // �v���C���[�̗̑̓o�[�����炷
                    playerHPBarOnject.PlayerHPDamege();

                }
                break;
        }
    }
}

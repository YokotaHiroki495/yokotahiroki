
using UnityEngine;
using UnityEngine.UI;

public class EffectImage : MonoBehaviour
{
    [SerializeField] Color damegeColor = new Color(0.5f, 0f, 0f, 0.5f);
    [SerializeField] Color getItemColor = new Color(0f, 0.5f, 0f, 0.5f);

    //�G�t�F�N�g�֌W
    [SerializeField] Image effect;
    Color effectcolor;

    void Start()
    {

        //�J���[�f�[�^�ɓ����
        effectcolor = effect.color;

        //�A���t�@�l��0��
        effectcolor.a = 0;
    }

    void Update()
    {
        // �G�t�F�N�g�̃J���[�̃A���t�@�l�𒲐����ē����ɂ���
        effectcolor.a = Mathf.Lerp(effectcolor.a, 0, Time.deltaTime);
        effect.color = effectcolor;
    }

    public void EnemyEffect()
    {
        // ��ʂ�ԐF��
        effectcolor = damegeColor;

        // �A���t�@�l�����������ĐF��Z������
        effectcolor.a = 0.5f;
    }

    public void ItemEffect()
    {
        // ��ʂ�ΐF��
        effectcolor = getItemColor;

        // �A���t�@�l�����������ĐF��Z������
        effectcolor.a = 0.5f;  
    }
}

using UnityEngine;
using UnityEngine.UI;

public class DamegeZone : MonoBehaviour
{

    //���֘A
    public AudioClip Sound1;
    AudioSource audioSource;

    //�G�t�F�N�g�֌W
    [SerializeField] Image effect;
    Color effectcolor;


    EffectImage effectimage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //////�J���[�f�[�^�ɓ����
        //effectcolor = effect.color;

        ////////�A���t�@�l��0��
        //effectcolor.a = 0;

        effectimage = GameObject.Find("Effect"). GetComponent<EffectImage>();


    }

    // Update is called once per frame
    void Update()
    {
        // �G�t�F�N�g�\������ɓ����ɂ���
        // �J���[��Leap�͏d���̂Ł@�A���t�@�l�����œ��߂�����

        //�G�t�F�N�g�̃J���[�̃A���t�@�l�𒲐����ē����ɂ���
        //effectcolor.a = Mathf.Lerp(effectcolor.a, 0, Time.deltaTime);
        //effect.color = effectcolor;

   
    
    }

  
    private void OnTriggerEnter(Collider other)
    {

        //swith��

        switch(other.gameObject.tag)
        {
            //case�@���������������case���o����
            case "Enemy":
            case "Enemy2":

                // �I�u�W�F�N�g���폜����
                Destroy(other.gameObject);

                // ���jSE��炷
                audioSource.PlayOneShot(Sound1);

                effectimage.EnemyEffect();

                //effectcolor.a = 0.5f;
                //effectcolor = new Color(0.5f, 0f, 0f, 0.5f);
                
                
                
                GameObject UIDirector = GameObject.Find("UIDirector");

                //HP�̏�����������
                UIDirector.GetComponent<UIDirector>().DecreaseHp();

                break;
        }





    }

}

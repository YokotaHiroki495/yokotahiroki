
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    

    // �V�[���ړ��֘A
    // �V�[���ړ����̃t�F�[�h�̃J���[
    [SerializeField]Color fadeColor;

    // �V�[�ړ��̎���
    [SerializeField]float fadeSpeed;


    //�X�R�A�֘A
    //�N���A�p�X�R�A
    [SerializeField] int clearScoreNumLimit;

    //�X�R�A�ϐ�
    public static int score;
    [SerializeField]int minScoreLimit;

    //�X�R�A�\���pText
    public TextMeshProUGUI scoreText;   

    //�V�[���̖��O��ۑ�����
    string sceneName;


    //���֘A
    //�{�X���j�{�C�X
    [SerializeField] AudioClip bossDestroyVoice;
    AudioSource audioSource;

    PlayerScript playerObject;
    float gmPlayerHPNumber;

    void Start()
    {
        playerObject = GameObject.Find("Player").GetComponent<PlayerScript>();
        audioSource = GetComponent<AudioSource>();
        sceneName = SceneManager.GetActiveScene().name;
        
        //���X�R�A
        //�X�R�A��0�ɂ���
        score = clearScoreNumLimit;

        //�X�R�A���e�L�X�g�ɕ\��
        scoreText.text = "Enemy:" + score;   
    }
       
    void Update()
    {
        //�v���C���[�̗̑͂��Q�[���}�l�[�W���[�ŕۑ�����
        gmPlayerHPNumber = playerObject.playeryHp;
        GMPlayerHP();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#else

         Application.Quit();

#endif

        }

    }

        public void GMPlayerHP()
    {
        //�v���C���[��HP��0�ȉ��ɂȂ�����
        if (gmPlayerHPNumber <= 0)
        {
            //���U���g�V�[���Ɉړ�
            Invoke("MoveResultScene", 2.0f);
        }
    }

    public void PlusScore()
    {
        //�G�̎c�萔�����炷
        score--;

        //�X�R�A���e�L�X�g�ɕ\��
        scoreText.text = "Enemy:" + score;

        if (score <= minScoreLimit)
        {
            score = minScoreLimit;

        }

        switch (sceneName)
        {
            case "BattleRoyaleScene":
                if (score == minScoreLimit)
                {
                    //�V�[�����ړ�������
                    Invoke("MoveBossStageScene", 3.0f);
                }
            break;

            case "BossScene":
                if (score == 0)
                {
                    //�{�X���jSE��炵��
                    audioSource.PlayOneShot(bossDestroyVoice);

                    //�V�[�����ړ�������
                    Invoke("MoveResultScene", 5.0f);
                }
                break;
        }
    }

    void MoveBossStageScene()
    {
        Initiate.Fade("BossScene", fadeColor, fadeSpeed);

    }
    
    void MoveResultScene()
    {
        Initiate.Fade("ResultScene", fadeColor, fadeSpeed);
    }
        
}

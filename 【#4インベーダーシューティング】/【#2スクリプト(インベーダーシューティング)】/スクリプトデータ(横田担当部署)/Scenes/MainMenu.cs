using UnityEditor;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
   

  
    void Start()
    {
        //�}�E�X�J�[�\���̕\��������
        Cursor.visible = false;
       
    }

    // Update is called once per frame
    void Update()
    {
       

        //�R���g���[���[�̃X�^�[�g�{�^���A�������̓R���g���[���[��A�{�^��������������
        if (Input.GetButtonDown("Start")�@|| Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Z))
        {

           //�v���O�C�����g�p

            Initiate.Fade("EnemyScene", Color.black, 1.0f);

        }

     

        //ESC�L�[����������Q�[���I��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            //�G�f�B�^�[�̍Đ����~�߂�
            EditorApplication.isPlaying = false;
#else
         //�Q�[�����I��
         Application.Quit();

#endif

        }
    }

    
    }







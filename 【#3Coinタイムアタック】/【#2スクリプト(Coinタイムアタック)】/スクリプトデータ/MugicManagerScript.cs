using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MugicManagerScript : MonoBehaviour
{

    //�^�C�g���V�[���pBGM��Script
    //�������g���ǂݍ��܂�Ă��邩����
    static bool isLoad = false;

    
    void Update()
    {

        //�Q�[���V�[���ɂȂ�����
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            //BGM������
            Destroy(this.gameObject);

        }
        else
        {
            //����ȊO(�Q�[���V�[���ȊO)�̎���
            //�������ɗ���
            DontDestroyOnLoad(this.gameObject);
        }

    }
}

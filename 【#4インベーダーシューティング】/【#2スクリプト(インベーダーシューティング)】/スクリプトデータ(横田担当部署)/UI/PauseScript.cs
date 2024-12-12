using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseScript: MonoBehaviour
{

    // �|�[�Y�������A�\������I�u�W�F�N�g
    [SerializeField] GameObject pauseMenu;

    //�|�[�Y�̃��g���C��I�����ꂽ�Ƃ��Ƀ��g���C����Q�[���V�[����ۑ����邽�߂̕ϐ�
    public static string retryScene;

   
    void Start()
    {
        Time.timeScale = 1f;

       // ���݂̃V�[�����擾
        retryScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        // �R���g���[���[�̃X�^�[�g�{�^���A�L�[�{�[�h��P�L�[�������ꂽ��
        if (Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.P))
        {

            // �|�[�Y�̃A�N�e�B�u�A��A�N�e�B�u�؂�ւ�
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            // �|�[�Y���\������Ă���Ԃ͒�~
            if (pauseMenu.activeSelf)
            {
                // �|�[�Y��ʎ���UI��\������
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                
                // ���Ԃ̗����0�ɂ���
                Time.timeScale = 0f;          
            }
            // ����ȊO�͍Đ�
            else
            {
                Time.timeScale = 1f;
            }
        }



        // �|�[�Y��ʒ���
        if (pauseMenu.activeSelf)
        {
            // �R���g���[���[��B�{�^���A�L�[�{�[�h��X�L�[�������ꂽ��
            if (Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.X))
            {
                // �|�[�Y��Ԃ��A�N�e�B�u�ɂ���
                pauseMenu.SetActive(false);

                // ���Ԃ�ʏ�ɖ߂�
                Time.timeScale = 1f;
            }

            // �����|�[�Y����Z�L�[����������A
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // �I�𒆂̃{�^���I�u�W�F�N�g������
                GameObject stpobj = EventSystem.current.currentSelectedGameObject;
                
               
            }

        }
        

    }



    public void Continue()
    {
        // �|�[�Y��Ԃ��A�N�e�B�u�ɂ���
        pauseMenu.SetActive(false);
        
        // ���Ԃ�ʏ�ɖ߂�
        Time.timeScale = 1f;
    }

    public void REstart()
    {
        // RESTARTbutton�����ꂽ�烊�Z�b�g���ăV�[���̍ŏ��ɖ߂�
        Debug.Log("���X�^�[�g");

        // ���X�^�[�g�����璼�O�̃V�[���ɖ߂�
        SceneManager.LoadScene(retryScene);
        Time.timeScale = 1f;

    }

    public void Title()
    {
        //�^�C�g���߂�
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1f;
    }

    
}

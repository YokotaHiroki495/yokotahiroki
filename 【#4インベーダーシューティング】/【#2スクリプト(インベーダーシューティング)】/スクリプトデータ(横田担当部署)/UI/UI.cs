using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    //SerializeField �����������unity��inspector�ҏW�ł���悤�ɂȂ�
    [SerializeField] GameObject gameClearText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] TextMeshProUGUI colortext;

    private GameObject[] EnemyObjects;
    private GameObject[] EnemyObjects2;
    private GameObject[] Playerobject;


   

    public GameObject targetObject;


    //public EnemyController EC;
    private int EnemyCount;
    // Start is called before the first frame update
    void Start()
    {
      
        //EC =GameObject.Find("Enemycontroller").GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        // ESC�L�[��������Q�[�����I��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR

            EditorApplication.isPlaying = false;
#else

         Application.Quit();

#endif

        }

      

        
     
    }

    

    void MoveGameClear()
    {
        SceneManager.LoadSceneAsync("GameClear");
    }

    void MoveGameOver()
    {
        SceneManager.LoadSceneAsync("GameOver");
    }

    void MoveMainmenu()
    {
        Debug.Log("���C�����j���[�ɖ߂�");
        SceneManager.LoadSceneAsync("MainMenu");
    }
}

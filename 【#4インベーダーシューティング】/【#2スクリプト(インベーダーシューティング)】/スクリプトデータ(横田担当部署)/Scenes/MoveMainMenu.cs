
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEditor;

public class MoveMainMenu : MonoBehaviour
{

    //リトライする際の現在のステージを取得する変数
    string nowStage;

    void Start()
    {
        nowStage = PauseScript.retryScene;
    }

    private void Update()
    {
        //ESCキーを押したら
        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            //エディターの再生を止めて
            EditorApplication.isPlaying = false;
#else
         //ゲームを終了
         Application.Quit();

#endif

        }
    }






        public void Retry()
        {
       
        //SceneManager.LoadScene("Enemy Test");
        Initiate.Fade(nowStage, Color.black, 1.0f);

        }

  
    public void Title()
    {
        //タイトル画面に戻る
        SceneManager.LoadScene("TitleScene");
    }

}

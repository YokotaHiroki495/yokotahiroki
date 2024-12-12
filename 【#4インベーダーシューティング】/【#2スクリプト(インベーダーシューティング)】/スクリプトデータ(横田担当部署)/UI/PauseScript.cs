using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseScript: MonoBehaviour
{

    // ポーズした時、表示するオブジェクト
    [SerializeField] GameObject pauseMenu;

    //ポーズのリトライを選択されたときにリトライするゲームシーンを保存するための変数
    public static string retryScene;

   
    void Start()
    {
        Time.timeScale = 1f;

       // 現在のシーンを取得
        retryScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        // コントローラーのスタートボタン、キーボードのPキーを押されたら
        if (Input.GetButtonDown("Start") || Input.GetKeyDown(KeyCode.P))
        {

            // ポーズのアクティブ、非アクティブ切り替え
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            // ポーズが表示されている間は停止
            if (pauseMenu.activeSelf)
            {
                // ポーズ画面時のUIを表示する
                EventSystem.current.SetSelectedGameObject(EventSystem.current.firstSelectedGameObject);
                
                // 時間の流れを0にする
                Time.timeScale = 0f;          
            }
            // それ以外は再生
            else
            {
                Time.timeScale = 1f;
            }
        }



        // ポーズ画面中に
        if (pauseMenu.activeSelf)
        {
            // コントローラーのBボタン、キーボードのXキーを押されたら
            if (Input.GetButtonDown("B Button") || Input.GetKeyDown(KeyCode.X))
            {
                // ポーズ状態を非アクティブにして
                pauseMenu.SetActive(false);

                // 時間を通常に戻す
                Time.timeScale = 1f;
            }

            // もしポーズ中にZキーを押したら、
            if (Input.GetKeyDown(KeyCode.Z))
            {
                // 選択中のボタンオブジェクトを押す
                GameObject stpobj = EventSystem.current.currentSelectedGameObject;
                
               
            }

        }
        

    }



    public void Continue()
    {
        // ポーズ状態を非アクティブにして
        pauseMenu.SetActive(false);
        
        // 時間を通常に戻す
        Time.timeScale = 1f;
    }

    public void REstart()
    {
        // RESTARTbutton押されたらリセットしてシーンの最初に戻る
        Debug.Log("リスタート");

        // リスタートしたら直前のシーンに戻る
        SceneManager.LoadScene(retryScene);
        Time.timeScale = 1f;

    }

    public void Title()
    {
        //タイトル戻る
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1f;
    }

    
}

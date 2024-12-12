using UnityEngine;

public class ExitGame : MonoBehaviour
{
    void Update()
    {
        SceneMove();
    }

    void SceneMove()
    {
        // ESCキーを押したら
        if (Input.GetKeyDown(KeyCode.Escape))
        { // Unityエディタで実行中かどうかをチェック
#if UNITY_EDITOR
            // Unityエディタで実行中の場合はプレイモードを終了
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // ビルドされたアプリケーションの場合はアプリケーションを終了
            Application.Quit();
#endif
        }
    }

  
}

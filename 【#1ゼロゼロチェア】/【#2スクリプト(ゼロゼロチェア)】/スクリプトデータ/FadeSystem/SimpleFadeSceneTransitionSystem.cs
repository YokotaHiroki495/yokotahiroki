using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleFadeSceneTransitionSystem : MonoBehaviour
{
    [System.Serializable] // インスペクター上で編集できるように設定

    class SceneTransition
    {
        public string sceneName;        // シーン名
        public Color fadeColor;         // フェードカラー
        [Min(0)] public float fadeDamp; // フェードする時間

        // クリックされたのはUIのボタンかマウスの左クリックか
        [Header("Noneの場合はマウスの左クリック")]
        public Button button;
    }

    // List型でSceneTransitionを表示および編集を可能にする
    [SerializeField] List<SceneTransition> _sceneTransitions = new List<SceneTransition>();

    
    // プロパティが変更されるときに実行される処理
    void OnValidate() 
    {
        // SceneTransitionリスト内の要素tに対してループを実行
        foreach (var t in _sceneTransitions)
        {
            // tのfadeColorのアルファ値(a)が1未満かどうかチェック
            if (t.fadeColor.a < 1)
            {
                // 現在のfadeColorを一時的にコピー
                Color fadeColor = t.fadeColor;
                // アルファ値を1に設定　色が完全に不透明になるようにする
                fadeColor.a = 1;
                // 変更されたfadeColorを元のtに戻す
                t.fadeColor = fadeColor;
            }
        }
    }

    void Start()
    {
        
        System.Action mouseClickAction = null;

        
        foreach (var t in _sceneTransitions)
        {
            // ボタンの登録が無かったら画面を左クリックされたらシーン移動
            if (t.button == null)
            {
                
                mouseClickAction = () => Initiate.Fade(t.sceneName, t.fadeColor, t.fadeDamp);
            }
            else
            {
                // ボタンの指定が有ったら
                t.button.onClick.AddListener(() => Initiate.Fade(t.sceneName, t.fadeColor, t.fadeDamp));
            }
        }

        // クリックされるまで待つ
        if (mouseClickAction != null)
        {
            StartCoroutine(_());
            IEnumerator _()
            {
                yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

                mouseClickAction();
            }
        }
    }

    public static void Fade(string sceneName, Color fadeColor, float fadeDamp)
    {
        Initiate.Fade(sceneName, fadeColor, fadeDamp);
    }

    public static Coroutine SceneMoveStay(MonoBehaviour owner,string sceneName, Color fadeColor, float fadeDamp)
    {
        return owner.StartCoroutine(_());
        IEnumerator _()
        {
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

            Initiate.Fade(sceneName, fadeColor, fadeDamp);
        }
    }


    public void MoveGameClear()
    {
        foreach (var t in _sceneTransitions)
        {
            Initiate.Fade(t.sceneName, t.fadeColor, t.fadeDamp);
        }
    }

    public void MoveGameOver()
    {
        foreach (var t in _sceneTransitions)
        {
            Initiate.Fade(t.sceneName, t.fadeColor, t.fadeDamp);
        }
    }
}

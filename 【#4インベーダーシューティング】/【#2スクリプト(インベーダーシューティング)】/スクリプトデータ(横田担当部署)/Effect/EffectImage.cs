
using UnityEngine;
using UnityEngine.UI;

public class EffectImage : MonoBehaviour
{
    [SerializeField] Color damegeColor = new Color(0.5f, 0f, 0f, 0.5f);
    [SerializeField] Color getItemColor = new Color(0f, 0.5f, 0f, 0.5f);

    //エフェクト関係
    [SerializeField] Image effect;
    Color effectcolor;

    void Start()
    {

        //カラーデータに入れる
        effectcolor = effect.color;

        //アルファ値を0に
        effectcolor.a = 0;
    }

    void Update()
    {
        // エフェクトのカラーのアルファ値を調整して透明にする
        effectcolor.a = Mathf.Lerp(effectcolor.a, 0, Time.deltaTime);
        effect.color = effectcolor;
    }

    public void EnemyEffect()
    {
        // 画面を赤色に
        effectcolor = damegeColor;

        // アルファ値を書き換えて色を濃くする
        effectcolor.a = 0.5f;
    }

    public void ItemEffect()
    {
        // 画面を緑色に
        effectcolor = getItemColor;

        // アルファ値を書き換えて色を濃くする
        effectcolor.a = 0.5f;  
    }
}

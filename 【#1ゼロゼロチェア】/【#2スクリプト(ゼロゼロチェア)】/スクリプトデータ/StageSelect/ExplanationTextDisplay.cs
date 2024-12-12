using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExplanationTextDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // IPointerEnterHandler     マウスカーソルが特定のゲームオブジェクトに重なった際にコールバックを受け取る為のインターフェース
    // IPointerExitHandler      マウスカーソルが特定のゲームオブジェクトから離れた際にコールバックを受け取る為のインターフェース
   

    // ボタンごとに異なるテキストとイメージを表示する処理
    [SerializeField] TextMeshProUGUI _hoverText; // ステージの説明を表示するテキスト
    [SerializeField] string _displayText;        // 表示するテキスト(ボタンごとに異なるテキスト)

    [SerializeField]Image _hoverImage;          // ステージのイメージイラストを表示する場所
    [SerializeField] Sprite _displaySprite;     // 表示する画像

    void Start()
    {
        
        // テキストと画像を非表示に
        _hoverText.gameObject.SetActive(false);
        _hoverImage.gameObject.SetActive(false);

    }


    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // マウスが重なったら設定したテキストを表示
        _hoverText.text = _displayText;
        _hoverText.gameObject.SetActive(true);

        // 設定した画像を表示
        _hoverImage.sprite = _displaySprite;
        _hoverImage.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // マウスが離れたらテキストと画像を非表示
        _hoverText.gameObject.SetActive(false);
        _hoverImage.gameObject.SetActive(false);
    }
}

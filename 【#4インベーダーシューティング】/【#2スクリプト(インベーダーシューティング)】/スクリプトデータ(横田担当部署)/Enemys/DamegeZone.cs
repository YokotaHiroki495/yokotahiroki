using UnityEngine;
using UnityEngine.UI;

public class DamegeZone : MonoBehaviour
{

    //音関連
    public AudioClip Sound1;
    AudioSource audioSource;

    //エフェクト関係
    [SerializeField] Image effect;
    Color effectcolor;


    EffectImage effectimage;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        //////カラーデータに入れる
        //effectcolor = effect.color;

        ////////アルファ値を0に
        //effectcolor.a = 0;

        effectimage = GameObject.Find("Effect"). GetComponent<EffectImage>();


    }

    // Update is called once per frame
    void Update()
    {
        // エフェクト表示を常に透明にする
        // カラーのLeapは重いので　アルファ値だけで透過させる

        //エフェクトのカラーのアルファ値を調整して透明にする
        //effectcolor.a = Mathf.Lerp(effectcolor.a, 0, Time.deltaTime);
        //effect.color = effectcolor;

   
    
    }

  
    private void OnTriggerEnter(Collider other)
    {

        //swith文

        switch(other.gameObject.tag)
        {
            //case　処理書か無ければcaseが出来る
            case "Enemy":
            case "Enemy2":

                // オブジェクトを削除して
                Destroy(other.gameObject);

                // 撃破SEを鳴らす
                audioSource.PlayOneShot(Sound1);

                effectimage.EnemyEffect();

                //effectcolor.a = 0.5f;
                //effectcolor = new Color(0.5f, 0f, 0f, 0.5f);
                
                
                
                GameObject UIDirector = GameObject.Find("UIDirector");

                //HPの処理をさせる
                UIDirector.GetComponent<UIDirector>().DecreaseHp();

                break;
        }





    }

}

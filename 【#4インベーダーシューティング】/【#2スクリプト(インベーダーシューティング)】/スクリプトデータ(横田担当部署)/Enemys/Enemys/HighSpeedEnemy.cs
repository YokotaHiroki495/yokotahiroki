using UnityEngine;

public class HighSpeedEnemy : MonoBehaviour
{

    // 内部の変数と　外部は離す

    //変数の中での並び　メソッドの中での並び
    
    //インスペクター表示されるものを最初に
    //されないものは後に



    //爆発エフェクト
    public GameObject explosionPrefab;

    //ドロップアイテム関連
    public GameObject CureBombPrefab;

    //メンバ変数か、ローカル変数かをしっかり判断して分ける
    int itemrand;

    //移動関連

    //動きのランダム生成
    int moverand;

    //下への移動スピード
    [SerializeField] float Speed = 3f;

    //左右の移動スピード
    public float XSpeed = 5f;

    //左右の移動判定
    bool XPlusMove = true;//X方向に＋移動か？

    //移動位置のランダム生成
    int Posrand;



    void Start()
    {
        //動きをランダム生成
        moverand = Random.Range(1, 3);

        //ランダムな生成位置
        Posrand = Random.Range(1, 4);

    }


    void Update()
    {


        //Debug.Log("移動値"+moverand);

        //transform取得

        //元のデータへのアクセスが無駄に増えるだけ
        //作らない方が良い
        
        //Transform Ene2Tra = this.transform;



        //ワールド座標を基準に座標を取得
        //Vector3 WorldPos = Ene2Tra.position;
        Vector3 WorldPos = transform.position;

        // 2　には名前をつけて定数にする
        if (WorldPos.z < 2)
        {
            //5にも名前をつけて定数に
            WorldPos.z += 5 * Time.deltaTime;

        }

        //手前に向かって移動


        //swith文
        //ifなら
        if (moverand == 1)
        {
            //位置パターン1のとき
            //こっちのほうがスイッチ文
            if (Posrand == 1)
            {
                //もし、生成位置がX10よりも大きかったら
                if (WorldPos.x > 10)
                {
                    //マイナス(左)方向に移動する
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                //もし、生成位置がX-10寄りも小さかったら
                else if (WorldPos.x < -10)
                {
                    //＋方向(右)に移動する
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }
            //位置パターン2のとき
            if (Posrand == 2)
            {
                if (WorldPos.x > 5)
                {
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                else if (WorldPos.x < -5)
                {
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }
            //位置パターン3のとき
            if (Posrand == 3)
            {
                if (WorldPos.x > 1)
                {
                    WorldPos.x -= XSpeed * Time.deltaTime;
                }
                else if (WorldPos.x < -1)
                {
                    WorldPos.x += XSpeed * Time.deltaTime;
                }

            }

            ////下に向かって移動
            //WorldPos.y -= Speed * Time.deltaTime;

        }
        //ジグザグ移動

        // if (moverand == 2)使うならここはelse ifを使う
        //if (moverand == 2)
        else if(moverand == 2)
        {
            ////下に向かって移動
            //WorldPos.y -= Speed * Time.deltaTime;

            //方向判定がtrueなら
            if (XPlusMove)
            {
                //そのまま＋方向に移動する
                WorldPos.x += XSpeed * Time.deltaTime;

                //もし一定以上に移動したら
                if (WorldPos.x >= 10)
                {
                    //＋方向に進んでいる判定をfalseにする
                    XPlusMove = false;
                }
            }
            else//もし方向判定がfalseになっていたら
            {
                //マイナス方向に移動する
                WorldPos.x -= XSpeed * Time.deltaTime;

                //マイナスに進んで判定外に出たら
                if (WorldPos.x <= -10)
                {
                    //方向判定をtrue(本当)にする
                    XPlusMove = true;
                }
            }
        }


        //下に向かって移動
        WorldPos.y -= Speed * Time.deltaTime;




        //ワールド座標を基準として座標を変更
        transform.position = WorldPos;

    }
    // メソッド化して、

    private void OnTriggerStay(Collider col)
    {




        // switchにする　　ケースバレットプレファブか　ケースビームで処理を変える

        if (col.gameObject.tag == "bulletPrefab")
        {
 
            //このオブジェクトを破壊する
            Destroy(gameObject);

            //爆発エフェクトを表示して、エフェクトを1秒後に削除する
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

           
            //ボム回復アイテムの生成
            itemrand = Random.Range(1, 31);
            if (itemrand == 1)
            {
                Instantiate(CureBombPrefab, transform.position, Quaternion.identity);

            }

        }

        if (col.gameObject.tag == "Beam")
        {

           

            //このオブジェクトを破壊する
            Destroy(gameObject);

            //爆発エフェクトを表示して、エフェクトを1秒後に削除する
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);


            //ボム回復アイテムの生成
            //マジックナンバーは厳しい
            //インスペクターとかで簡単に調整出来るように
            // %
            // Random.valueで設定した方が良い





            //itemrand = Random.Range(1, 1001);
            if (itemrand == 1)
            {
                Instantiate(CureBombPrefab, transform.position, Quaternion.identity);


            }


            // -------------------ここ直す
            //N = 5;

            //if (Random.value * 100 <= N) ;

        }
    }
}

using UnityEngine;

public class NormalEnemy : MonoBehaviour
{

    // 名前を考える

    //爆発を管理するクラスを作って持たせた方が良い
    //
    //爆発エフェクト
    public GameObject explosionPrefab;

    //ドロップアイテム関連
    public GameObject CureBombPrefab;

    //アイテムのドロップ率
    int itemrand;

    //移動関連


    [Header("移動関連")]
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
        Transform Ene2Tra = this.transform;

        //ワールド座標を基準に座標を取得
        Vector3 WorldPos = Ene2Tra.position;


        if (WorldPos.z < 2)
        {
            WorldPos.z += 5 * Time.deltaTime;

        }

        //手前に向かって移動
        if (moverand == 1)
        {
            //位置パターン1のとき
            if (Posrand ==1)
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

            //下に向かって移動
            WorldPos.y -= Speed * Time.deltaTime;

        }
        //ジグザグ移動
        if (moverand == 2)
        {
            //下に向かって移動
            WorldPos.y -= Speed * Time.deltaTime;

            //方向判定がtrue(本当)なら
            if (XPlusMove)
            {
                //そのまま＋方向に移動する
                WorldPos.x += XSpeed * Time.deltaTime;

                //もし一定以上に移動したら
                if (WorldPos.x >= 10)
                {
                    //＋方向に進んでいる判定をfalse(嘘)にする
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

        //ワールド座標を基準として座標を変更
        Ene2Tra.position = WorldPos;

      
        
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "bulletPrefab")
        {


            //このオブジェクトを破壊する
            Destroy(gameObject);

            //爆発エフェクトを表示して、エフェクトを1秒後に削除する
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            

            //ボム回復アイテムの生成
            itemrand = Random.Range(1, 51);
            if(itemrand == 1)
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

            itemrand = Random.Range(1, 101);
            if (itemrand == 1)
            {
                Instantiate(CureBombPrefab, transform.position, Quaternion.identity);


            }
        }

    }

}

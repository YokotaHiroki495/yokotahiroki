using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //-----移動関連-----
    [Header("移動スピード関連")]
    //移動スピード
    [SerializeField]private float moveSpeed;
    //加速度
    [SerializeField]private float acceleration;
    //左右移動のスピード
    public float leftRightSpeed;
    //左右移動の移動制限
    [SerializeField]private float limit;
    //public Animator animator;


    [Space(50)]
    [Header("移動関連")]
    //----スワイプ移動処理----
    //スワイプを開始したポジション

    Vector3 StartTouchPos;

    //X方向のスワイプ
    //Vector3 XStartTouchXPos;

    //Y方向のスワイプ
    //Vector3 YStartTouchYPos;

    //スワイプが修了したポジション
    Vector3 EndTouchPos;

    //どれだけX座標をスワイプしたか
    float flickValue_X;

    //どれだけY座標をスワイプしたか
    float flickValue_Y;

    //どれだけX座標をスワイプしたら移動するか
    [SerializeField] float SwipMoveXDis;

    //どれだけY座標をスワイプしたらダッシュするか
    [SerializeField] float SwipMoveYDis;

    //指定した値以上にスワイプしたら検知する
    [SerializeField] float Swip_Distance_Detection;

    //ダッシュしているか否か
    public bool DashNow;

    //キャラが左右移動したかどうか
    public bool MoveNow;



    //加速時のエフェクト
    [SerializeField] GameObject DashEffect;
    

    //加速時クールタイム
    private int AcceleItem;


    //-----ジャンプ関連-----
    private Vector3 jump;


    //ジャンプ力
    [SerializeField]
    private float jumpForce;


    //設置判定
    private bool isGrounded;
    Rigidbody rb;


    //取得コイン数
    public static int HubCoin;


    //取得アイテム数
    public static int HubItem;


    [SerializeField]private  TextMeshProUGUI ItemNumText;
    
    //アイテムマイナス数
    //GamaManagerの方でも取得して、一括で編集できるように
    public int MinusItemNum;


    //現在位置の取得
    private float InstansPos;
    private float NewInstansPos;
    Vector3 PlayerYPos;

    //GAmeManagerを取得
    public GameManager gameManager;

    //メインカメラのスクリプトを取得
    public CameraScript camerascript;
    
    //床生成関連
    //生成時間
    [SerializeField]float Insdis;
    //どれだけ床を作るか
    int currentZ = 0;

    ////[SerializeField] int forward = 10;

    private const int createcooltime = 10;


    //アニメーション関係
    Animator  Anima;
    [SerializeField] GameObject unitychan;

    //エフェクト関係
    [SerializeField] Image Effect;


    //音関係
    public AudioClip JumpSE;
    public AudioClip CoinSE;
    public AudioClip DamegeSE;
    public AudioClip damegeVoice;
    AudioSource audioSource;

    // ダメージを受けた時の点滅処理
    [Header("DamegeEffect")]
    [SerializeField] float flashInterval;    //点滅の間隔
    [SerializeField] int loopCount; //点滅のループカウント
    Collider collider;
    Renderer renderer;
    Material material;

    SkinnedMeshRenderer chilSkinnedMexhRenderer;        //自身の子オブジェクト
    SkinnedMeshRenderer grondchilSkinnedMexhRenderer;   //自身の孫オブジェクト
    SkinnedMeshRenderer greatGreatGrandSon;             //自身の玄孫オブジェクト
    //AudioSource audioSource;

    public bool isHit; //当たったかどうかのフラグ

    void Start()
    {
        //RigidBodyを取得
        rb = GetComponent<Rigidbody>();
        jump = new Vector3(0.0f, 2.0f, 0.0f);


        Anima = unitychan.GetComponent<Animator>();


        //最初の位置を保存
        InstansPos = transform.position.z;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        //エフェクト用の色を取得
        Effect.color = Color.clear;

        //音
        audioSource = GetComponent<AudioSource>();


        //所持コイン数(HubCoinのリセット)
        HubCoin = 0;


        //所持アイテム数
        HubItem = 0;
        //ItemNumText.text = "AccelItem:" + HubItem;

        //カメラを取得
        camerascript = GameObject.Find("Main Camera").GetComponent<CameraScript>();


        PlayerYPos = transform.position;

        //設置判定
        isGrounded = true;

        collider = GetComponent<CapsuleCollider>();
        renderer = GetComponent<MeshRenderer>();
        material = GetComponent<Material>();

        chilSkinnedMexhRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        grondchilSkinnedMexhRenderer = chilSkinnedMexhRenderer.GetComponentInChildren<SkinnedMeshRenderer>();
        greatGreatGrandSon　= grondchilSkinnedMexhRenderer.GetComponentInChildren<SkinnedMeshRenderer>();

    }


    void Update()
    {
        //Debug.Log(isGrounded);
        SwipeMove();
        Move();

        
        //エフェクトの色
        //エフェクトの色を常に透明にする
        //色は、障害物に当たった時に変更する
        Effect.color = Color.Lerp(Effect.color, Color.clear, Time.deltaTime);

        if (isGrounded == true)
        {
            
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }
        

    }




    //移動処理
    void Move()
    {
        //float pos_x = transform.position.x;

        //ゲーム内のtimescaleを増やすことで、アニメーションなどのスピードも速くなる
        //範囲を１倍～3倍の間に設定
        Time.timeScale = Mathf.Clamp(1 + acceleration * HubCoin,　1,3);

        //奥に移動する処理
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed/*, Space.World*/;
        


        //PCでの操作
        //レーン移動
        //左移動
        if (Input.GetKeyDown(KeyCode.LeftArrow )|| Input.GetKeyDown(KeyCode.A))
        {
            if (transform.position.x > -3)
            {
                transform.position += new Vector3((float)-3, 0, 0);
            }

        }
        
        


        //右移動
        if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D))
        {
            
            if (transform.position.x < 3)
            {
               
                transform.position += new Vector3((float)3, 0, 0);
            }

        }

        //加速処理(仮)
        if (Input.GetKeyDown(KeyCode.W)|| Input.GetKeyDown(KeyCode.UpArrow) && isGrounded)
        {
            if (!DashNow)
            {
                DashNow = true;
                //メインカメラについているカメラスクリプトのコルーチンを実行
                camerascript.StartCoroutine("CameraMove");

                //RigidBodyに衝撃を加えて加速
                rb.AddForce(0f, 0f, 10f, ForceMode.Impulse);

                //DashNow = false;
            }

            ////メインカメラについているカメラスクリプトのコルーチンを実行
            //camerascript.StartCoroutine("CameraMove");

            //    //RigidBodyに衝撃を加えて加速
            //    rb.AddForce(0f, 0f, 10f, ForceMode.Impulse);
           

        }

    }

    //スワイプ操作時の挙動
    void SwipeMove()
    {
        //スワイプ開始時点の座標を取得
        if (Input.GetMouseButtonDown(0) == true)
        {
            //画面を触れたら操作可能状態にする
            MoveNow = true;
            StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

        }

       

        if (Input.GetMouseButton(0) == true)
        {
            EndTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

            FlickDirection();
            GetDirection();
            //NewTestGetDirection();


        }




    }

    //スワイプされた量を計算
    void FlickDirection()
    {
        //X座標のスワイプ量を取得
        flickValue_X = EndTouchPos.x - StartTouchPos.x;

        //Y座標のスワイプ量を取得
        flickValue_Y = EndTouchPos.y - StartTouchPos.y;

        Debug.Log("Xスワイプ："+flickValue_X);

    }


    //一定以上スワイプされたら位置移動
    void GetDirection()
    {
        //キャラが移動可能状態なら
        if (MoveNow)
        {

            //左移動
            if (flickValue_X < -SwipMoveXDis)
            {


                if (transform.position.x > -3)
                {
                    //左に移動する
                    transform.position += new Vector3(-3, 0, 0);

                    //GetMouseButtonだった際、そのままスライドし続ければ連続で移動できる
                    //StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

                    //操作可能状態をfalseに変更
                    MoveNow = false;
                }
            }


            //右移動
            if (flickValue_X > SwipMoveXDis)
            {

                if (transform.position.x < 3)
                {
                    transform.position += new Vector3(3, 0, 0);

                    //追加
                    //StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

                    MoveNow = false;

                }
            }

            //ダッシュ
            //GetMouseButtonにすると、上にスライドすると連続で実行される。
            if (flickValue_Y > SwipMoveYDis)
            {
                Debug.Log("ダッシュ認識");
                if (!DashNow)
                {
                    DashNow = true;
                    //メインカメラについているカメラスクリプトのコルーチンを実行
                    camerascript.StartCoroutine("CameraMove");

                    //RigidBodyに衝撃を加えて加速
                    rb.AddForce(0f, 0f, 10f, ForceMode.Impulse);

                    //DashNow = false;
                }
            }
        }

    }



    //一定以上スワイプされたら位置移動&移動した後は指を離さないと移動できないようにする
    //void NewTestGetDirection()
    //{
    //    //キャラが移動可能状態なら
    //    if (MoveNow)
    //    {

    //        //左移動
    //        if (flickValue_X < -SwipMoveXDis)
    //        {


    //            if (transform.position.x > -3)
    //            {
    //                //左に移動する
    //                transform.position += new Vector3(-3, 0, 0);

    //                //GetMouseButtonだった際、そのままスライドし続ければ連続で移動できる
    //                //StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

    //                //操作可能状態をfalseに変更
    //                MoveNow = false;
    //            }
    //        }


    //        //右移動
    //        if (flickValue_X > SwipMoveXDis)
    //        {

    //            if (transform.position.x < 3)
    //            {
    //                transform.position += new Vector3(3, 0, 0);

    //                //追加
    //                //StartTouchPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);

    //                MoveNow = false;

    //            }
    //        }

    //        //ダッシュ
    //        //GetMouseButtonにすると、上にスライドすると連続で実行される。
    //        if (flickValue_Y > SwipMoveYDis)
    //        {
    //            Debug.Log("ダッシュ認識");
    //            if (!DashNow)
    //            {
    //                DashNow = true;
    //                //メインカメラについているカメラスクリプトのコルーチンを実行
    //                camerascript.StartCoroutine("CameraMove");

    //                //RigidBodyに衝撃を加えて加速
    //                rb.AddForce(0f, 0f, 10f, ForceMode.Impulse);

    //                //DashNow = false;
    //            }
    //        }
    //    }
    //}




//タップした瞬間、判定をtrueにして、指を離したらfalseにする


//当たり判定(Trigger)に接触したら
void OnTriggerEnter(Collider other)
    {
        //Obstacle(障害物)に当たったら
        if (other.gameObject.tag == "Obstacle")
        {
            StartCoroutine(PlayerDamegeEffect());
            //ダメージSEを再生
            audioSource.PlayOneShot(DamegeSE);
            audioSource.PlayOneShot(damegeVoice);


            //ゲームマネージャーのスクリプトのマイナスの値も一緒に変更できるように直す
            HubCoin -= MinusItemNum;
            if (HubCoin < 0)
            {
                HubCoin = 0;
            }

            //ゲームマネージャースクリプトのHPminusにアクセス
            gameManager.MinusScore();

            //エフェクトの色を変える　赤色
            this.Effect.color = new Color(0.5f, 0f, 0f, 0.5f);

        }

    }

    //コイン取得時に実行
    public void Coinplus()
    {
        //取得SEを再生
        audioSource.PlayOneShot(CoinSE);
        
        //ハブコインを1増やす
        HubCoin += 1;

        //ゲームマネージャーのプラススコアを実行
        gameManager.PlusScore();

    }

 
    //当たり判定(Collider)に当たったら
    void OnCollisionEnter(Collision collision)
    {
        //相手のタグが"ステージ"だったら
        if (collision.gameObject.tag == "Stage")
        {
            //接地判定をtrueにする
            isGrounded = true;
        }
    }

    //当たり判定(Collider)から離れたら
    void OnCollisionExit(Collision collision)
    {
        //相手のタグが"ステージ"だったら
        if (collision.gameObject.tag == "Stage")
        {
            //接地判定をfalseに
            isGrounded = false;

        }
    }



    IEnumerator PlayerDamegeEffect()
    {
        //当たり判定を変更
        isHit = true;

        //点滅ループ
        for (int i = 0; i < loopCount; i++)
        {

            yield return new WaitForSeconds(flashInterval);

            //レンダラーをオフ
            //renderer.enabled = false;
            //rendrer.enabled = false;
            //collider.enabled = false;

            //material.color = material.color - new Color(0, 0, 0, 1);
            greatGreatGrandSon.enabled = false;

            yield return new WaitForSeconds(flashInterval);

            renderer.enabled = true;
            //collider.enabled = true;
            //material.color = material.color - new Color(0, 0, 0, 0);

            greatGreatGrandSon.enabled = true;
        }

        isHit = false;
    }

}

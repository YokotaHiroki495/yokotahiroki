using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    //コイン(アイテム)に関するScript

    //コインの回転スピード
    [SerializeField]private int CoinrotateSpeed;

    public GameManager gameManager;

    public Player player;

    Renderer  renderer;


    
    void Start()
    {
        //ゲームマネージャーを取得
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        //プレイヤーを取得
        player = GameObject.Find("Player").GetComponent<Player>();

        renderer = GetComponent<MeshRenderer>();
        
        
    }


    void Update()
    {  
        //オブジェクト(コイン)を回転させる
        transform.Rotate(0, CoinrotateSpeed * Time.deltaTime, 0, Space.World);
    }

    //当たり判定(Trigger)が当たったら
    private void OnTriggerEnter(Collider other)
    {
        //相手がPlayerだったら
        if (other.gameObject.tag == "Player")
        {
            //レンダラーを非表示にして
            renderer.enabled = false;

            
            //PlayerのCoinpulsを実行
            player.Coinplus();

        }
        if (other.gameObject.tag == "DestroyArea")
        {
            //レンダラーを非表示にする
            //gameObject.SetActive(true);
            renderer.enabled = true;

        }
    }
}

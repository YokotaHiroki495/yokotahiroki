using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScript : MonoBehaviour
{
    //BattleRoyaleStage‚Ì°‚ÌScript
    //•‚‚¢‚Ä‚¢‚é°‚ğ‚·‚è”²‚¯‚ê‚é‚æ‚¤‚É‚·‚é


    PlatformEffector2D platFormEffector2d;


    bool SlipThrough = false; //°‚Ì‚·‚è”²‚¯”»’è

    void Start()
    {

        platFormEffector2d = GetComponent<PlatformEffector2D>();

    }

    void Update()
    {
        //‚±‚Ì‚Ü‚Ü‚¾‚ÆA‰½‚à‘€ì‚µ‚È‚¢‚Æ˜A‘±‚Å‚·‚è”²‚¯‚Ä‚µ‚Ü‚¤
        
        if (Input.GetKey(KeyCode.Space))
        {
            //platFormEffector2d.rotationalOffset = 0f;
        }
    }


   
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Player‚ª‰º•ûŒü‚ÉˆÚ“®‚µ‚æ‚¤‚Æ‚·‚é‚Æ
        if (Input.GetAxisRaw("Vertical") <0)
        {
            //“–‚½‚è”»’è‚ğ180“x”½“]‚³‚¹‚é
            platFormEffector2d.rotationalOffset = 180;
        }

    }
    void OnCollisionExit2D(Collision2D collision)
    {
        platFormEffector2d.rotationalOffset = 0f;
        
    }


}

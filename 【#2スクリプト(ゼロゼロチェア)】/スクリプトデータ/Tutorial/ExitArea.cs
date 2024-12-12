using UnityEngine;

public class ExitArea : MonoBehaviour
{
    // 音関係
    [SerializeField]AudioClip _exitSE;  // 再生するSE
    [SerializeField]float _soundVolume; // 再生する際の音量

    bool _firstEvent = true;
    void OnTriggerEnter(Collider other)
    {
        //if (_firstEvent&&other.gameObject.CompareTag("Player"))
        //{
        //    GetComponent<AudioSource>().PlayOneShot(_exitSE, _soundVolume);
        //    // シーンを移動する
        //    Initiate.Fade("GameClear", Color.black, 3.0f);
        //    _firstEvent = false;
        //}


        if (other.gameObject.CompareTag("Player"))
        {
            GetComponent<AudioSource>().PlayOneShot(_exitSE, _soundVolume);
            Initiate.Fade("GameClear", Color.black, 3.0f);
        }
    }
   
}

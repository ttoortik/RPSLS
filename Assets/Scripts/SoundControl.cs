using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public AudioSource sound;
    public Button button;
    public Sprite SoundOff;
    public Sprite SoundOn;
    void Start()
    {
        if(sound.mute==false)
        {
            sound.mute = true;
            button.image.sprite = SoundOff;
        }
        else
        {
            sound.mute = false;
            button.image.sprite = SoundOn;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

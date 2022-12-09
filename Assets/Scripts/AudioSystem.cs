using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AudioSystem: MonoBehaviour
{
    public static AudioSystem instance;
    public AudioClip[] audiosSoporte, audiosTanke, audiosGeneral;
    public AudioSource audioSourceGlobal,audioSourcePlayer;
    private void Awake()
    {
        if (instance is null)
        {
            instance = this;
        }
    }
    
    public void PonerSonido(AudioClip clip, AudioSource source) 
        => source.PlayOneShot(clip);
}

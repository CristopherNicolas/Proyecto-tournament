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
    
    /// <summary>
    /// pon un sonido que se ejecuta una vez
    /// </summary>
    /// <param name="clip"></param>
    /// <param name="source"></param>
    public void PonerSonido(AudioClip clip, AudioSource source) 
        => source.PlayOneShot(clip);
/// <summary>
/// pon un sonido que se repite constantemente
/// </summary>
/// <param name="clip"></param>
/// <param name="source"></param>
    public void PonerSonidoConLoop(AudioClip clip,AudioSource source)
    {
        source.Stop();
        source.loop = true;
        source.clip = clip;
        source.Play();
    }
}

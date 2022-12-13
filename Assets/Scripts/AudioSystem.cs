using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Netcode;
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
    {
        if (audioSourcePlayer is null)
            audioSourcePlayer = GameObject.FindObjectsOfType<FirstPersonMovement>()
            .Where(s => s.GetComponent<NetworkObject>().IsOwner).First().GetComponent<AudioSource>();

        source.PlayOneShot(clip);
    }
/// <summary>
/// pon un sonido que se repite constantemente
/// </summary>
/// <param name="clip"></param>
/// <param name="source"></param>
    public void PonerSonidoConLoop(AudioClip clip,AudioSource source)
    {
        if(audioSourcePlayer is null)
        audioSourcePlayer = GameObject.FindObjectsOfType<FirstPersonMovement>()
        .Where(s => s.GetComponent<NetworkObject>().IsOwner).First().GetComponent<AudioSource>();
            
        source.Stop();
        source.loop = true;
        source.clip = clip;
        source.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class voiceScript : MonoBehaviour
{

    [Header("Al recibir Dano")]
    public AudioSource hurtSound;
    public AudioClip[] hurtVoiceBank;

    [Header("Al curarse")]
    public AudioSource healSound;
    public AudioClip[] healVoiceBank;

    [Header("Al eliminar")]
    public AudioSource onKillSound;
    public AudioClip[] onKillVoiceBank;

    [Header("Al disparar")]
    public AudioSource shootSound;
    public AudioClip[] shootVoiceBank;

    [Header("Habilidad")]
    public AudioSource skillSound;
    public AudioClip[] skillVoiceBank;

    [Header("Recarga")]
    public AudioSource reloadSound;
    public AudioClip[] reloadVoiceBank;

    [Header("Ultimate")]
    public AudioSource ultimateSound;
    public AudioClip[] ultimateVoiceBank;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void voiceDamage() => hurtSound.clip = hurtVoiceBank[Random.Range(0, hurtVoiceBank.Length)];
    void voiceHeal() => healSound.clip = healVoiceBank[Random.Range(0, healVoiceBank.Length)];
    void voiceOnKill() => onKillSound.clip = onKillVoiceBank[Random.Range(0, onKillVoiceBank.Length)];
    void voiceOnShoot() => shootSound.clip = shootVoiceBank[Random.Range(0, shootVoiceBank.Length)];
    void voiceSkill() => skillSound.clip = skillVoiceBank[Random.Range(0, skillVoiceBank.Length)];
    void voiceReload() => reloadSound.clip = reloadVoiceBank[Random.Range(0, reloadVoiceBank.Length)];
    void voiceUltimate() => ultimateSound.clip = ultimateVoiceBank[Random.Range(0, ultimateVoiceBank.Length)];

}

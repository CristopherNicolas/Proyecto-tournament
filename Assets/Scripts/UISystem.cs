using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
public class UISystem : MonoBehaviour
{
    public static UISystem uISystem;
    public TMP_Text feed,banderasRedText,banderasBlueText;
    public Image teamDistintive;

    private void Awake()
    {
        if (uISystem==null)
        {
            uISystem = this;
        }
    }
    IEnumerator BorrarTexto(float tiempo)
    {
        yield return new WaitForSecondsRealtime(tiempo);
        feed.text = "";
        yield break;
    }
    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(3);
        feed.text = "";
    }

    public void ShowMessajeUI(string text)
    {
        feed.text = text;
        StartCoroutine(BorrarTexto(3));
    }
    [ClientRpc] 
    public void ShowMessajeUIClientRpc(FixedString128Bytes text)
    {
        feed.text = text.ConvertToString();
        StartCoroutine(BorrarTexto(3));
    }
    [ClientRpc]
    public void UpdateBanderasClientRpc(int redFlags,int blueFlags)
    {
        GameObject.Find("tba").GetComponent<TMP_Text>().text=  blueFlags.ToString();
        GameObject.Find("tbr").GetComponent<TMP_Text>().text = redFlags.ToString();
    }
    [ClientRpc]public void ResetUIClientRpc()
    {
        banderasBlueText.text = "0";
        banderasRedText.text = "0";
        Partida.instance.textTicketsBlue.text = "30";
        Partida.instance.textTicketsRed.text = "30";
    }
}
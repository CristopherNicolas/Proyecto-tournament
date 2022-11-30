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
    public TMP_Text feed;
    public Image teamDistintive;
    

    private void Awake()
    {
        if (uISystem==null)
        {
            uISystem = this;
        }
    }
    private IEnumerator Start()
    {
        yield return new WaitForSecondsRealtime(3);
        feed.text = "";
    }

    public void ShowMessajeUI(string text)
    {
        feed.text = text;
    }
    [ClientRpc] 
    public void ShowMessajeUIClientRpc(FixedString128Bytes text)
    {
        feed.text = text.ConvertToString();
    }
}
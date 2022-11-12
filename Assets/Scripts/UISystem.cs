using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
public class UISystem : MonoBehaviour
{
    public static UISystem uISystem;
    public TMP_Text feed;

    private void Awake()
    {
        if (uISystem==null)
        {
            uISystem = this;
        }
    }
    public void ShowMessajeUI(string text)
    {
        feed.text = text;
    }
}
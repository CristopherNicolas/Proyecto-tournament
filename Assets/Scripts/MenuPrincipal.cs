using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
public class MenuPrincipal : MonoBehaviour
{
    private IEnumerator Start()
    {
        GameObject.Find("Enter").GetComponent<Button>
           ().onClick.AddListener(() => {
               GameObject.Find("Enter").GetComponent<RectTransform>
                ().DOScale(Vector3.zero, 0.4f).WaitForCompletion();
           });
        var rtPanelOpciones = GameObject.Find("Panel").GetComponent<RectTransform>();
        var rtOpciones = GameObject.Find("Opciones").GetComponent<RectTransform>();
        GameObject.Find("Opciones").GetComponent<Button>()
            .onClick.AddListener(() => {
                rtPanelOpciones.DOScale(rtPanelOpciones.localScale.x > 0 ? Vector3.zero
                    : Vector3.one, 0.5f);
            });
                yield break;
    }
}

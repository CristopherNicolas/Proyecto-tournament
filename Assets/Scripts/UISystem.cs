using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;
public class UISystem : MonoBehaviour
{
    public static UISystem uISystem;

    Image marcaIzq, marcaDer, marcaArriba, marcaAtras;
    /// <summary>
    /// sistema que muestra de donde se recibe el daño
    /// </summary>
    /// <param name="dir"></param>
    public async void MostrarDano(DIR dir)
    {
         
        switch (dir)
        {
            case DIR.izq: await marcaIzq.DOColor(new Color(1, 1, 1, 1),0.3f).AsyncWaitForCompletion();
                break;
            case DIR.der:
                await marcaDer.DOColor(new Color(1, 1, 1, 1), 0.3f).AsyncWaitForCompletion();
                break;
            case DIR.arriba:
                await marcaArriba.DOColor(new Color(1, 1, 1, 1), 0.3f).AsyncWaitForCompletion();
                break;
            case DIR.abajo:
                await marcaAtras.DOColor(new Color(1, 1, 1, 1), 0.3f).AsyncWaitForCompletion();
                break;
            default:  return;
        }

    }
}
public enum DIR 
{
    izq,der,arriba,abajo
}
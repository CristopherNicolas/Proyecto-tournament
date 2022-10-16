using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System.Threading.Tasks;
public class UISystem : MonoBehaviour
{
    public static UISystem uISystem;
    private void Awake()
    {
        if (uISystem==null)
        {
            uISystem = this;
        }
    }
}
public enum DIR 
{
    izq,der,arriba,abajo
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Bandera : NetworkBehaviour
{
    public string colorBandera;
    public bool estaFueraDeBase = false;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("red") && colorBandera == "blue")
        {
            //azul captura la bandera roja
            estaFueraDeBase=true;
            transform.parent = other.transform;
            UISystem.uISystem.ShowMessajeUI("bandera azul atrapada");
        }
        else if(other.CompareTag("blue")&& colorBandera == "red")
        {
           // red captura la bandera azul;
           estaFueraDeBase=true;
            transform.parent = other.transform;
            UISystem.uISystem.ShowMessajeUI("bandera roja atrapada");
        }
        else if(other.CompareTag("blue") && colorBandera == "blue"&& estaFueraDeBase)
        {
            transform.parent = other.transform;
            UISystem.uISystem.ShowMessajeUI("bandera azul recuperada");
        }
        else if (other.CompareTag("red") && colorBandera == "red" && estaFueraDeBase)
        {
            transform.parent = other.transform;
            UISystem.uISystem.ShowMessajeUI("bandera roja recuperada");
        }

    }
    
}

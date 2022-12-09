using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class Bandera : NetworkBehaviour
{
    public string colorBandera;
    public bool estaFueraDeBase = false;
    protected Vector3 startPos;
    GameObject other;
    private void Start() => startPos = transform.position;
    
    private void OnTriggerEnter(Collider otherC)
    {
        if (!IsOwner) return;
        if(otherC.GetComponent<FirstPersonMovement>() )
        {
            other = otherC.gameObject;
            AtraparBanderaServerRpc();
        }
        if (otherC.CompareTag("baseRed") && colorBandera is "blue")
            CapturarBanderaServerRpc(true);
        else if (otherC.CompareTag("baseBlue") && colorBandera is "red")
            CapturarBanderaServerRpc(false);
        int rojas = Partida.instance.banderasRojasCapturadas.Value, azules = Partida.instance.banderasAzulesCapturadas.Value;
        UISystem.uISystem.UpdateBanderasClientRpc(rojas, azules);
        Debug.Log($"azules: {Partida.instance.banderasAzulesCapturadas.Value} , rojas: {Partida.instance.banderasRojasCapturadas.Value}");


    }
    [ServerRpc] void AtraparBanderaServerRpc()
    {
        if (other.CompareTag("red") && colorBandera == "blue")
        {
            //azul captura la bandera roja
            estaFueraDeBase = true;
            transform.parent = other.transform;
            UISystem.uISystem.ShowMessajeUIClientRpc("bandera azul atrapada");
        }
        else if (other.CompareTag("blue") && colorBandera == "red")
        {
            // red captura la bandera azul;
            estaFueraDeBase = true;
            transform.parent = other.transform;
            UISystem.uISystem.ShowMessajeUIClientRpc("bandera roja atrapada");
        }
        else if (other.CompareTag("blue") && colorBandera == "blue" && estaFueraDeBase)
        {
            transform.parent = other.transform;
            UISystem.uISystem.ShowMessajeUIClientRpc("bandera azul recuperada");
        }
        else if (other.CompareTag("red") && colorBandera == "red" && estaFueraDeBase)
        {
            transform.parent = other.transform;
            UISystem.uISystem.ShowMessajeUIClientRpc("bandera roja recuperada");
        }
    }
    
    [ServerRpc]
    void CapturarBanderaServerRpc(bool isRedFlag)
    {
        //poner la bandera nuevamente en su posicion inicial
        transform.SetParent(null);
        transform.position = startPos;
        if (isRedFlag) Partida.instance.banderasRojasCapturadas.Value+=1;
        else Partida.instance.banderasAzulesCapturadas.Value+=1;
        estaFueraDeBase = false;
            
    }
}

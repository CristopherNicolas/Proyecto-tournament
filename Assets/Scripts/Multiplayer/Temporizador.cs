using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Relay;
using Unity.Collections;
public class Temporizador : NetworkBehaviour
{
    public TMP_Text tiempoTexto;
    NetworkVariable<int> tiempo = new NetworkVariable<int>
        (readPerm: NetworkVariableReadPermission.Everyone,
        writePerm: NetworkVariableWritePermission.Owner);

    private IEnumerator Start()
    {
        if(IsHost)tiempo.Value = 0;
        //yield return new WaitUntil(() => GetComponent<Partida>().haComenzadoLaPartida);
        while (true)
        {
            if (IsHost) tiempo.Value++;
                tiempoTexto.text = tiempo.Value.ToString();
                  yield return new WaitForSecondsRealtime(1);
                    
        }                       
    }
  

}

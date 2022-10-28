using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;
using System.Threading.Tasks;
using Unity.Netcode;
public class Temporizador : NetworkBehaviour
{
    public TMP_Text tiempoTexto;
    NetworkVariable<int> tiempo = new NetworkVariable<int>
        (readPerm: NetworkVariableReadPermission.Everyone,
        writePerm: NetworkVariableWritePermission.Owner);

    private IEnumerator Start()
    {
        //yield return new WaitUntil(() => GetComponent<Partida>().haComenzadoLaPartida);
        if (IsHost) 
            while (true)
        {
            yield return new WaitForSeconds(1);
            tiempo.Value += 1;
            tiempoTexto.text = tiempo.Value.ToString();
        }
        else
        {
            while (true)
            {
                yield return new WaitForEndOfFrame();
                tiempoTexto.text = tiempo.Value.ToString();
            }

        }
    }
}

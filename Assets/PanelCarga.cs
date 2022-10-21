using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;
using System.Threading.Tasks;
using Unity.Netcode;
public class PanelCarga : NetworkBehaviour
{
    public List<Image> imagenes;
    public List<Sprite> splashArts;
    TMP_Text mensaje;
    public void AsignarPersonajeAlLobby(string nombrePersonaje)
    {
      if(NetworkManager.Singleton.ConnectedClientsList.Count<6)
        {
            imagenes[NetworkManager.Singleton.ConnectedClientsList.Count]
                .sprite = splashArts.Where(z => z.name == nombrePersonaje).First();
            mensaje.text = NetworkManager.Singleton.ConnectedClientsList.Count.ToString();
        }
    }

}

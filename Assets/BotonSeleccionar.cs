using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonSeleccionar : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(
            () =>
            {
                GameManager.instance.
            });
    }
}

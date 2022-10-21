using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class weaponSwitching : NetworkBehaviour
{
    public int selectedWeapon = 0;
    // Start is called before the first frame update
    void Start()
    {
        chooseWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner)
        {
            return;
            //psible bug  de cambio de arma aqui, quiza hay que desactivar componenete
        }
        int previousSelectedWeapon = selectedWeapon;

        #region rueda del mouse
        //Rueda del mouse para cambiar el arma
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
            {
                selectedWeapon = 0;
            }
            else
            {
                selectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
            {
                selectedWeapon = transform.childCount - 1;
            }
            else
            {
                selectedWeapon--;
            }
        }
        #endregion

        #region teclado numerico superior
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >=2)
        {
            selectedWeapon = 1;
        }

        //Si se incluyen más armas

        /*if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && transform.childCount >= 4)
        {
            selectedWeapon = 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && transform.childCount >= 5)
        {
            selectedWeapon = 4;
        }*/
        #endregion

        //Revisa el arma activa para el cambio a traves del valor
        if (previousSelectedWeapon != selectedWeapon)
        {
            chooseWeapon();
        }
    }

    void chooseWeapon() //activa y desactiva las armas (para la seleccionar una arma activa)
    {
        if (!IsOwner) return;
        int i = 0;
        foreach (Transform weapon in transform) //Revisa el transforms de los childs con el transform actual
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }
}

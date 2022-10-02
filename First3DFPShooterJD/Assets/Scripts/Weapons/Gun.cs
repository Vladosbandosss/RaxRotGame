using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public int currentAmmo;
    public int pickUpAmount;
    public Transform firePoint;

    public void GetAmmo()
    {
        currentAmmo += pickUpAmount;

        UIController.Instance.ammoTxt.text = currentAmmo.ToString();
    }
}

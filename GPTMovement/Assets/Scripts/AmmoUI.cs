using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AmmoUI : MonoBehaviour
{
    public int maxAmmo = 30;
    public int currentAmmo;
    public Text ammoText;

    public GunScript gunScript;

    public float ReloadTime;

    void Start()
    {
        currentAmmo = maxAmmo;

        ammoText.text = maxAmmo.ToString();

        ReloadTime = GameObject.Find("WPN_AKM").GetComponent<GunScript>().reloadTime;
    }

    public void showAmmo(int ammoToTake)
    {
        currentAmmo -= ammoToTake;
        ammoText.text = currentAmmo.ToString();
    }

    public IEnumerator setToMax()
    {
        currentAmmo = maxAmmo;
        yield return new WaitForSeconds(ReloadTime);
        ammoText.text = currentAmmo.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    public GameObject shopPanel;
    public AudioSource ClickSound;
    public AudioSource OpenShopSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            OpenShop();

    }

    void OpenShop()
    {
        shopPanel.SetActive(true);
        OpenShopSound.Play();
        Time.timeScale = 0;
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        ClickSound.Play();
        OpenShopSound.Play();
        Time.timeScale = 1;
    }
}

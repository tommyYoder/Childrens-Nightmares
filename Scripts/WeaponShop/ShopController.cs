using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    public GameObject shopPanel;
    public AudioSource ClickSound;
    public AudioSource OpenShopSound;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))               // If player collides with shop, then OpenShop is set to true.
            OpenShop();

    }

    void OpenShop()                                             // Shop panel is set to true, open sound plays, and time scale is set to 0.
    {
        shopPanel.SetActive(true);
        OpenShopSound.Play();
        Time.timeScale = 0;
    }

    public void CloseShop()                                    // If player hits exit, then shop panel is set to false, click sound plays, open shop plays, and time scale is set to 1.
    {
        shopPanel.SetActive(false);
        ClickSound.Play();
        OpenShopSound.Play();
        Time.timeScale = 1;
    }
}

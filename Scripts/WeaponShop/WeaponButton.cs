using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompleteProject
{
    public class WeaponButton : MonoBehaviour
    {
        public PlayerShooting playerShooting;
        public AudioSource BuySound;

        public int weaponNumber;

        public Text name;
        public Text cost;
        public Text description;

        public AudioSource ErrorSound;
        public AudioSource EquipGunSound;

        // Use this for initialization
        void Start()
        {
             GetComponent<AudioSource>();                                              // Looks for audio source on canvas and sets the buttons when Shop is true. 
            SetButton();
        }
        void SetButton()                                                              // Displays the text outlined on each button.
        {
            string costString = playerShooting.weapons[weaponNumber].cost.ToString();
            name.text = playerShooting.weapons[weaponNumber].name;
            cost.text = "$" + playerShooting.weapons[weaponNumber].cost;
            description.text = playerShooting.weapons[weaponNumber].description;
        }

        public void OnClick()                                                        // If score manager is greater or equal to weapon cost. Player is able to buy that weapon along with that weapon being updated on the player. Sounds will play.
        {
            if (ScoreManager.score >= playerShooting.weapons[weaponNumber].cost)
            {

                ScoreManager.score -= playerShooting.weapons[weaponNumber].cost;
                playerShooting.currentWeapon = weaponNumber;
                BuySound.Play();
                EquipGunSound.Play();


            } 
            else                                                                   // If score manager is less than weapon cost, then error sound will play and you will not be allowed to but that weapon.
            {
                ErrorSound.Play();
            }
        }

    }

}

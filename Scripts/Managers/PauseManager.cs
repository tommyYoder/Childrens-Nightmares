using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace CompleteProject {
    public class PauseManager : MonoBehaviour
    {

        public AudioMixerSnapshot paused;
        public AudioMixerSnapshot unpaused;
        public AudioSource OpenMenuSound;

        Canvas canvas;

        void Start()
        {
            canvas = GetComponent<Canvas>();              // Look for the pause canvas. 
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))         // If escape if pressed, then canvas is either set to true or false along with sound playing each time. Pause gets called when escape is pressed. 
            {
                canvas.enabled = !canvas.enabled;
                Pause();
                OpenMenuSound.Play();

            }
        }

        public void Pause()                             // If true or false, time scalse shifts from 0 to 1 and Low Pass is called when true. 
        {
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
            Lowpass();

        }

        void Lowpass()
        {
            if (Time.timeScale == 0)      // If true, then low pass lowers sound to .01.
            {
                paused.TransitionTo(.01f);
            }

            else                        // If false, then low pass thurns to false and sound goes back to normal.

            {
                unpaused.TransitionTo(.01f);
            }
        }

        public void Quit()    // If quite is pressed, then the game application ends.
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
        }
    }
}

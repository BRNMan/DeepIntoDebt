using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Image soundButtonImage;
    public Sprite soundOffSprite, soundOnSprite;

    private bool isSoundOn;

    // Start is called before the first frame update
    void Start()
    {
        isSoundOn = true;
        PlayerPrefs.SetString("isSoundOn", "true");
    }

    public void OnToggleSound() {
        isSoundOn = !isSoundOn;
        if(isSoundOn) {
            PlayerPrefs.SetString("isSoundOn", "true");
            soundButtonImage.sprite = soundOnSprite;
        } else {
            PlayerPrefs.SetString("isSoundOn", "false");
            soundButtonImage.sprite = soundOffSprite;
        }
    }

    public void OnPlay() {
        //Load game scene
    }

    public void OnExit() {
     // save any game data here
     #if UNITY_EDITOR
         // Application.Quit() does not work in the editor so
         // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
         UnityEditor.EditorApplication.isPlaying = false;
     #else
         Application.Quit();
     #endif
    }
}

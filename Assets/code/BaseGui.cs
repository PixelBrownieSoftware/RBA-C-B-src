using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BaseGui : MonoBehaviour {


    protected bool createdButtons;
    protected List<GameObject> buttons = new List<GameObject>();

    protected List<GameObject> guis = new List<GameObject>();

    public GameObject button;
    

    public void createButtons() {
    }

    public void PlaySelectSound() {
        SoundManager.sfx.PlaySound(Resources.Load("sfx/Select3") as AudioClip);
    }

    public void PlayBuySound() {
        SoundManager.sfx.PlaySound(Resources.Load("sfx/spend money") as AudioClip);
    }

    public void PlayDeclineSound() {
        SoundManager.sfx.PlaySound(Resources.Load("sfx/sound_decline") as AudioClip);
    }

    public void deleteButtons()
    {
        foreach (GameObject buttn in buttons)
        {
            Destroy(buttn.gameObject);
        }
        createdButtons = false;
    }

    public void deleteGUIs()
    {
        foreach (GameObject gui in guis)
        {
            Destroy(gui.gameObject);
        }
        createdButtons = false;
    }
}

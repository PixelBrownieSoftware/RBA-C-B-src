using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class MainMenu : BaseGui {

    public int menuchoice = 0;
    public static bool is_loaded;
    public Image fade;
    public Sprite[] images;
    public Image instrction;
    bool instructionson;

    public Text txt;
    const string inst = " - Press Z";
    enum gamestate {
        loadingin ,menu, loading
    }
    gamestate states = gamestate.loadingin;

    private void Start()
    {
        fade.GetComponent<Animator>().SetBool("fadein", false);
        fade.GetComponent<Animator>().SetBool("isfading", true);
    }

    void Update () {
        switch (states) {

            case gamestate.loadingin:
                if (!fade.GetComponent<Animator>().GetBool("isfading")) {
                    states = gamestate.menu;
                }
                break;

            case gamestate.menu:
                if (Input.GetButtonDown("Horizontal")) {
                    menuchoice += CheckKeyinput();
                    menuchoice %= 3;
                    if (menuchoice < 0) {
                        menuchoice = 2;
                    }
                }
                instrction.enabled = false;
                instructionson = false;

                if (Input.GetKeyDown(KeyCode.Z)) {
                    if (menuchoice == 0) {
                        PlaySelectSound();
                        is_loaded = false;
                        fade.GetComponent<Animator>().SetBool("isfading", true);
                        fade.GetComponent<Animator>().SetBool("fadein", true);
                        states = gamestate.loading;
                    }
                    if (menuchoice == 1)
                    {
                        PlaySelectSound();
                        instructionson = true;
                        menuchoice = 0;
                        states = gamestate.loading;
                    }
                    if (menuchoice == 2) {
                        if (File.Exists("save.RB"))
                        {
                            is_loaded = true;
                            PlaySelectSound();
                            fade.GetComponent<Animator>().SetBool("isfading", true);
                            fade.GetComponent<Animator>().SetBool("fadein", true);
                            states = gamestate.loading;
                        }
                        else {
                            PlayDeclineSound();
                        }
                    }
                }
                break;
            case gamestate.loading:
                if (instructionson) {

                    if (Input.GetButtonDown("Horizontal"))
                    {
                        menuchoice += CheckKeyinput();
                        menuchoice %= 4;
                        if (menuchoice < 0) {
                            menuchoice = 3;
                        }
                    }
                    instrction.enabled = true;

                    if (Input.GetKeyDown(KeyCode.X))
                    {
                        states = gamestate.menu;
                    }
                    instrction.sprite = images[menuchoice];
                }

                break;
        }
        displayText();
	}

    void displayText()
    {
        if (!instructionson) {
            if (menuchoice == 0) {
                txt.text = "New Game" + inst;
            }

            if (menuchoice == 1) {
                txt.text = "Instructions" + inst;
            }

            if (menuchoice == 2) {
                if (File.Exists("save.RB")) {
                    txt.text = "Load Game" + inst;
                } else {
                    txt.text = "No Save";
                }
            }
        } else {
            txt.text = "                                                                        X to quit";
        }
    }

    int CheckKeyinput() {
        return (int)Input.GetAxisRaw("Horizontal");
    }

}

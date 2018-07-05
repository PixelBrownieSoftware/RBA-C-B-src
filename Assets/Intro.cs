using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Intro : MonoBehaviour {

    public Image credits;

    private void Start()
    {

        Screen.SetResolution(1280, 720, false);
    }

    public void GoToGame() {
        SceneManager.LoadScene("Title");
    }

    public void Playsound() {
        GetComponent<AudioSource>().Play();
    }

    public void GoToRPG()
    {
        SceneManager.LoadScene("rpgdemo");
    }

    public void DoneFading() {
        GetComponent<Animator>().SetBool("isfading", false);
    }

    public void EndingText1() {
        GetComponent<Text>().text = "And so, Redler and his friends have conquered the Evil King Malculus and gave the land he taken over back to the former King: Lord Blue.";
    }

    public void EndingText2()
    {
        GetComponent<Text>().text = "Months later Lord Blue's kingdom managed to rebuild itself... "+"\n"+"Redler and his freinds disbanded to seek other jobs...";
    }

    public void EndingText3() {
        GetComponent<Text>().text = "Little did they know that the kingdom was still open to other, worse threats.";

    }
    public void EndingText4() {
        GetComponent<Text>().text = "The End...?";
    }

    public void EndingText5() {
        GetComponent<Text>().text = "Credits:";
    }

    public void EndingText6()
    {
        GetComponent<Text>().text = "Art, sound, programing:"+ "\n" +"Pixel Brownie Software (Hamza)";
    }

    public void EndingText7()
    {
        GetComponent<Text>().text = "Playtesting:" + "\n" + "FelixFFK" + "\n" + "klszewski2001" + "\n" + "Sami Kassim";
    }
    public void EndingText8()
    {
        credits.enabled = true;
        GetComponent<Text>().text = "RedBlue Adventures: Crash and Brawl" + "\n" + "\n" + "Created by Pixel Brownie Software 2017 - 2018.";
    }
}

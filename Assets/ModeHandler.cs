using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeHandler : BaseGui {

    public ShopClass ShopMenu;
    public static int money;
    public static ModeHandler modehandler;

    public List<Sprite> backdrops = new List<Sprite>();

    public List<PlayerCharacter> party = new List<PlayerCharacter>();
    public Text moneyTxt;
    public Text resultsText;
    int bg_num = 0;

    public AudioSource audio;

    public GameObject status;

    public GameObject BG;

    public GameObject saveObj;
    public AudioClip[] bgsounds;


    public List<string> statusLetters = new List<string>();

    private void Awake()
    {
        audio.Play();
        if (modehandler == null)
        {
            modehandler = this;
        }
        if (MainMenu.is_loaded) { load(); }
    }

    private void OnGUI()
    {
        moneyTxt.text = "   : " + money + "";
        money = money < 0 ? 0 : money;
    }

    public void ChangeBackGround()
    {
        if (battlehandler.BSM.eGen.currentPhase > 4 && bg_num == 0)
        {
            audio.Stop();
            bg_num++;
            audio.clip = bgsounds[bg_num];
            audio.Play();
        }
        if (battlehandler.BSM.eGen.currentPhase > 7 && bg_num == 1)
        {
            audio.Stop();
            bg_num++;
            audio.clip = bgsounds[bg_num];
            audio.Play();
        }
        if (battlehandler.BSM.eGen.currentPhase > 10 && bg_num == 2)
        {
            audio.Stop();
            bg_num++;
            audio.clip = bgsounds[bg_num];
            audio.Play();
        }
        BG.GetComponent<SpriteRenderer>().sprite = backdrops[bg_num];
    }

    public void changeToHubMode() {
        PlaySelectSound();
        ShopMenu.gameObject.SetActive(false);
        afterMenuSelect();
    }

    public void changeToShop() {
        PlaySelectSound();
        deleteButtons();
        ShopMenu.gameObject.SetActive(true);
        ShopMenu.shopMenuIntitalize();
    }

    public void changeToBattle() {
        ChangeBackGround();
        deleteButtons();
        battlehandler.BSM.gameObject.SetActive(true);
        ShopMenu.gameObject.SetActive(false);
    }

    public void changeToStatus()
    {
        PlaySelectSound();
        statusLetters.Clear();
        party.Clear();
        GameObject[] playersToAdd = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playersToAdd)
        {
            party.Add(player.GetComponent<PlayerCharacter>());
        }
        deleteButtons();
        showStatus();
    }

    public void afterMenuSelect() {

        ShopMenu.shopMenuIntitalize();

        GameObject shopButton = Instantiate(button, new Vector3(90, 90, 3), Quaternion.identity) as GameObject;
        shopButton.GetComponent<Button>().onClick.AddListener(() => changeToShop());
        Text buttonTxt = shopButton.transform.Find("Text").GetComponent<Text>();
        buttonTxt.text = "Shop";
        shopButton.transform.SetParent(this.transform);
        shopButton.GetComponent<Image>().gameObject.transform.position = new Vector3(90, 90, 3);
        buttons.Add(shopButton);

        GameObject statusButton = Instantiate(button, new Vector3(260, 90, 3), Quaternion.identity) as GameObject;
        statusButton.GetComponent<Button>().onClick.AddListener(() => battlehandler.BSM.RetryBattle());
        Text statsbuttonTxt = statusButton.transform.Find("Text").GetComponent<Text>();
        statsbuttonTxt.text = "Next Battle";
        statusButton.transform.SetParent(this.transform);
        statusButton.GetComponent<Image>().gameObject.transform.position = new Vector3(260, 90, 3);
        buttons.Add(statusButton);

        GameObject retryButton = Instantiate(button, new Vector3(430, 90, 3), Quaternion.identity) as GameObject;
        retryButton.GetComponent<Button>().onClick.AddListener(() => changeToStatus());
        Text buttonTxt2 = retryButton.transform.Find("Text").GetComponent<Text>();
        buttonTxt2.text = "Status";
        retryButton.transform.SetParent(this.transform);
        retryButton.GetComponent<Image>().gameObject.transform.position = new Vector3(430, 90, 3);
        buttons.Add(retryButton);

        if (GameObject.Find("EnemyAdder").GetComponent<EnemyGenerator>().Phase.isBoss && !GameObject.Find("EnemyAdder").GetComponent<EnemyGenerator>().bossDead)
        {
            GameObject bossButton = Instantiate(button, new Vector3(595, 90, 3), Quaternion.identity) as GameObject;
            bossButton.GetComponent<Button>().onClick.AddListener(() => battlehandler.BSM.bossBattle());
            Text bossButtontxt = bossButton.transform.Find("Text").GetComponent<Text>();
            bossButtontxt.text = "Fight Boss";
            bossButton.transform.SetParent(this.transform);
            bossButton.GetComponent<Image>().gameObject.transform.position = new Vector3(595, 90, 3);
            buttons.Add(bossButton);

            GameObject saveButton = Instantiate(button, new Vector3(760, 90, 3), Quaternion.identity) as GameObject;
            saveButton.GetComponent<Button>().onClick.AddListener(() => save());
            Text savebuttonTxt = saveButton.transform.Find("Text").GetComponent<Text>();
            savebuttonTxt.text = "Save";
            saveButton.transform.SetParent(this.transform);
            saveButton.GetComponent<Image>().gameObject.transform.position = new Vector3(760, 90, 3);
            buttons.Add(saveButton);
        }
        else
        {
            GameObject saveButton = Instantiate(button, new Vector3(595, 90, 3), Quaternion.identity) as GameObject;
            saveButton.GetComponent<Button>().onClick.AddListener(() => save());
            Text savebuttonTxt = saveButton.transform.Find("Text").GetComponent<Text>();
            savebuttonTxt.text = "Save";
            saveButton.transform.SetParent(this.transform);
            saveButton.GetComponent<Image>().gameObject.transform.position = new Vector3(595, 90, 3);
            buttons.Add(saveButton);
        }

    }

    public void save()
    {
        PlaySelectSound();
        GameObject sav = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        sav.AddComponent<Save>().SaveData();
    }

    public void load()
    {
        GameObject sav = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        sav.AddComponent<Save>().LoadData();
    }

    void showStatus() {
        string disp = "";
        float x = 410;

        foreach (PlayerCharacter character in party) {
            x += 150;

            GameObject go = Instantiate(status);
            go.transform.position = new Vector2(x, 580);
            go.transform.SetParent(GameObject.Find("GUI").transform);

            if (character.name == "Redler") {
                go.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(1f, 0.3f, 0.12f);
            }

            if (character.name == "Blueler") {
                go.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.9f);
            }

            if (character.name == "Greendori") {
                go.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(0.6f, 1f, 0.5f);
            }

            if (character.name == "Buckler") {
                go.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(0.9f, 0.6f, 0.2f);
            }

            Text txt = go.transform.Find("GUITEXT").GetComponent<Text>();
            txt.text = character.name.ToString() + "\n" + "      : " + character.attack + "\n" + "      : " + character.defence + "\n" + "      : " + character.intelligence + "\n" + "Exp : " + character.exp + " / " + "\n" + character.exptoNextLevel + "\n";

            guis.Add(go);
        }
        
        foreach (string msg in statusLetters)
        {
            disp = disp.ToString() + msg.ToString();
        }
        resultsText.text = disp;

        GameObject retryButton = Instantiate(button, new Vector3(230, 90, 3), Quaternion.identity) as GameObject;
        retryButton.GetComponent<Button>().onClick.AddListener(() => backToMenu());
        Text buttonTxt2 = retryButton.transform.Find("Text").GetComponent<Text>();
        buttonTxt2.text = "Back";
        retryButton.transform.SetParent(this.transform);
        retryButton.GetComponent<Image>().gameObject.transform.position = new Vector3(90, 140, 3);
        buttons.Add(retryButton);
    }

    void backToMenu()
    {
        PlaySelectSound();
        deleteGUIs();
        resultsText.text = "";
        deleteButtons();
        changeToHubMode();

        createdButtons = false;
    }
    
    void Start ()
    {
        changeToBattle();
	}
	
}

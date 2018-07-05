using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;

[System.Serializable]
public struct Move {
    public int price;
    public string character;
    public AttackBase move;
}

[System.Serializable]
public struct Character {
    public int price;
    public string characterToBuy;
}

[System.Serializable]
public struct LevelRequirementsShop {
    public int maxLevel;
    public List<Character> charsToAdd;
    public List<Move> movesToAdd;
    public List<FoodStuffs> foodToAdd;
}

public class ShopClass : BaseGui {

    private FoodStuffs Food;
    private Move move;
    private Character character;

    public List<FoodStuffs> foodlist = new List<FoodStuffs>();
    public List<Move> moveList = new List<Move>();
    public List<Character> characterList = new List<Character>();

    private bool isAdded = false;

    public int levelReqSelection;
    public List<LevelRequirementsShop> levelReq;
    public List<PlayerCharacter> party = new List<PlayerCharacter>();

    private List<string> letters = new List<string>();
    public Text shopDisplayText;

    public void AddStuffFromList(List<string> food, List<Move> moves, List<Character> character)
    {
        foodlist.Clear();
        characterList.Clear();
        moveList.Clear();

        for (int i = 0; i < food.Count; i++) {
            GameObject foodit = Resources.Load("Foodstuffs/" + food[i]) as GameObject;
            foodlist.Add(foodit.GetComponent<FoodStuffs>());
        }

        for (int i = 0; i < moves.Count; i++) {
            moveList.Add(moves[i]);
        }

        for (int i = 0; i < character.Count; i++) {
            characterList.Add(character[i]);
        }
    }

    public enum shopMenuCharOptions {
        main,
        characters
    }

    public enum ShopMenuCategory {
        main,
        food,
        characters,
        moves
    };
    public ShopMenuCategory shopMenu;
    public shopMenuCharOptions shopOptions;

    public void shopMenuIntitalize() {
        party.Clear();
        shopMenu = ShopMenuCategory.main;
        shopOptions = shopMenuCharOptions.main;

        GameObject[] playersToAdd = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playersToAdd)
        {
            party.Add(player.GetComponent<PlayerCharacter>());
        }
        if (levelReqSelection < levelReq.Count - 1) {
            if (party[0].level > levelReq[levelReqSelection].maxLevel) {
            levelReqSelection++;
            isAdded = false;
        }

        if (!isAdded) {
            if (levelReq[levelReqSelection].charsToAdd.Count > 0)
            {
                foreach (Character character in levelReq[levelReqSelection].charsToAdd)
                {
                    if (!CheckForSameCharacterName(character.characterToBuy)) { characterList.Add(character); }
                }
            }

            if (levelReq[levelReqSelection].movesToAdd.Count > 0)
            {
                foreach (Move move in levelReq[levelReqSelection].movesToAdd)
                {
                    if (!CheckForSameMoveName(move.move.name)) { moveList.Add(move); }  
                }
            }

            if (levelReq[levelReqSelection].foodToAdd.Count > 0)
            {
                foreach (FoodStuffs food in levelReq[levelReqSelection].foodToAdd)
                {
                    if (!CheckForSameFoodName(food.foodName)) { foodlist.Add(food); }
                }
            }
            isAdded = true;
        }
        }
        
    }

    bool CheckForSameCharacterName(string name) {
        foreach (BattleCharacter chara in battlehandler.BSM.players)
        {
            if (name == chara.name) { return true; } else { continue; }
        }
        foreach (Character chara in characterList)
        {
            if (name == chara.characterToBuy) { return true; } else { continue; }
        }
        return false;
    }

    bool CheckForSameFoodName(string name) {
        for (int i = 0; i < foodlist.Count; i++) {
            if (name != foodlist[i].foodName) { continue; }
            else { return true; }
        } return false;
    }

    bool CheckForSameMoveName(string name) {
        foreach (BattleCharacter player in party) {
            foreach (AttackBase move in player.moves) {
                if (move.name == name) {
                    return true;
                }
            }
        }
        for (int i = 0; i < moveList.Count; i++) {
            if (name != moveList[i].move.name) {continue;}
            else { return true; }
        } return false;
    }

    void OnGUI()
    {
        switch (shopMenu) {
            case ShopMenuCategory.main:
                letters.Clear();
                if (!createdButtons) {

                    GameObject foodButton = Instantiate(button, new Vector3(230, 90, 3), Quaternion.identity) as GameObject;
                    foodButton.GetComponent<Button>().onClick.AddListener(() => foodMenu());
                    Text foodbuttonTxt = foodButton.transform.Find("Text").GetComponent<Text>();
                    foodbuttonTxt.text = "Food";
                    foodButton.transform.SetParent(this.transform);
                    foodButton.GetComponent<Image>().gameObject.transform.position = new Vector3(230, 90, 3);
                    buttons.Add(foodButton);

                    GameObject backButton = Instantiate(button, new Vector3(60, 90, 3), Quaternion.identity) as GameObject;
                    backButton.GetComponent<Button>().onClick.AddListener(() => backToMenu());
                    Text backbuttonTxt = backButton.transform.Find("Text").GetComponent<Text>();
                    backbuttonTxt.text = "Back";
                    backButton.transform.SetParent(this.transform);
                    backButton.GetComponent<Image>().gameObject.transform.position = new Vector3(60, 90, 3);
                    buttons.Add(backButton);

                    GameObject charactersButton = Instantiate(button, new Vector3(370, 90, 3), Quaternion.identity) as GameObject;
                    charactersButton.GetComponent<Button>().onClick.AddListener(() => characterMenu());
                    Text charbuttonTxt = charactersButton.transform.Find("Text").GetComponent<Text>();
                    charbuttonTxt.text = "Characters";
                    charactersButton.transform.SetParent(this.transform);
                    charactersButton.GetComponent<Image>().gameObject.transform.position = new Vector3(370, 90, 3);
                    buttons.Add(charactersButton);

                    GameObject movesButton = Instantiate(button, new Vector3(510, 90, 3), Quaternion.identity) as GameObject;
                    movesButton.GetComponent<Button>().onClick.AddListener(() => movesMenu());
                    Text attackbuttonTxt = movesButton.transform.Find("Text").GetComponent<Text>();
                    attackbuttonTxt.text = "Moves";
                    movesButton.transform.SetParent(this.transform);
                    movesButton.GetComponent<Image>().gameObject.transform.position = new Vector3(510, 90, 3);
                    buttons.Add(movesButton);
                    createdButtons = true;
                }

                break;

            case ShopMenuCategory.food:
                foodMenuSystem();
                break;

            case ShopMenuCategory.characters:
                characterMenuSystem();
                break;

            case ShopMenuCategory.moves:
                moveMenuSystem();
                break;
        }    
    }


    /// <summary>
    /// MENU STYSTEMS
    /// 
    /// </summary>
    void characterMenuSystem()
    {
        letters.Clear();
        switch (shopOptions)
        {

            case shopMenuCharOptions.main:

                if (!createdButtons)
                {
                    int spacer_x = 170;
                    int initalpos = 90;
                    int xpos = initalpos;
                    int initialy = 90;

                    int ip = 0;
                    if (characterList.Count > 0) {
                        for (int i = 0; i < characterList.Count; i++)
                        {
                            int attacknumber = i;
                            GameObject attkButton = Instantiate(button, new Vector3(initalpos + ip * spacer_x, initialy, 3), Quaternion.identity) as GameObject;
                            attkButton.GetComponent<Image>().gameObject.transform.position = new Vector3(initalpos + ip * spacer_x, initialy, 3);
                            ip++;
                            if (attacknumber == 7) { initialy -= 40; attacknumber = 0; }

                            if (ModeHandler.money >= characterList[i].price) { attkButton.GetComponent<Button>().onClick.AddListener(() => getCharacterSelection(characterList[attacknumber])); }
                            xpos += spacer_x * attacknumber;
                            print("Select Food");

                            Text buttonTxt = attkButton.transform.Find("Text").GetComponent<Text>();
                            buttonTxt.text = characterList[i].characterToBuy + " £" + characterList[attacknumber].price;
                            attkButton.transform.SetParent(this.transform);

                            buttons.Add(attkButton);
                            
                        }

                    }

                    xpos += spacer_x;

                    GameObject foodButton = Instantiate(button, new Vector3(xpos, 90, 3), Quaternion.identity) as GameObject;
                    foodButton.GetComponent<Button>().onClick.AddListener(() => backToShop());
                    Text foodbuttonTxt = foodButton.transform.Find("Text").GetComponent<Text>();
                    foodbuttonTxt.text = "Back";
                    foodButton.transform.SetParent(this.transform);
                    foodButton.GetComponent<Image>().gameObject.transform.position = new Vector3(90, 140, 3);
                    buttons.Add(foodButton);

                    createdButtons = true;
                }
                
                break;

            case shopMenuCharOptions.characters:
                backToShop();
                break;
        }
    }


    void foodMenuSystem() {
        letters.Clear();
        switch (shopOptions)
        {
            case shopMenuCharOptions.main:

                if (!createdButtons)
                {
                    int spacer_x = 170;
                    int initalpos = 90;
                    int xpos = initalpos;
                    int ip = 0;
                    int initialy = 90;

                    for (int i = 0; i < foodlist.Count; i++)
                    {
                        int attacknumber = i;
                        GameObject attkButton = Instantiate(button, new Vector3(initalpos + ip * spacer_x, initialy, 3), Quaternion.identity) as GameObject;
                        attkButton.GetComponent<Image>().gameObject.transform.position = new Vector3(initalpos + ip * spacer_x, initialy, 3);
                        ip++;
                        if (ip == 7) { initialy -= 40; ip = 0; }

                        if (ModeHandler.money >= foodlist[i].price) { attkButton.GetComponent<Button>().onClick.AddListener(() => getFoodSelection(foodlist[attacknumber])); }
                        xpos += spacer_x * attacknumber;
                        print("Select Food");

                        Text buttonTxt = attkButton.transform.Find("Text").GetComponent<Text>();
                        buttonTxt.text = foodlist[i].name + " £" + foodlist[attacknumber].price;
                        attkButton.transform.SetParent(this.transform);

                        buttons.Add(attkButton);
                    }

                    string display = "";
                    foreach (string part in letters) {
                        display = part.ToString() + display.ToString() + "\n";
                    }
                    shopDisplayText.text = display;

                    
                    GameObject foodButton = Instantiate(button, new Vector3(90, 140, 3), Quaternion.identity) as GameObject;
                    foodButton.GetComponent<Button>().onClick.AddListener(() => backToShop());
                    Text foodbuttonTxt = foodButton.transform.Find("Text").GetComponent<Text>();
                    foodbuttonTxt.text = "Back";
                    foodButton.transform.SetParent(this.transform);
                    foodButton.GetComponent<Image>().gameObject.transform.position = new Vector3(90, 140, 3);
                    buttons.Add(foodButton);
                    createdButtons = true;
                }
                break;

            case shopMenuCharOptions.characters:

                if (!createdButtons)
                {
                    int spacer_x = 170;
                    int initalpos = 40;
                    int xpos = initalpos;

                    for (int i = 0; i < party.Count; i++)
                    {
                        int attacknumber = i;

                        GameObject attkButton = Instantiate(button, new Vector3(initalpos + i * spacer_x, 90, 3), Quaternion.identity) as GameObject;
                        attkButton.GetComponent<Image>().gameObject.transform.position = new Vector3(initalpos + i * spacer_x, 90, 3);
                        if (party[attacknumber].foodItem == null) { attkButton.GetComponent<Button>().onClick.AddListener(() => addFoodItem(party[attacknumber], Food)); }
                        xpos = initalpos + spacer_x * i;
                        shopDisplayText.text = Food.name + "\n" + "Attack + " + Food.attkInc + "\n" + "Defence + " + Food.defInc + "\n" + "Inteligence + " + Food.intInc + "\n" + "Health + " + Food.healthInc + "\n" + "Special Points + " + Food.spInc;
                        Text buttonTxt = attkButton.transform.Find("Text").GetComponent<Text>();

                        GameObject.Find("GUIShop").GetComponent<Image>().enabled = true;
                        buttonTxt.text = party[i].name;
                        attkButton.transform.SetParent(this.transform);
                        buttons.Add(attkButton);

                    }
                    xpos += spacer_x;
                    GameObject foodButton = Instantiate(button, new Vector3(90, 140, 3), Quaternion.identity) as GameObject;
                    foodButton.GetComponent<Button>().onClick.AddListener(() => backToShop());
                    Text foodbuttonTxt = foodButton.transform.Find("Text").GetComponent<Text>();
                    foodbuttonTxt.text = "Back";
                    foodButton.transform.SetParent(this.transform);
                    foodButton.GetComponent<Image>().gameObject.transform.position = new Vector3(90, 140, 3);
                    buttons.Add(foodButton);
                    createdButtons = true;
                }
                break;
        }
    }

    void moveMenuSystem()
    {
        letters.Clear();
        switch (shopOptions)
        {
            case shopMenuCharOptions.main:

                if (!createdButtons)
                {
                    int spacer_x = 170;
                    int initalpos = 90;
                    int xpos = initalpos;
                    int ip = 0;
                    int initialy = 90;

                    if (moveList.Count > 0)
                    {
                        for (int i = 0; i < moveList.Count; i++)
                        {
                            int attacknumber = i;
                            GameObject attkButton = Instantiate(button, new Vector3(initalpos + ip * spacer_x, initialy, 3), Quaternion.identity) as GameObject;
                            attkButton.GetComponent<Image>().gameObject.transform.position = new Vector3(initalpos + ip * spacer_x, initialy, 3);
                            if (ModeHandler.money >= moveList[i].price) { attkButton.GetComponent<Button>().onClick.AddListener(() => addMove(moveList[attacknumber])); }
                            
                            print("Select Food");
                            ip++;
                            if (ip == 7) { initialy -= 40; ip = 0;}
                            Text buttonTxt = attkButton.transform.Find("Text").GetComponent<Text>();
                            attkButton.GetComponent<Image>().color = CheckForCharacterColour(moveList[i].character);
                            buttonTxt.text = moveList[i].move.name + " £" + moveList[attacknumber].price;
                            attkButton.transform.SetParent(this.transform);

                            buttons.Add(attkButton);
                        }
                    }
                    
                    GameObject foodButton = Instantiate(button, new Vector3(-90, initialy, 3), Quaternion.identity) as GameObject;
                    foodButton.GetComponent<Button>().onClick.AddListener(() => backToShop());
                    Text foodbuttonTxt = foodButton.transform.Find("Text").GetComponent<Text>();
                    foodbuttonTxt.text = "Back";
                    foodButton.transform.SetParent(this.transform);
                    foodButton.GetComponent<Image>().gameObject.transform.position = new Vector3(90, 140, 3);
                    buttons.Add(foodButton);

                    createdButtons = true;
                }
                break;

           
        }
    }

    Color CheckForCharacterColour(string name) {
        if (name == "Redler") {
            return new Color(1f, 0.3f, 0.12f);
        }

        if (name == "Blueler") {
            return new Color(0.5f, 0.5f, 0.9f);
        }

        if (name == "Greendori") {
           return new Color(0.6f, 1f, 0.5f);
        }

        if (name == "Buckler") {
            return new Color(0.9f, 0.6f, 0.2f);
        }
        return Color.white;
    }


    /// <summary>
    /// BACK BUTTONS
    /// </summary>

    void backToShop()
    {
        GameObject.Find("GUIShop").GetComponent<Image>().enabled = false;
        PlaySelectSound();
        shopDisplayText.text = "";
        letters.Clear();
        shopMenu = ShopMenuCategory.main;
        shopOptions = shopMenuCharOptions.main;
        deleteButtons();

        createdButtons = false;
    }


    void backToMenu()
    {
        //PlaySelectSound();
        shopDisplayText.text = "";
        deleteButtons();
        ModeHandler.modehandler.changeToHubMode();

        createdButtons = false;
    }

    /// <summary>
    /// GET SELECTIONS
    /// </summary>
    /// <param name="character"></param>

    public void getCharacterSelection(Character character)
    {
        PlayBuySound();
        GameObject characterToCreate = Instantiate(Resources.Load("prefabs/Characters/Players/" + character.characterToBuy)) as GameObject;
        characterToCreate.name = character.characterToBuy;
        battlehandler.BSM.players.Add(characterToCreate.GetComponent<PlayerCharacter>());
        GameObject playersList = GameObject.Find("Players");

        party.Add(characterToCreate.GetComponent<PlayerCharacter>());
        characterToCreate.transform.SetParent(playersList.transform);
        characterToCreate.transform.position = new Vector2(party[0].transform.position.x, party[0].transform.position.y * party.Count + 1);
        PlayerCharacter playerchar = characterToCreate.GetComponent<PlayerCharacter>();
        playerchar.initPos = characterToCreate.transform.position;
        battlehandler.BSM.AddPlayerGUI(characterToCreate.GetComponent<PlayerCharacter>(), battlehandler.BSM.players.Count+1);
        ModeHandler.money -= character.price;

        characterList.Remove(character);
        shopOptions = shopMenuCharOptions.characters;
        deleteButtons();

        createdButtons = false;
    }


    public void getFoodSelection(FoodStuffs food) {

        PlaySelectSound();
        Food = food;
        shopOptions = shopMenuCharOptions.characters;
        print(food + "selected");
        deleteButtons();

        createdButtons = false;
    }

    void addFoodItem(PlayerCharacter playerchar, FoodStuffs foodItem) {

        if (playerchar.foodItem == null)
        {
            PlayBuySound();
            playerchar.foodItem = foodItem;
            playerchar.foodTurn = foodItem.duration;
            ModeHandler.money -= foodItem.price;
        }
        else {
            PlayDeclineSound();
        }
        GameObject.Find("GUIShop").GetComponent<Image>().enabled = false;
        shopMenu = ShopMenuCategory.main;
        shopOptions = shopMenuCharOptions.main;
        deleteButtons();

        createdButtons = false;
    }

    void addMove(Move selectedAttack)
    {
        if (GameObject.Find(selectedAttack.character) && GameObject.Find(selectedAttack.character).GetComponent<BattleCharacter>() != null)
        {

            PlayBuySound();
            GameObject.Find(selectedAttack.character).GetComponent<BattleCharacter>().moves.Add(selectedAttack.move);
            ModeHandler.money -= selectedAttack.price;
            moveList.Remove(selectedAttack);
            shopMenu = ShopMenuCategory.main;
            shopOptions = shopMenuCharOptions.main;
            deleteButtons();

            createdButtons = false;
        }
        else
        {
            PlayDeclineSound();
        }
    }

    public void foodMenu() {
        PlaySelectSound();
        deleteButtons();
        shopMenu = ShopMenuCategory.food;

        createdButtons = false;
    }

    public void characterMenu() {
        PlaySelectSound();
        deleteButtons();
        shopMenu = ShopMenuCategory.characters;

        createdButtons = false;
    }

    public void movesMenu() {
        PlaySelectSound();
        deleteButtons();
        shopMenu = ShopMenuCategory.moves;

        createdButtons = false;
    }
}

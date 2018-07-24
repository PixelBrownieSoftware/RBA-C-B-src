using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

/*
Notes 24/07/2018: 
A lot of this code was inspired from an RPG game called "JGA: Crossroads" which is a game made on the same engine as this one. 
I didn't copy-paste the code, I sort of copied it and manipulated it to suit my game's needs i.e. running, getting exp etc.

If I'm honest with you, It's quite a mess - but I have indeed learned a lot.
Should I make another RPG, I promise the code will look much less messier than this.
*/

public class battlehandler : BaseGui
{
    public static battlehandler BSM;
    public enum PreformAction
    {
      Wait,
      TakeAction,
      getPlayerAttack,
      Preformaction,
      CompleteTurn
    }
    public PreformAction battlestate;

    public enum battleLogType
    {
        attack,
        damage,
        heal,
        move,
        attackall,
        dead
    }
    public battleLogType battleLog;

    public int totalChars;
    public int currentTurn;

    private bool battleEnabled;

    public GameObject GuiObj;
    public List<BattleCharacter> players = new List<BattleCharacter>();
    public List<BattleCharacter> enemies = new List<BattleCharacter>();
    public List<BattleCharacter> allCharacters = new List<BattleCharacter>();

    List<GameObject> heroGuis = new List<GameObject>();
    List<GameObject> enemyGuis = new List<GameObject>();

    public List<string> battleLogString = new List<string>();

    public BattleCharacter targetCharacter;
    public AttackBase selectedAttack;

    public bool isboss = false;
    int totalExp;
    int totalMoney;
    public EnemyGenerator eGen;

    public Text Results;
    public Text battleLogText;

    private void Awake()
    {
        if (BSM == null)
        {
            BSM = this;
            print("BSM ON");
        }
        //
        if (!MainMenu.is_loaded)
        {
            GameObject player = Resources.Load("prefabs/Characters/Players/Redler") as GameObject;
            GameObject createdPlayer = Instantiate(player, new Vector3Int(-150, -30, 4), Quaternion.identity);
            PlayerCharacter playerchar = createdPlayer.GetComponent<PlayerCharacter>();
            playerchar.initPos = createdPlayer.transform.position;
            createdPlayer.transform.SetParent(GameObject.Find("Players").transform);
            players.Add(createdPlayer.GetComponent<PlayerCharacter>());
        }
    }

    private void Start()
    {
        battleCommence();
    }

    public void addCharacters(Save.playerdata playerdat)
    {
        GameObject player = Resources.Load("prefabs/Characters/Players/" + playerdat.name) as GameObject;
        GameObject createdPlayer = Instantiate(player, new Vector3Int(-150, -30 * playerdat.id, 4), Quaternion.identity);
        PlayerCharacter playerchar = createdPlayer.GetComponent<PlayerCharacter>();
        playerchar.initPos = createdPlayer.transform.position;
        createdPlayer.transform.SetParent(GameObject.Find("Players").transform);
        players.Add(playerchar);

        playerchar.exptoNextLevel = playerdat.exptoNextLevel;
        playerchar.exp = playerdat.exp;
        playerchar.level = playerdat.level;

        //TODO: ADD FOOD ITEM
        
        if (playerdat.foodItem != null) {
            GameObject food = Resources.Load("FoodStuffs/" + playerdat.foodItem) as GameObject;
            playerchar.foodItem = food.GetComponent<FoodStuffs>();
        }
        playerchar.foodTurn = playerdat.foodTurn;

        playerchar.loadfromsave = true;

        playerchar.name = playerdat.name;
        playerchar.maxHealth = playerdat.maxHealth;
        playerchar.attack = playerdat.attack;
        playerchar.intelligence = playerdat.intelligence;
        playerchar.defence = playerdat.defence;
        playerchar.maxSpecialPoints = playerdat.maxSpecialPoints;

        foreach (AttackBase move in playerdat.moves) { playerchar.moves.Add(move); }

    }

    void battleCommence() {

        int charnum = 1;
        int enemycharnum = 1;

        allCharacters.Clear();
        enemies.Clear();
        if (isboss) {
            eGen.generateBoss();
        } else {
            eGen.randomEnemyGen();
        }
        createdButtons = false;
        currentTurn = 0;
        battleEnabled = true;

        foreach (GameObject gui in heroGuis)
        {
            Destroy(gui);
        }
        heroGuis.Clear();

        DestoryEnemyGUIs();

        foreach (BattleCharacter enemy in enemies)
        {
            enemycharnum++;
            enemy.health = enemy.maxHealth;
            enemy.specialPoints = enemy.maxSpecialPoints;
            allCharacters.Add(enemy); 
            GameObject gui = Instantiate(GuiObj, new Vector2(1250, 690 + 85 * -enemycharnum), Quaternion.identity);
            gui.name = "GUIHP" + enemy.name;
            gui.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(0.9f, 0.8f, 0.4f);
            gui.gameObject.transform.position = new Vector2(1250, 690 + 85 * -enemycharnum);
            gui.transform.SetParent(GameObject.Find("GUI").transform);
            enemyGuis.Add(gui);
        }

        foreach (BattleCharacter player in players)
        {
            charnum++;
            player.health = player.maxHealth;
            player.specialPoints = player.maxSpecialPoints;
            AddPlayerGUI(player, charnum);
        }
        totalChars = allCharacters.Count;
    }

    public void AddPlayerGUI(BattleCharacter player, int charnum) {
        allCharacters.Add(player);

        GameObject gui = Instantiate(GuiObj, new Vector2(158, 635 + 95 * -charnum), Quaternion.identity);
        gui.name = "GUIHP" + player.name;
        gui.transform.SetParent(GameObject.Find("GUI").transform);

        if (player.name == "Redler") {
            gui.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(1f, 0.3f, 0.12f);
        }

        if (player.name == "Blueler") {
            gui.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.9f);
        }

        if (player.name == "Greendori") {
            gui.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(0.6f, 1f, 0.5f);
        }

        if (player.name == "Buckler") {
            gui.transform.Find("GUITHINGY").GetComponent<Image>().color = new Color(0.9f, 0.6f, 0.2f);
        }
        gui.gameObject.transform.position = new Vector2(158, 635 + 95 * -charnum);
        heroGuis.Add(gui);
    }

    void DestoryEnemyGUIs() {
        foreach (GameObject gui in enemyGuis)
        {
            Destroy(gui);
        }
        enemyGuis.Clear();
    }

    void OnGUI()
    {
        drawPlayerStats();
        if (battleEnabled)
        {
            drawEnemyStats();
            if (playerturn())
            {
                PlayerCharacter currentPlayer = (PlayerCharacter)players[currentTurn];

                if (currentPlayer.health == 0) { incrementTurn(); }

                switch (battlestate)
                {
                    case PreformAction.Wait:

                        if (currentPlayer.charAnimatior.GetBool("IsHurt") == false)
                            if (!createdButtons)
                            {
                                if (this.eGen.currentPhase > 0) {
                                    GameObject attkButton = Instantiate(button, new Vector3(350, 140, 3), Quaternion.identity) as GameObject;
                                    attkButton.GetComponent<Button>().onClick.AddListener(() => retreat());
                                    Text buttonTxt = attkButton.transform.Find("Text").GetComponent<Text>();
                                    buttonTxt.text = "Retreat";
                                    attkButton.transform.SetParent(this.transform);
                                    attkButton.GetComponent<Image>().gameObject.transform.position = new Vector3(350, 140, 3);
                                    buttons.Add(attkButton);
                                }

                                createAttackButtons();
                            }
                        break;

                    case PreformAction.TakeAction:
                        //char selects target
                        if (!createdButtons) {
                            if (selectedAttack.attkRng != AttackBase.attackRange.all) {
                                createTargetButtons(selectedAttack);
                            } else { getTarget(0, selectedAttack); }
                        }
                        break;

                    case PreformAction.getPlayerAttack:
                        if (selectedAttack.attkRng != AttackBase.attackRange.all) {
                            currentPlayer.Attack(targetCharacter, selectedAttack);
                        }
                        else {
                            currentPlayer.Attack(null, selectedAttack);
                        }

                        battlestate = PreformAction.Preformaction;
                        break;

                    case PreformAction.Preformaction:
                        
                        if (currentPlayer.animations == BattleCharacter.animationstate.finish)
                        {
                            battlestate = PreformAction.CompleteTurn;
                        }
                        break;
                }
            }
            else {
                EnemyCharacter currentEnemy = (EnemyCharacter)enemies[currentTurn - players.Count];

                switch (battlestate) {
                    case PreformAction.Wait:

                        if (currentEnemy.charAnimatior.GetBool("IsHurt") == false)
                        {
                            currentEnemy.autoAttack();
                            targetCharacter = currentEnemy.targetChar;
                            battlestate = PreformAction.Preformaction;
                        }
                        break;

                    case PreformAction.Preformaction:
                        if (currentEnemy.animations == BattleCharacter.animationstate.finish) {
                            battlestate = PreformAction.CompleteTurn;
                        }
                        break;
                }
            }

            if (battlestate == PreformAction.CompleteTurn) {
                if (!checkfordeadAnimations())
                {
                    battlestate = PreformAction.Wait;
                    checkForVictory();
                    incrementTurn();
                }
            }
        }
    }

    public void drawPlayerStats() {

        for (int i = 0; i < players.Count; i++)
        {
            int playerHp;
            int maxPlayerHp;
            int playerSp;
            int bites = 0;
            PlayerCharacter currentPlayer = players[i] as PlayerCharacter;
            if (currentPlayer.foodItem != null)
            {
                bites = currentPlayer.foodTurn;
            }
            maxPlayerHp = players[i].maxHealth;
            playerHp = players[i].health;
            playerSp = players[i].specialPoints;
            string statsstring = currentPlayer.name + " Level: " + currentPlayer.level + "\n" + "    : " + playerHp + "/ " + maxPlayerHp + "\n" + "    : " + playerSp + "/ " + players[i].maxSpecialPoints;

            Text stats = heroGuis[i].transform.Find("GUITEXT").GetComponent<Text>();
            if (currentPlayer.foodItem != null) {
                stats.text = statsstring + "\n" + "   : " + currentPlayer.foodItem.foodName + " Bites: " + bites;
            } else {
                stats.text = statsstring;
            }
            
        }
    }

    public void drawEnemyStats() {
        for (int i = 0; i < enemies.Count; i++)
        {
            int playerHp = 0;
            int playerSp = 0;
            EnemyCharacter currentEnemy = enemies[i] as EnemyCharacter;
            playerHp = (enemies[i].health == 0) ? 0 : Mathf.RoundToInt(((float)enemies[i].health / (float)enemies[i].maxHealth) * 100);
            playerSp = (enemies[i].specialPoints == 0) ? 0 : Mathf.RoundToInt(((float)enemies[i].specialPoints / (float)enemies[i].maxSpecialPoints) * 100);

            string statsstring = currentEnemy.name + " Level: " + currentEnemy.level + "\n" + "    : " + enemies[i].health + " (" + playerHp + "%)"  + "\n" + "    : " + enemies[i].specialPoints + " (" + playerSp + "%)" ;

            Text stats = enemyGuis[i].transform.Find("GUITEXT").GetComponent<Text>();
            stats.text = statsstring;
        }
    }

    void backToMenu() {
        deleteButtons();
        battlestate = PreformAction.Wait;
        createdButtons = false;
    }

/*
Notes 25/07/2018:
This is the ugliest part of the code, because I didn't really have much of a clue about what delegates were.
Or I could have even put this on an OnGUI function to draw the buttons to have an if statement rather than all this repeated code.
*/

    //creating attack buttons
    public void createAttackButtons() {
      //  PlaySelectSound();
        int numberOfAttacks = ((PlayerCharacter)players[currentTurn]).moves.Count;

        int spacer_x = 170;
        int initalpos = 90;
        int initialy = 90;

        int ip = 0;

        for (int i = 0; i < numberOfAttacks; i++) {
            GameObject attkButton = Instantiate(button, new Vector3(initalpos + ip * spacer_x, initialy, 3), Quaternion.identity) as GameObject;

            attkButton.GetComponent<Image>().gameObject.transform.position = new Vector3(initalpos + ip * spacer_x, initialy, 3);
            ip++;
            if (ip == 7) { initialy -= 40; ip = 0; }

            attkButton.GetComponent<Image>().color = GetButtonColour(players[currentTurn].moves[i]);
            int attacknumber = i;
            attkButton.GetComponent<Button>().onClick.AddListener(() => getAttack(attacknumber));
            attkButton.transform.SetParent(this.transform);
            Text buttonTxt = attkButton.transform.Find("Text").GetComponent<Text>();
            buttonTxt.text = players[currentTurn].moves[i].name + " " + players[currentTurn].moves[i].spCost + " SP";

            buttons.Add(attkButton);
        }
        createdButtons = true;
    }

    //creating target buttons
    public void createTargetButtons(AttackBase attkMove) 
    {
        PlaySelectSound();
        int numberOfAttacks = enemies.Count;

        int spacer_x = 170;
        int initalpos = 90;
        int initialy = 90;

        int ip = 0;
        int postion = 0;
        
        //flag for if the move is healing
        if (attkMove.attkType != AttackBase.attackType.heal) {
            for (int i = 0; i < enemies.Count; i++) {

                if (enemies[i].health <= 0) {
                    continue;
                }

                int attacknumber = i;
                GameObject attkButton = Instantiate(button, new Vector3(initalpos + ip * spacer_x, initialy, 3), Quaternion.identity) as GameObject;
                attkButton.GetComponent<Button>().onClick.AddListener(() => getTarget(attacknumber, attkMove));
                attkButton.GetComponent<Image>().gameObject.transform.position = new Vector3(initalpos + ip * spacer_x, initialy, 3);

                postion = initalpos + ip * spacer_x;
                ip++;
                if (ip == 6) { initialy -= 40; ip = 0; }

                float enemyHP = Mathf.RoundToInt(((float)enemies[i].health / (float)enemies[i].maxHealth) * 100);

                Text buttonTxt = attkButton.transform.Find("Text").GetComponent<Text>();
                buttonTxt.text = enemies[i].name + " " + enemyHP + "%";
                attkButton.transform.SetParent(this.transform);

                buttons.Add(attkButton);
            }
        } else  {
            //if the move is healing
            for (int i = 0; i < players.Count; i++) {

                int attacknumber = i;
                GameObject attkButton = Instantiate(button, new Vector3(initalpos + ip * spacer_x, 90, 3), Quaternion.identity) as GameObject;
                attkButton.GetComponent<Button>().onClick.AddListener(() => getTarget(attacknumber, attkMove));
                attkButton.GetComponent<Image>().gameObject.transform.position = new Vector3(initalpos + ip * spacer_x, initialy, 3);

                postion = initalpos + ip * spacer_x;
                ip++;
                if (ip == 6) { initialy -= 40; ip = 0; }

                Text buttonTxt = attkButton.transform.Find("Text").GetComponent<Text>();
                buttonTxt.text = players[i].name;
                attkButton.transform.SetParent(this.transform);

                buttons.Add(attkButton);
            }
        }

        GameObject backButton = Instantiate(button, new Vector3(postion + spacer_x, 90, 3), Quaternion.identity) as GameObject;
        backButton.GetComponent<Button>().onClick.AddListener(() => backToMenu());
        backButton.transform.SetParent(this.transform);
        Text backbuttonTxt = backButton.transform.Find("Text").GetComponent<Text>();
        backbuttonTxt.text = "Back";
        backButton.GetComponent<Image>().gameObject.transform.position = new Vector3(postion + spacer_x, 90, 3);

        buttons.Add(backButton);

        createdButtons = true;
    }

    public Color GetButtonColour(AttackBase attkmov)
    {
        Color colour = Color.white;
        if (attkmov.attkElement == AttackBase.attackElement.electric) { colour = new Color(1f, 0.92f ,0.06f); }
        if (attkmov.attkElement == AttackBase.attackElement.fire) { colour = new Color(1, 0.1f, 0); }
        if (attkmov.attkElement == AttackBase.attackElement.ice) { colour = new Color(0.2f, 1f, 1); }
        if (attkmov.attkElement == AttackBase.attackElement.shadow) { colour = new Color(0.4f, 0.4f, 0.5f); }
        if (attkmov.attkElement == AttackBase.attackElement.light) { colour = new Color(0.95f, 0.5f,0.45f);}
        return colour;
    }


    public AttackBase getAttack(int attackNum)
    {
        AttackBase attack;
        attack = ((PlayerCharacter)players[currentTurn]).moves[attackNum];
        int sp = ((PlayerCharacter)players[currentTurn]).specialPoints;
        if (sp >= attack.spCost)
        {
            selectedAttack = attack;
            battlestate = PreformAction.TakeAction;
            deleteButtons();
            return attack;
        } else {
            PlayDeclineSound();
            backToMenu(); return null; }
    }

    public BattleCharacter getTarget(int attackNum, AttackBase attkMove)
    {
        PlaySelectSound();
        BattleCharacter target = null;
        ((PlayerCharacter)players[currentTurn]).specialPoints -= attkMove.spCost;
        if (attkMove.attkRng != AttackBase.attackRange.all)
        {
            if (attkMove.attkType == AttackBase.attackType.heal) {
                target = ((PlayerCharacter)players[attackNum]);
            } else {
                target = ((EnemyCharacter)enemies[attackNum]);
            }

            targetCharacter = target;
        }

        battlestate = PreformAction.getPlayerAttack;
        deleteButtons();
        return target;
    }

    bool checkfordeadAnimations()
    {
        foreach (BattleCharacter character in allCharacters) {
            if (character.deadAnimComplete == false && character.charAnimatior.GetBool("IsDefeated") == true) {
                return true;
            }
        }
        return false;
    }

    private void checkForVictory() {

        List<BattleCharacter> aliveEnemies = enemies.FindAll(x => x.health > 0 );
        List<BattleCharacter> alivePlayers = players.FindAll(x => x.health > 0);

        //if all the enemies are dead
        if (aliveEnemies.Count == 0) {
            totalExp = 0;
            totalMoney = 0;

            //add up the expereince points of the enemies
            for (int i = 0; i < enemies.Count; i++) {
                totalExp += enemies[i].GetComponent<EnemyCharacter>().expToGive;
                totalMoney += enemies[i].GetComponent<EnemyCharacter>().moneyAmount;
            }
            ModeHandler.money += totalMoney;

            //total experience points
            for (int i = 0; i < players.Count; i++) {

                //checks if player has leveld up
                PlayerCharacter playerChar = players[i].GetComponent<PlayerCharacter>();
                playerChar.exp += totalExp;
                playerChar.checkForLevelUp();
                while (playerChar.needLvlUP) {
                    playerChar.levelUp();
                }
                //decreases food counter
                if (playerChar.foodItem != null) {
                    playerChar.foodTurn--;
                    playerChar.checkForEatenFood();
                }
            }
            winBattle();
        }

        //if all players are alive
        if (alivePlayers.Count == 0) { looseBattle(); }

    }

    public void winBattle() {
        DestoryEnemyGUIs();
        print("You win");
        

        if (eGen.Phase.isBoss && isboss) { isboss = false; eGen.isBossDefeatTrue();
            if (eGen.currentPhase >= eGen.Phases.Count -1) {
                SceneManager.LoadScene("Ending");
            }
        }
        string disp = "";

        battleLogString.Add("Gained " + totalExp + " experience points.");
        battleLogString.Add("Found £" + totalMoney + ".");

        //check all the character's exp and then give the experience to all the players
        for (int i = 0; i < players.Count; i++) {
            PlayerCharacter playerChar = players[i].GetComponent<PlayerCharacter>();

            //show this message if the player has leveled up
            if (playerChar.islevelUP) {
                SoundManager.sfx.PlaySound(Resources.Load("sfx/LevelUP") as AudioClip);
                battleLogString.Add(playerChar.name.ToString() + " has leveled up to " + playerChar.level);
                print("levelUP");
            }
            playerChar.islevelUP = false;
        }

        GameObject[] enemiesToDelete;
        enemiesToDelete = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemiesToDelete) {
            
            Destroy(enemy.gameObject);
        }

        //dsiable battle
        battleEnabled = false;
        // win screen
        Results.enabled = true;

        //assemble all of above
        foreach (string msg in battleLogString) {
            disp = disp.ToString() + msg.ToString() + "\n";
        }
        battleLogText.text = disp;

        ModeHandler.modehandler.afterMenuSelect();
    }


    public void battlelog(string text)
    {
        string disp = "";

        battleLogString.Add(text);
        foreach (string msg in battleLogString)
        {
            disp = disp.ToString() + msg.ToString() + "\n";
        }
        battleLogText.text = disp;
        if (battleLogString.Count > 6)
        {
            battleLogString.Clear();
        }
    }

    public void RetryBattle()
    {
        PlaySelectSound();
        if (eGen.Phase.isBoss == false)
        {
            isboss = false;
        }

        ModeHandler.modehandler.changeToBattle();
        deleteButtons();
        Results.enabled = false;
        battleCommence();
    }

    void retreat()
    {
        PlaySelectSound();
        DestoryEnemyGUIs();
        isboss = false;
        targetCharacter = null;
        battleEnabled = false;
        battlestate = PreformAction.Wait;
        foreach (BattleCharacter enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
        enemies.Clear();

        ModeHandler.money = Mathf.FloorToInt(ModeHandler.money * 0.75f);
        deleteButtons();
        ModeHandler.modehandler.afterMenuSelect();
    }

    public void bossBattle()
    {
        isboss = true;
        ModeHandler.modehandler.changeToBattle();
        deleteButtons();
        Results.enabled = false;
        battleCommence();
    }

    public void looseBattle()
    {
        DestoryEnemyGUIs();
        print("You lost");
        isboss = false;
        targetCharacter = null;
        battleEnabled = false;
        foreach (BattleCharacter enemy in enemies) {
            Destroy(enemy.gameObject);
        }
        enemies.Clear();
        ModeHandler.money = ModeHandler.money / 2;
        ModeHandler.modehandler.afterMenuSelect();
    }


    public void incrementTurn() {
        //increase turn
        currentTurn++;

        currentTurn %= totalChars;
        
        if (currentTurn < players.Count)
        {
            if (players[currentTurn].animations == BattleCharacter.animationstate.dead || players[currentTurn].health <= 0)
            {
                incrementTurn();
            }
        }
        else
        {
            if (enemies[currentTurn - players.Count].animations == BattleCharacter.animationstate.dead || enemies[currentTurn - players.Count].health <= 0)
            {
                incrementTurn();
            }
        }
    }

    bool roundComplete() {
        return currentTurn == totalChars;
    }

    public bool playerturn() {
        
        return currentTurn < players.Count;
    }

}

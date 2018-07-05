using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class Save : MonoBehaviour {

    public void LoadData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open("save.RB", FileMode.Open);
        savedata loadedsav = (savedata)bf.Deserialize(file);
        print(loadedsav.eGenPhase);
        ModeHandler.money = loadedsav.money;

        List<playerdata> players = loadedsav.playerdat;

        ModeHandler.modehandler.ShopMenu.AddStuffFromList(loadedsav.shopFood, loadedsav.shopMoves, loadedsav.shopCharacters);
        battlehandler.BSM.players.Clear();
        foreach (playerdata player in players)
        {
            //TODO: CALL FUNCTION OF BATTLE HANDLER AND HAVE IT INSTATNTIATE OBJECT ACCORDING TO STATS OF "PLAYER"
            battlehandler.BSM.addCharacters(player);
        }
        
        //foreach (string food in loadedsav.shopFood) { }

        battlehandler.BSM.eGen.currentPhase = loadedsav.eGenPhase;
        Destroy(this.gameObject);
    }

	public void SaveData () {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create("save.RB");

        savedata savedata = new savedata();
        //finds all the players
        for (int i = 0; i < battlehandler.BSM.players.Count; i++) {
            savedata.playerdat.Add(new playerdata());
        }

        //saving money, shop items and enemyGeneration
        savedata.money = ModeHandler.money;
        savedata.shop_phase = ModeHandler.modehandler.ShopMenu.levelReqSelection;
        foreach (FoodStuffs food in ModeHandler.modehandler.ShopMenu.foodlist) {
            savedata.shopFood.Add(food.foodName);
        }
        foreach (Move move in ModeHandler.modehandler.ShopMenu.moveList) {
            savedata.shopMoves.Add(move);
        }
        foreach (Character chara in ModeHandler.modehandler.ShopMenu.characterList) {
            savedata.shopCharacters.Add(chara);
        }
        //savedata.shopFood = ModeHandler.modehandler.ShopMenu.foodlist;
        //savedata.shopMoves = ModeHandler.modehandler.ShopMenu.moveList;
        //savedata.shopCharacters = ModeHandler.modehandler.ShopMenu.characterList;

        savedata.eGenPhase = battlehandler.BSM.eGen.currentPhase;

        //save all the exp, levels and stuff
        for (int i = 0; i < battlehandler.BSM.players.Count; i++)
        {
            PlayerCharacter player = (PlayerCharacter)battlehandler.BSM.players[i];
            savedata.playerdat[i].attack = player.attack;
            savedata.playerdat[i].exptoNextLevel = player.exptoNextLevel;
            savedata.playerdat[i].exp = player.exp;
            savedata.playerdat[i].level = player.level;
            savedata.playerdat[i].maxSpecialPoints = player.maxSpecialPoints;
            savedata.playerdat[i].maxHealth = player.maxHealth;
            savedata.playerdat[i].defence = player.defence;
            savedata.playerdat[i].intelligence = player.intelligence;

            savedata.playerdat[i].foodItem = (player.foodItem !=null) ? player.foodItem.name : null;
            savedata.playerdat[i].foodTurn = player.foodTurn;
            savedata.playerdat[i].name = player.name;
            savedata.playerdat[i].id = i;
            savedata.playerdat[i].moves = player.moves;
        }

        bf.Serialize(file, savedata);
        file.Close();
        print("saved");
        Destroy(this.gameObject);
    }
    

 

    [System.Serializable]
    public class savedata
    {
        public List<playerdata> playerdat = new List<playerdata>();
        public int money;
        public List<Move> shopMoves = new List<Move>();
        public List<Character> shopCharacters = new List<Character>();
        public List<string> shopFood = new List<string>();

        public int eGenPhase;
        public int shop_phase;
    }

    [System.Serializable]
    public class playerdata
    {
        public int id;

        public string name;
        public int level;
        public int maxHealth { get; set; }
        public int health { get; set; }

        public int attack { get; set; }
        public int defence { get; set; }
        public int intelligence { get; set; }
        public int specialPoints { get; set; }
        public int maxSpecialPoints { get; set; }

        protected AttackBase.attackElement weakness { get; set; }
        protected AttackBase.attackElement resistant { get; set; }

        public List<AttackBase> moves = new List<AttackBase>();
        
        public int exp;
        public int exptoNextLevel;

        public string foodItem;
        public int foodTurn;
    }

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

 abstract public class PlayerCharacter : BattleCharacter {

    public int exp;
    public int exptoNextLevel;
    public int baseExp { get; set; }
    public bool islevelUP;
    public bool needLvlUP;

    public FoodStuffs foodItem;
    public int foodTurn;

    public float attackIncPercentage { get; set; }
    public float defenceIncPercentage { get; set; }
    public float intelligenceIncPercentage { get; set; }
    public float specialIncPercentage { get; set; }

    public bool loadfromsave = false;
    public abstract void DefaultMoves();

    new public void Start () {
        base.Start();

        if (!loadfromsave) {
            DefaultMoves();
            if (level == 0) {
                exptoNextLevel = 0;
                level++;
                exptoNextLevel += (baseExp * 2);
                islevelUP = false;
            }
            initializePlayers();
        }
    }

    public void SetLevelFromSave(int lvl)
    {
        level = lvl;
    }

    void initializePlayers() {
        if (level == 1)
        {
            maxHealth = baseHealth;
            maxSpecialPoints = baseSpecialPoints;
            intelligence = baseIntelligence;
            attack = baseAttack;
            defence = baseDefence;
            attack = baseAttack;
            specialPoints = maxSpecialPoints;
            health = maxHealth;
        }
    }

    //player attack
    abstract public void Attack(BattleCharacter target, AttackBase move);

    //levelUp
    public void levelUp() {
        level++;
        int lastExptoNextLevel = Mathf.FloorToInt(exptoNextLevel/ 1.8f + exptoNextLevel/12.5f);
        exptoNextLevel += (lastExptoNextLevel);

        maxHealth += healthIncrementer;

        float attkincVal = Random.Range(0, 0.5f);
        if (attkincVal <= attackIncPercentage) {
            attack += attackIncrementer;
        }

        float defkincVal = Random.Range(0, 0.5f);
        if (defkincVal <= defenceIncPercentage)
        {
            defence += defenceIncrementer;
        }

        float intkincVal = Random.Range(0, 0.5f);
        if (intkincVal <= defenceIncPercentage)
        {
            intelligence += intelligenceIncrementer;
        }

        float specialincVal = Random.Range(0, 0.5f);
        if (specialincVal <= specialIncPercentage)
        {
            maxSpecialPoints += intelligenceIncrementer;
        }

        if (exp >= exptoNextLevel) {
            needLvlUP = true;
        } else { needLvlUP = false; }
            print(exptoNextLevel);
    }

    new public void Update()
    {
        base.Update();
        /*
        if (health <= 0)
        {
            battlehandler.BSM.players.Remove(this);
        }
        */
        checkForLevelUp();
    }
    

    //check for eaten food
    public void checkForEatenFood() {

        if (foodTurn == 0 && foodItem != null)
        {
            attack += foodItem.attkInc;
            defence += foodItem.defInc;
            maxHealth += foodItem.healthInc;
            maxSpecialPoints += foodItem.spInc;
            intelligence += foodItem.intInc;
            foodItem = null;
        }
    }

    //checking if the player has leveled up
    public void checkForLevelUp() {
        if (exp >= exptoNextLevel) {
            islevelUP = true;
            levelUp();
        } 
    }

}

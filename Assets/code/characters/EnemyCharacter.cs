using UnityEngine;
using System.Collections;
using System.Collections.Generic;

abstract public class EnemyCharacter : BattleCharacter {

    public int expIncrementer;
    public int expToGive;
    public int baseExpToGive;
    public int moneyAmount;
    protected bool isBeserk;

    protected bool customAI = false;

    new public void Start()
    {
        base.Start();
        if (level == 0)
        {
            level++;
        }
        initializeEnemyStats();
    }

    void initializeEnemyStats() {

        attack = attackIncrementer * (level - 1) + baseAttack;
        maxHealth = healthIncrementer * (level - 1) + baseHealth;
        defence = defenceIncrementer * (level - 1) + baseDefence;
        intelligence = intelligenceIncrementer * (level - 1) + baseIntelligence;
        maxSpecialPoints = specialIncrementer * (level - 1) + baseSpecialPoints;
        expToGive = expIncrementer * (level) + baseExpToGive;
        health = maxHealth;
        specialPoints = maxSpecialPoints;
    }

    public void autoAttack()
    {
        if (!customAI)
        {
            AttackBase currentMove = moves[Random.Range(0, moves.Count)];
            selectedAttack = currentMove;
            if (selectedAttack.attkType != AttackBase.attackType.heal)
            {
             List<BattleCharacter>target = battlehandler.BSM.players;
                targetChar = getSelectedChar(target);
            }
            else
            {
                List<BattleCharacter> target = battlehandler.BSM.enemies;
                targetChar = getSelectedChar(target);
            }
        }
        else { CustomAI(); }

        if (selectedAttack.spCost > specialPoints) {
            selectedAttack = getLowSpAttk();
            if (!isBeserk) {
                if (selectedAttack.attkType != AttackBase.attackType.heal) {
                    List<BattleCharacter> target = battlehandler.BSM.players;
                    targetChar = getSelectedChar(target);
                    autoAttack();
                } else {
                    List<BattleCharacter> target = battlehandler.BSM.enemies;
                    targetChar = getSelectedChar(target);
                    autoAttack();
                }
            }
        } else {
            battlehandler.BSM.battlelog("- " + this.name + " preformed " + selectedAttack.name);
            specialPoints -= selectedAttack.spCost;
            attackSelector();
        }
    }

    public void CustomAI() {

        //leave this for any character that has custom AI

    }

    abstract public void attackSelector();

    //attack opponents
    public BattleCharacter getSelectedChar(List<BattleCharacter> targets) {

        List<BattleCharacter> aliveTargets = targets.FindAll(x => x.health > 0);

        return aliveTargets[Random.Range(0, aliveTargets.Count)];
    }


    public BattleCharacter getEveryone(List<BattleCharacter> targets) {

        List<BattleCharacter> aliveTargets = targets.FindAll(x => x.health > 0);
        foreach (BattleCharacter target in targets) {
            if (target.name == this.name) {
                aliveTargets.Remove(target);
            }
        }
        return aliveTargets[Random.Range(0, aliveTargets.Count)];
    }

    public BattleCharacter identifyWeakness(List<BattleCharacter> targets) {
        foreach (BattleCharacter target in targets) {
            if (selectedAttack.attkElement == target.weakness)
            {
                return target;
            }
        }
        return getSelectedChar(targets);
    }

    public AttackBase getLowSpAttk() {
        List<AttackBase> lowSPMoves = moves.FindAll(x => x.spCost <= specialPoints);
        if (lowSPMoves.Count == 1)
        {
            AttackBase moveToPreform = moves[0];
            return moveToPreform;
        }
        else
        {
            AttackBase moveToPreform = moves[Random.Range(0, lowSPMoves.Count)];
            return moveToPreform;
        }
    }

    new public void Update ()
    {
        base.Update();
        /*
        if (health <= 0)
        {
            battlehandler.BSM.enemies.Remove(this);
        }*/
    }
    
}

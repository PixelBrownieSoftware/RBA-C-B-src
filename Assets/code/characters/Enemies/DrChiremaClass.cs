using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrChiremaClass : EnemyCharacter
{

    public enum animationDrChirema { firepot, icepot, mut, crunch, osmosis, healpot };
    public animationDrChirema drchiremaAnimation;

    public float speed;

    private void Awake()
    {
        name = "Dr.Chirema";
    }

    new void Start()
    {
        weakness = AttackBase.attackElement.electric;
        resistant = AttackBase.attackElement.light;

        transform.rotation = Quaternion.Euler(0, 180, 0);
        moneyAmount = 12;
        baseExpToGive = 35;

        expIncrementer = 23;
        attackIncrementer = 3;
        defenceIncrementer = 4;
        healthIncrementer = 13;
        specialIncrementer = 5;
        intelligenceIncrementer = 3;

        baseHealth = 35;
        baseAttack = 2;
        baseDefence = 3;
        baseIntelligence = 3;
        baseSpecialPoints = 3;

        base.Start();

        AttackBase turn2beast = new AttackBase();
        turn2beast.attkType = AttackBase.attackType.other;
        turn2beast.name = "Mutate";
        turn2beast.power = 0;
        moves.Add(turn2beast);

        AttackBase potionfire = new AttackBase();
        potionfire.name = "Fire Potion";
        potionfire.attkElement = AttackBase.attackElement.fire;
        potionfire.attkRng = AttackBase.attackRange.all;
        potionfire.power = 25;
        moves.Add(potionfire);

        AttackBase potionice = new AttackBase();
        potionice.name = "Ice Potion";
        potionice.attkElement = AttackBase.attackElement.ice;
        potionfire.attkRng = AttackBase.attackRange.all;
        potionice.power = 25;
        moves.Add(potionice);

        AttackBase potionheal = new AttackBase();
        potionheal.attkElement = AttackBase.attackElement.normal;
        potionheal.attkRng = AttackBase.attackRange.all;
        potionheal.attkType = AttackBase.attackType.heal;
        potionheal.name = "Healing Potion";
        potionheal.power = 100;
        moves.Add(potionheal);

        /*

        */

    }

    public override void moveSelector()
    {
        switch (drchiremaAnimation) {

            case animationDrChirema.firepot:
                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationDrChirema.icepot:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationDrChirema.mut:
                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationDrChirema.healpot:
                charAnimatior.SetInteger("Attack", 3);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationDrChirema.crunch:
                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationDrChirema.osmosis:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;
        }
    }

    void turnToBeast() {
        attack = attack + 25;
        defence = defence + 15;
        maxHealth += 150;
        expToGive += 1500;
        health = maxHealth;
        isBeserk = true;
        moves.Clear();
        charAnimatior.SetBool("IsForm", true);

        AttackBase hurt = new AttackBase();
        hurt.power = 35;
        hurt.attkElement = AttackBase.attackElement.normal;
        hurt.attkRng = AttackBase.attackRange.single;
        hurt.attkType = AttackBase.attackType.attack;
        hurt.name = "Crunch";
        moves.Add(hurt);

        AttackBase osmosis = new AttackBase();
        osmosis.attkType = AttackBase.attackType.magic;
        osmosis.attkElement = AttackBase.attackElement.ice;
        osmosis.attkRng = AttackBase.attackRange.single;
        osmosis.power = 15;
        osmosis.spCost = 4;
        osmosis.name = "Osmosis";
        moves.Add(osmosis);

    }

    new void Update()
    {
        base.Update();
    }

    public override void attackSelector()
    {
        if (isBeserk)
        {
            targetChar = getEveryone(battlehandler.BSM.allCharacters);
        }

        if (selectedAttack.name == "Mutate")
        {
            drchiremaAnimation = animationDrChirema.mut;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Fire Potion")
        {
            drchiremaAnimation = animationDrChirema.firepot;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Ice Potion")
        {
            drchiremaAnimation = animationDrChirema.icepot;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Crunch")
        {
            drchiremaAnimation = animationDrChirema.crunch;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Osmosis")
        {
            drchiremaAnimation = animationDrChirema.osmosis;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Healing Potion")
        {
            drchiremaAnimation = animationDrChirema.healpot;
            animations = animationstate.attack;
        }
        moveSelector();
    }
    
}

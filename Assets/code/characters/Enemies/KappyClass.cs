using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KappyClass : EnemyCharacter
{
    public enum animationKappy { slash, smite, mindsword, firestrike, impulse };
    public animationKappy kappAnimation;

    public float speed;

    private void Awake()
    {
        name = "Kappy";
    }

    new void Start()
    {
        moneyAmount = 5;
        baseExpToGive = 15;

        resistant = AttackBase.attackElement.electric;
        weakness = AttackBase.attackElement.ice;

        expIncrementer = 9;
        attackIncrementer = 3;
        defenceIncrementer = 3;
        healthIncrementer = 4;
        intelligenceIncrementer = 4;

        baseHealth = 35;
        baseAttack = 3;
        baseDefence = 3;
        baseIntelligence = 3;
        baseSpecialPoints = 16;

        base.Start();

        AttackBase slash = new AttackBase();
        slash.name = "Slash";
        slash.power = 7;
        moves.Add(slash);

        AttackBase fireStrike = new AttackBase();
        fireStrike.attkElement = AttackBase.attackElement.fire;
        fireStrike.attkRng = AttackBase.attackRange.single;
        fireStrike.attkType = AttackBase.attackType.skill;
        fireStrike.name = "Fire Strike";
        fireStrike.power = 25;
        fireStrike.spCost = 4;
        moves.Add(fireStrike);

        AttackBase mindsword = new AttackBase();
        mindsword.attkElement = AttackBase.attackElement.light;
        mindsword.attkType = AttackBase.attackType.magic;
        mindsword.name = "Mind Sword";
        mindsword.power = 15;
        mindsword.spCost = 2;
        moves.Add(mindsword);

        if (level > 11) {

            AttackBase smite = new AttackBase();
            smite.attkElement = AttackBase.attackElement.shadow;
            smite.attkType = AttackBase.attackType.skill;
            smite.name = "Smite";
            smite.power = 20;
            smite.spCost = 3;
            moves.Add(smite);

            AttackBase impulse = new AttackBase();
            impulse.attkElement = AttackBase.attackElement.electric;
            impulse.attkRng = AttackBase.attackRange.single;
            impulse.attkType = AttackBase.attackType.magic;
            impulse.name = "Impulse";
            impulse.spCost = 5;
            moves.Add(impulse);
        }
    }


    public override void moveSelector()
    {
        switch (kappAnimation)
        {
            case animationKappy.slash:

                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationKappy.firestrike:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationKappy.smite:
                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationKappy.mindsword:
                charAnimatior.SetInteger("Attack", 3);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationKappy.impulse:
                charAnimatior.SetInteger("Attack", 4);
                charAnimatior.SetBool("IsAttacking", true);
                break;

        }
    }

    new void Update()
    {
        base.Update();
    }

    public override void attackSelector()
    {
        if (selectedAttack.name == "Slash")
        {
            kappAnimation = animationKappy.slash;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Smite")
        {
            kappAnimation = animationKappy.smite;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Mind Sword")
        {
            kappAnimation = animationKappy.mindsword;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Fire Strike")
        {
            kappAnimation = animationKappy.firestrike;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Impulse")
        {
            kappAnimation = animationKappy.impulse;
            animations = animationstate.attack;
        }

        moveSelector();
    }

}

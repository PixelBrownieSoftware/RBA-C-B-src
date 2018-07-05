using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkarClass : EnemyCharacter {

    public enum animationSkar { slash, heal, frost_stirke };
    public animationSkar animSkar;

    public float speed;

    private void Awake()
    {
        name = "Skar";
    }

    new void Start () {

        moneyAmount = 4;
        baseExpToGive = 12;

        resistant = AttackBase.attackElement.shadow;
        weakness = AttackBase.attackElement.fire;

        expIncrementer = 8;
        attackIncrementer = 3;
        defenceIncrementer = 2;
        healthIncrementer = 6;
        intelligenceIncrementer = 3;
        specialIncrementer = 2;

        baseHealth = 43;
        baseSpecialPoints = 15;
        baseAttack = 3;
        baseDefence = 2;
        baseIntelligence = 3;
        
        base.Start();

        AttackBase slash = new AttackBase();
        slash.name = "Slash";
        slash.power = 7;
        slash.attkType = AttackBase.attackType.attack;
        moves.Add(slash);

        AttackBase heal = new AttackBase();
        heal.name = "Heal";
        if (level < 8) { heal.power = 10; } else if (level <= 14) { heal.power = 25; heal.spCost = 4; } else if (level > 14) { heal.power = 40; heal.spCost = 6; }
        heal.spCost = 2;
        heal.attkType = AttackBase.attackType.heal;
        heal.attkRng = AttackBase.attackRange.single;
        moves.Add(heal);

        if (level > 9)
        {
            AttackBase froststrike = new AttackBase();
            froststrike.attkElement = AttackBase.attackElement.ice;
            froststrike.attkType = AttackBase.attackType.skill;
            froststrike.name = "Frost Strike";
            froststrike.power = 25;
            froststrike.spCost = 6;
            moves.Add(froststrike);
        }
    }

    public override void moveSelector()
    {
        switch (animSkar)
        {
            case animationSkar.slash:

                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationSkar.heal:

                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationSkar.frost_stirke:

                charAnimatior.SetInteger("Attack", 2);
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
            animSkar = animationSkar.slash;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Heal")
        {
            animSkar = animationSkar.heal;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Frost Strike")
        {
            animSkar = animationSkar.frost_stirke;
            animations = animationstate.attack;
        }

        moveSelector();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenlerClass : EnemyCharacter
{

    public enum animationGreenler { slash, electricstrik , heal, lightning };
    public animationGreenler greenAnimation;

    public float speed;

    private void Awake()
    {
        name = "Greenler";
    }

    new void Start()
    {
        moneyAmount = 20;
        baseExpToGive = 5500;
        
        attackIncrementer = 4;
        defenceIncrementer = 5;
        healthIncrementer = 75;
        specialIncrementer = 8;
        intelligenceIncrementer = 5;

        baseHealth = 95;
        baseAttack = 2;
        baseDefence = 3;
        baseIntelligence = 9;
        baseSpecialPoints = 15;

        weakness = AttackBase.attackElement.fire;
        resistant = AttackBase.attackElement.electric;

        base.Start();

        AttackBase pound = new AttackBase();
        pound.name = "Stab";
        pound.attkType = AttackBase.attackType.attack;
        pound.attkRng = AttackBase.attackRange.single;
        pound.attkElement = AttackBase.attackElement.normal;
        pound.spCost = 0;
        pound.power = 15;
        moves.Add(pound);

        AttackBase thundrStrk = new AttackBase();
        thundrStrk.attkElement = AttackBase.attackElement.electric;
        thundrStrk.name = "Thunder Strike";
        thundrStrk.power = 25;
        thundrStrk.attkType = AttackBase.attackType.skill;
        thundrStrk.spCost = 4;
        moves.Add(thundrStrk);

        AttackBase lightning = new AttackBase();
        lightning.attkElement = AttackBase.attackElement.electric;
        lightning.attkType = AttackBase.attackType.magic;
        lightning.name = "Lightning";
        lightning.power = 45;
        lightning.spCost = 10;
        moves.Add(lightning);

        AttackBase heal = new AttackBase();
        heal.name = "Heal";
        heal.spCost = 5;
        heal.power = 75;
        heal.attkType = AttackBase.attackType.heal;
        heal.attkRng = AttackBase.attackRange.single;
        moves.Add(heal);

    }

    public override void moveSelector() {
        switch (greenAnimation) {
            case animationGreenler.slash:
                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationGreenler.electricstrik:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationGreenler.lightning:
                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationGreenler.heal:
                charAnimatior.SetInteger("Attack", 3);
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
        if (selectedAttack.name == "Stab")
        {
            greenAnimation = animationGreenler.slash;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Thunder Strike")
        {
            greenAnimation = animationGreenler.electricstrik;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Heal")
        {
            greenAnimation = animationGreenler.heal;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Lightning")
        {
            greenAnimation = animationGreenler.lightning;
            animations = animationstate.attack;
        }
        moveSelector();

    }

    
}

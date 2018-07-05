using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BenoClass : EnemyCharacter
{

    public enum animationBeno { punch, shoot, machinegun, shadowsnipe, bomb};
    public animationBeno benoanim;

    public float speed;
    
    new void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
        weakness = AttackBase.attackElement.electric;
        resistant = AttackBase.attackElement.shadow;

        name = "Beno";
        moneyAmount = 120;
        baseExpToGive = 12000;
        attackIncrementer = 7;
        defenceIncrementer = 6;
        healthIncrementer = 16;
        intelligenceIncrementer = 4;
        specialIncrementer = 5;

        baseHealth = 88;
        baseAttack = 7;
        baseDefence = 5;
        baseIntelligence = 5;
        baseSpecialPoints = 12;

        base.Start();

        AttackBase punch = new AttackBase();
        punch.name = "Punch";
        punch.attkElement = AttackBase.attackElement.normal;
        punch.attkType = AttackBase.attackType.attack;
        punch.power = 7;

        AttackBase shoot = new AttackBase();
        shoot.attkType = AttackBase.attackType.skill;
        shoot.name = "Shoot";
        shoot.power = 12;
        moves.Add(shoot);

        AttackBase machinegun = new AttackBase();
        machinegun.attkType = AttackBase.attackType.skill;
        machinegun.name = "Machine Gun";
        machinegun.power = 3;
        machinegun.spCost = 2;
        moves.Add(machinegun);

        AttackBase shadowSnipe = new AttackBase();
        shadowSnipe.attkElement = AttackBase.attackElement.shadow;
        shadowSnipe.attkRng = AttackBase.attackRange.single;
        shadowSnipe.name = "Shadow Snipe";
        shadowSnipe.power = 35;
        shadowSnipe.spCost = 6;
        shadowSnipe.attkType = AttackBase.attackType.skill;
        moves.Add(shadowSnipe);

        AttackBase bomb = new AttackBase();
        bomb.attkElement = AttackBase.attackElement.normal;
        bomb.attkRng = AttackBase.attackRange.all;
        bomb.attkType = AttackBase.attackType.skill;
        bomb.power = 22;
        bomb.spCost = 5;
        bomb.name = "Bomb";
    }

    public override void moveSelector()
    {
        switch (benoanim) {
            case animationBeno.punch:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack",0);
                break;
            case animationBeno.shoot:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 1);
                break;
            case animationBeno.machinegun:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 2);
                break;
            case animationBeno.shadowsnipe:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 3);
                break;
            case animationBeno.bomb:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 4);
                break;
        }
    }
    

    new void Update()
    {
        base.Update();
    }



    public override void attackSelector()
    {
        if (selectedAttack.name == "Punch")
        {
            benoanim = animationBeno.punch;
            animations = animationstate.attack;
            moveSelector();
        }
        if (selectedAttack.name == "Shoot")
        {
            benoanim = animationBeno.shoot;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Machine Gun")
        {
            benoanim = animationBeno.machinegun;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Shadow Snipe")
        {
            benoanim = animationBeno.shadowsnipe;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Bomb")
        {
            benoanim = animationBeno.bomb;
            animations = animationstate.attack;
        }
        moveSelector();
    }
}

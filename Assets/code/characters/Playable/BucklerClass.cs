using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucklerClass : PlayerCharacter
{

    public enum animationBuckler { shoot, flamethrower, doubleshot, shadowsnipe, bomb, machine_gun, electrcute, bonebreak };
    public animationBuckler buckAnimation;
    public float speed;

    private void Awake()
    {
        name = "Buckler";
    }

    new void Start()
    {
        specialIncrementer = 2;
        attackIncrementer = 3;
        defenceIncrementer = 2;
        healthIncrementer = 2;
        intelligenceIncrementer = 3;

        weakness = AttackBase.attackElement.normal;
        resistant = AttackBase.attackElement.shadow;

        baseHealth = 35;
        baseAttack = 5;
        baseDefence = 3;
        baseIntelligence = 2;
        baseSpecialPoints = 5;

        baseExp = 12;
        attackIncPercentage = 0.49f;
        defenceIncPercentage = 0.30f;
        intelligenceIncPercentage = 0.24f;
        specialIncPercentage = 0.18f;

        base.Start();
        
        /*
        AttackBase dblStrk = new AttackBase();
        dblStrk.name = "Double Strike";
        dblStrk.power = 11;
        dblStrk.attkType = AttackBase.attackType.attack;
        moves.Add(dblStrk);*/
    }

    public override void DefaultMoves()
    {
        AttackBase shoot = new AttackBase();
        shoot.attkElement = AttackBase.attackElement.normal;
        shoot.attkRng = AttackBase.attackRange.single;
        shoot.attkType = AttackBase.attackType.attack;
        shoot.power = 18;
        shoot.name = "Shoot";
        moves.Add(shoot);

        /*
        AttackBase doubleshot = new AttackBase();
        doubleshot.attkElement = AttackBase.attackElement.normal;
        doubleshot.attkRng = AttackBase.attackRange.single;
        doubleshot.attkType = AttackBase.attackType.skill;
        doubleshot.power = 12;
        doubleshot.name = "Double Shot";
        doubleshot.spCost = 2;
        moves.Add(doubleshot);

        AttackBase flamethrower = new AttackBase();
        flamethrower.attkElement = AttackBase.attackElement.fire;
        flamethrower.attkRng = AttackBase.attackRange.all;
        flamethrower.attkType = AttackBase.attackType.skill;
        flamethrower.name = "Flamethrower";
        flamethrower.power = 25;
        flamethrower.spCost = 4;
        moves.Add(flamethrower);

        AttackBase bomb = new AttackBase();
        bomb.attkElement = AttackBase.attackElement.normal;
        bomb.attkRng = AttackBase.attackRange.all;
        bomb.attkType = AttackBase.attackType.skill;
        bomb.power = 38;
        bomb.spCost = 12;
        bomb.name = "Bomb";
        moves.Add(bomb);

        AttackBase bone= new AttackBase();
        bone.attkElement = AttackBase.attackElement.normal;
        bone.attkRng = AttackBase.attackRange.single;
        bone.attkType = AttackBase.attackType.skill;
        bomb.power = 0;
        bomb.spCost = 12;
        bomb.name = "Bonebreak";
        moves.Add(bone);

        AttackBase electrcute = new AttackBase();
        electrcute.attkElement = AttackBase.attackElement.electric;
        electrcute.attkRng = AttackBase.attackRange.single;
        electrcute.attkType = AttackBase.attackType.skill;
        electrcute.name = "Electrocute";
        electrcute.power = 30;
        electrcute.spCost = 8;
        moves.Add(electrcute);

        AttackBase shadowsnipe = new AttackBase();
        shadowsnipe.attkElement = AttackBase.attackElement.shadow;
        shadowsnipe.attkRng = AttackBase.attackRange.single;
        shadowsnipe.attkType = AttackBase.attackType.skill;
        shadowsnipe.power = 40;
        shadowsnipe.spCost = 8;
        shadowsnipe.name = "Shadow Snipe";
        moves.Add(shadowsnipe);
        */
    }

    public override void moveSelector()
    {
        switch (buckAnimation)
        {
            case animationBuckler.shoot:

                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationBuckler.doubleshot:

                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationBuckler.flamethrower:

                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationBuckler.shadowsnipe:

                charAnimatior.SetInteger("Attack", 3);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationBuckler.bomb:
                charAnimatior.SetInteger("Attack", 4);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationBuckler.electrcute:
                charAnimatior.SetInteger("Attack", 6);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationBuckler.bonebreak:
                charAnimatior.SetInteger("Attack", 5);
                charAnimatior.SetBool("IsAttacking", true);
                break;
        }
    }

    void flameThrower(float rotation)
    {
        GameObject fx = Resources.Load("prefabs/projectiles/FlameThrower") as GameObject;
        GameObject flamefx = Instantiate(fx, transform.position, Quaternion.Euler(0, 0, rotation));
        flamefx.GetComponent<ProjectileClass>().StartCoroutine(flamefx.GetComponent<ProjectileClass>().projectileShoot(rotation));
    }

    void BoneBreak() {
        selectedAttack.power = (defence / 2) + attack + 35;
    }
    
    new void Update()
    {
        base.Update();
    }

    public override void Attack(BattleCharacter target, AttackBase move)
    {
        selectedAttack = move;

        battlehandler.BSM.battlelog("- " + this.name + " preformed " + selectedAttack.name + ".");
        if (selectedAttack.attkRng != AttackBase.attackRange.all)
        {
            targetChar = target;
        }
        else
        {
            targetChar = null;
        }

        if (move.name == "Shoot")
        {
            buckAnimation = animationBuckler.shoot;
            animations = animationstate.attack;
        }

        if (move.name == "Double Shot")
        {
            buckAnimation = animationBuckler.doubleshot;
            animations = animationstate.attack;
        }

        if (move.name == "Shadow Snipe")
        {
            buckAnimation = animationBuckler.shadowsnipe;
            animations = animationstate.attack;
        }

        if (move.name == "Flamethrower")
        {
            buckAnimation = animationBuckler.flamethrower;
            animations = animationstate.attack;
        }

        if (move.name == "Bomb")
        {
            buckAnimation = animationBuckler.bomb;
            animations = animationstate.attack;
        }
        if (move.name == "Electrocute")
        {
            buckAnimation = animationBuckler.electrcute;
            animations = animationstate.attack;
        }

        if (move.name == "Bonebreak")
        {
            buckAnimation = animationBuckler.bonebreak;
            animations = animationstate.attack;
        }

        moveSelector();
    }
}

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

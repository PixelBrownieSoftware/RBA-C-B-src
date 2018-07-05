using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlacklerClass : EnemyCharacter
{

    public enum animationBlackler { slam, chill, engulf, shadowwalk, blackhole };
    public animationBlackler blackAnimation;

    public float speed;

    private void Awake()
    {
        name = "Blackler";
    }

    new void Start()
    {
        transform.rotation = Quaternion.Euler( 0, 180,0);

        weakness = AttackBase.attackElement.light;
        resistant = AttackBase.attackElement.shadow;


        moneyAmount = 7;
        baseExpToGive = 24;

        expIncrementer = 25;
        attackIncrementer = 3;
        defenceIncrementer = 4;
        healthIncrementer = 6;
        intelligenceIncrementer = 3;
        specialIncrementer = 2;

        baseHealth = 45;
        baseAttack = 1;
        baseDefence = 3;
        baseIntelligence = 2;
        baseSpecialPoints = 15;

        base.Start();


        AttackBase bite = new AttackBase();
        bite.name = "Bite";
        bite.attkElement = AttackBase.attackElement.shadow;
        bite.power = 7;
        bite.spCost = 0;
        moves.Add(bite);

        AttackBase engulf = new AttackBase();
        engulf.name = "Engulf";
        engulf.attkRng = AttackBase.attackRange.single;
        engulf.attkElement = AttackBase.attackElement.shadow;
        engulf.attkType = AttackBase.attackType.magic;
        engulf.power = 25;
        engulf.spCost = 4;
        moves.Add(engulf);

        AttackBase shadowWalk = new AttackBase();
        shadowWalk.attkRng = AttackBase.attackRange.single;
        shadowWalk.attkElement = AttackBase.attackElement.shadow;
        shadowWalk.attkType = AttackBase.attackType.skill;
        shadowWalk.name = "Shadow-Walk";
        shadowWalk.power = 34;
        shadowWalk.spCost = 6;
        moves.Add(shadowWalk);

        AttackBase chill = new AttackBase();
        chill.attkElement = AttackBase.attackElement.ice;
        chill.attkRng = AttackBase.attackRange.all;
        chill.attkType = AttackBase.attackType.magic;
        chill.power = 27;
        chill.spCost = 5;
        chill.name = "Chill";
        moves.Add(chill);

        AttackBase blackhole = new AttackBase();
        blackhole.name = "Black Hole";
        blackhole.spCost = 10;
        blackhole.power = 40;
        blackhole.attkElement = AttackBase.attackElement.shadow;
        blackhole.attkRng = AttackBase.attackRange.all;
        blackhole.attkType = AttackBase.attackType.magic;
        moves.Add(blackhole);

    }

    void CreateBlackHole()
    {
        CreateFxAtPos("Black Hole", new Vector2(23, 64));
    }

    public override void moveSelector() {
        switch (blackAnimation) {
            case animationBlackler.slam:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 0);
                break;
            case animationBlackler.shadowwalk:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 1);
                break;
            case animationBlackler.engulf:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 2);
                break;
            case animationBlackler.chill:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 3);
                break;
            case animationBlackler.blackhole:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 4);
                break;
        }
    }

    void shadowwalk() {
        transform.position = new Vector2(targetChar.transform.position.x + 35f, targetChar.transform.position.y);
    }

    new void Update()
    {
        base.Update();
    }

    public override void attackSelector()
    {
        if (selectedAttack.name == "Bite")
        {
            blackAnimation = animationBlackler.slam;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Shadow-Walk")
        {
            blackAnimation = animationBlackler.shadowwalk;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Engulf")
        {
            blackAnimation = animationBlackler.engulf;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Chill")
        {
            blackAnimation = animationBlackler.chill;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Black Hole")
        {
            blackAnimation = animationBlackler.blackhole;
            animations = animationstate.attack;
        }
        moveSelector();
    }
    
}

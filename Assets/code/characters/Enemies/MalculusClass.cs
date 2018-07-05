using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MalculusClass : EnemyCharacter {

    public enum animationMalculus { sword, brutalblow, heal, blackhole, lightning, tripleblow, holystrike, Incenerate, Blizzard };
    public animationMalculus malAnimation;

    public float speed;

    private void Awake()
    {
        name = "King Malculus";
    }

    new void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);

        moneyAmount = 90;
        baseExpToGive = 0;
        
        attackIncrementer = 8;
        defenceIncrementer = 6;
        intelligenceIncrementer = 9;

        baseHealth = 2600;
        baseAttack = 5;
        baseDefence = 3;
        baseIntelligence = 5;
        baseSpecialPoints = 1000;

        base.Start();

        AttackBase swordSlash = new AttackBase();
        swordSlash.attkElement = AttackBase.attackElement.normal;
        swordSlash.attkRng = AttackBase.attackRange.single;
        swordSlash.attkType = AttackBase.attackType.attack;
        swordSlash.name = "Sword Slash";
        swordSlash.power = 25;
        moves.Add(swordSlash);

        AttackBase lightning = new AttackBase();
        lightning.attkElement = AttackBase.attackElement.electric;
        lightning.attkType = AttackBase.attackType.magic;
        lightning.name = "Lightning";
        lightning.power = 40;
        lightning.spCost = 10;
        moves.Add(lightning);

        AttackBase blizzard = new AttackBase();
        blizzard.power = 50;
        blizzard.spCost = 13;
        blizzard.attkElement = AttackBase.attackElement.ice;
        blizzard.attkRng = AttackBase.attackRange.single;
        blizzard.attkType = AttackBase.attackType.magic;
        blizzard.name = "Blizzard";
        moves.Add(blizzard);
        
        AttackBase brutalBlow = new AttackBase();
        brutalBlow.attkElement = AttackBase.attackElement.shadow;
        brutalBlow.attkRng = AttackBase.attackRange.single;
        brutalBlow.attkType = AttackBase.attackType.skill;
        brutalBlow.name = "Brutal Blow";
        brutalBlow.spCost = 10;
        brutalBlow.power = health/3;
        moves.Add(brutalBlow);

        AttackBase blackhole = new AttackBase();
        blackhole.name = "Black Hole";
        blackhole.spCost = 7;
        blackhole.power = 45;
        blackhole.attkElement = AttackBase.attackElement.shadow;
        blackhole.attkRng = AttackBase.attackRange.all;
        blackhole.attkType = AttackBase.attackType.magic;
        moves.Add(blackhole);

        AttackBase holystrike = new AttackBase();
        holystrike.attkElement = AttackBase.attackElement.light;
        holystrike.attkRng = AttackBase.attackRange.single;
        holystrike.attkType = AttackBase.attackType.skill;
        holystrike.name = "Holy Strike";
        holystrike.power = 30;
        holystrike.spCost = 6;
        moves.Add(holystrike);

        AttackBase incenerate = new AttackBase();
        incenerate.attkElement = AttackBase.attackElement.fire;
        incenerate.attkRng = AttackBase.attackRange.all;
        incenerate.attkType = AttackBase.attackType.magic;
        incenerate.name = "Incenerate";
        incenerate.power = 45;
        incenerate.spCost = 12;
        moves.Add(incenerate);

        AttackBase heal = new AttackBase();
        heal.attkElement = AttackBase.attackElement.normal;
        heal.attkRng = AttackBase.attackRange.single;
        heal.attkType = AttackBase.attackType.heal;
        heal.name = "Heal";
        heal.power = 300;
        heal.spCost = 45;
        moves.Add(heal);
    }
    

    public override void moveSelector()
    {
        switch (malAnimation)
        {
            case animationMalculus.sword:
                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationMalculus.brutalblow:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                selectedAttack.power = health / 3;
                break;
            case animationMalculus.tripleblow:
                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationMalculus.lightning:
                charAnimatior.SetInteger("Attack", 3);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationMalculus.blackhole:
                charAnimatior.SetInteger("Attack", 4);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationMalculus.holystrike:
                charAnimatior.SetInteger("Attack", 5);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationMalculus.Blizzard:
                charAnimatior.SetInteger("Attack",6);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationMalculus.Incenerate:
                charAnimatior.SetInteger("Attack", 7);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationMalculus.heal:
                charAnimatior.SetInteger("Attack", 8);
                charAnimatior.SetBool("IsAttacking", true);
                break;
        }
    }

    public new void CustomAI() {

    }

    public override void attackSelector()
    {
        if (selectedAttack.name == "Sword Slash")
        {
            malAnimation = animationMalculus.sword;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Brutal Blow")
        {
            malAnimation = animationMalculus.brutalblow;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Black Hole")
        {
            malAnimation = animationMalculus.blackhole;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Blizzard")
        {
            malAnimation = animationMalculus.Blizzard;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Heal")
        {
            malAnimation = animationMalculus.heal;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Incenerate")
        {
            malAnimation = animationMalculus.Incenerate;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Lightning")
        {
            malAnimation = animationMalculus.lightning;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Triple Blow")
        {
            malAnimation = animationMalculus.tripleblow;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Holy Strike")
        {
            malAnimation = animationMalculus.holystrike;
            animations = animationstate.attack;
        }
        moveSelector();
    }


    void CreateBlackHole()
    {
        CreateFxAtPos("Black Hole", new Vector2(23, 64));
    }

    new void Update()
    {
        base.Update();
    }
}

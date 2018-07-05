using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunkoClass : EnemyCharacter {

    public enum animationTrunko{ meteor, mindsword, fire, frost, incenerate, blizzard, heal, osmosis };
    public animationTrunko animTrunko;

    public GameObject meteorObj;

    private void Awake()
    {
        name = "Trunko";
    }

    new void Start () {
        weakness = AttackBase.attackElement.shadow;
        resistant = AttackBase.attackElement.fire;

        moneyAmount = 10;
        baseExpToGive = 19;
        expIncrementer = 23;

        attackIncrementer = 2;
        defenceIncrementer = 4;
        healthIncrementer = 3;
        specialIncrementer = 2;
        intelligenceIncrementer = 5;

        baseSpecialPoints = 15;
        baseHealth = 75;
        baseAttack = 1;
        baseDefence = 2;
        baseIntelligence = 4;
        
        base.Start();

        
        AttackBase slash = new AttackBase();
        slash.name = "Shoot Meteor";
        slash.power = 10;
        slash.attkElement = AttackBase.attackElement.normal;
        slash.attkRng = AttackBase.attackRange.single;
        slash.attkType = AttackBase.attackType.magic;
        moves.Add(slash);

        AttackBase heal = new AttackBase();
        heal.name = "Heal";
        heal.power = 55;
        heal.spCost = 7;
        heal.attkType = AttackBase.attackType.heal;
        slash.attkElement = AttackBase.attackElement.normal;
        slash.attkRng = AttackBase.attackRange.single;
        moves.Add(heal);

        if (level > 9)
        {
            AttackBase frost = new AttackBase();
            frost.name = "Frost";
            frost.power = 25;
            frost.attkType = AttackBase.attackType.magic;
            frost.attkElement = AttackBase.attackElement.ice;
            frost.spCost = 5;
            moves.Add(frost);
        }
        if (level > 15)
        {
            AttackBase osmosis = new AttackBase();
            osmosis.attkType = AttackBase.attackType.magic;
            osmosis.attkElement = AttackBase.attackElement.ice;
            osmosis.attkRng = AttackBase.attackRange.single;
            osmosis.spCost = 2;
            osmosis.power = 5;
            osmosis.name = "Osmosis";
            moves.Add(osmosis);

            AttackBase mindsword = new AttackBase();
            mindsword.attkElement = AttackBase.attackElement.light;
            mindsword.attkType = AttackBase.attackType.magic;
            mindsword.attkRng = AttackBase.attackRange.single;
            mindsword.name = "Mind Sword";
            mindsword.power = 20;
            mindsword.spCost = 6;
            moves.Add(mindsword);
        }
        if (level > 28)
        {
            AttackBase incenerate = new AttackBase();
            incenerate.attkElement = AttackBase.attackElement.fire;
            incenerate.attkRng = AttackBase.attackRange.all;
            incenerate.attkType = AttackBase.attackType.magic;
            incenerate.name = "Incenerate";
            incenerate.power = 45;
            incenerate.spCost = 12;
            moves.Add(incenerate);

            AttackBase blizzard = new AttackBase();
            blizzard.power = 50;
            blizzard.spCost = 13;
            blizzard.attkElement = AttackBase.attackElement.ice;
            blizzard.attkRng = AttackBase.attackRange.single;
            blizzard.attkType = AttackBase.attackType.magic;
            blizzard.name = "Blizzard";
            moves.Add(blizzard);

        }

    }

    new void Update () {
        base.Update();
	}

    void shootMeteor()
    {
        GameObject projectile = Instantiate(meteorObj, new Vector3(34, 130, 2), Quaternion.identity);
        ProjectileClass meteor = projectile.GetComponent<ProjectileClass>();
        meteor.StartCoroutine(meteor.meteorStrike(targetChar.transform.position));
    }

    public override void moveSelector()
    {
        switch (animTrunko) {
            case animationTrunko.meteor:
                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationTrunko.heal:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationTrunko.frost:
                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationTrunko.fire:
                charAnimatior.SetInteger("Attack", 3);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationTrunko.mindsword:
                charAnimatior.SetInteger("Attack", 4);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationTrunko.incenerate:
                charAnimatior.SetInteger("Attack", 5);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationTrunko.blizzard:
                charAnimatior.SetInteger("Attack", 6);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationTrunko.osmosis:
                charAnimatior.SetInteger("Attack", 7);
                charAnimatior.SetBool("IsAttacking", true);
                break;
        }
    }

    public override void attackSelector()
    {
        if (selectedAttack.name == "Shoot Meteor")
        {
            animTrunko = animationTrunko.meteor;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Heal")
        {
            animTrunko = animationTrunko.heal;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Mind Sword")
        {
            animTrunko = animationTrunko.mindsword;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Burn")
        {
            animTrunko = animationTrunko.fire;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Frost")
        {
            animTrunko = animationTrunko.frost;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Blizzard")
        {
            animTrunko = animationTrunko.blizzard;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Osmosis")
        {
            animTrunko = animationTrunko.osmosis;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Incenerate")
        {
            animTrunko = animationTrunko.incenerate;
            animations = animationstate.attack;
        }
        moveSelector();
    }
}

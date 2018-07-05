using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LooneyClass : EnemyCharacter {
    
    public enum animationLooney { slam, brutal_blow };
    public animationLooney looAnimation;
    

    private void Awake()
    {
        if (level <= 28) { name = "Looney"; } else { name = "Lord Looney"; }
    }

    new void Start () {

        weakness = AttackBase.attackElement.electric;
        resistant = AttackBase.attackElement.normal;

        moneyAmount = 2;

        baseExpToGive = 1;

        expIncrementer = 2;
        attackIncrementer = 2;
        defenceIncrementer = 1;
        healthIncrementer = 7;
        intelligenceIncrementer = 1;

        baseHealth = 40;
        baseAttack = 2;
        baseDefence = 2;
        baseIntelligence = 1;

        base.Start();

        AttackBase pound = new AttackBase();
        pound.name = "Slam";
        pound.power = 10;
        pound.attkElement = AttackBase.attackElement.normal;
        pound.attkType = AttackBase.attackType.attack;
        moves.Add(pound);

        if (level > 28) {

            //Long time, no see Redler! I've... well improved since you last saw my other freinds...
            //I've been training and eating a lot, so watch your back
            //The king also taught me a very valuable move that nobody else has ever learnt...

            defence += 155;
            attack += 85;
            maxHealth += 100;
            health = maxHealth;
            //
            AttackBase brutalBlow = new AttackBase();
            brutalBlow.attkElement = AttackBase.attackElement.shadow;
            brutalBlow.attkRng = AttackBase.attackRange.single;
            brutalBlow.attkType = AttackBase.attackType.skill;
            brutalBlow.name = "Brutal Blow";
            brutalBlow.power = maxHealth / 2;
            moves.Add(brutalBlow);
        }
    }
    

    public override void moveSelector()
    {
        switch (looAnimation)
        {
            case animationLooney.slam:

                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationLooney.brutal_blow:

                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;
        }
    }
    
    
    new void Update () {
        base.Update();
    }

    public override void attackSelector()
    {
        if (selectedAttack.name == "Slam") {
            looAnimation = animationLooney.slam;
            animations = animationstate.attack;
            moveSelector();
        }

        if (selectedAttack.name == "Brutal Blow") {
            looAnimation = animationLooney.brutal_blow;
            animations = animationstate.attack;
            moveSelector();
        }
    }

}

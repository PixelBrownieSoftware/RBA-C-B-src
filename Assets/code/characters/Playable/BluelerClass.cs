using UnityEngine;
using System.Collections;

public class BluelerClass : PlayerCharacter {

    public enum animationBlueler { wrenchSmash, froststrike, Heal, impulse, blue_frenzy, triple_strike, chill, diamond_gleam, holy_strike, vitalize };
    public animationBlueler blueAnimation;
    public float speed;

    private void Awake()
    {
        name = "Blueler";
    }

    new void Start() {
        specialIncrementer = 2;
        attackIncrementer = 2;
        defenceIncrementer = 2;
        healthIncrementer = 5;
        intelligenceIncrementer = 3;

        weakness = AttackBase.attackElement.fire;
        resistant = AttackBase.attackElement.ice;

        baseHealth = 32;
        baseAttack = 3;
        baseDefence = 3;
        baseIntelligence = 3;
        baseSpecialPoints = 7;

        baseExp = 12;
        attackIncPercentage = 0.26f;
        defenceIncPercentage = 0.40f;
        intelligenceIncPercentage = 0.37f;
        specialIncPercentage = 0.22f;

        base.Start();

    }

    void BluelerBigSpawn() {
        CreateFxAtPos("BigBlue", transform.position);
    }

    void DiamondGleam() {
        CreateFxAtPos("Diamond", new Vector2(0, 0));
    }

    void createBlueler() {
        GameObject mndswordfx = Resources.Load("prefabs/projectiles/CloneBlueler") as GameObject;
        GameObject mind = Instantiate(mndswordfx, transform.position, Quaternion.identity);
        ProjectileClass proj = mind.GetComponent<ProjectileClass>();

        BattleCharacter bc = battlehandler.BSM.enemies[Random.Range(0, battlehandler.BSM.enemies.Count)];

        proj.StartCoroutine(proj.meteorStrike(bc.gameObject.transform.position));
    }



    public override void Attack(BattleCharacter target, AttackBase move) {
        selectedAttack = move;
        targetChar = target;

        battlehandler.BSM.battlelog("- " + this.name + " preformed " + selectedAttack.name + ".");
        if (move.name == "Wrench Whack")
        {
            blueAnimation = animationBlueler.wrenchSmash;
            animations = animationstate.attack;
        }

        if (move.name == "Frost Strike")
        {
            blueAnimation = animationBlueler.froststrike;
            animations = animationstate.attack;
        }

        if (move.name == "Restore")
        {
            blueAnimation = animationBlueler.Heal;
            animations = animationstate.attack;
        }

        if (move.name == "Impulse")
        {
            blueAnimation = animationBlueler.impulse;
            animations = animationstate.attack;
        }

        if (move.name == "Blue Frenzy")
        {
            blueAnimation = animationBlueler.blue_frenzy;
            animations = animationstate.attack;
        }
        if (move.name == "Triple Strike")
        {
            blueAnimation = animationBlueler.triple_strike;
            animations = animationstate.attack;
        }

        if (move.name == "Chill")
        {
            blueAnimation = animationBlueler.chill;
            animations = animationstate.attack;
        }
        if (move.name == "Holy Strike")
        {
            blueAnimation = animationBlueler.holy_strike;
            animations = animationstate.attack;
        }

        if (move.name == "Diamond Gleam")
        {
            blueAnimation = animationBlueler.diamond_gleam;
            animations = animationstate.attack;
        }

        if (move.name == "Vitalize")
        {
            blueAnimation = animationBlueler.vitalize;
            animations = animationstate.attack;
        }

        moveSelector();
    }

    public override void moveSelector()
    {
        switch (blueAnimation)
        {
            case animationBlueler.wrenchSmash:
                charAnimatior.SetBool("IsAttacking",true);
                charAnimatior.SetInteger("Attack",0);
                break;
            case animationBlueler.Heal:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 1);
                break;
            case animationBlueler.froststrike:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 2);
                break;
            case animationBlueler.impulse:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 3);
                break;
            case animationBlueler.blue_frenzy:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 4);
                break;
            case animationBlueler.triple_strike:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 5);
                break;
            case animationBlueler.chill:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 6);
                break;
            case animationBlueler.diamond_gleam:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 7);
                break;
            case animationBlueler.holy_strike:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 8);
                break;
            case animationBlueler.vitalize:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 9);
                break;
        }
    }

    void FrenzyPow() {
        selectedAttack.power = 10 + (health / 2) + (intelligence / 2);
    }

    void ImpulsePowe() {
        selectedAttack.power = 20 + Mathf.FloorToInt(health / 1.7f);
    }

    public override void DefaultMoves()
    {
        AttackBase wWack = new AttackBase();
        wWack.name = "Wrench Whack";
        wWack.power = 12;
        wWack.attkRng = AttackBase.attackRange.single;
        wWack.attkType = AttackBase.attackType.attack;
        wWack.attkElement = AttackBase.attackElement.normal;
        moves.Add(wWack);
    }

    new void Update() {
        base.Update();
    }
}

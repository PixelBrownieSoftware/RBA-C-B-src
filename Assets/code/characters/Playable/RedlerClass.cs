using UnityEngine;
using System.Collections;
public class RedlerClass : PlayerCharacter {

    public enum animationRedler { bBallSmash, back, Heal, doubleStrike, smite, trioAttk, fireStrike, holyBall, mindSword, red_frenzy, incenerate, optimize};
    public animationRedler redAnimation;
    public float speed;

    private void Awake()
    {
        name = "Redler";
    }

    new void Start () {

        attackIncrementer = 2;
        specialIncrementer = 1;
        defenceIncrementer = 2;
        healthIncrementer = 3;
        intelligenceIncrementer = 3;

        weakness = AttackBase.attackElement.ice;
        resistant = AttackBase.attackElement.fire;
        
        baseHealth = 55;
        baseAttack = 3;
        baseDefence = 3;
        baseSpecialPoints = 15;
        baseIntelligence = 3;

        baseExp = 12;
        attackIncPercentage = 0.33f;
        defenceIncPercentage = 0.32f;
        intelligenceIncPercentage = 0.28f;
        specialIncPercentage = 0.26f;

        base.Start();

        
        /*
        AttackBase dblStrk = new AttackBase();
        dblStrk.name = "Double Strike";
        dblStrk.power = 11;
        dblStrk.attkType = AttackBase.attackType.attack;
        moves.Add(dblStrk);*/
    }

    void fireness()
    {
        selectedAttack.attkElement = AttackBase.attackElement.fire;
        dealDamage();
        GameObject fx = Resources.Load("prefabs/projectiles/fireTrio") as GameObject;
        Instantiate(fx, new Vector2(50, 10), Quaternion.identity);
    }

    void whiteness()
    {
        selectedAttack.attkElement = AttackBase.attackElement.light;
        GameObject fx = Resources.Load("prefabs/projectiles/whiteTrio") as GameObject;
        Instantiate(fx, new Vector2(120, 10), Quaternion.identity);
    }

    void darkness()
    {
        selectedAttack.attkElement = AttackBase.attackElement.shadow;
        dealDamage();
        GameObject fx = Resources.Load("prefabs/projectiles/blackTrio") as GameObject;
        Instantiate(fx, new Vector2(70, 60), Quaternion.identity);
    }

    void FrenzyPow() {
        selectedAttack.power = (health / 2) + (intelligence / 2);
    }

    void createRedler()
    {
        GameObject mndswordfx = Resources.Load("prefabs/projectiles/RedlerClone") as GameObject;
        GameObject mind = Instantiate(mndswordfx, transform.position, Quaternion.identity);
        ProjectileClass proj = mind.GetComponent<ProjectileClass>();

        BattleCharacter bc = battlehandler.BSM.enemies[Random.Range(0, battlehandler.BSM.enemies.Count)];

        proj.StartCoroutine(proj.meteorStrike(bc.gameObject.transform.position));
    }

    public override void DefaultMoves()
    {
        AttackBase bbalSmash = new AttackBase();
        bbalSmash.name = "Baseball Smash";
        bbalSmash.power = 12;
        bbalSmash.attkType = AttackBase.attackType.attack;
        bbalSmash.attkRng = AttackBase.attackRange.single;
        moves.Add(bbalSmash);

        AttackBase heal = new AttackBase();
        heal.name = "Heal";
        heal.spCost = 4;
        heal.power = 25;
        heal.attkType = AttackBase.attackType.heal;
        heal.attkRng = AttackBase.attackRange.single;
        moves.Add(heal);



        /*       
         *     
         *     
        
        AttackBase redfrenzy = new AttackBase();
        redfrenzy.attkElement = AttackBase.attackElement.fire;
        redfrenzy.attkRng = AttackBase.attackRange.all;
        redfrenzy.attkType = AttackBase.attackType.magic;
        redfrenzy.name = "Red Frenzy";
        redfrenzy.spCost = 5;
        moves.Add(redfrenzy);


        AttackBase opt = new AttackBase();
        opt.name = "Optimize";
        opt.spCost = 9;
        opt.power = 65;
        opt.attkElement = AttackBase.attackElement.normal;
        opt.attkRng = AttackBase.attackRange.all;
        opt.attkType = AttackBase.attackType.heal;
        moves.Add(opt);

        AttackBase incenerate = new AttackBase();
        incenerate.attkElement = AttackBase.attackElement.fire;
        incenerate.attkRng = AttackBase.attackRange.all;
        incenerate.attkType = AttackBase.attackType.magic;
        incenerate.name = "Incenerate";
        incenerate.power = 45;
        incenerate.spCost = 12;
        moves.Add(incenerate);

        AttackBase mindsword = new AttackBase();
        mindsword.attkElement = AttackBase.attackElement.light;
        mindsword.attkType = AttackBase.attackType.magic;
        mindsword.name = "Mind Sword";
        mindsword.power = 11;
        mindsword.spCost = 2;
        moves.Add(mindsword);

        AttackBase trioattack = new AttackBase();
        trioattack.attkElement = AttackBase.attackElement.normal;
        trioattack.attkRng = AttackBase.attackRange.all;
        trioattack.attkType = AttackBase.attackType.magic;
        trioattack.name = "Trio Attack";
        trioattack.power = 45;
        trioattack.spCost = 7;
        moves.Add(trioattack);

        AttackBase holystrike = new AttackBase();
        holystrike.attkElement = AttackBase.attackElement.light;
        holystrike.attkRng = AttackBase.attackRange.single;
        holystrike.attkType = AttackBase.attackType.skill;
        holystrike.name = "Holy Strike";
        holystrike.power = 30;
        holystrike.spCost = 6;
        moves.Add(holystrike);



        AttackBase fireStrike = new AttackBase();
        fireStrike.attkElement = AttackBase.attackElement.fire;
        fireStrike.attkRng = AttackBase.attackRange.single;
        fireStrike.attkType = AttackBase.attackType.skill;
        fireStrike.name = "Fire Strike";
        fireStrike.power = 25;
        fireStrike.spCost = 4;
        moves.Add(fireStrike);

        */
    }

    public override void moveSelector()
    {
        switch (redAnimation)
        {
            case animationRedler.bBallSmash:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 0);
                print("Heal");
                break;

            case animationRedler.Heal:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 2);
                break;

            case animationRedler.doubleStrike:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 1);
                break;

            case animationRedler.smite:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 3);
                break;

            case animationRedler.trioAttk:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 4);
                break;

            case animationRedler.fireStrike:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 5);
                break;

            case animationRedler.holyBall:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 6);
                break;

            case animationRedler.mindSword:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 7);
                break;

            case animationRedler.red_frenzy:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 8);
                break;

            case animationRedler.incenerate:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 9);
                break;

            case animationRedler.optimize:
                charAnimatior.SetBool("IsAttacking", true);
                charAnimatior.SetInteger("Attack", 10);
                break;
        }
    }

    new void Update () {
        base.Update();
    }

    void RedlerBigSpawn() {
        GameObject mndswordfx = Resources.Load("prefabs/projectiles/BigRed") as GameObject;
        GameObject mind = Instantiate(mndswordfx, transform.position, Quaternion.identity);
        ProjectileClass proj = mind.GetComponent<ProjectileClass>();
    }

    public override void Attack(BattleCharacter target, AttackBase move)
    {
        selectedAttack = move;
        if (selectedAttack.attkRng != AttackBase.attackRange.all) {
            targetChar = target;
        } else {
            targetChar = null;
        }
        battlehandler.BSM.battlelog("- " + this.name + " prefromed " + selectedAttack.name);

        if (move.name == "Baseball Smash") {
            redAnimation = animationRedler.bBallSmash;
            animations = animationstate.attack;
        }

        if (move.name == "Double Strike") {
            redAnimation = animationRedler.doubleStrike;
            animations = animationstate.attack;
        }

        if (move.name == "Flame Strike") {
            redAnimation = animationRedler.fireStrike;
            animations = animationstate.attack;
        }

        if (move.name == "Smite") {
            redAnimation = animationRedler.smite;
            animations = animationstate.attack;
        }

        if (move.name == "Ominous Strike") {
            redAnimation = animationRedler.holyBall;
            animations = animationstate.attack;
        }

        if (move.name == "Trio Attack") {
            redAnimation = animationRedler.trioAttk;
            animations = animationstate.attack;
        }

        if (move.name == "Heal") {
            redAnimation = animationRedler.Heal;
            animations = animationstate.attack;
        }

        if (move.name == "Mind Sword")
        {
            redAnimation = animationRedler.mindSword;
            animations = animationstate.attack;
        }

        if (move.name == "Red Frenzy") {
            redAnimation = animationRedler.red_frenzy;
            animations = animationstate.attack;
        }

        if (move.name == "Incenerate") {
            redAnimation = animationRedler.incenerate;
            animations = animationstate.attack;
        }

        if (move.name == "Optimize") {
            redAnimation = animationRedler.optimize;
            animations = animationstate.attack;
        }
        moveSelector();
    }
}

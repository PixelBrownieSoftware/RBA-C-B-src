using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LordRedClass : EnemyCharacter
{

    public enum animationLordRed { slam, trioattk, red_frenzy, holystrike, flamestrike, heal, mindsword, incinerate, double_strike };
    public animationLordRed lrdredAnimation;

    public float speed;

    private void Awake()
    {
        name = "Lord Red";
    }

    new void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        weakness = AttackBase.attackElement.ice;
        resistant = AttackBase.attackElement.fire;

        moneyAmount = 40;
        baseExpToGive = 35000;

        attackIncrementer = 5;
        defenceIncrementer = 4;
        healthIncrementer = 38;
        intelligenceIncrementer = 5;
        specialIncrementer = 4;

        baseHealth = 90;
        baseAttack = 2;
        baseDefence = 3;
        baseIntelligence = 3;
        baseSpecialPoints = 20;

        base.Start();

        AttackBase swordslash = new AttackBase();
        swordslash.attkElement = AttackBase.attackElement.normal;
        swordslash.attkRng = AttackBase.attackRange.single;
        swordslash.attkType = AttackBase.attackType.attack;
        swordslash.name = "Sword Slash";
        swordslash.power = 17;
        moves.Add(swordslash);

        AttackBase trioattack = new AttackBase();
        trioattack.attkElement = AttackBase.attackElement.normal;
        trioattack.attkRng = AttackBase.attackRange.all;
        trioattack.attkType = AttackBase.attackType.magic;
        trioattack.name = "Trio Attack";
        trioattack.power = 40;
        trioattack.spCost = 25;
        moves.Add(trioattack);

        AttackBase holystrike = new AttackBase();
        holystrike.attkElement = AttackBase.attackElement.light;
        holystrike.attkRng = AttackBase.attackRange.single;
        holystrike.attkType = AttackBase.attackType.skill;
        holystrike.name = "Holy Strike";
        holystrike.power = 30;
        holystrike.spCost = 6;
        moves.Add(holystrike);

        AttackBase redfrenzy = new AttackBase();
        redfrenzy.attkElement = AttackBase.attackElement.fire;
        redfrenzy.attkRng = AttackBase.attackRange.all;
        redfrenzy.attkType = AttackBase.attackType.magic;
        redfrenzy.name = "Red Frenzy";
        redfrenzy.spCost = 25;
        moves.Add(redfrenzy);

        AttackBase fireStrike = new AttackBase();
        fireStrike.attkElement = AttackBase.attackElement.fire;
        fireStrike.attkRng = AttackBase.attackRange.single;
        fireStrike.attkType = AttackBase.attackType.skill;
        fireStrike.name = "Fire Strike";
        fireStrike.power = 45;
        fireStrike.spCost = 5;
        moves.Add(fireStrike);

        AttackBase mindsword = new AttackBase();
        mindsword.attkElement = AttackBase.attackElement.light;
        mindsword.attkType = AttackBase.attackType.magic;
        mindsword.name = "Mind Sword";
        mindsword.power = 55;
        mindsword.spCost = 6;
        moves.Add(mindsword);

        AttackBase heal = new AttackBase();
        heal.name = "Heal";
        heal.spCost = 15;
        heal.power = 250;
        heal.attkType = AttackBase.attackType.heal;
        heal.attkRng = AttackBase.attackRange.single;
        moves.Add(heal);

        AttackBase doublestrike = new AttackBase();
        doublestrike.attkElement = AttackBase.attackElement.normal;
        doublestrike.attkRng = AttackBase.attackRange.single;
        doublestrike.attkType = AttackBase.attackType.attack;
        doublestrike.name = "Double Strike";
        doublestrike.power = 11;
        doublestrike.spCost = 2;
        moves.Add(doublestrike);

    }

    public void RedFrenzyPow() {
        selectedAttack.power = (health / 8);
    }


    public override void moveSelector() {
        switch (lrdredAnimation) {

            case animationLordRed.slam:
                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationLordRed.trioattk:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationLordRed.red_frenzy:
                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationLordRed.holystrike:
                charAnimatior.SetInteger("Attack", 3);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationLordRed.flamestrike:
                charAnimatior.SetInteger("Attack", 4);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationLordRed.mindsword:
                charAnimatior.SetInteger("Attack", 5);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationLordRed.heal:
                charAnimatior.SetInteger("Attack", 6);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationLordRed.incinerate:
                charAnimatior.SetInteger("Attack", 7);
                charAnimatior.SetBool("IsAttacking", true);
                break;
            case animationLordRed.double_strike:
                charAnimatior.SetInteger("Attack", 8);
                charAnimatior.SetBool("IsAttacking", true);
                break;
        }
    }

    void createLordRed()
    {
        GameObject mndswordfx = Resources.Load("prefabs/projectiles/RedClone") as GameObject;
        GameObject mind = Instantiate(mndswordfx, transform.position, Quaternion.identity);
        ProjectileClass proj = mind.GetComponent<ProjectileClass>();

        BattleCharacter bc = battlehandler.BSM.players[Random.Range(0, battlehandler.BSM.players.Count)];

        proj.StartCoroutine(proj.meteorStrike(bc.gameObject.transform.position));
    }

    void fireness()
    {
        selectedAttack.attkElement = AttackBase.attackElement.fire;
        dealDamage();
        GameObject fx = Resources.Load("prefabs/projectiles/fireTrio") as GameObject;
        Instantiate(fx, targetChar.transform.position, Quaternion.identity);
    }

    void whiteness()
    {
        selectedAttack.attkElement = AttackBase.attackElement.light;
        GameObject fx = Resources.Load("prefabs/projectiles/whiteTrio") as GameObject;
        Instantiate(fx, targetChar.transform.position, Quaternion.identity);
    }

    void darkness()
    {
        selectedAttack.attkElement = AttackBase.attackElement.shadow;
        dealDamage();
        GameObject fx = Resources.Load("prefabs/projectiles/blackTrio") as GameObject;
        Instantiate(fx, targetChar.transform.position, Quaternion.identity);
    }

    new void Update()
    {
        base.Update();
    }

    public override void attackSelector()
    {
        if (selectedAttack.name == "Sword Slash")
        {
            lrdredAnimation = animationLordRed.slam;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Trio Attack")
        {
            lrdredAnimation = animationLordRed.trioattk;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Red Frenzy")
        {
            lrdredAnimation = animationLordRed.red_frenzy;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Fire Strike")
        {
            lrdredAnimation = animationLordRed.flamestrike;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Incinerate")
        {
            lrdredAnimation = animationLordRed.incinerate;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Heal")
        {
            lrdredAnimation = animationLordRed.heal;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Holy Strike")
        {
            lrdredAnimation = animationLordRed.holystrike;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Mind Sword")
        {
            lrdredAnimation = animationLordRed.mindsword;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Double Strike")
        {
            lrdredAnimation = animationLordRed.double_strike;
            animations = animationstate.attack;
        }
        moveSelector();
    }

}

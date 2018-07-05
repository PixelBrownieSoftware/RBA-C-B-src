using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreendoriClass : PlayerCharacter {

    public enum animationGreendori { magicball, transfer, energyDrain, burn, frost, chill, blackhole, incinerate, blizzard, thunder, jump, osmosis, energy_duct, energize};
    public animationGreendori grenAnimation;
    public float speed;

    private void Awake()
    {
        name = "Greendori";
    }

    new void Start () {
        
        attackIncrementer = 1;
        defenceIncrementer = 2;
        healthIncrementer = 3;
        intelligenceIncrementer = 3;

        weakness = AttackBase.attackElement.shadow;
        resistant = AttackBase.attackElement.electric;

        baseHealth = 65;
        baseAttack = 2;
        baseDefence = 4;
        baseIntelligence = 5;
        baseSpecialPoints = 13;

        baseExp = 12;
        attackIncPercentage = 0.17f;
        defenceIncPercentage = 0.32f;
        intelligenceIncPercentage = 0.45f;
        specialIncPercentage = 0.47f;

        base.Start();
    }

    public override void DefaultMoves()
    {
        AttackBase magicBall = new AttackBase();
        magicBall.name = "Magic ball";
        magicBall.attkElement = AttackBase.attackElement.light;
        magicBall.attkType = AttackBase.attackType.magic;
        magicBall.attkRng = AttackBase.attackRange.single;
        magicBall.power = 10;
        magicBall.spCost = 0;
        moves.Add(magicBall);

        AttackBase transfer = new AttackBase();
        transfer.name = "Transfer";
        transfer.attkType = AttackBase.attackType.heal;
        transfer.attkElement = AttackBase.attackElement.normal;
        magicBall.attkRng = AttackBase.attackRange.single;
        transfer.power = 10;
        transfer.spCost = 10;
        transfer.healSP = true;
        moves.Add(transfer);

        AttackBase mpsuck = new AttackBase();
        mpsuck.name = "Energy Drain";
        mpsuck.power = 5;
        mpsuck.spCost = 0;
        mpsuck.attkElement = AttackBase.attackElement.normal;
        mpsuck.attkRng = AttackBase.attackRange.single;
        mpsuck.attkType = AttackBase.attackType.other;
        mpsuck.healSP = true;
        moves.Add(mpsuck);



        /*
        
        AttackBase educt = new AttackBase();
        educt.attkElement = AttackBase.attackElement.normal;
        educt.attkRng = AttackBase.attackRange.all;
        educt.attkType = AttackBase.attackType.other;
        educt.healSP = true;
        educt.name = "Energy Duct";
        educt.spCost = 0;
        educt.power = 5;
        moves.Add(educt);

        AttackBase energize = new AttackBase();
        energize.attkElement = AttackBase.attackElement.normal;
        energize.attkRng = AttackBase.attackRange.all;
        energize.attkType = AttackBase.attackType.heal;
        energize.healSP = true;
        energize.name = "Energize";
        energize.power = 10;
        energize.spCost = 10;
        moves.Add(energize);


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

        AttackBase osmosis = new AttackBase();
        osmosis.attkType = AttackBase.attackType.magic;
        osmosis.attkElement = AttackBase.attackElement.ice;
        osmosis.attkRng = AttackBase.attackRange.single;
        osmosis.power = 2;
        osmosis.power = 5;
        osmosis.name = "Osmosis";
        moves.Add(osmosis);

        AttackBase blackhole = new AttackBase();
        blackhole.name = "Black Hole";
        blackhole.spCost = 7;
        blackhole.power = 45;
        blackhole.attkElement = AttackBase.attackElement.shadow;
        blackhole.attkRng = AttackBase.attackRange.all;
        blackhole.attkType = AttackBase.attackType.magic;
        moves.Add(blackhole); 

        AttackBase incenerate = new AttackBase();
        incenerate.attkElement = AttackBase.attackElement.fire;
        incenerate.attkRng = AttackBase.attackRange.all;
        incenerate.attkType = AttackBase.attackType.magic;
        incenerate.name = "Incenerate";
        incenerate.power = 45;
        incenerate.spCost = 12;
        moves.Add(incenerate);

        AttackBase jump = new AttackBase();
        jump.attkType = AttackBase.attackType.attack;
        jump.attkRng = AttackBase.attackRange.single;
        jump.attkElement = AttackBase.attackElement.normal;
        jump.name = "Jump";
        jump.power = 1;
        jump.spCost = 0;
        moves.Add(jump);


        */
    }

    public override void moveSelector()
    {
        switch (grenAnimation)
        {
            case animationGreendori.magicball:
                charAnimatior.SetInteger("Attack", 0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.transfer:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.energyDrain:
                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.burn:
                charAnimatior.SetInteger("Attack", 3);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.frost:
                charAnimatior.SetInteger("Attack", 4);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.jump:
                charAnimatior.SetInteger("Attack", 5);
                charAnimatior.SetBool("IsAttacking", true);
                selectedAttack.power = Random.Range(1,55);
                break;

            case animationGreendori.chill:
                charAnimatior.SetInteger("Attack", 6);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.blackhole:
                charAnimatior.SetInteger("Attack", 7);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.thunder:
                charAnimatior.SetInteger("Attack", 8);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.blizzard:
                charAnimatior.SetInteger("Attack", 9);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.incinerate:
                charAnimatior.SetInteger("Attack", 10);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.osmosis:
                charAnimatior.SetInteger("Attack", 11);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.energy_duct:
                charAnimatior.SetInteger("Attack", 12);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationGreendori.energize:
                charAnimatior.SetInteger("Attack", 13);
                charAnimatior.SetBool("IsAttacking", true);
                break;
        }
    }

    public void spSuck()
    {
        if (selectedAttack.attkRng == AttackBase.attackRange.single)
        {
            if (targetChar.specialPoints >= selectedAttack.power)
            {
                targetChar.specialPoints -= selectedAttack.power;
                specialPoints += selectedAttack.power;
                GameObject hitfx = Resources.Load("prefabs/projectiles/sphealfx") as GameObject;
                Instantiate(hitfx, this.transform.position, Quaternion.identity);
                battlehandler.BSM.battlelog(this.name + " recovered " + selectedAttack.power + " SP.");
            }
            else
            {
                battlehandler.BSM.battlelog("Failed to suck SP");
            }
        }
        else if (selectedAttack.attkRng == AttackBase.attackRange.all)
        {
            int sp_suck = 0;
            foreach (EnemyCharacter enemychar in battlehandler.BSM.enemies) {
                if (enemychar.health <= 0) { continue; }

                if (enemychar.specialPoints >= selectedAttack.power)
                {
                    enemychar.specialPoints -= selectedAttack.power;
                    sp_suck += selectedAttack.power;
                }
            }
            this.specialPoints += sp_suck;
            battlehandler.BSM.battlelog(this.name + " recovered " + sp_suck + " SP.");
            GameObject hitfx = Resources.Load("prefabs/projectiles/sphealfx") as GameObject;
            Instantiate(hitfx, this.transform.position, Quaternion.identity);
        }
    }

    void magicball()
    {
        GameObject mndswordfx = Resources.Load("prefabs/projectiles/frost") as GameObject;
        Instantiate(mndswordfx, targetChar.transform.position, Quaternion.identity);
    }

    new void Update () {
        base.Update();
        for (int i = 0; i < moves.Count; i++) {
            if (moves[i].name == "Energize") {
                moves[i].spCost = 10 * (battlehandler.BSM.players.Count - 1);
            }
        }

    }

    public IEnumerator jumpstart()
    {
        while (transform.position.y <= 230)
        {
            transform.position += new Vector3(0, 5 * speed * 5) * Time.deltaTime;
            yield return null;
        }
        if (transform.position.y >= 230)
        {
            charAnimatior.SetInteger("Jumpphase", 1);
        }
    }

    public void jumpOnInit()
    {

        transform.position = new Vector2(targetChar.transform.position.x, 100);
    }

    void CreateBlackHole() {
        CreateFxAtPos("Black Hole", new Vector2(23,64));
    }

    public IEnumerator Jump()
    {
        while (transform.position.y > targetChar.transform.position.y)
        {

            transform.position += new Vector3(0, 7 * -speed * 5) * Time.deltaTime;
            yield return null;
        }

        charAnimatior.SetInteger("Jumpphase", 0);
    }

    public override void Attack(BattleCharacter target, AttackBase move)
    {
        selectedAttack = move;
        targetChar = target;
        battlehandler.BSM.battlelog("- " + this.name + " preformed " + selectedAttack.name);

        if (selectedAttack.name == "Magic ball")
        {
            grenAnimation = animationGreendori.magicball;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Transfer")
        {
            grenAnimation = animationGreendori.transfer;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Energy Drain")
        {
            grenAnimation = animationGreendori.energyDrain;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Lightning")
        {
            grenAnimation = animationGreendori.thunder;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Frost")
        {
            grenAnimation = animationGreendori.frost;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Chill")
        {
            grenAnimation = animationGreendori.chill;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Burn")
        {
            grenAnimation = animationGreendori.burn;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Jump")
        {
            grenAnimation = animationGreendori.jump;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Black Hole")
        {
            grenAnimation = animationGreendori.blackhole;
            animations = animationstate.attack;
        }
        if (selectedAttack.name == "Blizzard")
        {
            grenAnimation = animationGreendori.blizzard;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Incenerate")
        {
            grenAnimation = animationGreendori.incinerate;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Osmosis")
        {
            grenAnimation = animationGreendori.osmosis;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Energize")
        {
            grenAnimation = animationGreendori.energize;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Energy Duct")
        {
            grenAnimation = animationGreendori.energy_duct;
            animations = animationstate.attack;
        }

        moveSelector();
    }
}

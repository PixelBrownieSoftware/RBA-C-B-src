using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinklerClass : EnemyCharacter
{

    public enum animationPinkler { heal, icestrike, jump, slam, blizzard };
    public animationPinkler pinkAnimation;

    public float speed;

    private void Awake()
    {
        name = "Pinkler";
    }

    new void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 180, 0);
        weakness = AttackBase.attackElement.fire;
        resistant = AttackBase.attackElement.ice;

        moneyAmount = 12;
        baseExpToGive = 27;

        expIncrementer = 25;
        attackIncrementer = 2;
        defenceIncrementer = 5;
        healthIncrementer = 8;
        intelligenceIncrementer = 5;
        specialIncrementer = 4;

        baseHealth = 65;
        baseAttack = 2;
        baseDefence = 4;
        baseIntelligence = 6;
        baseSpecialPoints = 25;

        base.Start();

        speed = 15;

        AttackBase slam = new AttackBase();
        slam.attkElement = AttackBase.attackElement.normal;
        slam.attkRng = AttackBase.attackRange.single;
        slam.attkType = AttackBase.attackType.attack;
        slam.name = "Slam";
        slam.power = 20;
        moves.Add(slam);

        AttackBase jump = new AttackBase();
        jump.attkType = AttackBase.attackType.attack;
        jump.attkRng = AttackBase.attackRange.single;
        jump.attkElement = AttackBase.attackElement.normal;
        jump.name = "Jump";
        jump.power = 1;
        jump.spCost = 2;
        moves.Add(jump);


        AttackBase heal = new AttackBase();
        heal.name = "Heal All";
        heal.spCost = level <= 30 ? 7 : 15;
        heal.power = level <= 30? 65: 160;
        heal.attkType = AttackBase.attackType.heal;
        heal.attkRng = AttackBase.attackRange.all;
        moves.Add(heal);

        if (level > 29)
        {
            AttackBase froststrike = new AttackBase();
            froststrike.attkElement = AttackBase.attackElement.ice;
            froststrike.attkType = AttackBase.attackType.skill;
            froststrike.name = "Frost Strike";
            froststrike.power = 15;
            froststrike.spCost = 5;
            moves.Add(froststrike);

            AttackBase blizzard = new AttackBase();
            blizzard.power = 35;
            blizzard.spCost = 13;
            blizzard.attkElement = AttackBase.attackElement.ice;
            blizzard.attkRng = AttackBase.attackRange.single;
            blizzard.attkType = AttackBase.attackType.magic;
            blizzard.name = "Blizzard";
            moves.Add(blizzard);
        }



    }
    public override void moveSelector()
    {
        switch (pinkAnimation) {
            
            case animationPinkler.slam:
                charAnimatior.SetInteger("Attack",0);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationPinkler.jump:
                charAnimatior.SetInteger("Attack", 1);
                charAnimatior.SetBool("IsAttacking", true);
                moves[0].power = Random.Range(3,75);
                break;

            case animationPinkler.icestrike:
                charAnimatior.SetInteger("Attack", 2);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationPinkler.heal:
                charAnimatior.SetInteger("Attack", 3);
                charAnimatior.SetBool("IsAttacking", true);
                break;

            case animationPinkler.blizzard:
                charAnimatior.SetInteger("Attack", 4);
                charAnimatior.SetBool("IsAttacking", true);
                break;
        }
    }

    

    public IEnumerator jumpstart()
    {
        selectedAttack.power = Random.Range(1,100);
        while (transform.position.y <= 230)
        {
            transform.position += new Vector3(0, 5 * speed * 5) * Time.deltaTime;
            yield return null;
        }
        if (transform.position.y >= 230)
        {
            charAnimatior.SetInteger("PinklerJumppahse", 1);
        }
    }

    public void jumpOnInit() {

        transform.position = new Vector2 (targetChar.transform.position.x, 100);
    }

    public IEnumerator Jump()
    {
        while (transform.position.y > targetChar.transform.position.y)
        {

            transform.position += new Vector3(0, 7 * -speed * 5) * Time.deltaTime;
            yield return null;
        }

        charAnimatior.SetInteger("PinklerJumppahse", 0);
    }

    new void Update()
    {
        base.Update();
    }

    public override void attackSelector()
    {
        if (selectedAttack.name == "Jump")
        {
            pinkAnimation = animationPinkler.jump;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Frost Strike")
        {
            pinkAnimation = animationPinkler.icestrike;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Heal All")
        {
            pinkAnimation = animationPinkler.heal;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Slam")
        {
            pinkAnimation = animationPinkler.slam;
            animations = animationstate.attack;
        }

        if (selectedAttack.name == "Blizzard")
        {
            pinkAnimation = animationPinkler.blizzard;
            animations = animationstate.attack;
        }

        moveSelector();
    }
}

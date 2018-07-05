using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BattleCharacter : MonoBehaviour {

    public bool deadAnimComplete { get; set; }

    public int level;
    public int maxHealth { get; set; }
    public int health { get; set; }

    public int baseAttack { get; set; }
    public int baseDefence { get; set; }
    public int baseIntelligence { get; set; }
    public int baseHealth { get; set; }
    public int baseSpecialPoints { get; set; }

    public int attack { get; set; }
    public int defence { get; set; }
    public int intelligence { get; set; }
    public int specialPoints { get; set; }
    public int maxSpecialPoints { get; set; }

    protected int attackIncrementer { get; set; }
    protected int intelligenceIncrementer { get; set; }
    protected int defenceIncrementer { get; set; }
    protected int healthIncrementer { get; set; }
    protected int specialIncrementer { get; set; }

    public AttackBase.attackElement weakness;
    public AttackBase.attackElement resistant;

    public List<AttackBase> moves = new List<AttackBase>();
    public enum animationstate { idle, attack, finish, dead };
    public animationstate animations;
    public BattleCharacter targetChar;
    public AttackBase selectedAttack;

    public Animator charAnimatior;
    public Vector2 initPos;

	public void Start ()
    {
        deadAnimComplete = false;
        charAnimatior = GetComponent<Animator>();
    }

    //character getting hurt ususaly used for status effects
    public void TakeDamage(int amount) {
        health -= amount;
    }

    void PlaySoundEffect(AudioClip sound) {
        SoundManager.sfx.PlaySound(sound);
    }

    //character hurting another
    public int DealDamage(int amount)
    {
        int damage = 0;

        if (selectedAttack.attkRng != AttackBase.attackRange.all)
        {
            damage = calculateNumbers(targetChar, amount);

            targetChar.health -= damage;

            if (targetChar.health <= 0) {
                targetChar.charAnimatior.SetBool("IsDefeated", true);
                battlehandler.BSM.battlelog(targetChar.name + " was defeated!");
            }
        } else {
            if (this.tag == "Player") {
                foreach (EnemyCharacter enemy in battlehandler.BSM.enemies)
                {
                    if (enemy.health <= 0) { continue; }
                    damage = calculateNumbers(enemy, amount);
                    enemy.health -= damage;
                    battlehandler.BSM.battlelog(enemy.name + " took " + damage + " damage.");
                    if (enemy.health <= 0)
                    {
                        enemy.charAnimatior.SetBool("IsDefeated", true);
                        battlehandler.BSM.battlelog(enemy.name + " was defeated!");
                    }
                }
            }
            else if (this.tag == "Enemy")
            {
                foreach (PlayerCharacter enemy in battlehandler.BSM.players)
                {
                    if (enemy.health == 0) { continue; }
                    damage = calculateNumbers(enemy, amount);
                    enemy.health -= damage;
                    battlehandler.BSM.battlelog(enemy.name + " took " + damage + " damage.");
                    if (enemy.health <= 0)
                    {
                        enemy.charAnimatior.SetBool("IsDefeated", true);
                        battlehandler.BSM.battlelog(enemy.name + " was defeated!");
                    }
                }
            }
        }
        return damage;
    }

    void ChangePos()
    {
        transform.position = new Vector3(initPos.x + Random.Range(-20,20), initPos.y + Random.Range(-20,20), 0);
    }

    void changePowerToHealth()
    {
        selectedAttack.power = Mathf.FloorToInt(health / 2.7f);
    }

    IEnumerator Hurt() {
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);
        ChangePos();
        yield return new WaitForSeconds(0.01f);

        BacktoNormPos();

    }

    void BacktoNormPos()
    {
        transform.position = initPos;
        charAnimatior.SetBool("IsHurt", false);
    }

    public int calculateNumbers(BattleCharacter targetChar ,int amount) {
        int damage = 0;
        if (selectedAttack.attkType != AttackBase.attackType.heal)
        {
            GameObject hitfx = Resources.Load("prefabs/projectiles/hitFX") as GameObject;
            Instantiate(hitfx, targetChar.transform.position, Quaternion.identity);
            PlaySoundEffect(Resources.Load("sfx/hit") as AudioClip);
        }

        if (selectedAttack.attkType == AttackBase.attackType.attack) { damage = (amount * attack) / (targetChar.defence); }
        else if (selectedAttack.attkType == AttackBase.attackType.skill) { damage = (amount * Mathf.RoundToInt((attack / 1.2f) + intelligence / 2)) / (targetChar.defence); }
        else if (selectedAttack.attkType == AttackBase.attackType.magic) { damage = (amount * intelligence) / (targetChar.defence/ 2 + targetChar.intelligence/2); }
        
        if (targetChar.weakness == selectedAttack.attkElement && targetChar.weakness != AttackBase.attackElement.normal) { damage = Mathf.RoundToInt(damage * 1.9f);  }
        else if (targetChar.resistant == selectedAttack.attkElement && targetChar.resistant != AttackBase.attackElement.normal) { damage = Mathf.RoundToInt(damage / 1.9f); }

        targetChar.charAnimatior.SetBool("IsHurt", true);
        return damage;
    }

    public void Heal(int amount)
    {
        if (selectedAttack.attkRng != AttackBase.attackRange.all) {
            if (!selectedAttack.healSP) {
                targetChar.health += amount;

                GameObject hitfx = Resources.Load("prefabs/projectiles/healfx") as GameObject;
                Instantiate(hitfx, targetChar.transform.position, Quaternion.identity);
                PlaySoundEffect(Resources.Load("sfx/heal2") as AudioClip);

                battlehandler.BSM.battlelog(targetChar.name + " healed " + amount + " HP.");
            } else {
                GameObject hitfx = Resources.Load("prefabs/projectiles/sphealfx") as GameObject;
                Instantiate(hitfx, targetChar.transform.position, Quaternion.identity);
                targetChar.specialPoints += amount;
                battlehandler.BSM.battlelog(targetChar.name + " healed " + amount + " SP.");
            }
        } else {
            if (this.tag == "Enemy") {
                foreach (EnemyCharacter enemy in battlehandler.BSM.enemies) {
                    if (enemy.health == 0) { continue; }

                    if (!selectedAttack.healSP) {

                        GameObject hitfx = Resources.Load("prefabs/projectiles/healfx") as GameObject;
                        Instantiate(hitfx, enemy.transform.position, Quaternion.identity);
                        PlaySoundEffect(Resources.Load("sfx/heal2") as AudioClip);

                        enemy.health += amount;
                        battlehandler.BSM.battlelog(enemy.name + " healed " + amount + " HP.");
                    } else {
                        GameObject hitfx = Resources.Load("prefabs/projectiles/sphealfx") as GameObject;
                        Instantiate(hitfx, targetChar.transform.position, Quaternion.identity);
                        enemy.specialPoints += amount;
                        battlehandler.BSM.battlelog(enemy.name + " healed " + amount + " SP.");
                    }
                }
            } else if (this.tag == "Player") {
                foreach (PlayerCharacter enemy in battlehandler.BSM.players) {

                    if (!selectedAttack.healSP) {

                        GameObject hitfx = Resources.Load("prefabs/projectiles/healfx") as GameObject;
                        Instantiate(hitfx, enemy.transform.position, Quaternion.identity);
                        PlaySoundEffect(Resources.Load("sfx/heal2") as AudioClip);

                        enemy.health += amount;
                        battlehandler.BSM.battlelog(enemy.name + " healed " + amount + " HP.");
                    } else {
                        if (enemy != this) {
                        GameObject hitfx = Resources.Load("prefabs/projectiles/sphealfx") as GameObject;
                            Instantiate(hitfx, enemy.transform.position, Quaternion.identity);
                            enemy.specialPoints += amount;
                            battlehandler.BSM.battlelog(enemy.name + " healed " + amount + " SP.");
                        }
                    }
                }
            }
        }
    }

    public void SuckHP() {
        int amount = calculateNumbers(targetChar,selectedAttack.power);
        doneAction();
        targetChar = this;
        Heal(amount);
    }

    public void HealSP(int amount)
    {
        GameObject hitfx = Resources.Load("prefabs/projectiles/sphealfx") as GameObject;
        Instantiate(hitfx, this.transform.position, Quaternion.identity);
        targetChar.specialPoints += amount; 
    }

    void NoDyingAnim() {
        if (this.tag == "Enemy")
        {
            transform.Find("Shadow").gameObject.SetActive(false);
            PlaySoundEffect(Resources.Load("sfx/DeadEnemy") as AudioClip);
        }
        else if (this.tag == "Player")
        {
            PlaySoundEffect(Resources.Load("sfx/DeadPlayer") as AudioClip);
        }
        deadAnimComplete = true;
    }

    public void Update () {
        if (health > maxHealth) {
            health = maxHealth;
        }
        if (specialPoints > maxSpecialPoints) {
            specialPoints = maxSpecialPoints;
        }

        if (health <= 0) {
            health = 0;
        } else {
            
            charAnimatior.SetBool("IsDefeated", false);
            deadAnimComplete = false;
        }

        if (specialPoints <= 0) {
            specialPoints = 0;
        }
    }

    void CreateStandingAllFX(string name)
    {
        if (this.tag == "Player") {
            foreach (BattleCharacter chara in battlehandler.BSM.enemies)
            {
                GameObject fx = Resources.Load("prefabs/projectiles/" + name) as GameObject;
                Instantiate(fx, chara.transform.position, Quaternion.identity);
            }
        } else if (this.tag == "Enemy"){
            foreach (BattleCharacter chara in battlehandler.BSM.players) {
                GameObject fx = Resources.Load("prefabs/projectiles/" + name) as GameObject;
                Instantiate(fx, chara.transform.position, Quaternion.identity);
            }
        }
    }

    //obeselete stuff
    void createFireFX()
    {
        GameObject firefx = Resources.Load("prefabs/projectiles/fire") as GameObject;
        Instantiate(firefx, targetChar.transform.position, Quaternion.identity);
    }

    void createElectricFX()
    {
        GameObject electricfx = Resources.Load("prefabs/projectiles/lighting") as GameObject;
        Instantiate(electricfx, targetChar.transform.position, Quaternion.identity);
    }

    void smiteStrike()
    {
        GameObject smitefx = Resources.Load("prefabs/projectiles/smite") as GameObject;
        Instantiate(smitefx, targetChar.transform.position, Quaternion.identity);
    }

    void frostStrike()
    {
        GameObject frostfx = Resources.Load("prefabs/projectiles/frost") as GameObject;
        Instantiate(frostfx, targetChar.transform.position, Quaternion.identity);
    }

    void CreateStandingFX(string name)
    {
        GameObject fx = Resources.Load("prefabs/projectiles/" + name) as GameObject;
        Instantiate(fx, targetChar.transform.position, Quaternion.identity);
    }

    public void CreateFxAtPos(string name, Vector2 pos) {
        GameObject fx = Resources.Load("prefabs/projectiles/" + name) as GameObject;
        Instantiate(fx, pos, Quaternion.identity);
    }

    void mindSword()
    {
        GameObject mndswordfx = Resources.Load("prefabs/projectiles/MindSword") as GameObject;
        GameObject mind = Instantiate(mndswordfx, transform.position, Quaternion.identity);
        ProjectileClass proj = mind.GetComponent<ProjectileClass>();
        proj.StartCoroutine(proj.meteorStrike(targetChar.gameObject.transform.position));
    }

    void CreateShootFx(string name)
    {
        GameObject fx = Resources.Load("prefabs/projectiles/" + name) as GameObject;
        GameObject f = Instantiate(fx, transform.position, Quaternion.identity);
        ProjectileClass proj = f.GetComponent<ProjectileClass>();
        targetChar = battlehandler.BSM.targetCharacter;
        if (targetChar != null) {
            proj.StartCoroutine(proj.meteorStrike(new Vector2(targetChar.gameObject.transform.position.x, targetChar.gameObject.transform.position.y)));
        } else {
            proj.StartCoroutine(proj.meteorStrike(new Vector2(200,10)));
        }
    }

    public void dealDamage()
    {
        if (selectedAttack.attkRng != AttackBase.attackRange.all)
        {
            battlehandler.BSM.battlelog(targetChar.name + " took " + DealDamage(selectedAttack.power) + " damage.");
        } else {
            DealDamage(selectedAttack.power);
            //battlehandler.BSM.battlelog(, this.name, this.name, battlehandler.battleLogType.damage, selectedAttack.name, false);
        }
    }

    public abstract void moveSelector();

    public void doneAction()
    {
        if (selectedAttack.attkType != AttackBase.attackType.other) {
            if (selectedAttack.attkType != AttackBase.attackType.heal) {
                if (selectedAttack.attkRng != AttackBase.attackRange.all) {
                    int dmg = DealDamage(selectedAttack.power);
                    battlehandler.BSM.battlelog(targetChar.name + " took " + dmg + " damage.");
                } else {
                    int dmg = DealDamage(selectedAttack.power);
                }
            } else {
                Heal(selectedAttack.power);
            }
        }
        charAnimatior.SetBool("IsAttacking", false);
        transform.position = initPos;
        animations = animationstate.finish;
    }

    public IEnumerator MoveToPosition(float speed, Vector2 positon)
    {
        while (Mathf.Abs(positon.x - transform.position.x) > 2 && Mathf.Abs(positon.y - transform.position.y) > 2) {

            Vector3 charToPos = new Vector3(positon.x - transform.position.x, positon.y - transform.position.y).normalized;
            transform.position = charToPos * speed;
        }

        yield return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct LevelRequirements
{
    public bool isBoss;
    public int bossNum;

    public int maxLevel;
    public int enemyRngMax, enemyRngMin;
    public int enemyLevelMax, enemyLevelMin;
    public List<GameObject> enemyTypes;
}

public class EnemyGenerator : MonoBehaviour {

    public List<LevelRequirements> Phases;

    private int enemyCount;
    public int currentPhase;
    public GameObject[] enemyprefabs;

    private List<GameObject> enemyList;
    public battlehandler bh;
    public bool bossDead;
    public LevelRequirements Phase;

    void Awake () {
        bh = GameObject.Find("BattleCanvas").GetComponent<battlehandler>();
    }

    public void isBossDefeatTrue() {
        if (currentPhase < 12)
        {
            currentPhase++;
            Phase = Phases[currentPhase];
        }
    }

    public void generateBoss() {

        GameObject enemyCopy = Instantiate(Phase.enemyTypes[0], new Vector3(90, -40, 3), Quaternion.Euler(0, 0, 0)) as GameObject;
        BattleCharacter enemyCopyData = enemyCopy.GetComponent<BattleCharacter>();
        enemyCopyData.level = Phases[currentPhase].enemyLevelMax;
        bh.enemies.Add(enemyCopyData);
        enemyCopyData.initPos = enemyCopy.transform.position;
        enemyCopy.transform.parent = GameObject.Find("Enemies").transform;
        bossDead = false;
    }

	public void randomEnemyGen () {

        Phase = Phases[currentPhase];

        if (bh.players[0].level >= Phase.maxLevel && !Phase.isBoss) {
            bossDead = false;
            currentPhase++;
        }

        enemyCount = Random.Range(Phase.enemyRngMin, Phase.enemyRngMax);

        for (int i = 0; i < enemyCount; i++)
        {
            int enemyType = Random.Range(0, Phase.enemyTypes.Count);

            if (Phase.isBoss)
            {
                enemyType = Random.Range(1, Phase.enemyTypes.Count);
            }

            GameObject enemyCopy = Instantiate(Phase.enemyTypes[enemyType], new Vector3(90, 30 + -30 * i, 3), Quaternion.Euler(0, 0, 0)) as GameObject;

            BattleCharacter enemyCopyData = enemyCopy.GetComponent<BattleCharacter>();
            enemyCopyData.initPos = enemyCopy.transform.position;
            enemyCopy.name = enemyCopyData.name;
            enemyCopyData.level = Random.Range(Phases[currentPhase].enemyLevelMin, Phases[currentPhase].enemyLevelMax);
            bh.enemies.Add(enemyCopyData);
            enemyCopy.transform.parent = GameObject.Find("Enemies").transform;
        }

        /*
        if (Phase.isBoss && bossDead)
        {
            currentPhase++;
        }*/

    }
}

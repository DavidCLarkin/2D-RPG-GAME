using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public List<GameObject> bosses;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "SpawnPoints")
            {
                foreach (Transform spawnPoint in child)
                    spawnPoints.Add(spawnPoint);
            }
        }

        SelectBoss();
    }

    void SelectBoss()
    {
        int bossToSpawn = Random.Range(0, bosses.Count); // random between 1 and spawnpoints size
        Debug.Log(bossToSpawn);
        int chosenPoint = Random.Range(0, spawnPoints.Count); // save this to remove it from the list later
        GameObject boss = Instantiate(bosses[bossToSpawn], spawnPoints[chosenPoint].transform.position, Quaternion.identity);
        spawnPoints.Remove(spawnPoints[chosenPoint]);

        GameManagerSingleton.instance.GetComponent<TextDisplay>().boss = boss;
        Debug.Log(boss.GetComponent<Enemy>().enemyName);
        GameManagerSingleton.instance.GetComponent<TextDisplay>().bossName.text = boss.GetComponent<Enemy>().enemyName.ToString();
        GameManagerSingleton.instance.GetComponent<TextDisplay>().bossHealth = boss.GetComponent<HealthComponent>();
    }
}

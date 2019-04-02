using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllyWisp : MonoBehaviour
{
    public GameObject target;

    private void Start()
    {
        target = FindClosestTarget(GetCloseEnemies(GameManagerSingleton.instance.enemyTag, GameManagerSingleton.instance.bossTag));
        //Debug.Log("Closest: "+ target.name);
    }

    public void Update()
    {
        if (target)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 3 * 0.5f * Time.deltaTime);
        else
            Destroy(gameObject);

    }

    /*
     * Find closest enemy from a list
     */ 
    GameObject FindClosestTarget(List<GameObject> objects)
    {
        return objects.FirstOrDefault();
    }

    /*
     * Find close enemies and add to a list (just using a tag)
     */ 
    List<GameObject> GetCloseEnemies(params string[] tags)
    {
        List<GameObject> objects = new List<GameObject>();
        foreach (string tag in tags)
            objects.AddRange(GameObject.FindGameObjectsWithTag(tag));

        return objects.OrderBy(o => Vector2.Distance(transform.position, o.transform.position)).ToList();

    }


}

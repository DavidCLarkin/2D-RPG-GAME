using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AllyWisp : MonoBehaviour
{
    private GameObject target;

    private void Start()
    {
        target = FindClosestTarget(GameManagerSingleton.instance.enemyTag, GameManagerSingleton.instance.bossTag);
        Debug.Log("Closest: "+ target.name);
    }

    public void Update()
    {
        if (target)
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, 3 * 0.5f * Time.deltaTime);
        else
            Destroy(gameObject);

    }

    /*
     * Find closest game object using lambda, and return the first element in list
     */ 
    GameObject FindClosestTarget(params string[] trgt)
    {
        Vector3 position = transform.position;
        List<GameObject> objects = new List<GameObject>();

        foreach(string tag in trgt)
        {
            objects.AddRange(GameObject.FindGameObjectsWithTag(tag).ToList());
            objects.OrderBy(o => (o.transform.position - position).sqrMagnitude);
        }


        return objects.First();
    }


}

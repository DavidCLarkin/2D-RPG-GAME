using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    public List<Interactable> items; // Set a list of items that enemy can drop, and choose randomly
    Interactable item;

	// Use this for initialization
	void Start ()
    {
        GetComponent<HealthComponent>().OnDie += DropItem;
    }

    void DropItem()
    {
        item = items[Random.Range(0, items.Count)]; // make it so that a certain weapon is rare, maybe 1/10 etc. if number is 10, drop weapon, else 1-9, drop something else
        Instantiate(item, transform.position, Quaternion.identity);
    }
}

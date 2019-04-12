using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleItem : MonoBehaviour
{

    private Animator anim;
    public List<Item> items = new List<Item>();
    public List<Item> rareItems = new List<Item>();

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == GameManagerSingleton.instance.playerWeaponColliderTag)
        {
            anim.SetBool("Break", true);
        }
    }

    // Small method for a small chance of a potion from destroying a pot
    public void DropRandomItem()
    {
        float randomNum = Random.value;
        if(randomNum <= 0.03f)
            Instantiate(rareItems[Random.Range(0, rareItems.Count - 1)], transform.position, Quaternion.identity);
        else if (randomNum <= 0.1 && randomNum > 0.03f)
            Instantiate(items[Random.Range(0, items.Count - 1)], transform.position, Quaternion.identity);
    }
}

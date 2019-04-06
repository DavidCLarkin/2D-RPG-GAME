using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTable : MonoBehaviour
{
    public List<GameObject> guaranteedDrops;

    public List<GameObject> commonItems; // Set a list of items that enemy can drop, and choose randomly
    public List<GameObject> uncommonItems;
    public List<GameObject> rareItems;

    Interactable item;

	// Use this for initialization
	void Start ()
    {
        GetComponent<HealthComponent>().OnDie += DropItem;
    }

    void DropItem()
    {
        int randomNumber = Random.Range(0, 100);
        if (rareItems.Count > 0 && randomNumber <= 20)
        {
            item = rareItems[Random.Range(0, rareItems.Count)].GetComponent<Item>();
        }
        else if (uncommonItems.Count > 0 && randomNumber > 20 && randomNumber <= 50)
        {
            item = uncommonItems[Random.Range(0, uncommonItems.Count)].GetComponent<Item>();
        }
        else if (commonItems.Count > 0 && randomNumber > 50)
        {
            item = commonItems[Random.Range(0, commonItems.Count)].GetComponent<Item>();
        }

        if(item)
            Instantiate(item, transform.position, Quaternion.identity);

        if(guaranteedDrops.Count > 0)
        {
            for(int i = 0; i < guaranteedDrops.Count; i++)
                Instantiate(guaranteedDrops[i], (Vector2)transform.position + Vector2.right * (i+1), Quaternion.identity);
        }
    }
}

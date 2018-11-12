using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Interactable
{
    protected PolygonCollider2D[] weaponColliders;
    public int damage;

	// Use this for initialization
	void Start ()
    {
        player = GameManagerSingleton.instance.player.transform;
        weaponColliders = GetComponentsInChildren<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	public override void Update ()
    {
        //Debug.Log(weaponColliders);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals(player.tag))
        {
            Debug.Log("Collided");
            player.GetComponent<PlayerController>().health -= damage; //probably want to change this to invoke method
            
        }
    }
}

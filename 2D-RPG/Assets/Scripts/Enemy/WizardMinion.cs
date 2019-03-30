using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardMinion : Enemy
{
    public float DASH_TIME_COOLDOWN;
    public float DASH_TIME_TIMER;

    public int numbDashes;

    // Use this for initialization
    protected override void Start ()
    {
        StartCoroutine(DashToPlayer(DASH_TIME_COOLDOWN));
    }
	
	// Update is called once per frame
	public override void Update ()
    {
        if (DASH_TIME_TIMER >= 0)
            DASH_TIME_TIMER -= Time.deltaTime;
	}

    IEnumerator DashToPlayer(float delayBetweenDashes)
    {
        while (numbDashes > 0)
        {
            Vector2 dir = GameManagerSingleton.instance.player.transform.position - gameObject.transform.position;
            gameObject.GetComponent<Rigidbody2D>().velocity = dir.normalized * speed;
            numbDashes--;
            yield return new WaitForSeconds(delayBetweenDashes);

            //GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if(numbDashes <= 0)
                Destroy(gameObject);

        }
    }
}

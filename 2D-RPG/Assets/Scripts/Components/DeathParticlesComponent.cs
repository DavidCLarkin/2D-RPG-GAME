using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticlesComponent : MonoBehaviour
{
    public GameObject particles;

	// Use this for initialization
	void Start ()
    {
        GetComponent<HealthComponent>().OnDie += SpawnParticles;
	}

    void SpawnParticles()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
    }
}

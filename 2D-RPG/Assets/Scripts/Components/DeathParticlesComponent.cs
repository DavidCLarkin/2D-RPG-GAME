using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticlesComponent : MonoBehaviour
{
    public GameObject particles;

	void Start ()
    {
        GetComponent<HealthComponent>().OnDie += SpawnParticles;
	}

    void SpawnParticles()
    {
        Instantiate(particles, transform.position, Quaternion.identity);
    }
}

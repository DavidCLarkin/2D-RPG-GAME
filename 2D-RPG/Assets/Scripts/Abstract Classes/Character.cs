using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    public int health;
    [SerializeField]
    protected int maxHealth;
}

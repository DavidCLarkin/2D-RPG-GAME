using System;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public bool IsAttacking { get; set; }

    public event Action AnimateAttack = delegate { };

    private void Awake()
    {
        GetComponent<InputComponent>().OnAttack += Attack; // Subscribe the OnAttack to this component's attack method
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Attack()
    {
        
    }
}

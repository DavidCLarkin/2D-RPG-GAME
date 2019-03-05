using UnityEngine;
using System;

public class InputComponent : MonoBehaviour
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    private bool Attack { get; set; }
    private bool Interact { get; set; }
    public bool Dodge { get; set; }
    private Rigidbody2D rigidbody;

    public KeyCode[] keys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    public event Action OnAttack = delegate { };
    public event Action OnInteract = delegate { };
    public event Action OnDodge = delegate { };

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        Attack = Input.GetMouseButtonDown(0); // Left mouse click
        Interact = Input.GetKeyDown(KeyCode.E); // Press E to interact
        Dodge = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("B");


        if (Attack)
        {
            //Debug.Log("Attacking");
            OnAttack();
        }

        if(Dodge)
            OnDodge();

        if (Interact)
        {
            //Debug.Log("Interacting");
            OnInteract();
        }
    }
}

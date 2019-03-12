using UnityEngine;
using System;

public class InputComponent : MonoBehaviour
{
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public bool Attack { get; set; }
    public bool Interact { get; set; }
    public bool Dodge { get; set; }
    public bool Pause { get; set; }

    private Rigidbody2D rigidbody;

    public KeyCode[] keys = new KeyCode[] { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };

    public event Action OnAttack = delegate { };
    public event Action OnInteract = delegate { };
    public event Action OnDodge = delegate { };
    public event Action OnPause = delegate { };

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Vertical = Input.GetAxisRaw("Vertical");
        Attack = Input.GetMouseButtonDown(0) || Input.GetButtonDown("A"); // Left mouse click or A on controller to Attack
        Interact = Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("Y"); // Press E or Y on controller to Interact
        Dodge = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("B"); // Press Space or B on controller to Dodge
        Pause = Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start");
        


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

        if (Pause)
            OnPause();
    }
}

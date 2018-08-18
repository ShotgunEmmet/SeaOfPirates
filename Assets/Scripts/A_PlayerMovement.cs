using UnityEngine;

[RequireComponent(typeof(Animator))]
public class A_PlayerMovement : MonoBehaviour {

    private Animator animator;

    [SerializeField]
    private float speed = 2.4f;

    private PlayerMotor motor;

    private void Start()
    {
        animator = GetComponent<Animator>();
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        Vector2 move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed;

        motor.Move(move);
        if (move.magnitude > 0.1f) { 
            animator.SetFloat("FaceX", move.x);
            animator.SetFloat("FaceY", move.y);
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Idle");
        }

    }

}

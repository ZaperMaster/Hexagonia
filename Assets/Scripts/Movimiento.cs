using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MovimientoJugador : MonoBehaviourPunCallbacks
{
    public float speed = 5.0f;
    public float rotationSpeed = 10f;
    public float jumpForce = 7f;
    private Animator animator;
    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        if (photonView.IsMine)
        {
            DontDestroyOnLoad(gameObject); // 🔹 Evita que el jugador desaparezca al cambiar de escena
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveZ = Input.GetAxisRaw("Vertical");

            moveDirection = new Vector3(moveX, 0, moveZ).normalized;

            animator.SetFloat("Speed", moveDirection.magnitude);
            animator.SetFloat("Horizontal", moveX);
            animator.SetFloat("Vertical", moveZ);

            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                animator.SetTrigger("Jump");
            }
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Superficie"))
        {
            isGrounded = true;
        }
    }
}

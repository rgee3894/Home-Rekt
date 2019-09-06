using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    public float speed = 10f;
    
    private bool facingRight;

    private Vector3 velocity=Vector3.zero;

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
    
    private Rigidbody2D rb;
    [HideInInspector] public bool canMove;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        facingRight=true;
        canMove=true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canMove) return;
        horizontalMove = Input.GetAxisRaw("Horizontal")* speed;
        verticalMove = Input.GetAxisRaw("Vertical")*speed;
        
    }

    void FixedUpdate()
    {

        // Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(horizontalMove , verticalMove);
		// And then smoothing it out and applying it to the character
		rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

    }
}

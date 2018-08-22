using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    private DialogueManager manager;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRaduis;

    [SerializeField]
    private LayerMask ground;

    private bool isGrounded;

    private bool jump;

    private bool airControl;

    private bool playerInControl;

    [SerializeField]
    private float jumpForce;

    private int dialogueNum;

	// Use this for initialization
	void Start () {
        animator.SetBool("sitting", true);
        facingRight = true;
        playerInControl = false;
        dialogueNum = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<DialogueManager>();

	}

    private void Update(){
        CheckInput();
    }

	// Update is called once per frame
	void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        if (playerInControl)
        {
            Move(horizontal);
            Flip(horizontal);
            ResetValues();
        }
        else { PlayIntroAnimation(horizontal); }
		
	}
    private void Move(float horizontal) {
        //rb.velocity = Vector2.left;
        if (isGrounded || airControl) {
            rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        }
        animator.SetFloat("speed", Mathf.Abs(horizontal));
        animator.SetBool("jump", isGrounded);
        if (isGrounded && jump){
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));

        }
    }

    private void Flip(float horizontal) {
        if(horizontal > 0 && !facingRight || horizontal < 0 && facingRight) {
            facingRight = !facingRight;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    private void CheckInput() {
        jump |= Input.GetKeyDown(KeyCode.Space);
    }

    private bool IsGrounded() {
        if(rb.velocity.y <= 0) {
            foreach (Transform pt in groundPoints){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(pt.position, groundRaduis, ground);
                foreach (Collider2D c in colliders){
                    if (c.gameObject != gameObject){
                        return true;
                    }
                    
                }
            }
        }
        return false;
    }
    private void ResetValues(){
        jump = false;
    }

    public void PlayIntroAnimation(float horizontal){
        if(Input.GetKeyDown(KeyCode.Return)){
            dialogueNum++;
        }
        manager.ShowBox(dialogueNum);
        if(dialogueNum==5){
            animator.SetTrigger("lookup")
            //princess falls, boy looks up then keeps reading
        }

        if(dialogueNum==7){
            //beasts come down and steal princess
        }
        if(dialogueNum==9){
            //intro scene is over, player gets control of game
            playerInControl = true;
            //when player moves
            if (!animator.GetBool("sitting")) {
                animator.SetBool("playerInControl", true);
            }
            if (Mathf.Abs(horizontal)>0 || jump)
            {
                animator.SetBool("sitting", false);
            }


        }
        
    }
}

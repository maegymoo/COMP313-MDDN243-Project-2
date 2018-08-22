using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator animator;
    private DialogueManager manager;
    private Princess princess;
    private AirMonster airMonster;

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
    private bool attack;
    private bool magicAttack;

    private bool airControl;

    private bool playerInControl;

    [SerializeField]
    private float jumpForce;

    bool hasMagic;

    bool introover;

    private int dialogueNum;

	// Use this for initialization
	void Start() {
        facingRight = true;
        playerInControl = false;
        hasMagic = false;
        dialogueNum = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<DialogueManager>();
        princess = FindObjectOfType<Princess>();
        airMonster = FindObjectOfType<AirMonster>();
        introover = false;
        StartCoroutine(checkDead());
	}

    private void Update(){
        
        CheckInput();
        float horizontal = Input.GetAxis("Horizontal");
    }

	// Update is called once per frame
	void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        if (playerInControl)
        {
            isGrounded = IsGrounded();
            Move(horizontal);
            Flip(horizontal);
            ResetValues();
        }
        else { PlayIntroAnimation(); }
		
	}
    private void Move(float horizontal) {
        
        if ((Mathf.Abs(horizontal) > 0)&&!introover)
        {
            Debug.Log("triggered to standup");
            animator.SetTrigger("standup");
            introover = true;
        }


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

    private void Attack(){
        
    }

    private void CheckInput() {
        jump |= Input.GetKeyDown(KeyCode.Space);
        attack |= Input.GetKeyDown(KeyCode.Return);
        magicAttack |= Input.GetKeyDown(KeyCode.RightShift);


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

    IEnumerator checkDead(){
        //while (true) {
            if (rb.position.y < -5)
            {
                manager.ShowBox(16);
                yield return new WaitForSeconds(5);
                SceneManager.LoadScene("Scene_Name");
            }
        //}
    }

    public void PickUpSword(){
        animator.SetTrigger("hassword");
    }

    public void DrinkPotion(){
        hasMagic = true;
    }

    public void PlayIntroAnimation(){
        
        if(Input.GetKeyDown(KeyCode.Return)){
            dialogueNum++;
        }
        manager.ShowBox(dialogueNum);
        if(dialogueNum==4){
            animator.SetTrigger("lookup");
            princess.TurnOnGravity();
            //princess falls, boy looks up then keeps reading
        }
        if(dialogueNum==5){
            animator.SetTrigger("lookdown");
        }

        if(dialogueNum==7){
            airMonster.StealPrincess();
            Debug.Log("stealing princess");
            //beasts come down and steal princess

        }
        if(dialogueNum==9){
            //intro scene is over, player gets control of game
            playerInControl = true;

        }
        
    }
}

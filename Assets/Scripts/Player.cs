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


    private bool playerInControl;

    [SerializeField]
    private float jumpForce;

    bool hasMagic;
    bool hasSword;

    public GameObject magicBall;

    bool introover;
    float timeElapsed;

    float health;

    private int dialogueNum;

	// Use this for initialization
	void Start() {
        health = 100;
        facingRight = true;
        playerInControl = false;
        hasMagic = false;
        hasSword = false;
        dialogueNum = 0;
        timeElapsed = 0;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<DialogueManager>();
        princess = FindObjectOfType<Princess>();
        airMonster = FindObjectOfType<AirMonster>();
        introover = false;
	}

    private void Update(){
        CheckDead();
        CheckInput();
    }

	// Update is called once per frame
	void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        if (playerInControl)
        {
            isGrounded = IsGrounded();
            CheckAttack();
            Move(horizontal);
            Flip(horizontal);
            ResetValues();
        }
        else { PlayIntroAnimation(); }
		
	}
    private void Move(float horizontal) {

        if ((Mathf.Abs(horizontal) > 0) && !introover)
        {
            if (dialogueNum == 9)
            {
                manager.ShowBox(12);
            }
            else
            {
                manager.ShowBox(14);
            }
            animator.SetTrigger("standup");
            introover = true;
        }
        if (!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("attack")&&isGrounded){
                rb.velocity = new Vector2(horizontal * movementSpeed, rb.velocity.y);
        }

        animator.SetFloat("speed", Mathf.Abs(horizontal));
        if (isGrounded&&jump){
            animator.SetBool("jump", isGrounded);
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

    private void CheckAttack(){
        if(attack&&hasSword&&!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("attack")){
            animator.SetTrigger("attack");
        }
        if(magicAttack&&hasMagic&&!this.animator.GetCurrentAnimatorStateInfo(0).IsTag("magicAttack")){
            animator.SetTrigger("magicAttack");
            Instantiate(magicBall, transform.position, Quaternion.identity);
        }
    }

    private void CheckInput() {
        jump |= Input.GetKeyDown(KeyCode.Space);
        attack |= Input.GetMouseButtonDown(0);
        magicAttack |= Input.GetMouseButtonDown(1);
        if(magicAttack){
            Debug.Log("Click");
        }


    }

    private bool IsGrounded() {
        if(rb.velocity.y <= 0) {
            foreach (Transform pt in groundPoints){
                Collider2D[] colliders = Physics2D.OverlapCircleAll(pt.position, groundRaduis, ground);
                foreach (Collider2D c in colliders){
                    if (c.gameObject != gameObject && c.gameObject.tag == "Ground"){
                        return true;
                    }
                    
                }
            }
        }
        return false;
    }
    private void ResetValues(){
        jump = false;
        attack = false;
        magicAttack = false;
    }



    private void CheckDead(){
        if (rb.position.y < -5)
        {
            manager.ShowBox(18);
            Time.timeScale = 0;
        }
        else if (health <=0){
            manager.ShowBox(25);
            Time.timeScale = 0;
        }
    }

    public void PickUpSword(){
        animator.SetTrigger("hassword");
        hasSword = true;
    }

    public void DrinkPotion(){
        hasMagic = true;
    }
    public void TakeDamage(){
        health -= 20;
    }

    public void PlayIntroAnimation(){
        
        if(Input.GetKeyDown(KeyCode.Return)&&dialogueNum<9){
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
            //beasts come down and steal princess

        }
        if (dialogueNum == 9||dialogueNum==10) {
            if (timeElapsed < 300) {
                //intro scene is over, player gets control of game
                if (Input.GetKeyDown(KeyCode.A)||Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown(KeyCode.Space)) {
                    playerInControl = true;
                }
            }
            else { 
                dialogueNum++;
                timeElapsed = 0;
            }
            timeElapsed++;
        }
        if (dialogueNum == 11) {
             //intro scene is over, player gets control of game
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Space))
            {
                playerInControl = true;
            }
        }
        
    }
}

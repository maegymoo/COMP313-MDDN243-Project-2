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

    bool introover;
    float timeElapsed;

    private int dialogueNum;

	// Use this for initialization
	void Start() {
        facingRight = true;
        playerInControl = false;
        hasMagic = false;
        dialogueNum = 0;
        timeElapsed = 0;
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
        Debug.Log(isGrounded);
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
        }
    }

    private void CheckInput() {
        jump |= Input.GetKeyDown(KeyCode.Space);
        attack |= Input.GetKeyDown(KeyCode.Mouse0);
        magicAttack |= Input.GetKeyDown(KeyCode.Mouse1);


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

    IEnumerator checkDead(){
        if (rb.position.y < -5)
        {
            manager.ShowBox(16);
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene("Scene_Name");
        }
    }

    public void PickUpSword(){
        animator.SetTrigger("hassword");
        hasSword = true;
    }

    public void DrinkPotion(){
        hasMagic = true;
        animator.SetTrigger("hasMagic");
    }
    public void TakeDamage(){
        //enter here
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


using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    public float speed;
    public int PlayerNumber;
    private Vector2 movementInput;
    private Rigidbody2D rb2d;
    private TwoPlayerActionControl playerActionControl;

    private Animator anim;

    void Awake()
    {
        playerActionControl = new TwoPlayerActionControl();

        anim=GetComponent<Animator>();
    }

    void OnEnable()
    {
        playerActionControl.Enable();
    }

    void OnDisable()
    {
        playerActionControl.Disable();
    }

    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        
        if (PlayerNumber == 1)
        {
            movementInput = playerActionControl.main.Move.ReadValue<Vector2>();
        } else
        {
            movementInput = playerActionControl.secondary.Move.ReadValue<Vector2>();
        }
        
        Debug.Log(movementInput);
        // Basic movement input
        // TODO - refactor to be able to choose what keys affect the Input axes
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Use the two store floats to create a new Vector2 variable movement.
        //Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Set the velocity property of our Rigidbody2D rb2d to give our player some movement.
        // Elected to use velocity instead of AddForce because AddForce feels really floaty.
        rb2d.velocity = movementInput * speed;

        //animating the socks
        if(movementInput.y<0){
            anim.SetBool("facingForward",true);
        }else if(movementInput.y>0){
            anim.SetBool("facingForward",false);
        }

        if (rb2d.velocity!=Vector2.zero){
            anim.SetBool("walking",true);
        }else{
            anim.SetBool("walking",false);
        }

        if(movementInput.x>0){
            GetComponent<SpriteRenderer>().flipX=true;
        }else if(movementInput.x<0){
            GetComponent<SpriteRenderer>().flipX=false;
        }
    }
}
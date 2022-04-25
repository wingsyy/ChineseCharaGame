using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float horizontalmove;
    private float verticalmove;
    public Transform groundCheck;
    public float speed;
    public float jumpForce;
    public float arrowForce;
    private Rigidbody2D rb;
    public bool isGround;
    private bool isJump;
    private bool jumpPressed;
    private bool shootPressed;
    public bool isRight;
    public Material diffuse;
    public GameObject arrow;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        PlayerMove();
        Jump();
        Shoot();
    }
    private void Update()
    {
        IsGround();
        IsShoot();
    }
    void PlayerMove()
    {
        horizontalmove = Input.GetAxisRaw("Horizontal");
        //verticalmove = Input.GetAxisRaw("Vertical");
        rb.velocity =  new Vector2(speed * Time.deltaTime*horizontalmove, rb.velocity.y);
        if (horizontalmove != 0)
        {
            transform.localScale = new Vector3(-horizontalmove*0.6f, transform.localScale.y, transform.localScale.z);
            if(horizontalmove>0)
            {
                isRight = true;
            }
            else
            {
                isRight = false;
            }
        }
        
    }
    void IsGround()
    {
        isGround = Physics2D.Linecast(transform.position, groundCheck.position, LayerMask.GetMask("Ground"));
        if (isGround)
        {
            isJump = true;
        }
        if (Input.GetButtonDown("Jump")&&isGround)
        {
            jumpPressed = true;
        }
    }
    void Jump()
    {
        if (isJump && jumpPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce*Time.deltaTime);
            isJump = false;
            jumpPressed = false;
        }
    }
    void IsShoot()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            shootPressed = true;
        }
    }
    void Shoot()
    {
        if(shootPressed)
        {
            if (isRight)
            {
                var arrowObj = Instantiate(arrow, transform.position + new Vector3(1f, 1f, 0), Quaternion.identity);
                arrowObj.transform.localScale = new Vector3(-arrowObj.transform.localScale.x, arrowObj.transform.localScale.y, arrowObj.transform.localScale.z);
                arrowObj.GetComponent<Rigidbody2D>().velocity = new Vector2(arrowForce * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                var arrowObj = Instantiate(arrow, transform.position + new Vector3(-1f, 1f, 0), Quaternion.identity);
                arrowObj.GetComponent<Rigidbody2D>().velocity = new Vector2(-arrowForce * Time.deltaTime, rb.velocity.y);
            }
            shootPressed = false;
        }
    }
}

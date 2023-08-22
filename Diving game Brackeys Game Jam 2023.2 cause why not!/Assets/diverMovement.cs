using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diverMovement : MonoBehaviour
{
    public float ozTank = -0.5f;
    public float tankChange = 0.002f;
    public Rigidbody2D rb;
    public float sideMoveSpeed = 1f;
    public float sinkRate = 4f;
    public float VCap;
    public float SideVCap;
    private float horizaltal, vertical;
    public GameObject ozbar;
    public Animator anim;
    public AnimationClip swimleft;
    public AnimationClip swimright;
    public AnimationClip idle;
    public LayerMask raylayer;
    public TrailRenderer trail;
    public bool grounded;
    public GameObject groundCheck;
    public float groundcheckheight;
    public master master;
   

    [Header("Dashing")]
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime;
    private Vector2 dashingDir;
    private bool isDashing;
        private bool canDash = true;


    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        tankStuff();
        movement();
        dash();
    }
    private void tankStuff()
    {


        ozTank -= 0.01f * Time.deltaTime;
        if (ozTank < -0.4999)
        {
            master.GAMEOVER();
        }
        if (rb.velocity.y >= VCap) rb.velocity = new Vector2(rb.velocity.x, VCap);
        if (rb.velocity.y <= -VCap) rb.velocity = new Vector2(rb.velocity.x, -VCap);

        if (ozTank >= 0.5f) ozTank = 0.5f;
        if (ozTank <= -0.5f) ozTank = -0.5f;
        //rb.gravityScale = -((ozTank * 2) / sinkRate);
        ozbar.transform.localScale = new Vector3(ozbar.transform.localScale.x, (ozTank + 0.5f)*1f, ozbar.transform.localScale.z);
    }
    private void movement()
    {
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, groundcheckheight, raylayer);

        if (hit.collider != null)
        {
            grounded = true;
           
        }
        else
        {
            grounded = false;
           
        }




        if (!grounded)
        {
            sideMoveSpeed = 30;
            rb.drag = 0.6f;
            if (Input.GetKey(KeyCode.D))
            {

                //transform.Translate(Vector3.right * Time.deltaTime * sideMoveSpeed, Space.World);
                rb.AddForce(Vector2.right * sideMoveSpeed * Time.deltaTime);
                if (!Input.GetKey(KeyCode.A)) anim.Play("swim right");

            }
            if (Input.GetKey(KeyCode.A))
            {
                // transform.Translate(Vector3.left * Time.deltaTime * sideMoveSpeed, Space.World);
                rb.AddForce(Vector2.left * sideMoveSpeed * Time.deltaTime);
                if (!Input.GetKey(KeyCode.D)) anim.Play("swim left");
            }
            if (!Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A)) anim.Play("idle");
            if (rb.velocity.x >= SideVCap) rb.velocity = new Vector2(SideVCap, rb.velocity.y);
            if (rb.velocity.x <= -SideVCap) rb.velocity = new Vector2(-SideVCap, rb.velocity.y);
        }
        else
        {
          

            sideMoveSpeed = 4000;
            rb.drag = 1f;
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(Vector2.right * sideMoveSpeed * Time.deltaTime);
                if (!Input.GetKey(KeyCode.A)) anim.Play("swim right");

            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(Vector2.left * sideMoveSpeed * Time.deltaTime);
                if (!Input.GetKey(KeyCode.D)) anim.Play("swim left");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector2.up * 2000);
            }

            if (!Input.GetKey(KeyCode.D) & !Input.GetKey(KeyCode.A)) anim.Play("idle");
            if (rb.velocity.x >= 1) rb.velocity = new Vector2(1, rb.velocity.y);
            if (rb.velocity.x <= -1) rb.velocity = new Vector2(-1, rb.velocity.y);
        }
      

        

    }
    private void dash()
    {
        if (ozTank > -0.49f)
        {
            canDash = true;
        }
        else
        {
            canDash = false;
        }
        if (grounded) canDash = true;
        horizaltal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        var Dashinput = Input.GetKeyDown(KeyCode.Space);
        if (Dashinput && canDash && !grounded)
        {
            isDashing = true;
            ozTank -= tankChange;
            
            dashingDir = new Vector2(horizaltal, vertical);
            if (dashingDir == Vector2.zero)
            {
                
                return;
            }
            StartCoroutine(StopDashing());
            if (isDashing)
            {
                rb.velocity = dashingDir.normalized * dashingVelocity;
            }
        }
    }
    private IEnumerator StopDashing()
    {
        yield return new WaitForSeconds(dashingTime);
       
        isDashing = false;
    }

    public void addAir(int air)
    {
        ozTank += tankChange * air;
    }
   
}

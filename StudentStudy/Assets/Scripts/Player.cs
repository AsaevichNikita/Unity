
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;
    public float speed;
    public float jampHeight;
    public Transform grondCheck;
    bool isGrounded;
    Animator anim;
    int curHp;
    int maxHp = 3;
    bool isHit = false;
    public Main main;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        curHp = maxHp;
    }


    void Update()
    {
        CheckGround();
        if(Input.GetAxis("Horizontal") == 0 && isGrounded)
        {
            anim.SetInteger("State", 1);
        }
        else
        {
            Flip();
            if (isGrounded)
            {
                anim.SetInteger("State", 2);
            }
        }
    }

    private void LateUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            rb.AddForce(transform.up * jampHeight, ForceMode2D.Impulse);
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(grondCheck.position, 0.2f);
        isGrounded = colliders.Length > 1;
        if(!isGrounded)
        {
            anim.SetInteger("State", 3);
        }
    }

    public void RecountHp(int deltaHp)
    {
        curHp += deltaHp;
        if (deltaHp < 0)
            StopCoroutine(OnHit());
            isHit = true;
            StartCoroutine(OnHit());
        if (curHp <= 0)
        {
            GetComponent<CapsuleCollider2D>().enabled = false;
            Invoke("Lose", 1.5f);
        }
            
    }

    IEnumerator OnHit()
    {
        if(isHit)
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g - 0.04f, GetComponent<SpriteRenderer>().color.b - 0.04f);
        else
            GetComponent<SpriteRenderer>().color = new Color(1f, GetComponent<SpriteRenderer>().color.g + 0.04f, GetComponent<SpriteRenderer>().color.b + 0.04f);


        if (GetComponent<SpriteRenderer>().color.g <= 0)
        {
            isHit = false;
        }

        if(GetComponent<SpriteRenderer>().color.g == 1)
        {
            StopCoroutine(OnHit());
        }

        yield return new WaitForSeconds(0.02f);
        StartCoroutine(OnHit());

    }


    void Lose()
    {
        main.GetComponent<Main>().Lose();
    }
}

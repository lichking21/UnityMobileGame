using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    private Animator anim;

    public GameObject floatingText;
    public Transform attackPoint;
    public Transform target;
    public LayerMask playerLay;
    public float pushForce;
    public float speed;
    public float atkRange;
    public bool walk = true;
    public int points;
    public int hit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float disToPlayer = Vector2.Distance(transform.position, target.position);

        if (collision == player)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("move", false);
            target = null;
        }
        else
            target = player.transform;
    }

    private void Update()
    {
        float disToPlayer = Vector2.Distance(transform.position, target.position);

        if (target != null && disToPlayer > 0.5)
        {
            Walk();
            anim.SetBool("attack", false);
        }
        else if (player.GetComponent<Player>().currHp <= 0)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("attack", false);
            anim.SetBool("move", false);
        }
        else
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("move", false);
            anim.SetBool("attack", true);
        }

        Speed();
    }

    private void ShowPoints(string text)
    {
        if (floatingText)
        {
            GameObject prefab = Instantiate(floatingText, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
    private void Walk()
    {
        anim.SetBool("move", true);
        anim.SetBool("attack", false);
        if (transform.position.x < target.position.x)
        {
           rb.velocity = new Vector3(speed, 0, 0);
           transform.localScale = new Vector2(-2, 2);
        }
       else if (transform.position.x > target.position.x)
       {
            rb.velocity = new Vector3(-speed, 0, 0);
            transform.localScale = new Vector2(2, 2);
       }
    }
    private void Speed()
    {
        if (GameManager.instance.streek < 10) speed = 1.5f;
        else if (GameManager.instance.streek >= 10 && GameManager.instance.streek < 25) speed = 2.5f;
        else if (GameManager.instance.streek >= 25 && GameManager.instance.streek < 50) speed = 3.3f;
        else if (GameManager.instance.streek >= 50) speed = 4f;
    }
    public void Attack()
    {
        anim.SetBool("attack", true);

        Collider2D attack = Physics2D.OverlapCircle(attackPoint.position, atkRange, playerLay);

        if (attack != null)
            player.GetComponent<Player>().GetHit(1);
        else
            anim.SetBool("attack", false);
    }

    public void GetHit(int damage)
    {
        hit -= damage;
        anim.SetTrigger("hit");

        if (hit <= 0)
        {
            Destroy(gameObject);

            GameManager.instance.streek++;
            points = GameManager.instance.streek * points;
            GameManager.instance.score += points;

            ShowPoints(points.ToString());
        }
    }
    public void PlayerHitted()
    {
        float time = 0;
        anim.SetBool("attack", false);
        speed = 1f;

        if (1 > time)
        {
            time += Time.deltaTime;
            Vector2 pushDirection = (target.transform.position - this.transform.position).normalized;
            rb.AddForce(-pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, atkRange);
    }
}

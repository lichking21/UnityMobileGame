using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private GameObject closeEnemy;
    private Enemy enemy = new Enemy();
    public bool alive = true;

    public AudioSource hitSound;
    public AudioSource takeDmgSound;
    public Button right;
    public Button left;
    public Transform attackPoint;
    public Transform pointHit;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public LayerMask enemyLayer;

    public int wasDead;
    public bool hitted;
    public int currHp;
    public int maxHp;
    public float attackRange;
    public float hitRange;
    public bool rightHit;
    public bool leftHit;

    public static Player instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currHp = maxHp;
    }
    private void Update()
    {
        closeEnemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Attack()
    {
        float dis = Vector2.Distance(transform.position, closeEnemy.transform.position);
        Collider2D[] detEnemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
        foreach (Collider2D enemy in detEnemy)
        {
            enemy.GetComponent<Enemy>().GetHit(1);
            transform.position = enemy.transform.position;
        }

        if (dis > 2f)
            GameManager.instance.streek /= 2;
    }
    private void Hearts()
    {
        if (currHp > maxHp)
            currHp = maxHp;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currHp)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;

            if (i < maxHp)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }
    public void rHit()
    {
        if (alive)
        {
            transform.localScale = new Vector2(-2, 2);
            Attack();
            GameManager.instance.streekAnim.SetTrigger("show");

            int rnd = Random.Range(1, 3);

            hitSound.Play();
            if (rnd == 1)
                anim.SetTrigger("Attack");
            else if (rnd == 2)
                anim.SetTrigger("Attack2");
            else if (rnd == 3)
                anim.SetTrigger("Attack3");
        }
    }
    public void lHit()
    {
        if (alive)
        {
            transform.localScale = new Vector2(2, 2);
            Attack();
            GameManager.instance.streekAnim.SetTrigger("show");

            int rnd = Random.Range(1, 4);

            hitSound.Play();
            if (rnd == 1)
                anim.SetTrigger("Attack");
            else if (rnd == 2)
                anim.SetTrigger("Attack2");
            else if (rnd == 3)
                anim.SetTrigger("Attack3");
        }
    }

    private void HitKill()
    {
        Collider2D[] enemyIn = Physics2D.OverlapCircleAll(pointHit.position, hitRange, enemyLayer);
        foreach (Collider2D enemy in enemyIn)
            enemy.GetComponent<Enemy>().PlayerHitted();
    }

    //public void RightHit()
    //{
    //    sr.flipX = false;
        
    //    if (alive)
    //    {
    //        GameObject[] enemieS = GameObject.FindGameObjectsWithTag("Enemy");
    //        GameObject closeEnemy = null;
    //        float minDist = 2;

    //        foreach(GameObject enemy in enemieS)
    //        {
    //            float disToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
                
    //            if (disToEnemy < minDist)
    //            {
    //                minDist = disToEnemy;
    //                closeEnemy = enemy;
    //            }
    //        }

    //        if (closeEnemy != null && minDist <= 2f)
    //        {
    //            if (closeEnemy.transform.position.x > transform.position.x)
    //            {
    //                transform.position = closeEnemy.transform.position;
    //                closeEnemy.GetComponent<Enemy>().GetHit(1);
    //            }
    //        }
    //        else if (closeEnemy == null && minDist > 2f)
    //        {
    //            GameController.instance.streek /= 2;
    //        }

    //        if (Random.value < 0.5)
    //            anim.SetTrigger("Attack");
    //        else
    //            anim.SetTrigger("Attack2");
    //    }
    //}
    //public void LeftHit()
    //{
    //    sr.flipX = true;

    //    if (alive)
    //    {
    //        GameObject[] enemieS = GameObject.FindGameObjectsWithTag("Enemy");
    //        GameObject closeEnemy = null;
    //        float minDist = 2;

    //        foreach (GameObject enemy in enemieS)
    //        {
    //            float disToEnemy = Vector2.Distance(transform.position, enemy.transform.position);

    //            if (disToEnemy < minDist)
    //            {
    //                minDist = disToEnemy;
    //                closeEnemy = enemy;
    //            }
    //        }

    //        if (closeEnemy != null && minDist <= 2f)
    //        {
    //            if (closeEnemy.transform.position.x < transform.position.x)
    //            {
    //                transform.position = closeEnemy.transform.position;
    //                closeEnemy.GetComponent<Enemy>().GetHit(1);
    //            }
    //        }
    //        else if (closeEnemy == null && minDist > 2f)
    //        {
    //            GameController.instance.streek /= 2;
    //        }

    //        if (Random.value < 0.5)
    //            anim.SetTrigger("Attack");
    //        else
    //            anim.SetTrigger("Attack2");
    //    }
    //}





    //private void Attack()
    //{
    //    GameObject[] enemyeS = GameObject.FindGameObjectsWithTag("Enemy");
    //    GameObject closeEnemy = null;
    //    float minDist = 2f;
    //    Vector3 mousePos = UnityEngine.Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    foreach(GameObject enemy in enemyeS)
    //    {
    //        float disToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
    //        if (disToEnemy < minDist)
    //        {
    //            closeEnemy = enemy;
    //            minDist = disToEnemy;
    //        }
    //    }

    //    if (Input.GetKeyUp(KeyCode.Mouse0) && alive)
    //    {
    //        if (Random.value < 0.5)
    //            anim.SetTrigger("Attack");
    //        else
    //            anim.SetTrigger("Attack2");
            
    //        if (closeEnemy != null && minDist <= 2f)
    //        {
    //            if (mousePos.x > transform.position.x && closeEnemy.transform.position.x > transform.position.x)
    //            {
    //                closeEnemy.GetComponent<Enemy>().GetHit(1);
    //                transform.position = closeEnemy.transform.position;
    //                sr.flipX = false;
    //            }
    //            else if (mousePos.x < transform.position.x && closeEnemy.transform.position.x < transform.position.x)
    //            {
    //                closeEnemy.GetComponent<Enemy>().GetHit(1);
    //                transform.position = closeEnemy.transform.position;
    //                sr.flipX = true;
    //            }
    //            else
    //            {
    //                if (mousePos.x > transform.position.x)
    //                {
    //                    closeEnemy.GetComponent<Enemy>().GetHit(1);
    //                    transform.position = closeEnemy.transform.position;
    //                    sr.flipX = false;
    //                }
    //                else if (mousePos.x < transform.position.x)
    //                {
    //                    closeEnemy.GetComponent<Enemy>().GetHit(1);
    //                    transform.position = closeEnemy.transform.position;
    //                    sr.flipX = true;
    //                }
    //            }
    //        }
    //        else if (closeEnemy == null && minDist > 2f)
    //        {
    //            GameController.instance.streek /= 2;
    //        }
    //    }
    //}

    public void GetHit(int damage)
    {
        currHp -= damage;
        enemy.speed = 0.5f;

        anim.SetTrigger("Hit");
        takeDmgSound.Play();

        Hearts();
        HitKill();

        if (currHp <= 0)
        {
            alive = false;
            anim.SetBool("Death", true);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(pointHit.position, hitRange);
    }
}

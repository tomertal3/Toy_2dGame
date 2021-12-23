using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : MonoBehaviour
{
    [SerializeField] private float MovementSpeed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float[] positions;
    [SerializeField] private bool LookingRight;


    public GameObject Shadow;

    private Rigidbody2D rb;

    private int Side;
    private int index;

    private bool start = false;

    private SpriteRenderer sr;
    //#####################################################
    IEnumerator jumping()
    {
        if (start)
        {
            while (true)
            {
                int seconds = Random.RandomRange(1, 4);
                yield return new WaitForSecondsRealtime(seconds);
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                Shadow.SetActive(false);
                yield return new WaitForSecondsRealtime(0.5f);
                Shadow.SetActive(true);
            }
        }

    }
    IEnumerator wait()
    {
        yield return new WaitForSecondsRealtime(2);
        start = true;

    }
    //#####################################################
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        sr.flipX = LookingRight;
        StartCoroutine("jumping");
        StartCoroutine("wait");
    }
    //#####################################################
    void Update()
    {
        if (start)
        {
            Vector3 directionTranslation = (LookingRight) ? transform.right : -transform.right;
            directionTranslation *= Time.deltaTime * MovementSpeed;
            transform.Translate(directionTranslation);

            if ((LookingRight && transform.position.x >= positions[index]) || (!LookingRight && transform.position.x <= positions[index]))
            {
                LookingRight = !LookingRight;
                sr.flipX = LookingRight;
                if (index == positions.Length - 1)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
            }
        }
    }

    //#####################################################
    private void OnCollisionEnter2D(Collision2D collision)
    {
/*        if (collision.gameObject.CompareTag("Wall"))
        {
            LookingRight = !LookingRight;
            sr.flipY = LookingRight;
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            LookingRight = !LookingRight;
            sr.flipY = LookingRight;
        }*/
        if (collision.gameObject.CompareTag("EnemyHit"))
        {
            BoxCollider2D[] colliders1 =GetComponentsInChildren<BoxCollider2D>();
            BoxCollider2D[] colliders2 =collision.gameObject.GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D element1 in colliders1)
            {
                foreach (BoxCollider2D element2 in colliders2)
                {
                    Physics2D.IgnoreCollision(element1, element2);
                }
            }
        }

        if (collision.gameObject.CompareTag("KillZone"))
        {
            BoxCollider2D[] colliders1 = GetComponentsInChildren<BoxCollider2D>();
            BoxCollider2D[] colliders2 = collision.gameObject.GetComponentsInChildren<BoxCollider2D>();
            foreach (BoxCollider2D element1 in colliders1)
            {
                foreach (BoxCollider2D element2 in colliders2)
                {
                    Physics2D.IgnoreCollision(element1, element2);
                }
            }
        }
    }
    

}

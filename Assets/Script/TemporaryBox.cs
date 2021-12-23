using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryBox : MonoBehaviour
{
    private Rigidbody2D rb;

    Vector3 startPos;
    Vector3 startScale;

    Quaternion startRotation;

    private bool falling = false;

    private AudioSource audio;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.position;
        startRotation = transform.rotation;
        startScale = transform.localScale;
        audio = GetComponent<AudioSource>();
    }
    //#########################################################################
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("Fall");
        }
    }
    //#########################################################################
    IEnumerator Fall()
    {
        if (!falling)
        {
            falling = true;
            audio.Play();
            yield return new WaitForSecondsRealtime(0.2f);
            rb.bodyType = RigidbodyType2D.Dynamic;
            yield return new WaitForSecondsRealtime(2.5f);
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = new Vector2(0,0);
            rb.angularVelocity = 0;
            rb.rotation = 0;
            transform.rotation = startRotation;
            transform.position = startPos;
            transform.localScale = startScale;
            falling = false;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalItem : MonoBehaviour
{

    private float speed = 4;
    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("hit");
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine("FadeHorizontal");
        }
    }

    IEnumerator FadeHorizontal()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
    }
}

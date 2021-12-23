using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            startFading();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            CoinGenrator.instance.Decrease();
            Destroy(gameObject);
        }
    }
    
    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.08f);
        }
        Destroy(gameObject);
    }
    public void startFading()
    {
        StartCoroutine("FadeOut");
    }
}

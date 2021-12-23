using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public GameObject Shadow;


    private void Start()
    {
        if (ScoreManager.instance != null && ScoreManager.instance.collectedCoins.Count > 0)
        {
            Destroy(gameObject);
            Destroy(Shadow);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
     {
        if (collision.gameObject.CompareTag("Player"))
        {
            ScoreManager.instance.ChangeScore(coinValue);
            ScoreManager.instance.collectedCoins.Add(gameObject.GetInstanceID());

        }

    }

    private void OnDestroy()
    {
        Destroy(Shadow);
    }




}

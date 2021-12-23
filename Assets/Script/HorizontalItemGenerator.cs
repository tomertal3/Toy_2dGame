using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HorizontalItemGenerator : MonoBehaviour
{
    public GameObject Horizontal;

    private List<float> yPos = new List<float>();


    void Start()
    {
        yPos.Add(-2.8f); yPos.Add(0.9f); yPos.Add(3.1f);
        StartCoroutine("Puse");
    }
    IEnumerator Puse()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            Vector3 v3Pos = Camera.main.ViewportToWorldPoint(new Vector3(1.1f, 0.5f, 10.0f));
            float spawnY = Random.Range(0, 3);
            float spawnX = 44.7f;
             Vector2 spawnPosition = new Vector2(v3Pos.x, yPos[(int)spawnY]);
            // Vector2 spawnPosition = new Vector2(spawnX, yPos[(int)spawnY]);
            Instantiate(Horizontal, spawnPosition, Quaternion.identity);
        }
    }

    void Update()
    {
    }




}

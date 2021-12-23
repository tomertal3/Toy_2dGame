using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontDestroy : MonoBehaviour
{
    private GameObject[] objects;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        objects = GameObject.FindGameObjectsWithTag("MainPuse");
        if (objects.Length > 1)
        {
            Destroy(objects[1]);
        }
         GetComponent<Canvas>().worldCamera = Camera.main;
         
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{
    public static GameMusic gameMusic;



private void Awake()
    {
        if (gameMusic != null && gameMusic != this)
        {
            Destroy(this.gameObject);
            return;
        }

        gameMusic = this;
        DontDestroyOnLoad(this);
    }
    private void Update()
    {
        if(LoadLevel.instance.count == 7)
        {
            GetComponent<AudioSource>().Stop(); ;
        }
    }
}

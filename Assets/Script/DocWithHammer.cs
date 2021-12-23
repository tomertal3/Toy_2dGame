using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DocWithHammer : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private Vector3 position;

    private bool canMove = false;

    private Animator anim;

    private bool activated= false;

    private AudioSource audio;


  
    public void waitForPatint()
    {
        if (!activated)
        {
            canMove = true;
        }
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    IEnumerator startHammer()
    {
        activated = true;
        anim.SetBool("GotToPoint", true);
        canMove = false;
        while (!SkillCheck.instance.finshed)
        {
            audio.Play();
            yield return new WaitForSecondsRealtime(1.3f);
        }
        anim.SetBool("GotToPoint", false);
        anim.SetBool("isRunning", false);
     }

    void Update()
    {
        if (canMove)
        {
            anim.SetBool("isRunning", true);
            transform.position = Vector2.MoveTowards(transform.position, position, Time.deltaTime * speed);
        }
        if (transform.position == position && !activated)
        {
            StartCoroutine("startHammer");
        }
    }
}



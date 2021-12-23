using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharcterController : MonoBehaviour
{

    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip walk1;
    [SerializeField] AudioClip walk2;
    [SerializeField] AudioClip itemCollect;

    public float MovmentSpeed;
    public float JumpForce;


    public Sprite faceIdle;
    public Sprite faceMove;

    public SpriteRenderer face;
    public GameObject Shadow;

    private Animator anim;

    private Rigidbody2D rb;

    private GameObject[] players;

    private SpriteRenderer sr;
    private SpriteRenderer[] sprites;

    private AudioSource audio;
    
    private bool canMove = true;
    private bool firstIntruction = false;
    private bool CanPressSapce = false;
    private int spaceCount = 0;
    private bool hitTheGround = false;
    private bool walkingSound = false;
    private bool isWalking = false;
    private bool isGrounded = false;
    private bool JumpigOnEnemy = false;
    private bool gotHit = false;
    //  for skillcheck level
    private bool skillCheckActivated = false;

//#########################################################################

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprites = GetComponentsInChildren<SpriteRenderer>();
      // sr = GetComponent<SpriteRenderer>();
        audio = GetComponent<AudioSource>();
        StartCoroutine(normalMas());
    }
//#########################################################################

    //for falling from coluds
    IEnumerator normalMas()
    {
        canMove = false;
        rb.gravityScale = 0.2f;
        anim.SetBool("isFalling", true);
        while (!hitTheGround)
        {
            yield return new WaitForSecondsRealtime(0.001f);
        }
        anim.SetBool("isFalling", false);
        canMove = true;
        rb.gravityScale = 3f;
        spaceCount = 0;
    }
    //for skill check level
    IEnumerator disableMovment()
    {
        Shadow.SetActive(false);
        CanPressSapce = true;
        Color c;
        for (int i = 0; i < sprites.Length; i++)
        {
            c = sprites[i].material.color;
            c.a = 0.0f;
            sprites[i].material.color = c;
        }
        //Color c = sr.material.color;
        //c.a = 0.0f;
        //sr.material.color = c;
        while (!SkillCheck.instance.finshed)
        {
            yield return new WaitForSecondsRealtime(0.1f);
        }
        canMove = true;
        skillCheckActivated = true;
        anim.SetBool("firstChange",true);
        for (int i = 0; i < sprites.Length; i++)
        {
            c = sprites[i].material.color;
            c.a = 1;
            sprites[i].material.color = c;
        }
        //c.a = 1;
        //sr.material.color = c;
        Shadow.SetActive(true);
        CanPressSapce = false;
        spaceCount = 0;
    }

    // for final boss game
    IEnumerator Die()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        DisapearItem(sr);
    }

    //for main room
    IEnumerator firstInstrucations()
    {
        spaceCount = 0;
        CanPressSapce = true;
        while (spaceCount < 3)
        {

            yield return new WaitForSecondsRealtime(0.5f);
        }
        CanPressSapce = false;
        canMove = true;
        firstIntruction = true;
        spaceCount = 0;
    }
    // for level fade in
    IEnumerator FadeIn()
    {
        SpriteRenderer fade = GameObject.Find("FadeOut").GetComponent<SpriteRenderer>();
        Color c = fade.color;
        while (fade.color.a >= 0)
        {
            c.a -= 0.05f;
            fade.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    IEnumerator stopALitle()
    {
        canMove = false;
        CanPressSapce = true;
        yield return new WaitForSeconds(1.1f);
        CanPressSapce = false;
    }
    // make walking sound good
    IEnumerator Walk()
    {
        walkingSound = true;
        audio.PlayOneShot(walk1, 1f);
        yield return new WaitForSecondsRealtime(0.5f);
        if (!isWalking || !isGrounded)
        {
            walkingSound = false;
            yield break;
        }
        audio.PlayOneShot(walk2, 1f);
        yield return new WaitForSecondsRealtime(0.6f);
        walkingSound = false;
    }
    //for simple enemies
    IEnumerator DestroyEmemy(Collider2D collision)
    {
        collision.GetComponent<AudioSource>().Play();
        JumpigOnEnemy = true;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        audio.PlayOneShot(jumpSound, 0.67f);
        anim.SetTrigger("Jump");
        yield return new WaitForSecondsRealtime(0.3f);
        Destroy(collision.gameObject);
        JumpigOnEnemy = false;
        SimpleEnemyManager.instance.KillEnemy();
        if (SimpleEnemyManager.instance.counter == 6)
        {
            while (!isGrounded)
            {
                yield return new WaitForSecondsRealtime(0.001f);
            }
            face.sprite = faceIdle;
            anim.SetBool("isRunning", false);
            Time.timeScale = 0;
        }

    }
    IEnumerator puseHit()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        gotHit = false;
    }
    //#########################################################################

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine("FadeIn");
        if (level == 5 || level == 4 || level == 3)
        {
            StartCoroutine("stopALitle");
        }
        if (level == 2)
        {
            if (!firstIntruction)
            {
                anim.SetBool("isRunning", false);
                canMove = false;
                StartCoroutine("firstInstrucations");
            }
            if (LoadLevel.instance.count == 2)
            {
                GameObject.FindWithTag("StartPos").transform.position = new Vector3(1.99f, -0.0898f, 0f);

            }
            else if (LoadLevel.instance.count == 3)
            {
                GameObject.FindWithTag("StartPos").transform.position = new Vector3(7.68f, -0.0976f, 0f);
            }
        }
        findStartPos();
        players = GameObject.FindGameObjectsWithTag("Player");
        if(players.Length >1)
        {
            Destroy(players[1]);
        }
    }
//#########################################################################
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("jump"))
            {
                anim.SetTrigger("Grounded");
            }
            Shadow.SetActive(true);
            audio.PlayOneShot(walk1);
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            audio.PlayOneShot(itemCollect);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("StopFall"))
        {
            hitTheGround = true;
        }

        //for progresbar  burger level 
        if (collision.gameObject.CompareTag("Point"))
        {
            BurgerManager.instance.OnPoint();
        }
        if (collision.gameObject.CompareTag("EyeEnemy"))
        {
            BurgerManager.instance.InSight();

        }

        //for skill check level
        if (collision.gameObject.CompareTag("skillCheck"))
        {
            if (!skillCheckActivated)
            {
                anim.SetBool("isRunning", false);
                canMove = false;
                StartCoroutine("disableMovment");
                GameObject.Find("HammerDoctor").GetComponent<DocWithHammer>().waitForPatint();
            }
        }

        //for Boss Gmae
        if (collision.gameObject.CompareTag("finalBoss"))
        {
            canMove = false;
            CanPressSapce = true;
            StartCoroutine("Die");
        }

        // for sipmple enemies
        if (collision.gameObject.CompareTag("EnemyHit"))
        {
            collision.GetComponent<AudioSource>().Play();
            SimpleEnemyManager.instance.EnemyHit(); 
        }

        if (collision.gameObject.CompareTag("KillZone"))
        {
            if (!JumpigOnEnemy)
            {
                StartCoroutine("DestroyEmemy", collision);
            }

        }
        //for parkour lvevl
        if (collision.gameObject.CompareTag("Spikes"))
        {
            collision.GetComponent<AudioSource>().Play();
            ParkorManager.instance.CheckPoint();
        }
        if (collision.gameObject.CompareTag("Trampoline"))
        {
            rb.velocity = new Vector2(0,0);
            rb.AddForce(new Vector2(0, 22), ForceMode2D.Impulse);
            collision.GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.CompareTag("GiftCard"))
        {
            Destroy(collision.gameObject);
            CanPressSapce = true;
            anim.SetBool("isRunning", false);
            Color c = sprites[0].material.color;
            c.a = 0.0f;
            sprites[0].material.color = c;
            canMove = false;
            EndManager.instance.End();
        }

    }
    //#########################################################################

    private void OnTriggerStay2D(Collider2D collision)
    {
        //for burger level
        if (collision.gameObject.CompareTag("Point"))
        {
            BurgerManager.instance.UpOrDown();
        }
        if (collision.gameObject.CompareTag("EnemyHit"))
        {
            if (transform.position.x < collision.transform.position.x)
            {
                rb.AddForce(new Vector2(-2, 0), ForceMode2D.Impulse);
                gotHit = true;
                StartCoroutine("puseHit");
            }
            else
            {
                rb.AddForce(new Vector2(2, 0), ForceMode2D.Impulse);
                gotHit = true;
                StartCoroutine("puseHit");
            }
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

    }
//#########################################################################
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Shadow.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Point"))
        {
            BurgerManager.instance.OutOfPoint();
        }
        if (collision.gameObject.CompareTag("EyeEnemy"))
        {
            BurgerManager.instance.OutOfEnemySight();
        }
        }
    //#########################################################################
    void Update()
    {
        if(Time.timeScale == 0)
        {
            canMove = false;
        }
        else
        {
            if (!CanPressSapce)
            {
                canMove = true;
            }
        }
        if (canMove)
        {
            float movment = Input.GetAxis("Horizontal");
            //bad code for simlpe enemy
            if (gotHit)
            {
                movment = 0;
            }
            transform.position += new Vector3(movment, 0, 0) * Time.deltaTime * MovmentSpeed;
            if (Input.GetButtonDown("Jump") && isGrounded)//&& Mathf.Abs(rb.velocity.y) < 0.001f)
            {
                //face.sprite = faceMove;
                rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
                audio.PlayOneShot(jumpSound,0.67f);
                anim.SetTrigger("Jump");
            }
            if (movment > 0)
            {
                face.sprite = faceMove;
                transform.localRotation = Quaternion.Euler(0, 180, 0);
                anim.SetBool("isRunning", true);
                if (isGrounded)
                {
                    if (!walkingSound)
                    {
                        StartCoroutine("Walk");
                    }
                }
            }
            else if (movment < 0)
            {
                face.sprite = faceMove;
                transform.localRotation = Quaternion.Euler(0, 0, 0);
                anim.SetBool("isRunning", true);
                if (isGrounded)
                {
                    if (!walkingSound)
                    {
                        StartCoroutine("Walk");
                    }
                }
            }
            else
            {
                anim.SetBool("isRunning", false);
                face.sprite = faceIdle;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isWalking = true;
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                isWalking = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                isWalking = true;
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                isWalking = false;
            }
        }
        // when not moving, check if plyaer hit space
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && CanPressSapce)
            {
                spaceCount++;
                if (sprites[0].material.color.a >= 1)
                {
                    //pop up click sound
                    GameObject.Find("PauseButton").GetComponent<AudioSource>().Play();
                }
            }
        }
     /*   if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetBool("isFalling", false);
            canMove = true;
            rb.gravityScale = 3f;
            spaceCount = 0;
            SceneManager.LoadScene("Door1");

        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetBool("isFalling", false);
            canMove = true;
            rb.gravityScale = 3f;
            spaceCount = 0;
            SceneManager.LoadScene("Door3");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetBool("isFalling", false);
            canMove = true;
            rb.gravityScale = 3f;
            spaceCount = 0;
            SceneManager.LoadScene("Door2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetBool("isFalling", false);
            canMove = true;
            rb.gravityScale = 3f;
            spaceCount = 0;
            SceneManager.LoadScene("Door4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            anim.SetBool("isFalling", false);
            canMove = true;
            rb.gravityScale = 3f;
            spaceCount = 0;
            SceneManager.LoadScene("Door5");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("isFalling", false);
            canMove = true;
            rb.gravityScale = 3f;
            spaceCount = 0;
            SceneManager.LoadScene("MainRoom");
        }*/

    }
//#########################################################################
    void findStartPos()
    {
        if (GameObject.FindWithTag("StartPos") != null)
        {
            transform.position = GameObject.FindWithTag("StartPos").transform.position;
        }
    }
    //#########################################################################
    private void DisapearItem(SpriteRenderer s)
    {
        Color c = s.color;
            c.a = 0f;
            s.color = c;
    }
    //#########################################################################
    private void ApearItem(SpriteRenderer s)
    {
        Color c = s.color;
        c.a = 1f;
        s.color = c;
    }

}

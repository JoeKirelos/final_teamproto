using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float nukeCD = 15f;
    public float nukeStart = 0f;
    public bool nuked = false;
    public float blankCD = 3f;
    public float blankStart = 0f;
    public bool blanked = false;
    public float forceCD = 1.5f;
    public float forceStart = 0f;
    public bool forced = false;
    public Animator animator;
    public GameObject blankController;
    Vector2 direction;
    Vector2 mousePosition;
    float horDir;
    float verDir;
    Vector2 horMove = new Vector2(1f,0f);
    Vector2 verMove = new Vector2(0f,1f);
    public Rigidbody2D player;
    public float speed;
    public Transform firePoint;
    public LineRenderer lr;
    public static int hitPoints = 20;
    public static int enemiesKilled = 0;

    public AudioClip shooting;
    public AudioClip enemyDeath;
    public AudioClip blankActivation;
    public AudioClip nukeActivation;
    public AudioClip deflecting;

   
    
    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 20;
        enemiesKilled = 0;
        lr.enabled = false;
        GetComponent<AudioSource>().clip = shooting;
        GetComponent<AudioSource>().clip = enemyDeath;
        GetComponent<AudioSource>().clip = blankActivation;
        GetComponent<AudioSource>().clip = nukeActivation;
        GetComponent<AudioSource>().clip = deflecting;
        GetComponent<AudioSource>().playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        horDir = Input.GetAxisRaw("Horizontal");
        verDir = Input.GetAxisRaw("Vertical");
        Aiming();
        StartCoroutine(Shooting());
        Moving();
        EndGame();
        StartCoroutine(Nuke());
        StartCoroutine(Blank());
        StartCoroutine(Force());
    }
    IEnumerator Nuke()
    {
        if (Input.GetKeyDown("space"))
        {
            if(Time.time > nukeStart + nukeCD)
            {
                GetComponent<AudioSource>().PlayOneShot(nukeActivation);
                if (nukeCD > 5)
                {
                    nukeCD--;
                }
                
                nuked = true;
                yield return 0;
                nuked = false;
                nukeStart = Time.time;
            }
        }
    }
    IEnumerator Blank()
    {
        if (Input.GetKeyDown("e"))


        {
            if(Time.time > blankStart + blankCD)
            {
                blankController.GetComponent<BlankController>().animator.SetTrigger("Activate");
                GetComponent<AudioSource>().PlayOneShot(blankActivation);
                blanked = true;
                
                yield return 0;
                blanked = false;
                blankStart = Time.time;
            }
        }
    }
    IEnumerator Force()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (Time.time > forceStart + forceCD)
            {
                GetComponent<AudioSource>().PlayOneShot(deflecting);
                forced = true;
                forceStart = Time.time;
                yield return 0;
                forced = false;
            }
        }
    }
    void Aiming()
    {
        direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }
    IEnumerator Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Shoot");
           RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, firePoint.up);
            GetComponent<AudioSource>().PlayOneShot(shooting);
            if (hitInfo)
            {
                lr.SetPosition(0, firePoint.position);
                lr.SetPosition(1, hitInfo.point);


               enemyA enemyA = hitInfo.transform.GetComponent<enemyA>();
                if( enemyA != null)
                {
                    enemyA.DestroySelf();
                }
                enemyB enemyB = hitInfo.transform.GetComponent<enemyB>();
                if (enemyB != null)
                {
                    enemyB.DestroySelf();
                }
                bullets bullet = hitInfo.transform.GetComponent<bullets>();
                if (bullet != null)
                {
                    bullet.DestroySelf();
                }
                Miniboss miniboss = hitInfo.transform.GetComponent<Miniboss>();
                if (miniboss != null)
                {
                    miniboss.TakeDamage(1);
                }
                Boss boss = hitInfo.transform.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.TakeDamage(1);
                }

            }
            else
            {
                lr.SetPosition(0, firePoint.position);
                lr.SetPosition(1, firePoint.position + firePoint.up * 100);
            }
            lr.enabled = true;
            yield return 0.02f;
            lr.enabled = false;

        }
    }
    void Moving()
    {

        player.position += (horMove * horDir * speed * Time.deltaTime);
        player.position += (verMove * verDir * speed * Time.deltaTime);
    }

    public void TakeDamage()
    {
        hitPoints-= 2;
    }

    void EndGame()
    {
        if(hitPoints <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        //GetComponent<AudioSource>().PlayOneShot(enemyDeath);
    }
}

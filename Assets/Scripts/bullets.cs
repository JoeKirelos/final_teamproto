using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullets : MonoBehaviour
{
    public GameObject playerPower;
    public Transform player;
    public float speed;
    public GameObject self;
    Vector3 target;
    public bool sent;
    // Start is called before the first frame update
    void Start()
    {
        sent = false;
        playerPower = GameObject.FindWithTag("Player");
        player = GameObject.FindWithTag("Player").transform;
        target = (transform.position - player.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (playerPower.GetComponent<Player>().nuked|| playerPower.GetComponent<Player>().blanked)
        {
            DestroySelf();
        }
        if (playerPower.GetComponent<Player>().forced)
        {
            sent = true;
        }
    }

    public void Move()
    {
        if (sent)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed*Time.deltaTime);
        }
        else
        {
        transform.position -= target * speed * Time.deltaTime;
        }
    }
    public void DestroySelf()
    {
        Destroy(self);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            Destroy(self);
        }
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();
            Destroy(self);
        }
        if (sent)
        {
            if (collision.CompareTag("EnemyA"))
            {
                collision.GetComponent<enemyA>().DestroySelf();
            }
            if (collision.CompareTag("EnemyB"))
            {
                collision.GetComponent<enemyB>().DestroySelf();
            }
            if (collision.CompareTag("Boss"))
            {
                collision.GetComponent<Boss>().TakeDamage(1);
            }
            if (collision.CompareTag("MiniBoss"))
            {
                collision.GetComponent<Miniboss>().TakeDamage(1);
            }
        }
    }
}

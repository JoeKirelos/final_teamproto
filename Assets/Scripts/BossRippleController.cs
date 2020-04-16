using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRippleController : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().TakeDamage();
        }
    }
}

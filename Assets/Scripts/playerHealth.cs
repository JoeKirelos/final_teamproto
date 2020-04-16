using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{

    public Text playerHP;
    [SerializeField] private HealthBar healthBar;

    void Start()
    {
        healthBar.setSize(1f);
    }

    // Update is called once per frame
    void Update()
    {
        playerHP.text = "Sanity:";

        
        if (Player.hitPoints >= 16)
        {
            playerHP.color = new Color(3.0f / 255.0f, 252.0f / 255.0f, 111.0f / 255.0f);
        } else if (Player.hitPoints >= 8 && Player.hitPoints <= 15)
        {
            playerHP.color = new Color(247.0f / 255.0f, 235.0f / 255.0f, 0.0f / 255.0f);
        } else if (Player.hitPoints < 7)
        {
            playerHP.color = new Color(230.0f / 255.0f, 16.0f / 255.0f, 66.0f / 255.0f);
        }

        if (Player.hitPoints > 16)
        {
            healthBar.setSize(1f);
        }
        else if (Player.hitPoints <= 16 && Player.hitPoints > 12)
        {
            healthBar.setSize(0.8f);
        }
        else if (Player.hitPoints <= 12 && Player.hitPoints > 8)
        {
            healthBar.setSize(0.6f);
        }
        else if (Player.hitPoints <= 8 && Player.hitPoints > 4)
        {
            healthBar.setSize(0.4f);
        }
        else if (Player.hitPoints <= 4 && Player.hitPoints > 0)
        {
            healthBar.setSize(0.2f);
        }
        else if (Player.hitPoints == 0)
        {
            healthBar.setSize(0f);
        }
    }
}

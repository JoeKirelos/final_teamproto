using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemiesKilled : MonoBehaviour
{

    public Text Kills;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float killValue = Player.enemiesKilled;
        Kills.text = killValue.ToString();
        
        if (Player.enemiesKilled <= 5)
        {
            Kills.fontSize = 32;
        } else if (Player.enemiesKilled > 5 && Player.enemiesKilled <= 10)
        {
            Kills.fontSize = 34;
        } else if (Player.enemiesKilled > 10 && Player.enemiesKilled <= 15)
        {
            Kills.fontSize = 36;
        } else if (Player.enemiesKilled > 15 && Player.enemiesKilled <= 20)
        {
            Kills.fontSize = 38;
        } else if (Player.enemiesKilled > 20)
        {
            Kills.fontSize = 40;
        }

        //if (Player.enemiesKilled == 25 || Player.enemiesKilled == 50 || Player.enemiesKilled == 75 || Player.enemiesKilled == 100)
        //{
           // Player.hitPoints += 4;
            //Player.enemiesKilled += 1;
        //}

        if (Player.enemiesKilled % 25 == 0)
        {
            Player.hitPoints += 4;
            Player.enemiesKilled += 1;
        }
    }
}

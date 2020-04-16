using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeflectTimer : MonoBehaviour

{
    public Text Deflect;

    public float deflectCooldown;
    public float deflectStart;
    public float deflectRefresh;
    public float deflectDisplay;

    public GameObject player;
   
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        {
            deflectStart = player.GetComponent<Player>().forceStart;
            deflectCooldown = player.GetComponent<Player>().forceCD;
            deflectRefresh = deflectStart + deflectCooldown;

            if (Time.time < deflectStart + deflectCooldown)
            {
                deflectDisplay = deflectRefresh - Time.time;
            }

            Deflect.text = deflectDisplay.ToString("f0");

            if (deflectDisplay < 0.1)
            {
                Deflect.color = Color.green;
                Deflect.fontSize = 40;
            }
            else
            {
                Deflect.color = Color.white;
                Deflect.fontSize = 32;
            }
        }
    }
}

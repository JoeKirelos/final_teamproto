using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreGameover : MonoBehaviour
{
    public Text EnemiesKilled;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float scoreValue = Player.enemiesKilled;
        string texters = scoreValue.ToString();



        EnemiesKilled.text = "[You held off " + texters + " blobs before being cuddled to the ground.]";
    }
}

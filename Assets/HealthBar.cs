using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    
    void Start()
    {
        bar = transform.Find("Bar");
    }

    // Update is called once per frame
    public void setSize(float sizeNormalized)
    {
        bar.localScale = new Vector3(sizeNormalized, 1f);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    float timer = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 2f)
        {
            timer = 0f;
            transform.localScale = new Vector3(-1f,1f, 1f);
        }
        else if(timer >= 1)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

    }
}

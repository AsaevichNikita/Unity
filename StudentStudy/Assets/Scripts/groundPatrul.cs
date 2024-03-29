using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class groundPatrul : MonoBehaviour
{
    public float speed = 1.5f;
    public bool moveLeft = true;
    public Transform groundDetect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D grounInfo = Physics2D.Raycast(groundDetect.position, Vector2.down, 1f);

        if(grounInfo.collider == false)
        {
            if (moveLeft)
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                moveLeft = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveLeft = true;
            }

        }
    }
}

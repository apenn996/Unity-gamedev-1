using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMMovement : MonoBehaviour
{
    private bool updown = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed=6;
        if(updown == true )
        transform.Translate(Vector3.down * Time.deltaTime * speed);
        if(updown == false  )
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        if (transform.position.y < 1 && transform.position.y> 0)
            updown = false;
        if (transform.position.y < 10 && transform.position.y > 9)
            updown = true;
    }
}

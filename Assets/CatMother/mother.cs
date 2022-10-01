using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mother : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

        // Update is called once per frame
    void Update()
    {
        var speed = 10;
        var velocity = new Vector3(0, 0, 0);

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            velocity.x -= speed;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            velocity.x += speed;
        }

        var newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;

        transform.position = newPosition;
    }

}

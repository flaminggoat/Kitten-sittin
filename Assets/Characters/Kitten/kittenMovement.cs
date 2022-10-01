using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kittenMovement : MonoBehaviour
{
    public float speed;

    private Vector3 forward = new Vector3(0,1,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var movementVector = transform.rotation * (speed * Time.deltaTime * forward);

        transform.position += movementVector;
    }
}

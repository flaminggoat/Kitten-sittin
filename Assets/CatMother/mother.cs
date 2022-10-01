using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mother : MonoBehaviour
{
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public static float to_multiple(float value, float multipleOf) 
    {
        return (float) Mathf.Round(value / multipleOf) * multipleOf;
    }

    // Update is called once per frame
    void Update()
    {
        var speed = 2;
        var velocity = new Vector3(0, 0, 0);

        velocity.x += Input.GetAxis("Horizontal") * speed;
        velocity.y += Input.GetAxis("Vertical") * speed;

        if (Mathf.Abs(velocity.magnitude) > 0) {
            float angle = Mathf.Atan2(-velocity.x, velocity.y) * Mathf.Rad2Deg;
            // Clamp angle to steps
            angle = to_multiple(angle, 45/2);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        _animator.speed = velocity.magnitude;

        var newPosition = transform.position;
        newPosition += velocity * Time.deltaTime;

        transform.position = newPosition;
    }

}

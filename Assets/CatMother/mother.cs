using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mother : MonoBehaviour
{   
    bool _is_leaping = false;
    float _leap_time;
    Vector3 _leap_vec;
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

    void do_leap(float delta) {
        var leap_duration = 0.6f;
        var leap_speed = 2f;
        _leap_time += delta;
        if (_leap_time > leap_duration / 2) {
            transform.localScale -= Vector3.one * delta;
        } else {
            transform.localScale += Vector3.one * delta;
        }

        transform.position += _leap_vec * leap_speed * Time.deltaTime;
        
        if (_leap_time > leap_duration) {
            _is_leaping = false;
            GetComponent<Drown>().isLeaping = false;
            _animator.SetBool("leaping", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        var speed = 2;
        var velocity = new Vector3(0, 0, 0);

        velocity.x += Input.GetAxis("Horizontal") * speed;
        velocity.y += Input.GetAxis("Vertical") * speed;

        if (Input.GetButtonDown("Leap")) {
            _is_leaping = true;
            _leap_time = 0f;
            _leap_vec = velocity;
            GetComponent<Drown>().isLeaping = true;
            _animator.SetBool("leaping", true);
        }

        if (_is_leaping) {
            do_leap(Time.deltaTime);
        } else {
             if (Mathf.Abs(velocity.magnitude) > 0) {
                float angle = Mathf.Atan2(-velocity.x, velocity.y) * Mathf.Rad2Deg;
                // Clamp angle to steps
                angle = to_multiple(angle, 45);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            _animator.speed = velocity.magnitude;
            transform.position += velocity * Time.deltaTime;
        }
        
    }

}

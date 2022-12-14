using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mother : MonoBehaviour
{   
    bool _is_leaping = false;
    float _leap_time;
    Vector3 _leap_vec;
    Animator[] _animators;

    public float leapDuration = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        _animators = GetComponentsInChildren<Animator>();
    }

    public static float to_multiple(float value, float multipleOf) 
    {
        return (float) Mathf.Round(value / multipleOf) * multipleOf;
    }

    void do_leap(float delta) {
        var leap_speed = 2f;
        _leap_time += delta;
        if (_leap_time > leapDuration / 2) {
            transform.localScale -= Vector3.one * delta;
        } else {
            transform.localScale += Vector3.one * delta;
        }

        transform.position += _leap_vec * leap_speed * Time.deltaTime;
        
        if (_leap_time > leapDuration) {
            transform.localScale = Vector3.one * 1f;
            _is_leaping = false;
            GetComponent<Drown>().isLeaping = false;

            foreach(Animator a in _animators) {
                a.SetBool("leaping", false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var speed = 2;
        var velocity = new Vector3(0, 0, 0);

        velocity.x += Input.GetAxis("Horizontal") * speed;
        velocity.y += Input.GetAxis("Vertical") * speed;

        if (Input.GetButtonDown("Leap") && !_is_leaping) {
            _is_leaping = true;
            _leap_time = 0f;
            _leap_vec = velocity;
            GetComponent<Drown>().isLeaping = true;

            foreach(Animator a in _animators) {
                a.SetBool("leaping", true);
            }
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
            foreach(Animator a in _animators) {
                a.SetFloat("walk_speed", velocity.magnitude);
            }
            transform.position += velocity * Time.deltaTime;
        }
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{
    float _scale = 1;
    bool _isDrowning = false;

    public bool isLeaping = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isDrowning)
        {
            if (_scale <= 0) {
                Destroy(gameObject);
            }

            _scale -= Time.deltaTime * 0.3f;
            
            transform.localScale = transform.localScale * _scale;
        }
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.GetComponent<IsWater>() != null && !isLeaping && !_isDrowning)
        {
            // Prevent any movement
            var m = GetComponent<mother>();
            if (m != null) {
                Destroy(GetComponent<mother>());
            }

            // Kill the kitten
            var k = GetComponent<Kitten>();
            if (k != null) {
                k.manager.deadKittens++;
            }

            var a = GetComponent<AudioSource>();
            if (m != null) {
                a.Play();
            }
            _isDrowning = true;
        }
    }

}

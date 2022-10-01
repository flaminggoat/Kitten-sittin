using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drown : MonoBehaviour
{
    float _scale = 1;
    bool _is_drowning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_is_drowning)
        {
            if (_scale <= 0) {
                Destroy(gameObject);
            }

            _scale -= Time.deltaTime * 0.3f;
            
            transform.localScale = transform.localScale * _scale;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (true)
        {
            // Prevent any movement
            Destroy(GetComponent<mother>());

            _is_drowning = true;
        }
    }

}

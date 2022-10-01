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

    private void OnTriggerStay2D(Collider2D other) {
        if (other.GetComponent<IsWater>() != null && !isLeaping && !_isDrowning)
        {
            // Prevent any movement
            Destroy(GetComponent<mother>());
            GetComponent<AudioSource>().Play();
            _isDrowning = true;
        }
    }

}

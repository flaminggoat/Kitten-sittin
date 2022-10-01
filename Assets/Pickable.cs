using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    [Range(0.0f, 360.0f)]
    public float carryingAngle = 0;

    [HideInInspector]
    public bool isCarried = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

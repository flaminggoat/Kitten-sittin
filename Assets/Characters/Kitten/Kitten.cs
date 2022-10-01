using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kitten : MonoBehaviour
{
    [HideInInspector]
    public bool isBorn = false;
    public float speed;

    [Range(0.0f, 360.0f)]
    public float speedDirectionDegrees;

    [HideInInspector]
    public KittenManager manager;

    private Vector3 _forward = new Vector3(0,1,0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCarried()) {
            Move();
            ClampRotation();
        }
    }

    // Returns true if the kitten is being carried
    bool isCarried() {
        var p = GetComponent<Pickable>();

        if (p == null) {
            return false;
        }

        return p.isCarried;
    }

    // Moves the kitten if it is not being carried
    void Move() {
        var movementVector = Quaternion.AngleAxis(speedDirectionDegrees, Vector3.forward) * (speed * Time.deltaTime * _forward);
        transform.position += movementVector;
    }

    // Clamps the rotation of the kitten to ensure it doesn't display in strange angles
    void ClampRotation() {
        var angle = to_multiple(speedDirectionDegrees, 45);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static float to_multiple(float value, float multipleOf) 
    {
        return (float) Mathf.Round(value / multipleOf) * multipleOf;
    }
}

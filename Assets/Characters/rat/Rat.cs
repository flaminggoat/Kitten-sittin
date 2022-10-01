using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [HideInInspector]
    public bool isBorn = false;
    public float speed;

    [Range(0.0f, 360.0f)]
    public float speedDirectionDegrees;

    public float changeDirTime = 2f;

    private Vector3 _forward = new Vector3(0,1,0);
    private float _changeDirTimer;

    // Start is called before the first frame update
    void Start()
    {
        _changeDirTimer = changeDirTime;
    }

    // Update is called once per frame
    void Update()
    {
        _changeDirTimer -= Time.deltaTime;

        if (!isCarried()) {
            Move();
            ClampRotation();
        }

        if (_changeDirTimer < 0) {
            speedDirectionDegrees += Random.Range(-40,40);
            _changeDirTimer = changeDirTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<IsWater>() != null)
        {
            // Turn around
            speedDirectionDegrees += 180;
        }
    }

    // Returns true if the rat is being carried
    bool isCarried() {
        var p = GetComponent<Pickable>();

        if (p == null) {
            return false;
        }

        return p.isCarried;
    }

    // Moves the rat if it is not being carried
    void Move() {
        var movementVector = Quaternion.AngleAxis(speedDirectionDegrees, Vector3.forward) * (speed * Time.deltaTime * _forward);
        transform.position += movementVector;
    }

    // Clamps the rotation of the rat to ensure it doesn't display in strange angles
    void ClampRotation() {
        var angle = to_multiple(speedDirectionDegrees, 45);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static float to_multiple(float value, float multipleOf) 
    {
        return (float) Mathf.Round(value / multipleOf) * multipleOf;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public GameObject carrying;

    private GameObject _inRangeToCarry;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pick")) {
            PickOrDrop();
        }
    }

    private void PickOrDrop() {
        if (_inRangeToCarry != null) {
            carrying = _inRangeToCarry;
            _inRangeToCarry = null;
            return;
        }

        if (carrying != null) {
            AddToInRageIfTouching(carrying);
            carrying = null;
            return;
        }
    }

    private void AddToInRageIfTouching(GameObject item) {
        var c1 = GetComponent<Collider2D>();
        var c2 = item.GetComponent<Collider2D>();

        if (c1.IsTouching(c2)) {
            _inRangeToCarry = item;
        }
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        _inRangeToCarry = other.gameObject;
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_inRangeToCarry == other.gameObject) {
            _inRangeToCarry = null;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picker : MonoBehaviour
{
    public GameObject pickingPosition;

    [HideInInspector]
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

        Move();
    }

    // Move object being carried
    private void Move() {
        if (carrying != null) {
            var p = carrying.gameObject.GetComponent<Pickable>();

            carrying.transform.position = pickingPosition.transform.position;
            carrying.transform.rotation = Quaternion.AngleAxis(p.carryingAngle, Vector3.forward) * transform.rotation;
        }
    }

    // If carrying an object, it drops it. If not but in range of one, it picks it.
    private void PickOrDrop() {
        if (IsCarrying()) {
            AddToInRangeIfTouching(carrying);
            Drop();
            return;
        }

        if (IsInRangeToCarry()) {
            Pick(_inRangeToCarry);
            _inRangeToCarry = null;
            return;
        }
    }
    
    // Picks the item that is given
    private void Pick(GameObject item) {
        if (carrying == item) {
            return;
        }

        var p = item.gameObject.GetComponent<Pickable>();
        
        if (IsCarrying()) {
            throw new Exception("Cannot carry more than one thing at once");
        }

        if (p == null) {
            throw new Exception("Cannot carry an unpickable object!");
        }

        carrying = item;
        p.isCarried = true;
    }

    // Drops the object that is being carried
    private void Drop() {
        if (!IsCarrying()) {
            return;
        }

        var p = carrying.gameObject.GetComponent<Pickable>();

        carrying = null;
        p.isCarried = false;
    }

    // Adds the object to "inRange" if it is touching
    private void AddToInRangeIfTouching(GameObject item) {
        var c1 = GetComponent<Collider2D>();
        var c2 = item.GetComponent<Collider2D>();

        if (c1.IsTouching(c2)) {
            _inRangeToCarry = item;
        }
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Pickable>() != null) {
            _inRangeToCarry = other.gameObject;
        }
    }

    //Upon collision with another GameObject, this GameObject will reverse direction
    private void OnTriggerExit2D(Collider2D other)
    {
        if (_inRangeToCarry == other.gameObject) {
            _inRangeToCarry = null;
        }
    }

    // Returns true if it is carrying something
    bool IsCarrying() {
        return carrying != null;
    }

    // Returns true if it is in range to an object on the ground
    bool IsInRangeToCarry() {
        return _inRangeToCarry != null;
    }
}

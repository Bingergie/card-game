using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    // events for mousedown, mouseup, mousemove
    public EventHandler<Vector3> OnMouseDown;
    public EventHandler<Vector3> OnMouseUp;
    public EventHandler<Vector3> OnMouseMove;
    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            OnMouseDown?.Invoke(this, Input.mousePosition);
        }
        
        if (Input.GetMouseButtonUp(0)) {
            OnMouseUp?.Invoke(this, Input.mousePosition);
        }
        
        if (Input.GetMouseButton(0)) {
            OnMouseMove?.Invoke(this, Input.mousePosition);
        }
    }
}
using UnityEngine;

public class GolfBallController : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Attributes")]
    [SerializeField] private float maxPower = 10.0f;
    [SerializeField] private float power = 2.0f;
    

    private bool isDragging;
    private bool inHole;

    private void Update(){
        PlayerInput();
    }

    private void PlayerInput(){
        Vector2 inputPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(transform.position, inputPosition);

        // Left mouse button
        if (Input.GetMouseButtonDown(0) && distance <= 0.5f) DragStart();
        if (Input.GetMouseButton(0) && isDragging) DragChange();
        if (Input.GetMouseButtonUp(0) && isDragging) DragRelease(inputPosition);
    }

    private void DragStart(){
        isDragging = true;
    }

    private void DragChange(){

    }

    private void DragRelease(Vector2 inputPosition){
        float distance = Vector2.Distance((Vector2)transform.position, inputPosition);
        isDragging = false;

        // Didn't drag far enough
        if (distance < 1.0f) return;
        
        Vector2 direction = (Vector2)transform.position - inputPosition;
        rb.linearVelocity = Vector2.ClampMagnitude(direction * power, maxPower);
    }
}
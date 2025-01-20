using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpaceshipController2D : MonoBehaviour
{
    public float rotationSpeed = 50.0f; // How fast the ship rotates
    public float thrustForce = 10.0f; // The force applied to move the ship forward
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearDamping = 0; // No drag to simulate space
    }

    void FixedUpdate()
    {
        // Rotation
        float rotationInput = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
        if (rotationInput != 0)
        {
            rb.MoveRotation(rb.rotation - rotationInput);
        }

        // Forward Thrust
        float thrustInput = Input.GetAxis("Vertical");
        Debug.Log(thrustInput);
        if (thrustInput > 0)
        {
            Vector2 force = thrustForce * thrustInput * Time.deltaTime * transform.up;
            rb.AddForce(force);
        }
        Debug.Log(rb.linearVelocity);
    }
}

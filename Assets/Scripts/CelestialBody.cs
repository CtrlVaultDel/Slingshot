using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class CelestialBody : MonoBehaviour
{
    public float celestialMass = 10000.0f;
    public float celestialRadius = 100.0f;
    public float gravityWellRadius = 150.0f; // Radius of gravitational influence
    public Vector3 velocity = Vector3.zero;
    public float gravitationalConstant = 0.1f; // Gravitational constant

    public virtual void Initialize()
    {
        // Initialization logic common to all celestial bodies
    }

    void FixedUpdate()
    {
        // Find all objects with Rigidbody2D in a certain radius
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, gravityWellRadius);
        foreach (Collider2D obj in objectsInRange)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null && rb != GetComponent<Rigidbody2D>())
            {
                ApplyGravity(rb);
            }
        }
    }

    private void ApplyGravity(Rigidbody2D rb)
    {
        // Calculate direction to the star
        Vector2 direction = (Vector2)transform.position - rb.position;
        float distance = direction.magnitude;
        direction.Normalize();

        // Apply gravitational force based on Newton's law of universal gravitation
        float forceMagnitude = gravitationalConstant * celestialMass * rb.mass / Mathf.Pow(distance, 2);
        Vector2 force = direction * forceMagnitude;

        rb.AddForce(force);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a sphere to visualize the area of effect of the star's gravity
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravityWellRadius);
    }
}

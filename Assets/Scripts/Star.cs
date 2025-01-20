using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Star : MonoBehaviour
{
    public float mass = 10000.0f; // Mass of the star
    public float gravitationalConstant = 0.1f; // Gravitational constant
    public float gravitationalRadius = 50.0f; // Radius of gravitational influence

    void FixedUpdate()
    {
        // Find all objects with Rigidbody2D in a certain radius
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, gravitationalRadius);
        foreach (Collider2D obj in objectsInRange)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null && rb != GetComponent<Rigidbody2D>())
            {
                ApplyGravity(rb);
            }
        }
    }

    void ApplyGravity(Rigidbody2D rb)
    {
        // Calculate direction to the star
        Vector2 direction = (Vector2)transform.position - rb.position;
        float distance = direction.magnitude;
        direction.Normalize();

        // Apply gravitational force based on Newton's law of universal gravitation
        float forceMagnitude = gravitationalConstant * mass * rb.mass / Mathf.Pow(distance, 2);
        Vector2 force = direction * forceMagnitude;

        rb.AddForce(force);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a sphere to visualize the area of effect of the star's gravity
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravitationalRadius);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<TrajectoryVisualizer>().AddStar(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<TrajectoryVisualizer>().RemoveStar(this);
        }
    }
}

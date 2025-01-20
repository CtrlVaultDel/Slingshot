using UnityEngine;
using System.Collections.Generic;

public class TrajectoryVisualizer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int steps = 100; // Number of steps to predict
    public float timeStep = 0.1f; // Time step for each prediction step

    private List<Star> activeStars = new List<Star>();
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        DrawTrajectory();
    }

    void DrawTrajectory()
    {
        Vector2[] positions = new Vector2[steps];
        Vector2 currentPosition = rb.position;
        Vector2 currentVelocity = rb.linearVelocity;

        for (int i = 0; i < steps; i++)
        {
            // Calculate gravitational forces from each active star
            Vector2 gravityForce = Vector2.zero;
            foreach (Star star in activeStars)
            {
                Vector2 direction = (Vector2)star.transform.position - currentPosition;
                float distance = direction.magnitude;
                direction.Normalize();
                float forceMagnitude = star.gravitationalConstant * star.mass / Mathf.Pow(distance, 2);
                gravityForce += direction * forceMagnitude;
            }

            // Apply velocity and gravity to calculate the next position
            currentVelocity += gravityForce * timeStep;
            currentPosition += currentVelocity * timeStep;
            positions[i] = currentPosition;

            // Check if the trajectory is influenced by gravity
            Debug.Log("Gravity Force: " + gravityForce);
            Debug.Log("Velocity: " + currentVelocity);
            Debug.Log("Position: " + currentPosition);
        }

        lineRenderer.positionCount = steps;
        for (int i = 0; i < steps; i++)
        {
            lineRenderer.SetPosition(i, positions[i]);
        }
    }

    public void AddStar(Star star)
    {
        if (!activeStars.Contains(star))
        {
            activeStars.Add(star);
        }
    }

    public void RemoveStar(Star star)
    {
        if (activeStars.Contains(star))
        {
            activeStars.Remove(star);
        }
    }
}

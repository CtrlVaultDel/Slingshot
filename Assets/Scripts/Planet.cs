using UnityEngine;

public class Planet : CelestialBody
{
    public bool hasAtmosphere;
    public float atmosphereThickness;
    public float dragCoefficient;

    public override void Initialize()
    {
        base.Initialize();
        // Planet-specific initialization logic
    }

    public void ApplyAtmosphericDrag(Rigidbody spaceshipRb)
    {
        if (hasAtmosphere)
        {
            float distanceToPlanet = Vector3.Distance(transform.position, spaceshipRb.position) - celestialRadius;
            if (distanceToPlanet < atmosphereThickness)
            {
                float drag = dragCoefficient * (1 - distanceToPlanet / atmosphereThickness);
                spaceshipRb.AddForce(-spaceshipRb.linearVelocity * drag);
            }
        }
    }
}

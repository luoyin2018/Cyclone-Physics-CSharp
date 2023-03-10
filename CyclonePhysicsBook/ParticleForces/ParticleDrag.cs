using System.Numerics;

namespace Cyclone.ParticleForces
{
    public class ParticleDrag : IParticleForceGenerator
    {
        public float K1 { get; }
        public float K2 { get; }
        public ParticleDrag(float k1, float k2)
        {
            K1 = k1;
            K2 = k2;
        }

        // F = - vel_dir *(k1 * speed + k2 * speed^2); 
        public void UpdateForce(Particle particle, float duration)
        {
            if (!particle.HasFiniteMass) return;

            Vector3 vel = particle.Velocity;
            float dragCoeff = vel.Length();
            dragCoeff = K1 * dragCoeff + K2 * dragCoeff * dragCoeff;

            Vector3 force = Vector3.Normalize(vel) * -dragCoeff;
            particle.AddForce(force);
        }
    }
}

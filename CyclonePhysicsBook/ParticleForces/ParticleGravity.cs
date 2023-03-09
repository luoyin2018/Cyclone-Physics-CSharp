using System.Numerics;

namespace Cyclone.ParticleForces
{
    public class ParticleGravity : IParticleForceGenerator
    {
        public Vector3 Gravity { get; }

        public ParticleGravity(Vector3 gravity)
        {
            Gravity = gravity;
        }

        public void UpdateForce(Particle particle, float duration)
        {
            if (!particle.HasFiniteMass) return;

            particle.AddForce(Gravity * particle.Mass);
        }
    }
}

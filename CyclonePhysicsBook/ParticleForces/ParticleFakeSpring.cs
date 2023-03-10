using System;
using System.Numerics;

namespace Cyclone.ParticleForces
{
    public class ParticleFakeSpring : IParticleForceGenerator
    {
        public Vector3 Anchor { get; }
        public float SpringConstant { get; }
        public float Damping { get; }

        public ParticleFakeSpring(Vector3 anchor, float springConstant, float damping)
        {
            Anchor = anchor;
            SpringConstant = springConstant;
            Damping = damping;
        }

        public void UpdateForce(Particle particle, float duration)
        {
            if (!particle.HasFiniteMass) return;

            Vector3 p0 = particle.Position - Anchor;

            double gamma = Math.Sqrt(4 * SpringConstant - Damping * Damping) / 2;
            float fgamma = (float)gamma;

            Vector3 c =  p0 * (Damping / fgamma / 2) + particle.Velocity / fgamma;

            Vector3 target = p0 * (float)Math.Cos(gamma * duration)
                + c * (float)Math.Sin(gamma * duration);
            target *= (float)Math.Exp(duration * Damping / 2);

            Vector3 accel = (target - p0) / (duration * duration) - particle.Velocity / duration;
            particle.AddForce(accel * particle.Mass);
        }
    }
}

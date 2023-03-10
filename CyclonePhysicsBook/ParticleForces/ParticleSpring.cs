using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace Cyclone.ParticleForces
{
    public class ParticleSpring : IParticleForceGenerator
    {
        public Particle Other { get; }
        public float SpringConstant { get; }
        public float RestLength { get; }

        public ParticleSpring(Particle other, float springConstant, float restLen)
        {
            Other = other;
            SpringConstant = springConstant;
            RestLength = restLen;
        }

        public void UpdateForce(Particle particle, float duration)
        {
            if (!particle.HasFiniteMass) return;

            Vector3 toOther = Other.Position - particle.Position;
            float magnitude = (toOther.Length() - RestLength) * SpringConstant;

            Vector3 force = Vector3.Normalize(toOther) * magnitude;
            particle.AddForce(force);
        }
    }
}

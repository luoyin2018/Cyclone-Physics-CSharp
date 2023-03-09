using System.Numerics;

namespace Cyclone.ParticleForces
{
    public class ParticleBungee
    {
        public Particle Other { get; }
        public float SpringConstant { get; }
        public float RestLength { get; }

        public ParticleBungee(Particle other, float springConstant, float restLen)
        {
            Other = other;
            SpringConstant = springConstant;
            RestLength = restLen;
        }

        public void UpdateForce(Particle particle, float duration)
        {
            Vector3 toOther = Other.Position - particle.Position;
            float magnitude = toOther.Length();

            if (magnitude < RestLength) return;

            magnitude = (magnitude - RestLength) * SpringConstant;

            Vector3 force = Vector3.Normalize(toOther) * magnitude;
            particle.AddForce(force);
        }
    }
}

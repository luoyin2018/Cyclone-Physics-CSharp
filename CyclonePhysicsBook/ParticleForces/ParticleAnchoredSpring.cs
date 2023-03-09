using System.Numerics;

namespace Cyclone.ParticleForces
{
    public class ParticleAnchoredSpring : IParticleForceGenerator
    {
        public Vector3 Anchor { get; }
        public float SpringConstant { get; }
        public float RestLength { get; }
        public ParticleAnchoredSpring(Vector3 anchor, float springConstant, float restLen)
        {
            Anchor = anchor;
            SpringConstant = springConstant;
            RestLength = restLen;
        }
        public void UpdateForce(Particle particle, float duration)
        {
            Vector3 toAnchor = Anchor - particle.Position;
            float magnitude = (toAnchor.Length() - RestLength) * SpringConstant;

            Vector3 force = Vector3.Normalize(toAnchor) * magnitude;
            particle.AddForce(force);
        }
    }
}

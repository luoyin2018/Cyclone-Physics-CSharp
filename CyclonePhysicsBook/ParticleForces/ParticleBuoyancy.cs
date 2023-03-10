using System.Numerics;

namespace Cyclone.ParticleForces
{
    public class ParticleBuoyancy : IParticleForceGenerator
    {
        public float MaxDepth { get; }   // 产生最大浮力时的最大深度, 可以理解为一个立方体，MaxDepth即为立方体高度的一半
        public float Volumn { get; }
        public float WaterHeight { get; }   // 水平面平行于XY平面
        public float LiquidDensity { get; }

        public ParticleBuoyancy(float maxDepth, float volumn, float waterHeight, float density = 1000)
        {
            MaxDepth = maxDepth;
            Volumn = volumn;
            WaterHeight = waterHeight;
            LiquidDensity = density;
        }

        public void UpdateForce(Particle particle, float duration)
        {
            if (!particle.HasFiniteMass) return;

            float depth = particle.Position.Z - WaterHeight;
            if (depth >= MaxDepth) return;

            Vector3 force = Vector3.UnitZ;
            if (depth <= -MaxDepth)
            {
                var magnitude = LiquidDensity * Volumn;
                particle.AddForce(force * magnitude);
            }
            else
            {
                var magnitude = LiquidDensity * Volumn * (depth + MaxDepth) / 2 * MaxDepth;
                particle.AddForce(force * magnitude);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;

namespace Cyclone
{
    public class ParticleContact
    {
        private static readonly float eps = (float)1e-10;
        public Particle ParticalA { get; }   // main particle, contact normal is described from its perspective
        public Particle ParticalB { get; }
        public float Restituion { get; }
        public Vector3 ContactNormal { get; }
        protected void Resolve(float duration)
        {
            ResolveVelocity(duration);
            ResolveInterpenetration(duration);
        }
        protected float CalculateSeparatingVelocity()
        {
            Vector3 relativeVel = ParticalB is null ?
                ParticalA.Velocity : ParticalA.Velocity - ParticalB.Velocity;
            return Vector3.Dot( relativeVel , ContactNormal);
        }
        private void ResolveVelocity(float duration)
        {
            float totalInverseMass = ParticalB is null ?
                ParticalA.InverseMass : ParticalA.InverseMass + ParticalB.InverseMass;
            if (totalInverseMass < eps) return;

            float sepVel = CalculateSeparatingVelocity();
            if (sepVel > 0) return;

            float newSepVel = -sepVel * Restituion;
            float deltaVel = newSepVel - sepVel;

            float impulse = deltaVel / totalInverseMass;
            Vector3 momentum = ContactNormal * impulse;

            ParticalA.AddImpulse(momentum);
            ParticalB?.AddImpulse(-momentum);
        }

        private void ResolveInterpenetration(float duration)
        {

        }
    }
}

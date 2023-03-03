using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using Cyclone;

namespace Fireworks
{
    public class Firework : Particle
    {
        public Firework(float damping, float age)
            : base(1, damping, Vector3Helper.GravityAccleration)
        {
            Age = age;
        }

        public float Age { get; private set; }
        public int Type { get; set; }

        public bool Update(float duration)
        {
            Integrate(duration);
            Age -= duration;
            return Age < 0; //if true, detonate
        }
    }
    class FireworkRule
    {
        static Random rd = new Random();
        public int type;
        public float minAge;
        public float maxAge;

        public Vector3 minVelocity;
        public Vector3 maxVelocity;

        public float damping;

        public List<Payload> payloads = new List<Payload>();

        public float getAge()
        {
            return minAge + (maxAge - minAge) * (float)rd.NextDouble();
        }

        public Vector3 getVel()
        {
            return minVelocity + (maxVelocity - minVelocity) * (float)rd.NextDouble();
        }

        public Firework Create(Firework parent)
        {
            Firework firework = new Firework(damping, getAge());
            firework.Type = type;

            Vector3 vel = getVel();
            if (parent != null)
            {
                vel += parent.Velocity;
                firework.SetInitState(parent.Position, vel);
            }
            else
            {
                firework.SetInitState(Vector3.Zero, vel);
            }
            firework.ForceAccum = Vector3.Zero;
            return firework;
        }
    }

    struct Payload
    {
        public int type;
        public int count;
    }
}

using System;
using System.Numerics;
using System.Diagnostics;

namespace Cyclone
{
    public class Particle
    {
        private static readonly float eps = (float)1e-10;
        private float inverseMass;
        public float Mass => inverseMass < eps ? float.MaxValue : 1 / inverseMass;
        public float Damping { get; private set; }  // 不超过1

        //状态变量
        //位置和速度根据施加的力积分求得，计算过程中不会在外部直接更改这些值
        public Vector3 Position { get; private set; }
        public Vector3 Velocity { get; private set; } 
        public Vector3 Acceleration { get; private set; }  // 运动中不变，用于施加重力

        //受到的力
        public Vector3 ForceAccum { get; set; }  // 状态变化的根源

        private Particle() { }
        public static Particle FixParticle=> new Particle() { inverseMass = 0 };
        public Particle(float mass, float damping)
        {
            Debug.Assert(mass > eps);
            inverseMass = 1 / mass;
            Damping = damping;
        }

        public void SetState(Vector3 pos, Vector3 vel, Vector3 acc)
        {
            Position = pos;
            Velocity = vel;
            Acceleration = acc;
        }
        
        public void Integrate(float duration)
        {
            if (inverseMass <= eps) return;
            Debug.Assert(duration > 0);

            // 根据受到的力更新内部状态
            Position += Velocity * duration;

            Vector3 resultingAcc = Acceleration;
            resultingAcc += ForceAccum * inverseMass;

            Velocity += resultingAcc * duration;
            Velocity *= (float)Math.Pow(Damping, duration);   // 施加阻力，能量耗散，改善计算稳定性

            // 清除力
            ForceAccum = Vector3.Zero;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Windows.Media.Media3D;
using System.Windows.Input;
using Cyclone;
using Cyclone.WPF;

namespace Fireworks
{
    public class FireworksApp:AppBase
    {
        private ParticleWorld World = new ParticleWorld();
        private ParticleWorldRender _worldRender;

        FireworkRule[] Rules;

        public override void Init(Model3DGroup holder)
        {
            InitRules();
            _worldRender = new ParticleWorldRender(World, holder);
        }

        public override void Update(double time)
        {
            List<Firework> notAlive = new List<Firework>();

            foreach(var particle in World.Particles)
            {
                var firework = particle as Firework;
                if(firework!=null)
                {
                    if(firework.Update((float)time))
                    {
                        notAlive.Add(firework);
                    }
                }
            }
            foreach (var p in notAlive)
            {
                var rule = Rules[p.Type-1];

                foreach (var payload in rule.payloads)
                {
                    create(payload.type, payload.count, p);
                }
                World.RemoveParticle(p);
            }
        }

        public override void OnKeyUp(Key key)
        {
            switch (key)
            {
                case Key.D1:
                    create(1, 1, null);
                    break;
                case Key.D2:
                    create(2, 1, null);
                    break;
                case Key.D3:
                    create(3, 1, null);
                    break;
                case Key.D4:
                    create(4, 1, null);
                    break;
                case Key.D5:
                    create(5, 1, null);
                    break;
            }
        }

        void create(int type, int count, Firework parent)
        {
            for(int i=0; i<count; i++)
            {
                create(type, parent);
            }
        }

        void create(int type, Firework parent)
        {
            FireworkRule rule = Rules[type - 1];
            var firework = rule.Create(parent);
            World.AddParticle(firework);
        }

        private void InitRules()
        {
            Rules = new FireworkRule[5];

            Rules[0] = new FireworkRule
            {
                type = 1,
                minAge = 0.5f,
                maxAge = 1.4f,
                minVelocity = new Vector3(-5, 25, -5),
                maxVelocity = new Vector3(5, 28, 5),
                damping = 0.1f,
                payloads = new List<Payload>
                {
                    new Payload{type=3, count=5},
                    new Payload{type=4, count=5},
                }
            };

            Rules[1] = new FireworkRule
            {
                type = 2,
                minAge = 0.5f,
                maxAge = 1.0f,
                minVelocity = new Vector3(-5, 10, -5),
                maxVelocity = new Vector3(5, 20, 5),
                damping = 0.8f,
                payloads = new List<Payload>
                {
                    new Payload{type=4, count=2},
                }
            };

            Rules[2] = new FireworkRule
            {
                type = 3,
                minAge = 0.5f,
                maxAge = 1.5f,
                minVelocity = new Vector3(-5, -5, -5),
                maxVelocity = new Vector3(5, 5, 5),
                damping = 0.1f,
            };

            Rules[3] = new FireworkRule
            {
                type = 4,
                minAge = 0.5f,
                maxAge = 1.5f,
                minVelocity = new Vector3(-20, 5, -5),
                maxVelocity = new Vector3(20, 5, 5),
                damping = 0.2f,
            };

            Rules[4] = new FireworkRule
            {
                type = 5,
                minAge = 0.5f,
                maxAge = 1.0f,
                minVelocity = new Vector3(-20, 2, -5),
                maxVelocity = new Vector3(20, 18, 5),
                damping = 0.01f,
                payloads = new List<Payload>
                {
                    new Payload{type=3, count=5}
                }
            };
        }
    }
}

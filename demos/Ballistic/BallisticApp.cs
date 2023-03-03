using System.Numerics;
using System.Windows.Media.Media3D;
using System.Windows.Input;
using Cyclone;
using Cyclone.WPF;
using System.Collections.Generic;

namespace Ballistic
{
    enum BulletType
    {
        PISTOL,
        ARTILLERY,
        FIREBALL,
        LASR
    }
    public class BallisticApp:AppBase
    {
        private readonly ParticleWorld World = new ParticleWorld();
        private ParticleWorldRender _worldRender;
        private BulletType currentType;

        public override void Init(Model3DGroup holder)
        {
            _worldRender = new ParticleWorldRender(World, holder);
        }

        public override void Update(double time)
        {
            var notalive = new List<Particle>();
            foreach (var p in World.Particles)
            {
                p.Integrate(time);
                if (p.Position.X > 200 || p.Position.Z < 0 || p.Position.Z > 20)
                {
                    notalive.Add(p);
                }
            }

            foreach (var p in notalive)
            {
                World.RemoveParticle(p);
            }
        }
        public override void OnMouseUp()
        {
            Particle p = null;
            Vector3 position = new Vector3(0, 0, 1.5f);
            switch (currentType)
            {
                case BulletType.PISTOL:
                    p = new Particle(2.0f, 0.99f, new Vector3(0, 0, -1));
                    p.SetInitState(position, new Vector3(35, 0, 0));
                    break;
                case BulletType.ARTILLERY:
                    p = new Particle(200, 0.99f, new Vector3(0, 0, -20));
                    p.SetInitState(position, new Vector3(40, 0, 30));
                    break;
                case BulletType.FIREBALL:
                    p = new Particle(1, 0.9f, new Vector3(0, 0, 0.6f));
                    p.SetInitState(position, new Vector3(10, 0, 0));
                    break;
                case BulletType.LASR:
                    p = new Particle(0.1f, 0.99f, new Vector3(0, 0, 0));
                    p.SetInitState(position, new Vector3(100, 0, 0));
                    break;
            }
            if(p != null)
            {
                World.AddParticle(p);
            }
        }

        public override void OnKeyUp(Key key)
        {
            switch(key)
            {
                case Key.D1:
                    currentType = BulletType.PISTOL;
                    break;
                case Key.D2:
                    currentType = BulletType.ARTILLERY;
                    break;
                case Key.D3:
                    currentType = BulletType.FIREBALL;
                    break;
                case Key.D4:
                    currentType = BulletType.LASR;
                    break;
            }
        }
    }
}

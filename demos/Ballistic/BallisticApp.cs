using System;
using System.Collections.Generic;
using System.Text;
using HelixToolkit.Wpf;
using System.Numerics;
using System.Windows.Media.Media3D;
using System.Windows.Input;
using Cyclone;

namespace Ballistic
{
    enum BulletType
    {
        PISTOL,
        ARTILLERY,
        FIREBALL,
        LASR
    }
    public class BallisticApp
    {
        public BallisticWorld World { get; } = new BallisticWorld();

        private WorldRender _worldRender;

        private BulletType currentType;

        public void Init(Model3DGroup holder)
        {
            _worldRender = new WorldRender(World, holder);
        }

        public void Update(double time)
        {
            World.Update(time);
        }

        public void OnMouseUp()
        {

            Particle p = null;
            switch (currentType)
            {
                case BulletType.PISTOL:
                    p = new Particle(2.0f, 0.99f, new Vector3(0, 0, -1));
                    p.SetInitState(Vector3.Zero, new Vector3(35, 0, 0));
                    break;
                case BulletType.ARTILLERY:
                    p = new Particle(200, 0.99f, new Vector3(0, 0, -20));
                    p.SetInitState(Vector3.Zero, new Vector3(40, 0, 30));
                    break;
                case BulletType.FIREBALL:
                    p = new Particle(1, 0.9f, new Vector3(0, 0, 0.6f));
                    p.SetInitState(Vector3.Zero, new Vector3(10, 0, 0));
                    break;
                case BulletType.LASR:
                    p = new Particle(0.1f, 0.99f, new Vector3(0, 0, 0));
                    p.SetInitState(Vector3.Zero, new Vector3(100, 0, 0));
                    break;
            }
            if(p != null)
            {
                World.AddParticle(p);
            }
        }

        public void OnKeyUp(Key key)
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

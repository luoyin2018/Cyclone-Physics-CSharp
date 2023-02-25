using Cyclone;
using System;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using CommonEx.WPF;

namespace Ballistic
{
    public class ParticleRender
    {
        private Particle _particle;
        public Model3D Model { get; }
        public ParticleRender(Particle p)
        {
            var pos = p.Position;

            _particle = p;

            var meshbuilder = new MeshBuilder();
            meshbuilder.AddSphere(new Point3D(), 50);
            Model = new GeometryModel3D
            {
                Material = ColorHelper.HexToColor("#22ff33").ToMaterial(),
                Geometry = meshbuilder.ToMesh()
            };

            _particle.LocationUpdated += _particle_LocationUpdated;
       }

        private void _particle_LocationUpdated(object sender, EventArgs e)
        {
            Matrix3D m = Matrix3D.Identity;
            Vector3D pos = new Vector3D(_particle.Position.X, _particle.Position.Y, _particle.Position.Z);
            m.TranslatePrepend(pos * 50);

            Model.Transform = new MatrixTransform3D(m);
        }

        public void Destroy()
        {
            _particle.LocationUpdated-=_particle_LocationUpdated;
            _particle = null;
        }
    }
}

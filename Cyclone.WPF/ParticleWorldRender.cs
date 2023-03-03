using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace Cyclone.WPF
{
    public class ParticleWorldRender
    {
        private Model3DGroup _holder;
        private ParticleWorld _world;

        private Dictionary<Particle, ParticleRender> _mapper 
            = new Dictionary<Particle, ParticleRender>();

        public ParticleWorldRender(ParticleWorld world, Model3DGroup geomHolder)
        {
            _holder = geomHolder;
            _world = world;

            _world.ParticleAdded += _world_ParticleAdded;
            _world.ParticleRemoved += _world_ParticleRemoved;
        }

        private void _world_ParticleRemoved(object sender, EventArgs<Particle> e)
        {
            ParticleRender pRender = _mapper[e.Data];
            _holder.Children.Remove(pRender.Model);
            pRender.Destroy();
            _mapper.Remove(e.Data);
        }

        private void _world_ParticleAdded(object sender, EventArgs<Particle> e)
        {
            ParticleRender pRender = new ParticleRender(e.Data);
            _holder.Children.Add(pRender.Model);
            _mapper.Add(e.Data, pRender);
        }
    }
}

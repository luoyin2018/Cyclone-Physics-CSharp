using System;
using Cyclone;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Ballistic
{
    public class BallisticWorld
    {
        private List<Particle> _particles = new List<Particle>();
        public IReadOnlyList<Particle> Particles => new ReadOnlyCollection<Particle>(_particles);

        public void AddParticle(Particle p)
        {
            _particles.Add(p);

            ParticleAdded?.Invoke(this, EventArgs<Particle>.Create(p));
        }
        public void RemoveParticle(Particle p)
        {
            _particles.Remove(p);
            ParticleRemoved?.Invoke(this, EventArgs<Particle>.Create(p));
        }

        public void Update(double time)
        {
            var notalive = new List<Particle>();
            foreach (var p in _particles)
            {
                p.Integrate(time);
                if (p.Position.X > 16)
                {
                    notalive.Add(p);
                }
            }

            foreach(var p in notalive)
            {
                RemoveParticle(p);
            }
            
        }
        
        public event EventHandler<EventArgs<Particle>> ParticleAdded;
        public event EventHandler<EventArgs<Particle>> ParticleRemoved;
    }
}

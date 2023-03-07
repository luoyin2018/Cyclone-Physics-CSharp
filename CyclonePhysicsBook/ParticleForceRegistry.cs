using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace Cyclone
{
    public interface IParticleForceGenerator
    {
        void UpdateForce(Particle particle, float duration);
    }

    public class ParticleForceRegistry
    {
        List<(IParticleForceGenerator, Particle)> registration
            = new List<(IParticleForceGenerator, Particle)>();
        public IReadOnlyCollection<(IParticleForceGenerator,Particle)> Registration 
            => new ReadOnlyCollection<(IParticleForceGenerator, Particle)>(registration);

        public void Add(IParticleForceGenerator forceGenerator, Particle paricle)
        {
            registration.Add((forceGenerator, paricle));
        }
        public void Remove(IParticleForceGenerator forceGenerator, Particle particle)
        {
            var items = registration.Where(r => r.Item1 == forceGenerator && r.Item2 == particle);
            foreach (var item in items)
            {
                registration.Remove(item);
            }
        }
        public void Clear()
        {
            registration.Clear();
        }

        public void Update(float duration)
        {
            foreach(var (forcegenerator, paricle) in registration)
            {
                forcegenerator.UpdateForce(paricle, duration);
            }
        }
    }
}

using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace Cyclone.WPF
{
    public abstract class AppBase
    {
        public abstract void Init(Model3DGroup holder);
        public abstract void Update(double time);
        public virtual void OnMouseUp() { }
        public virtual void OnKeyUp(Key key) { }
    }
}

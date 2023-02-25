using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CommonEx.WPF
{
    public static class CommonExtensions
    {
        public static Material ToMaterial(this Color mainColor)
        {
            var materialGroup = new MaterialGroup();
            //EmissiveMaterial emissMat = new EmissiveMaterial(new SolidColorBrush(mainColor));
            //materialGroup.Children.Add(emissMat);

            DiffuseMaterial diffMat = new DiffuseMaterial(new SolidColorBrush(mainColor));
            materialGroup.Children.Add(diffMat);

            SpecularMaterial specMat = new SpecularMaterial(new SolidColorBrush(mainColor), 200);
            materialGroup.Children.Add(specMat);
            return materialGroup;
        }

    }
}

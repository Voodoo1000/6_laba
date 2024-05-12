using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_laba
{
    public class ParticleColorful : Particle
    {
        public Color InitialColor;
        // два новых поля под цвет начальный и конечный
        public Color FromColor;
        public Color ToColor;

        // для смеси цветов
        public static Color MixColor(Color color1, Color color2, float k)
        {
            return Color.FromArgb(
                (int)(color2.A * k + color1.A * (1 - k)),
                (int)(color2.R * k + color1.R * (1 - k)),
                (int)(color2.G * k + color1.G * (1 - k)),
                (int)(color2.B * k + color1.B * (1 - k))
            );
        }

        // ну и отрисовку перепишем
        public override void Draw(Graphics g)
        {
            float k = Math.Min(1f, Life / 100);

            // так как k уменьшается от 1 до 0, то порядок цветов обратный
            var color = MixColor(ToColor, FromColor, k);
            var b = new SolidBrush(color);

            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            b.Dispose();
        }
    }
}

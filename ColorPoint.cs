using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_laba
{
    public class ColorPoint : IImpactPoint
    {
        public Color ChangeToColor; // Цвет, на который будет изменяться цвет частицы
        public float Radius = 50;
        public bool ChangeColorEnabled = false; // Флаг для включения и выключения смены цвета
        public bool Enabled = true; // Флаг для включения и выключения круга
        public override void ImpactParticle(Particle particle)
        {
            if (!Enabled) return; // Если круг выключен, выходим из метода

            float dx = X - particle.X;
            float dy = Y - particle.Y;
            if (dx * dx + dy * dy <= Radius * Radius && ChangeColorEnabled) // Учитываем состояние CheckBox
            {
                if (particle is ParticleColorful colorParticle)
                {
                    colorParticle.FromColor = ChangeToColor;
                    colorParticle.ToColor = Color.FromArgb(0, ChangeToColor);
                }
            }
        }

        public override void Render(Graphics g)
        {
            if (!Enabled || !ChangeColorEnabled) return; // Если круг выключен, не отрисовываем его

            g.FillEllipse(new SolidBrush(Color.FromArgb(128, ChangeToColor)), X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }
    }
}

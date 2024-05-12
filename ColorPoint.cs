using System;

namespace _6_laba
{
    // Класс ColorPoint представляет собой точку, которая меняет цвет частицы в заданном радиусе
    public class ColorPoint : IImpactPoint
    {
        public Color ChangeToColor; // Цвет, на который будет изменяться цвет частицы
        public float Radius = 50; // Радиус области действия точки
        public bool ChangeColorEnabled = false; // Флаг для включения и выключения смены цвета
        public bool Enabled = true; // Флаг для включения и выключения круга

        // Метод воздействия на частицу
        public override void ImpactParticle(Particle particle)
        {
            // Если круг выключен, выходим из метода
            if (!Enabled) return;

            // Вычисляем расстояние между точкой и частицей
            float dx = X - particle.X;
            float dy = Y - particle.Y;

            // Проверяем, находится ли частица в пределах радиуса и включена ли смена цвета
            if (dx * dx + dy * dy <= Radius * Radius && ChangeColorEnabled)
            {
                // Если частица является экземпляром класса ParticleColorful,
                // то устанавливаем начальный и конечный цвета для смены цвета
                if (particle is ParticleColorful colorParticle)
                {
                    colorParticle.FromColor = ChangeToColor; // Устанавливаем начальный цвет
                    colorParticle.ToColor = Color.FromArgb(0, ChangeToColor); // Устанавливаем конечный цвет
                }
            }
        }

        // Метод отрисовки круга
        public override void Render(Graphics g)
        {
            // Если круг выключен или смена цвета выключена, не отрисовываем его
            if (!Enabled || !ChangeColorEnabled) return;

            // Отрисовываем круг с полупрозрачным цветом в заданном радиусе
            g.FillEllipse(new SolidBrush(Color.FromArgb(128, ChangeToColor)), X - Radius, Y - Radius, Radius * 2, Radius * 2);
        }
    }
}

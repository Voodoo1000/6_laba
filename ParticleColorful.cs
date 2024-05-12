using System;
using System.Drawing;

namespace _6_laba
{
    // Класс ParticleColorful наследуется от класса Particle и расширяет его функциональность для работы с цветом
    public class ParticleColorful : Particle
    {
        // Поля для хранения начального цвета и цветовых интервалов
        public Color FromColor;
        public Color ToColor;

        // Метод для смешивания цветов
        public static Color MixColor(Color color1, Color color2, float k)
        {
            // Рассчитываем новый цвет на основе двух цветов и коэффициента k
            return Color.FromArgb(
                (int)(color2.A * k + color1.A * (1 - k)),
                (int)(color2.R * k + color1.R * (1 - k)),
                (int)(color2.G * k + color1.G * (1 - k)),
                (int)(color2.B * k + color1.B * (1 - k))
            );
        }

        // Переопределение метода отрисовки частицы с учетом цветовых интервалов
        public override void Draw(Graphics g)
        {
            // Рассчитываем коэффициент прозрачности по шкале от 0 до 1.0
            float k = Math.Min(1f, Life / 100);

            // Рассчитываем новый цвет с помощью метода MixColor
            var color = MixColor(ToColor, FromColor, k);
            var b = new SolidBrush(color);

            // Отрисовываем круг частицы с новым цветом
            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            b.Dispose(); // Освобождаем ресурсы SolidBrush
        }
    }
}

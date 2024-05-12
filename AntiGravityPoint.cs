using System;

namespace _6_laba
{
    // Класс AntiGravityPoint представляет точку антигравитации, которая отталкивает частицы
    public class AntiGravityPoint : IImpactPoint
    {
        // Сила антигравитации, которая воздействует на частицы
        public int Power = 100;

        // Метод воздействия на частицу
        public override void ImpactParticle(Particle particle)
        {
            // Вычисляем расстояние между точкой антигравитации и частицей
            float gX = X - particle.X;
            float gY = Y - particle.Y;
            // Для более плавного изменения силы антигравитации используем максимальное значение расстояния r2
            // чтобы избежать деления на ноль или получения очень больших значений
            float r2 = (float)Math.Max(100, gX * gX + gY * gY);

            // Вычисляем изменение скорости частицы по осям X и Y
            // Чем ближе частица к точке антигравитации, тем сильнее её отталкиваем
            particle.SpeedX -= gX * Power / r2; // Отталкивание по оси X
            particle.SpeedY -= gY * Power / r2; // Отталкивание по оси Y
        }
    }
}

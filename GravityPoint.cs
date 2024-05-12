using System;

namespace _6_laba
{
    // Класс GravityPoint представляет точку притяжения, воздействующую на частицы
    public class GravityPoint : IImpactPoint
    {
        public int Power = 100; // Сила притяжения

        // Метод, воздействующий на частицу
        public override void ImpactParticle(Particle particle)
        {
            // Рассчитываем расстояние по X и Y между точкой притяжения и частицей
            float gX = X - particle.X;
            float gY = Y - particle.Y;

            // Рассчитываем квадрат расстояния
            float r2 = (float)Math.Max(100, gX * gX + gY * gY);

            // Изменяем скорость частицы по X и Y в соответствии с притяжением
            particle.SpeedX += gX * Power / r2;
            particle.SpeedY += gY * Power / r2;
        }
    }
}

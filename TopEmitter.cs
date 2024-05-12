using System;

namespace _6_laba
{
    public class TopEmitter : Emitter
    {
        public int Width; // длина экрана

        // Метод для сброса параметров частицы
        public override void ResetParticle(Particle particle)
        {
            // Вызываем базовый сброс частицы, где переопределена жизнь и другие параметры
            base.ResetParticle(particle);

            // Задаем позицию частицы в произвольной точке по ширине экрана и в верхней части экрана
            particle.X = Particle.rand.Next(Width);
            particle.Y = 0;

            // Устанавливаем скорость частицы для падения вниз
            particle.SpeedY = 1;

            // Задаем разброс по горизонтали
            particle.SpeedX = Particle.rand.Next(-2, 2);
        }
    }
}

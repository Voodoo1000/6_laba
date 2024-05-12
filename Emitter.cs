using System;
using System.Collections.Generic;
using System.Drawing; // Добавлен для использования типа Color
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_laba
{
    // Класс Emitter представляет собой источник частиц
    public class Emitter
    {
        public List<Particle> particles = new List<Particle>(); // Список частиц
        public List<IImpactPoint> impactPoints = new(); // Список точек воздействия на частицы

        // Координаты центра эмиттера
        public int X;
        public int Y;

        // Направление, в котором сыпет эмиттер, и разброс частиц
        public int Direction = 0;
        public int Spreading = 360;

        // Гравитация, влияющая на частицы
        public float GravitationX = 0;
        public float GravitationY = 0.5f;

        // Настройки скорости частиц
        public int SpeedMin = 1;
        public int SpeedMax = 10;

        // Настройки радиуса частиц
        public int RadiusMin = 2;
        public int RadiusMax = 10;

        // Настройки времени жизни частиц
        public int LifeMin = 20;
        public int LifeMax = 100;

        // Количество частиц, создаваемых за каждый такт таймера
        public int ParticlesPerTick = 1;

        // Начальный и конечный цвета частиц
        public Color ColorFrom = Color.White;
        public Color ColorTo = Color.FromArgb(0, Color.Black);

        // Создание новой частицы
        public virtual Particle CreateParticle()
        {
            var particle = new ParticleColorful(); // Используем ParticleColorful для поддержки разноцветных частиц
            particle.FromColor = ColorFrom; // Устанавливаем начальный цвет частицы
            particle.ToColor = ColorTo; // Устанавливаем конечный цвет частицы

            return particle;
        }

        // Обновление состояния эмиттера
        public virtual void UpdateState()
        {
            int particlesToCreate = ParticlesPerTick;

            foreach (var particle in particles)
            {
                if (particle.Life < 0)
                {
                    if (particlesToCreate > 0)
                    {
                        particlesToCreate -= 1;
                        ResetParticle(particle); // Перезапускаем частицу
                    }
                }
                else
                {
                    particle.X += particle.SpeedX; // Обновляем положение частицы по оси X
                    particle.Y += particle.SpeedY; // Обновляем положение частицы по оси Y

                    foreach (var point in impactPoints)
                    {
                        point.ImpactParticle(particle); // Проверяем воздействие точек на частицу
                    }

                    particle.SpeedX += GravitationX; // Применяем гравитацию по оси X
                    particle.SpeedY += GravitationY; // Применяем гравитацию по оси Y
                }
            }

            // Создаем новые частицы
            while (particlesToCreate >= 1)
            {
                particlesToCreate -= 1;
                var particle = CreateParticle(); // Создаем новую частицу
                ResetParticle(particle); // Сбрасываем ее состояние
                particles.Add(particle); // Добавляем частицу в список
            }
        }

        // Отрисовка всех частиц и точек воздействия
        public void Render(Graphics g)
        {
            foreach (var particle in particles)
            {
                particle.Draw(g); // Отрисовываем каждую частицу
            }

            foreach (var point in impactPoints)
            {
                point.Render(g); // Отрисовываем каждую точку воздействия
            }
        }

        // Сброс состояния частицы
        public virtual void ResetParticle(Particle particle)
        {
            // Устанавливаем случайное время жизни частицы в заданном диапазоне
            particle.Life = Particle.rand.Next(LifeMin, LifeMax);

            // Устанавливаем начальные координаты частицы в центре эмиттера
            particle.X = X;
            particle.Y = Y;

            // Вычисляем случайное направление для частицы в пределах разброса
            var direction = Direction + (double)Particle.rand.Next(Spreading) - Spreading / 2;

            // Вычисляем случайную скорость частицы в заданном диапазоне
            var speed = Particle.rand.Next(SpeedMin, SpeedMax);

            // Устанавливаем компоненты скорости частицы на основе направления и скорости
            particle.SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            particle.SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            // Устанавливаем случайный радиус частицы в заданном диапазоне
            particle.Radius = Particle.rand.Next(RadiusMin, RadiusMax);
        }
    }
}

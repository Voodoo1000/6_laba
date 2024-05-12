using System;
using System.Collections.Generic;
using System.Drawing;

namespace _6_laba
{
    public class Radar : IImpactPoint
    {
        // Позиция радара
        public PointF Position;
        // Радиус области
        public float Radius;
        // Количество частиц в области
        public int ParticlesCount;
        // Флаг, определяющий, включен ли радар
        public bool Enabled = false;

        // Ссылка на эмиттер частиц
        Emitter emitter;

        // Конструктор
        public Radar(PointF position, float radius, Emitter emitter)
        {
            Position = position;
            Radius = radius;
            this.emitter = emitter;
        }

        // Метод для обновления количества частиц внутри области радара
        public void UpdateParticlesCount(List<Particle> particles)
        {
            if (Enabled)
            {
                ParticlesCount = 0;

                foreach (var particle in particles)
                {
                    // Проверяем, находится ли частица внутри радара
                    if (IsInsideRadar(particle))
                    {
                        ParticlesCount++;
                        // Помечаем частицу, что она попала в радар
                        particle.IsInRadarArea = true;

                        // Перекрашиваем частицу в другой цвет
                        if (particle is ParticleColorful colorfulParticle)
                        {
                            // Например, устанавливаем ей красный цвет
                            colorfulParticle.FromColor = Color.LimeGreen;
                            colorfulParticle.ToColor = Color.LimeGreen;
                        }
                    }
                    else
                    {
                        // Сбрасываем пометку о попадании частицы в радар
                        particle.IsInRadarArea = false;

                        // Возвращаем частицам исходные цвета
                        if (particle is ParticleColorful colorfulParticle)
                        {
                            colorfulParticle.FromColor = emitter.ColorFrom;
                            colorfulParticle.ToColor = emitter.ColorTo; ;
                        }
                    }
                }
            }
        }

        // Метод для проверки, находится ли частица внутри области радара
        private bool IsInsideRadar(Particle particle)
        {
            // Проверяем, находится ли частица внутри области радара
            float dx = particle.X - Position.X;
            float dy = particle.Y - Position.Y;
            return dx * dx + dy * dy <= Radius * Radius;
        }

        // Метод для отрисовки радара
        public override void Render(Graphics g)
        {
            if (Enabled)
            {
                // Рисуем радар
                g.DrawEllipse(Pens.Gray, Position.X - Radius, Position.Y - Radius, Radius * 2, Radius * 2);

                // Выводим количество частиц внутри радара
                Font font = new Font("Arial", 10);
                g.DrawString($"{ParticlesCount}", font, Brushes.Gray, Position.X - 10, Position.Y - 10);
                font.Dispose();
            }
        }

        // Метод для воздействия радара на частицу
        public override void ImpactParticle(Particle particle)
        {
            // Нет необходимости в этом методе для радара
        }
    }
}

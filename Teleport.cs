using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_laba
{
    public class Teleport : IImpactPoint
    {
        public float Radius = 50;
        public PointF Entry { get; set; }
        public PointF Exit { get; set; }
        // Свойство для управления активностью телепорта
        public bool Enabled { get; set; } = false;

        // Свойство для хранения текущего направления выхода телепорта
        public int ExitDirection { get; set; }
        Emitter emitter;

        public Teleport(PointF entry, PointF exit, Emitter emitter)
        {
            Entry = entry;
            Exit = exit;
            this.emitter = emitter;
        }

        public void TeleportParticles(List<Particle> particles)
        {
            if (Enabled)
            {
                foreach (var particle in particles)
                {
                    // Проверяем, находится ли частица внутри радиуса входа телепорта
                    if (IsInsideTeleport(particle, Entry, Radius))
                    {
                        // Вычисляем угол направления выхода в радианах
                        double exitAngle = ExitDirection * Math.PI / 180;

                        // Вычисляем новые координаты для точки выхода на основе текущего направления
                        float newX = Exit.X + (float)Math.Cos(exitAngle) * Radius;
                        float newY = Exit.Y + (float)Math.Sin(exitAngle) * Radius;

                        // Телепортируем частицу в новую точку выхода
                        particle.X = newX;
                        particle.Y = newY;

                        var speed = Particle.rand.Next(emitter.SpeedMin, emitter.SpeedMax);

                        // Вычисляем новую скорость по осям X и Y на основе угла выхода
                        particle.SpeedX = (float)Math.Cos(exitAngle) * speed;
                        particle.SpeedY = (float)Math.Sin(exitAngle) * speed;
                    }
                }
            }
        }





        private bool IsInsideTeleport(Particle particle, PointF center, float radius)
        {
            // Проверяем, находится ли частица внутри круга телепорта
            float dx = particle.X - center.X;
            float dy = particle.Y - center.Y;
            return dx * dx + dy * dy <= radius * radius;
        }

        public override void Render(Graphics g)
        {
            if (Enabled)
            {
                // Размер шрифта для текста "ВХОД" и "ВЫХОД"
                Font font = new Font("Arial", 10);

                // Отрисовываем вход телепорта
                g.FillEllipse(Brushes.Transparent, Entry.X - Radius, Entry.Y - Radius, Radius * 2, Radius * 2);
                g.DrawEllipse(Pens.Blue, Entry.X - Radius, Entry.Y - Radius, Radius * 2, Radius * 2);
                g.DrawString("ВХОД", font, Brushes.Blue, Entry.X - 25, Entry.Y - 5);

                // Отрисовываем выход телепорта
                g.FillEllipse(Brushes.Transparent, Exit.X - Radius, Exit.Y - Radius, Radius * 2, Radius * 2);
                g.DrawEllipse(Pens.Red, Exit.X - Radius, Exit.Y - Radius, Radius * 2, Radius * 2);
                g.DrawString("ВЫХОД", font, Brushes.Red, Exit.X - 30, Exit.Y - 7);

                // Освобождаем ресурсы шрифта
                font.Dispose();
            }
        }

        public override void ImpactParticle(Particle particle)
        {
            // Для телепорта нет необходимости в этом методе
        }
    }
}

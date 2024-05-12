using System;
using System.Drawing;

namespace _6_laba
{
    // Абстрактный класс IImpactPoint представляет точку воздействия на частицы
    public abstract class IImpactPoint
    {
        public float X; // Координата X точки воздействия
        public float Y; // Координата Y точки воздействия

        // Абстрактный метод, определяющий воздействие на частицу
        public abstract void ImpactParticle(Particle particle);

        // Виртуальный метод для отрисовки точки визуально
        public virtual void Render(Graphics g)
        {
            // Отрисовываем точку красным кругом
            g.FillEllipse(
                new SolidBrush(Color.Red), // Задаем красный цвет
                X - 5, // Вычисляем координаты для отрисовки круга
                Y - 5,
                10,
                10
            );
        }
    }
}

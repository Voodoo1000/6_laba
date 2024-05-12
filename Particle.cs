using System;
using System.Drawing;

namespace _6_laba
{
    // Класс Particle представляет частицу
    public class Particle
    {
        // Поля для хранения радиуса и координат частицы
        public int Radius;
        public float X;
        public float Y;

        // Поля для хранения скорости частицы по осям X и Y
        public float SpeedX;
        public float SpeedY;

        // Время жизни частицы
        public float Life;

        // Статический объект Random для генерации случайных чисел
        public static Random rand = new Random();

        // Свойство для определения нахождения частицы в области радара
        public bool IsInRadarArea;

        // Конструктор класса Particle
        public Particle()
        {
            // Генерируем случайное направление и скорость
            var direction = (double)rand.Next(360);
            var speed = 1 + rand.Next(10);

            // Рассчитываем вектор скорости
            SpeedX = (float)(Math.Cos(direction / 180 * Math.PI) * speed);
            SpeedY = -(float)(Math.Sin(direction / 180 * Math.PI) * speed);

            // Генерируем случайный радиус и время жизни
            Radius = 2 + rand.Next(10);
            Life = 20 + rand.Next(100);
        }

        // Метод для отрисовки частицы
        public virtual void Draw(Graphics g)
        {
            // Рассчитываем коэффициент прозрачности по шкале от 0 до 1.0
            float k = Math.Min(1f, Life / 100);
            // Рассчитываем значение альфа канала в шкале от 0 до 255
            // По аналогии с RGB, он используется для задания прозрачности
            int alpha = (int)(k * 255);

            // Создаем цвет из уже существующего, но привязываем к нему значение альфа канала
            var color = Color.FromArgb(alpha, Color.Black);
            var b = new SolidBrush(color);

            // Отрисовываем круг частицы
            g.FillEllipse(b, X - Radius, Y - Radius, Radius * 2, Radius * 2);

            b.Dispose(); // Освобождаем ресурсы SolidBrush
        }
    }
}

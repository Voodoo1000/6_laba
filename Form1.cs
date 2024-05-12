using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6_laba
{
    // Класс Form1 представляет главную форму приложения
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new(); // Список источников частиц
        Emitter emitter; // Текущий источник частиц
        Teleport teleport; // Телепорт
        Radar radar; // Радар

        // Флаги для определения текущего действия мыши (перемещение входа или выхода телепорта)
        bool isMovingEntry = false;
        bool isMovingExit = false;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            // Создание источника частиц (эмиттера)
            this.emitter = new Emitter
            {
                Direction = 0,
                Spreading = 10,
                SpeedMin = 10,
                SpeedMax = 10,
                ColorFrom = Color.Gold,
                ColorTo = Color.FromArgb(0, Color.Red),
                ParticlesPerTick = 10,
                X = picDisplay.Width / 2,
                Y = picDisplay.Height / 2,
            };

            emitters.Add(this.emitter); // Добавление источника частиц в список

            // Добавление точки воздействия на частицы (цветной круг)
            emitter.impactPoints.Add(new ColorPoint
            {
                X = picDisplay.Width / 2 - 100,
                Y = picDisplay.Height / 2,
                ChangeToColor = Color.Blue
            });

            // Создание и инициализация телепорта и радара
            teleport = new Teleport(new PointF(picDisplay.Width / 2 - 100, picDisplay.Height / 2), new PointF(picDisplay.Width / 2 + 100, picDisplay.Height / 2), emitter);
            radar = new Radar(new PointF(picDisplay.Width / 2, picDisplay.Height / 2), 50, emitter);

            // Обработчик события изменения колесика мыши на элементе picDisplay
            picDisplay.MouseWheel += picDisplay_MouseWheel;
        }

        // Обработчик события таймера (обновление состояния эмиттера и отрисовка)
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); // Обновление состояния эмиттера

            teleport.TeleportParticles(emitter.particles); // Обработка телепортирования частиц
            radar.UpdateParticlesCount(emitter.particles); // Обновление счетчика частиц на радаре

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g); // Отрисовка частиц
                teleport.Render(g); // Отрисовка телепорта
                radar.Render(g); // Отрисовка радара
            }

            picDisplay.Invalidate(); // Перерисовка элемента picDisplay
        }

        // Обработчик события перемещения мыши на элементе picDisplay
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMovingEntry)
            {
                teleport.Entry = new PointF(e.X, e.Y); // Перемещение входа телепорта
            }
            else if (isMovingExit)
            {
                teleport.Exit = new PointF(e.X, e.Y); // Перемещение выхода телепорта
            }
            else
            {
                // Перемещение цветной точки или радара
                if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
                {
                    colorPoint.X = e.X;
                    colorPoint.Y = e.Y;
                }

                radar.Position = new PointF(e.X, e.Y);

                // Перерисовка радара и эмиттера
                using (var g = Graphics.FromImage(picDisplay.Image))
                {
                    g.Clear(Color.Black);
                    emitter.Render(g);
                    teleport.Render(g);
                    radar.Render(g);
                }
            }
        }

        // Обработчик события изменения положения ползунка tbDirection
        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value; // Установка направления эмиттера
            lblDirection.Text = $"{tbDirection.Value}°"; // Обновление текста на метке
        }

        // Обработчик события щелчка мыши на элементе picDisplay
        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            // Перемещение входа телепорта при клике левой кнопкой мыши
            if (e.Button == MouseButtons.Left)
            {
                teleport.Entry = new PointF(e.X, e.Y);
            }
            // Перемещение выхода телепорта при клике правой кнопкой мыши
            else if (e.Button == MouseButtons.Right)
            {
                teleport.Exit = new PointF(e.X, e.Y);
            }
        }

        // Обработчик события изменения положения ползунка tbColor
        private void tbColor_Scroll(object sender, EventArgs e)
        {
            if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
            {
                colorPoint.Radius = tbColor.Value;
                lblRColor.Text = $"{tbColor.Value}"; // Обновление текста на метке
            }
        }

        // Обработчик события нажатия на кнопку btnChooseColor
        private void btnChooseColor_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
                {
                    colorPoint.ChangeToColor = colorDialog.Color;
                }
            }
        }

        // Обработчик события изменения положения ползунка tbSpreading
        private void tbSpreading_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = tbSpreading.Value;
            lblSpreading.Text = $"{tbSpreading.Value}°"; // Обновление текста на метке
        }

        // Обработчик события изменения состояния флажка chkColorChange
        private void chkColorChange_CheckedChanged(object sender, EventArgs e)
        {
            if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
            {
                colorPoint.ChangeColorEnabled = chkColorChange.Checked; // Установка состояния смены цвета

                tbColor.Enabled = chkColorChange.Checked; // Установка состояния элемента управления
                btnChooseColor.Enabled = chkColorChange.Checked; // Установка состояния элемента управления
            }
        }

        // Обработчик события изменения состояния флажка chkEnableTeleport
        private void chkEnableTeleport_CheckedChanged(object sender, EventArgs e)
        {
            teleport.Enabled = chkEnableTeleport.Checked; // Установка состояния телепорта

            tbTeleportDirection.Enabled = chkEnableTeleport.Checked; // Установка состояния элемента управления
        }

        // Обработчик события изменения положения ползунка tbTeleportDirection
        private void tbTeleportDirection_Scroll(object sender, EventArgs e)
        {
            teleport.ExitDirection = tbTeleportDirection.Value;
            lblTeleportDirection.Text = $"{tbTeleportDirection.Value}°"; // Обновление текста на метке
        }

        // Обработчик события изменения состояния флажка chkEnableRadar
        private void chkEnableRadar_CheckedChanged(object sender, EventArgs e)
        {
            radar.Enabled = chkEnableRadar.Checked; // Установка состояния радара
        }

        // Обработчик события прокрутки колесика мыши на элементе picDisplay
        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            // Получаем текущий радиус радара
            float currentRadius = radar.Radius;

            // Устанавливаем новый радиус радара в зависимости от вращения колесика мыши
            float newRadius = currentRadius + (e.Delta > 0 ? 5 : -5);

            // Ограничиваем радиус радара максимальным и минимальным значениями
            float maxRadius = 200;
            float minRadius = 25;

            // Устанавливаем новый радиус радара, ограничив его максимальным и минимальным значениями
            radar.Radius = Math.Min(maxRadius, Math.Max(minRadius, newRadius));

            // Перерисовываем радар и область в picDisplay
            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
                teleport.Render(g);
                radar.Render(g);
            }
        }
    }
}

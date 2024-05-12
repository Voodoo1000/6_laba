

namespace _6_laba
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new();
        Emitter emitter;
        Teleport teleport;
        Radar radar;

        // Флаг для определения текущего действия мыши (перемещение входа или выхода телепорта)
        bool isMovingEntry = false;
        bool isMovingExit = false;

        bool isResizingRadar = false;
        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);


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

            emitters.Add(this.emitter);

            /* // добавил гравитон
             emitter.impactPoints.Add(new GravityPoint
             {
                 X = picDisplay.Width / 2 + 100,
                 Y = picDisplay.Height / 2,
             });

             // добавил второй гравитон
             emitter.impactPoints.Add(new GravityPoint
             {
                 X = picDisplay.Width / 2 - 100,
                 Y = picDisplay.Height / 2,
             });*/

            emitter.impactPoints.Add(new ColorPoint
            {
                X = picDisplay.Width / 2 - 100,
                Y = picDisplay.Height / 2,
                ChangeToColor = Color.Blue
            });

            teleport = new Teleport(new PointF(picDisplay.Width / 2 - 100, picDisplay.Height / 2), new PointF(picDisplay.Width / 2 + 100, picDisplay.Height / 2), emitter);
            radar = new Radar(new PointF(picDisplay.Width / 2, picDisplay.Height / 2), 50, emitter);

            picDisplay.MouseWheel += picDisplay_MouseWheel;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState();

            teleport.TeleportParticles(emitter.particles);
            radar.UpdateParticlesCount(emitter.particles);

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g);
                teleport.Render(g);
                radar.Render(g);
            }

            picDisplay.Invalidate();
        }

        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMovingEntry)
            {
                teleport.Entry = new PointF(e.X, e.Y);
            }
            else if (isMovingExit)
            {
                teleport.Exit = new PointF(e.X, e.Y);
            }
            else
            {
                // Если мы не перемещаем телепорт, то, возможно, перемещаем цветную точку
                if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
                {
                    colorPoint.X = e.X;
                    colorPoint.Y = e.Y;
                }

                radar.Position = new PointF(e.X, e.Y);

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

        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value;
            lblDirection.Text = $"{tbDirection.Value}°";
        }
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

        private void tbColor_Scroll(object sender, EventArgs e)
        {
            if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
            {
                colorPoint.Radius = tbColor.Value;
                lblRColor.Text = $"{tbColor.Value}";
            }
        }

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

        private void tbSpreading_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = tbSpreading.Value;
            lblSpreading.Text = $"{tbSpreading.Value}°";
        }

        private void chkColorChange_CheckedChanged(object sender, EventArgs e)
        {
            if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
            {
                colorPoint.ChangeColorEnabled = chkColorChange.Checked;

                tbColor.Enabled = chkColorChange.Checked;
                btnChooseColor.Enabled = chkColorChange.Checked;
            }
        }

        private void chkEnableTeleport_CheckedChanged(object sender, EventArgs e)
        {
            teleport.Enabled = chkEnableTeleport.Checked;

            // Блокируем или разблокируем TrackBar в зависимости от состояния CheckBox
            tbTeleportDirection.Enabled = chkEnableTeleport.Checked;
        }

        private void tbTeleportDirection_Scroll(object sender, EventArgs e)
        {
            teleport.ExitDirection = tbTeleportDirection.Value;
            lblTeleportDirection.Text = $"{tbTeleportDirection.Value}°";
        }

        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            // Если мы не в режиме изменения размера радара, выходим
            /*if (!isResizingRadar)
                return;*/

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

        private void chkEnableRadar_CheckedChanged(object sender, EventArgs e)
        {
            radar.Enabled = chkEnableRadar.Checked;
        }
    }
}

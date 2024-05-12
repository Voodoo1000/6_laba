

namespace _6_laba
{
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new();
        Emitter emitter;
        Teleport teleport;
        Radar radar;

        // ���� ��� ����������� �������� �������� ���� (����������� ����� ��� ������ ���������)
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

            /* // ������� ��������
             emitter.impactPoints.Add(new GravityPoint
             {
                 X = picDisplay.Width / 2 + 100,
                 Y = picDisplay.Height / 2,
             });

             // ������� ������ ��������
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
                // ���� �� �� ���������� ��������, ��, ��������, ���������� ������� �����
                if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
                {
                    colorPoint.X = e.X;
                    colorPoint.Y = e.Y;
                }

                radar.Position = new PointF(e.X, e.Y);

                // �������������� ����� � ������� � picDisplay
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
            lblDirection.Text = $"{tbDirection.Value}�";
        }
        private void picDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            // ����������� ����� ��������� ��� ����� ����� ������� ����
            if (e.Button == MouseButtons.Left)
            {
                teleport.Entry = new PointF(e.X, e.Y);
            }
            // ����������� ������ ��������� ��� ����� ������ ������� ����
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
            lblSpreading.Text = $"{tbSpreading.Value}�";
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

            // ��������� ��� ������������ TrackBar � ����������� �� ��������� CheckBox
            tbTeleportDirection.Enabled = chkEnableTeleport.Checked;
        }

        private void tbTeleportDirection_Scroll(object sender, EventArgs e)
        {
            teleport.ExitDirection = tbTeleportDirection.Value;
            lblTeleportDirection.Text = $"{tbTeleportDirection.Value}�";
        }

        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
            // ���� �� �� � ������ ��������� ������� ������, �������
            /*if (!isResizingRadar)
                return;*/

            // �������� ������� ������ ������
            float currentRadius = radar.Radius;

            // ������������� ����� ������ ������ � ����������� �� �������� �������� ����
            float newRadius = currentRadius + (e.Delta > 0 ? 5 : -5);

            // ������������ ������ ������ ������������ � ����������� ����������
            float maxRadius = 200;
            float minRadius = 25;

            // ������������� ����� ������ ������, ��������� ��� ������������ � ����������� ����������
            radar.Radius = Math.Min(maxRadius, Math.Max(minRadius, newRadius));

            // �������������� ����� � ������� � picDisplay
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

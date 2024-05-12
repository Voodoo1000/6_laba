using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _6_laba
{
    // ����� Form1 ������������ ������� ����� ����������
    public partial class Form1 : Form
    {
        List<Emitter> emitters = new(); // ������ ���������� ������
        Emitter emitter; // ������� �������� ������
        Teleport teleport; // ��������
        Radar radar; // �����

        // ����� ��� ����������� �������� �������� ���� (����������� ����� ��� ������ ���������)
        bool isMovingEntry = false;
        bool isMovingExit = false;

        public Form1()
        {
            InitializeComponent();
            picDisplay.Image = new Bitmap(picDisplay.Width, picDisplay.Height);

            // �������� ��������� ������ (��������)
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

            emitters.Add(this.emitter); // ���������� ��������� ������ � ������

            // ���������� ����� ����������� �� ������� (������� ����)
            emitter.impactPoints.Add(new ColorPoint
            {
                X = picDisplay.Width / 2 - 100,
                Y = picDisplay.Height / 2,
                ChangeToColor = Color.Blue
            });

            // �������� � ������������� ��������� � ������
            teleport = new Teleport(new PointF(picDisplay.Width / 2 - 100, picDisplay.Height / 2), new PointF(picDisplay.Width / 2 + 100, picDisplay.Height / 2), emitter);
            radar = new Radar(new PointF(picDisplay.Width / 2, picDisplay.Height / 2), 50, emitter);

            // ���������� ������� ��������� �������� ���� �� �������� picDisplay
            picDisplay.MouseWheel += picDisplay_MouseWheel;
        }

        // ���������� ������� ������� (���������� ��������� �������� � ���������)
        private void timer1_Tick(object sender, EventArgs e)
        {
            emitter.UpdateState(); // ���������� ��������� ��������

            teleport.TeleportParticles(emitter.particles); // ��������� ���������������� ������
            radar.UpdateParticlesCount(emitter.particles); // ���������� �������� ������ �� ������

            using (var g = Graphics.FromImage(picDisplay.Image))
            {
                g.Clear(Color.Black);
                emitter.Render(g); // ��������� ������
                teleport.Render(g); // ��������� ���������
                radar.Render(g); // ��������� ������
            }

            picDisplay.Invalidate(); // ����������� �������� picDisplay
        }

        // ���������� ������� ����������� ���� �� �������� picDisplay
        private void picDisplay_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMovingEntry)
            {
                teleport.Entry = new PointF(e.X, e.Y); // ����������� ����� ���������
            }
            else if (isMovingExit)
            {
                teleport.Exit = new PointF(e.X, e.Y); // ����������� ������ ���������
            }
            else
            {
                // ����������� ������� ����� ��� ������
                if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
                {
                    colorPoint.X = e.X;
                    colorPoint.Y = e.Y;
                }

                radar.Position = new PointF(e.X, e.Y);

                // ����������� ������ � ��������
                using (var g = Graphics.FromImage(picDisplay.Image))
                {
                    g.Clear(Color.Black);
                    emitter.Render(g);
                    teleport.Render(g);
                    radar.Render(g);
                }
            }
        }

        // ���������� ������� ��������� ��������� �������� tbDirection
        private void tbDirection_Scroll(object sender, EventArgs e)
        {
            emitter.Direction = tbDirection.Value; // ��������� ����������� ��������
            lblDirection.Text = $"{tbDirection.Value}�"; // ���������� ������ �� �����
        }

        // ���������� ������� ������ ���� �� �������� picDisplay
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

        // ���������� ������� ��������� ��������� �������� tbColor
        private void tbColor_Scroll(object sender, EventArgs e)
        {
            if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
            {
                colorPoint.Radius = tbColor.Value;
                lblRColor.Text = $"{tbColor.Value}"; // ���������� ������ �� �����
            }
        }

        // ���������� ������� ������� �� ������ btnChooseColor
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

        // ���������� ������� ��������� ��������� �������� tbSpreading
        private void tbSpreading_Scroll(object sender, EventArgs e)
        {
            emitter.Spreading = tbSpreading.Value;
            lblSpreading.Text = $"{tbSpreading.Value}�"; // ���������� ������ �� �����
        }

        // ���������� ������� ��������� ��������� ������ chkColorChange
        private void chkColorChange_CheckedChanged(object sender, EventArgs e)
        {
            if (emitter.impactPoints.FirstOrDefault() is ColorPoint colorPoint)
            {
                colorPoint.ChangeColorEnabled = chkColorChange.Checked; // ��������� ��������� ����� �����

                tbColor.Enabled = chkColorChange.Checked; // ��������� ��������� �������� ����������
                btnChooseColor.Enabled = chkColorChange.Checked; // ��������� ��������� �������� ����������
            }
        }

        // ���������� ������� ��������� ��������� ������ chkEnableTeleport
        private void chkEnableTeleport_CheckedChanged(object sender, EventArgs e)
        {
            teleport.Enabled = chkEnableTeleport.Checked; // ��������� ��������� ���������

            tbTeleportDirection.Enabled = chkEnableTeleport.Checked; // ��������� ��������� �������� ����������
        }

        // ���������� ������� ��������� ��������� �������� tbTeleportDirection
        private void tbTeleportDirection_Scroll(object sender, EventArgs e)
        {
            teleport.ExitDirection = tbTeleportDirection.Value;
            lblTeleportDirection.Text = $"{tbTeleportDirection.Value}�"; // ���������� ������ �� �����
        }

        // ���������� ������� ��������� ��������� ������ chkEnableRadar
        private void chkEnableRadar_CheckedChanged(object sender, EventArgs e)
        {
            radar.Enabled = chkEnableRadar.Checked; // ��������� ��������� ������
        }

        // ���������� ������� ��������� �������� ���� �� �������� picDisplay
        private void picDisplay_MouseWheel(object sender, MouseEventArgs e)
        {
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
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace AircraftLightsGUI
{
    public class MainForm : Form
    {
        // GUI components
        private Panel planePanel;
        private Button emergencyButton;
        private Button exitButton;
        private TextBox infoTextBox;

        // List of all lights
        private List<StatusLight> lights;
        private StatusLight selectedLight;

        // Possible light states
        public enum LightStatus
        {
            Off,
            On,
            Fault,
            Emergency
        }

        // Class that defines the properties for each light
        public class StatusLight
        {
            public string ID { get; set; } // Unique ID
            public string DisplayName { get; set; } // Display Name
            public PointF Position { get; set; } // Position
            public LightStatus Status { get; set; } // State
            public float Radius { get; set; } = 8f; // Size (of the circle)
            public bool IsAisleLight { get; set; } = false; 
            public float Height { get; set; } = 30f; // Size (Seperate for isle lights)

            // Detects if a light has been clicked
            public bool Contains(PointF point)
            {
                if (IsAisleLight)
                {
                    RectangleF rect = new RectangleF(Position.X - 3, Position.Y - (Height / 2), 6, Height);
                    return rect.Contains(point);
                }
                else
                {
                    float dx = point.X - Position.X;
                    float dy = point.Y - Position.Y;
                    return (dx * dx + dy * dy) <= (Radius * Radius);
                }
            }

            // Updates the colour of the lights circle
            public Color GetColor() => Status switch
            {
                LightStatus.Off => Color.White,
                LightStatus.On => Color.LimeGreen,
                LightStatus.Fault => Color.Yellow,
                LightStatus.Emergency => Color.Red,
                _ => Color.White
            };
        }

        // Constructor for the form
        public MainForm()
        {
            InitializeComponent();
            InitializeLights();
        }

        // Sets up the form and the controls
        private void InitializeComponent()
        {
            Text = "Aircraft Status Monitor";
            Size = new Size(400, 550);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

            // Plane drawing panel
            planePanel = new Panel
            {
                Location = new Point(10, 10),
                Size = new Size(370, 395),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };
            planePanel.Paint += PlanePanel_Paint;
            planePanel.MouseClick += PlanePanel_MouseClick;
            Controls.Add(planePanel);

            // Emergency button
            emergencyButton = new Button
            {
                Text = "EMERGENCY",
                Location = new Point(10, 415),
                Size = new Size(100, 40),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };
            emergencyButton.Click += EmergencyButton_Click;
            Controls.Add(emergencyButton);

            // Exit button
            exitButton = new Button
            {
                Text = "Exit",
                Location = new Point(10, 465),
                Size = new Size(100, 40)
            };
            exitButton.Click += (s, e) => Close();
            Controls.Add(exitButton);

            // Textbox
            infoTextBox = new TextBox
            {
                Location = new Point(130, 415),
                Size = new Size(250, 90),
                Multiline = true,
                ReadOnly = true,
                BackColor = Color.White,
                Text = ""
            };
            Controls.Add(infoTextBox);
        }

        // Creates and positions all lights
        private void InitializeLights()
        {
            lights = new List<StatusLight>();

            // Nose section
            lights.AddRange(new[]
            {
                new StatusLight { ID = "cockpit_front", DisplayName = "Cockpit Front", Position = new PointF(185, 70) },
                new StatusLight { ID = "cockpit_left", DisplayName = "Cockpit Left", Position = new PointF(170, 95) },
                new StatusLight { ID = "cockpit_right", DisplayName = "Cockpit Right", Position = new PointF(200, 95) }
            });

            // Passenger seat rows
            float startY = 140;
            int numRows = 4;
            for (int row = 1; row <= numRows; row++)
            {
                float y = startY + (row - 1) * 45;
                lights.Add(new StatusLight { ID = $"seat_{row}A", DisplayName = $"Seat {row}A", Position = new PointF(165, y) });
                lights.Add(new StatusLight { ID = $"seat_{row}B", DisplayName = $"Seat {row}B", Position = new PointF(205, y) });
            }

            // Aisle lights (3 longer vertical lights)
            lights.AddRange(new[]
            {
                new StatusLight { ID = "aisle_1", DisplayName = "Aisle Light 1", Position = new PointF(185, 150), IsAisleLight = true, Height = 50f },
                new StatusLight { ID = "aisle_2", DisplayName = "Aisle Light 2", Position = new PointF(185, 200), IsAisleLight = true, Height = 50f },
                new StatusLight { ID = "aisle_3", DisplayName = "Aisle Light 3", Position = new PointF(185, 250), IsAisleLight = true, Height = 50f }
            });

            // Tail
            lights.Add(new StatusLight { ID = "tail", DisplayName = "Tail", Position = new PointF(185, 330) });

            // Wing tips
            lights.AddRange(new[]
            {
                new StatusLight { ID = "left_wing", DisplayName = "Left Wing", Position = new PointF(40, 195) },
                new StatusLight { ID = "right_wing", DisplayName = "Right Wing", Position = new PointF(330, 195) }
            });
        }

          private void PlanePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawAircraftOutline(g);
            DrawLegend(g);
            DrawLights(g);
        }

        // Draws aircraft outline
        private void DrawAircraftOutline(Graphics g)
        {
            using (Pen planePen = new Pen(Color.Black, 2.5f))
            {
                GraphicsPath path = new GraphicsPath();

                // Draw aircraft main body
                path.AddArc(160, 40, 50, 50, 180, 180);
                path.AddLine(210, 65, 218, 120);
                path.AddLine(218, 120, 222, 185);
                path.AddLine(222, 185, 340, 185);
                path.AddLine(340, 185, 337, 205);
                path.AddLine(337, 205, 222, 205);
                path.AddLine(222, 205, 215, 310);
                path.AddLine(215, 310, 200, 355);
                path.AddLine(200, 355, 185, 362);
                path.AddLine(185, 362, 170, 355);
                path.AddLine(170, 355, 155, 310);
                path.AddLine(155, 310, 148, 205);
                path.AddLine(148, 205, 30, 205);
                path.AddLine(30, 205, 33, 185);
                path.AddLine(33, 185, 148, 185);
                path.AddLine(148, 185, 152, 120);
                path.AddLine(152, 120, 160, 65);
                path.CloseFigure();
                g.DrawPath(planePen, path);

                // Tail 
                g.DrawLine(planePen, 160, 330, 120, 330);
                g.DrawLine(planePen, 120, 330, 123, 342);
                g.DrawLine(planePen, 123, 342, 160, 342);

                g.DrawLine(planePen, 210, 330, 250, 330);
                g.DrawLine(planePen, 250, 330, 247, 342);
                g.DrawLine(planePen, 247, 342, 210, 342);
            }
        }

        // Draws the 'key' for the colours
        private void DrawLegend(Graphics g)
        {
            int keyX = 265, keyY = 30;
            using (Font keyFont = new Font("Arial", 10, FontStyle.Bold))
            using (Font labelFont = new Font("Arial", 8))
            {
                g.DrawString("Key:", keyFont, Brushes.Black, keyX, keyY);

                var legendItems = new (Color color, string text)[]
                {
                    (Color.Red, "Emergency"),
                    (Color.Yellow, "Fault"),
                    (Color.LimeGreen, "On"),
                    (Color.White, "Off")
                };

                for (int i = 0; i < legendItems.Length; i++)
                {
                    var item = legendItems[i];
                    int y = keyY + 25 + (i * 25);
                    g.FillEllipse(new SolidBrush(item.color), keyX, y, 14, 14);
                    g.DrawEllipse(Pens.Black, keyX, y, 14, 14);
                    g.DrawString(item.text, labelFont, Brushes.Black, keyX + 18, y + 1);
                }
            }
        }

        // Draws all lights (circles and rectangles)
        private void DrawLights(Graphics g)
        {
            foreach (var light in lights)
            {
                if (light.IsAisleLight)
                {
                    RectangleF rect = new RectangleF(light.Position.X - 3, light.Position.Y - (light.Height / 2), 6, light.Height);
                    g.FillRectangle(new SolidBrush(light.GetColor()), rect);
                    using var pen = new Pen(light == selectedLight ? Color.Blue : Color.Black, light == selectedLight ? 3 : 1);
                    g.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                }
                else
                {
                    g.FillEllipse(new SolidBrush(light.GetColor()),
                        light.Position.X - light.Radius, light.Position.Y - light.Radius,
                        light.Radius * 2, light.Radius * 2);
                    using var pen = new Pen(light == selectedLight ? Color.Blue : Color.Black, light == selectedLight ? 3 : 1);
                    g.DrawEllipse(pen, light.Position.X - light.Radius, light.Position.Y - light.Radius,
                        light.Radius * 2, light.Radius * 2);
                }
            }
        }

        // Handles mouse clicks to select lights
        private void PlanePanel_MouseClick(object sender, MouseEventArgs e)
        {
            PointF clickPoint = new PointF(e.X, e.Y);
            selectedLight = lights.FirstOrDefault(l => l.Contains(clickPoint));
            if (selectedLight != null)
                ShowLightContextMenu(selectedLight, e.Location);
            planePanel.Invalidate();
        }

        // Shows the pop-up when a light is clicked
        private void ShowLightContextMenu(StatusLight light, Point location)
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.Items.Add($"{light.DisplayName}").Enabled = false;
            menu.Items.Add(new ToolStripSeparator());

            void AddItem(string text, LightStatus status)
            {
                var item = new ToolStripMenuItem(text);
                item.Click += (s, e) => SetLightStatus(light, status);
                menu.Items.Add(item);
            }

            AddItem("Off (White)", LightStatus.Off);
            AddItem("On (Green)", LightStatus.On);
            AddItem("Fault (Yellow)", LightStatus.Fault);
            AddItem("Emergency (Red)", LightStatus.Emergency);

            menu.Show(planePanel, location);
        }

        // Updates a light status (ID, status)
        private void SetLightStatus(StatusLight light, LightStatus status)
        {
            light.Status = status;
            planePanel.Invalidate();
        }

        // Emergency toggle
        private void EmergencyButton_Click(object sender, EventArgs e)
        {
            bool anyNonEmergency = lights.Any(l => l.Status != LightStatus.Emergency);
            foreach (var light in lights)
                light.Status = anyNonEmergency ? LightStatus.Emergency : LightStatus.Off;

            planePanel.Invalidate();
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
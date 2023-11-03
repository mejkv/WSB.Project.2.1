using System;
using System.Windows.Forms;

namespace MagazynEdu.Windows.Forms
{
    public partial class MainForm : Form
    {
        private TextBox textBox1;
        private Button button1;
        private Label label1;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            textBox1 = new TextBox();
            button1 = new Button();
            label1 = new Label();

            SuspendLayout();

            // TextBox
            textBox1.Location = new System.Drawing.Point(50, 50);
            textBox1.Size = new System.Drawing.Size(150, 20);
            Controls.Add(textBox1);

            // Button
            button1.Location = new System.Drawing.Point(50, 80);
            button1.Size = new System.Drawing.Size(75, 23);
            button1.Text = "Wyświetl";
            button1.Click += new EventHandler(button1_Click);
            Controls.Add(button1);

            // Label
            label1.Location = new System.Drawing.Point(50, 120);
            label1.Size = new System.Drawing.Size(200, 20);
            Controls.Add(label1);

            // MainForm
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(300, 200);
            Text = "AirShop Desktop App";

            ResumeLayout(false);
            PerformLayout();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "Wprowadzony tekst: " + textBox1.Text;
        }
    }
}

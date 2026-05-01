namespace StokTakip.Views
{
    partial class VerificationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerificationForm));
            guna2Panel_girisYap = new Guna.UI2.WinForms.Guna2Panel();
            label1 = new Label();
            button_onayla = new Guna.UI2.WinForms.Guna2Button();
            textBox_kod = new Guna.UI2.WinForms.Guna2TextBox();
            label_dogrulama = new Label();
            button_close = new Guna.UI2.WinForms.Guna2ControlBox();
            guna2DragControl_VerificationForm = new Guna.UI2.WinForms.Guna2DragControl(components);
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            guna2Panel_girisYap.SuspendLayout();
            guna2Panel1.SuspendLayout();
            SuspendLayout();
            // 
            // guna2Panel_girisYap
            // 
            guna2Panel_girisYap.BackColor = Color.Transparent;
            guna2Panel_girisYap.BorderRadius = 20;
            guna2Panel_girisYap.Controls.Add(label1);
            guna2Panel_girisYap.Controls.Add(button_onayla);
            guna2Panel_girisYap.Controls.Add(textBox_kod);
            guna2Panel_girisYap.Controls.Add(label_dogrulama);
            guna2Panel_girisYap.CustomizableEdges = customizableEdges5;
            guna2Panel_girisYap.FillColor = Color.White;
            guna2Panel_girisYap.Location = new Point(112, 54);
            guna2Panel_girisYap.Name = "guna2Panel_girisYap";
            guna2Panel_girisYap.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2Panel_girisYap.Size = new Size(414, 299);
            guna2Panel_girisYap.TabIndex = 10;
            guna2Panel_girisYap.Paint += guna2Panel_girisYap_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(14, 67);
            label1.Name = "label1";
            label1.Size = new Size(382, 20);
            label1.TabIndex = 11;
            label1.Text = "Lütfen e-posta adresinize gönderilen 6 haneli kodu girin.";
            // 
            // button_onayla
            // 
            button_onayla.BorderRadius = 20;
            button_onayla.CustomizableEdges = customizableEdges1;
            button_onayla.DisabledState.BorderColor = Color.DarkGray;
            button_onayla.DisabledState.CustomBorderColor = Color.DarkGray;
            button_onayla.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            button_onayla.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            button_onayla.FillColor = Color.FromArgb(24, 160, 114);
            button_onayla.Font = new Font("Segoe UI", 9F);
            button_onayla.ForeColor = Color.White;
            button_onayla.Location = new Point(85, 197);
            button_onayla.Name = "button_onayla";
            button_onayla.ShadowDecoration.CustomizableEdges = customizableEdges2;
            button_onayla.Size = new Size(250, 45);
            button_onayla.TabIndex = 10;
            button_onayla.Text = "Doğrula";
            button_onayla.Click += button_onayla_Click;
            // 
            // textBox_kod
            // 
            textBox_kod.BorderRadius = 20;
            textBox_kod.CustomizableEdges = customizableEdges3;
            textBox_kod.DefaultText = "";
            textBox_kod.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBox_kod.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBox_kod.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBox_kod.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBox_kod.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox_kod.Font = new Font("Microsoft Sans Serif", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 162);
            textBox_kod.ForeColor = Color.FromArgb(64, 64, 64);
            textBox_kod.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            textBox_kod.Location = new Point(85, 125);
            textBox_kod.Margin = new Padding(3, 5, 3, 5);
            textBox_kod.Name = "textBox_kod";
            textBox_kod.PlaceholderText = "000000";
            textBox_kod.SelectedText = "";
            textBox_kod.ShadowDecoration.CustomizableEdges = customizableEdges4;
            textBox_kod.Size = new Size(250, 45);
            textBox_kod.TabIndex = 8;
            textBox_kod.TextAlign = HorizontalAlignment.Center;
            textBox_kod.TextOffset = new Point(10, 0);
            // 
            // label_dogrulama
            // 
            label_dogrulama.AutoSize = true;
            label_dogrulama.Font = new Font("Segoe UI Semibold", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 162);
            label_dogrulama.ForeColor = Color.FromArgb(64, 64, 64);
            label_dogrulama.Location = new Point(109, 11);
            label_dogrulama.Name = "label_dogrulama";
            label_dogrulama.Size = new Size(192, 46);
            label_dogrulama.TabIndex = 7;
            label_dogrulama.Text = "Doğrulama";
            label_dogrulama.Click += label_dogrulama_Click;
            // 
            // button_close
            // 
            button_close.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button_close.BackColor = Color.Transparent;
            button_close.CustomizableEdges = customizableEdges7;
            button_close.FillColor = Color.Transparent;
            button_close.IconColor = Color.White;
            button_close.Location = new Point(555, 2);
            button_close.Name = "button_close";
            button_close.ShadowDecoration.CustomizableEdges = customizableEdges8;
            button_close.Size = new Size(56, 36);
            button_close.TabIndex = 11;
            // 
            // guna2DragControl_VerificationForm
            // 
            guna2DragControl_VerificationForm.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl_VerificationForm.UseTransparentDrag = true;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.Controls.Add(button_close);
            guna2Panel1.Controls.Add(guna2Panel_girisYap);
            guna2Panel1.CustomizableEdges = customizableEdges9;
            guna2Panel1.FillColor = Color.FromArgb(150, 0, 0, 0);
            guna2Panel1.Location = new Point(-1, -1);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2Panel1.Size = new Size(614, 410);
            guna2Panel1.TabIndex = 12;
            // 
            // VerificationForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            ClientSize = new Size(612, 408);
            Controls.Add(guna2Panel1);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.None;
            Name = "VerificationForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VerificationForm";
            Load += VerificationForm_Load;
            guna2Panel_girisYap.ResumeLayout(false);
            guna2Panel_girisYap.PerformLayout();
            guna2Panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel_girisYap;
        private Guna.UI2.WinForms.Guna2Button button_onayla;
        private Guna.UI2.WinForms.Guna2TextBox textBox_kod;
        private Label label_dogrulama;
        private Guna.UI2.WinForms.Guna2ControlBox button_close;
        private Guna.UI2.WinForms.Guna2DragControl guna2DragControl_VerificationForm;
        private Label label1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
    }
}
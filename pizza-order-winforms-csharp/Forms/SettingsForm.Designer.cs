using System.Drawing;
using System.Windows.Forms;

namespace PizzaOrderSystem.Forms
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;
        private GroupBox grpLanguage;
        private RadioButton rbEnglish;
        private RadioButton rbArabic;
        private Button btnSave;
        private Button btnCancel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpLanguage = new GroupBox();
            rbEnglish = new RadioButton();
            rbArabic = new RadioButton();
            btnSave = new Button();
            btnCancel = new Button();
            grpLanguage.SuspendLayout();
            SuspendLayout();

            grpLanguage.Controls.Add(rbEnglish);
            grpLanguage.Controls.Add(rbArabic);
            grpLanguage.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            grpLanguage.Location = new Point(12, 12);
            grpLanguage.Name = "grpLanguage";
            grpLanguage.Size = new Size(260, 100);
            grpLanguage.TabIndex = 0;
            grpLanguage.TabStop = false;
            grpLanguage.Text = "Language";

            rbEnglish.AutoSize = true;
            rbEnglish.Font = new Font("Segoe UI", 10F);
            rbEnglish.Location = new Point(20, 30);
            rbEnglish.Name = "rbEnglish";
            rbEnglish.Size = new Size(80, 27);
            rbEnglish.Text = "English";
            rbEnglish.UseVisualStyleBackColor = true;

            rbArabic.AutoSize = true;
            rbArabic.Font = new Font("Segoe UI", 10F);
            rbArabic.Location = new Point(20, 60);
            rbArabic.Name = "rbArabic";
            rbArabic.Size = new Size(81, 27);
            rbArabic.Text = "العربية";
            rbArabic.UseVisualStyleBackColor = true;

            btnSave.BackColor = Color.FromArgb(50, 180, 70);
            btnSave.FlatStyle = FlatStyle.Flat;
            btnSave.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnSave.ForeColor = Color.White;
            btnSave.Location = new Point(12, 130);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += btnSave_Click;

            btnCancel.BackColor = Color.FromArgb(200, 80, 80);
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnCancel.ForeColor = Color.White;
            btnCancel.Location = new Point(152, 130);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;

            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 245, 240);
            ClientSize = new Size(284, 191);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(grpLanguage);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SettingsForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Settings";
            grpLanguage.ResumeLayout(false);
            grpLanguage.PerformLayout();
            ResumeLayout(false);
        }
    }
}
using System;
using System.Windows.Forms;
using pizza_order_winforms_csharp.Properties;
using PizzaOrderSystem.Services;

namespace PizzaOrderSystem.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            LoadCurrentLanguage();
            ApplyResources();
        }

        private void LoadCurrentLanguage()
        {
            string currentLang = Settings.Default.Language;
            rbArabic.Checked = (currentLang == "ar");
            rbEnglish.Checked = (currentLang != "ar");
        }

        private void ApplyResources()
        {
            this.Text = LanguageManager.GetString("Settings");
            grpLanguage.Text = LanguageManager.GetString("Language");
            btnSave.Text = LanguageManager.GetString("Save");
            btnCancel.Text = LanguageManager.GetString("Cancel");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string newLang = rbArabic.Checked ? "ar" : "en";
            string oldLang = Settings.Default.Language;
            if (newLang != oldLang)
            {
                LanguageManager.SetLanguage(newLang);
                MessageBox.Show(LanguageManager.GetString("LanguageChangedRestart"),
                                LanguageManager.GetString("Information"),
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                // إعادة تشغيل التطبيق تلقائياً
                Application.Restart();
                Environment.Exit(0);
            }
            else
                Close();
        }

        private void btnCancel_Click(object sender, EventArgs e) => Close();
    }
}
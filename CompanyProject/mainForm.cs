using System.Windows.Forms;

namespace CompanyProject
{
    public partial class mainForm : DevExpress.XtraEditors.XtraForm
    {
        private Form openedMiniForm;

        public mainForm()
        {
            InitializeComponent();

            SqlConnector.Init(@"Server = DESKTOP-IAADCGV\SQLSERVER; Database = CompanyProject; Trusted_Connection = True; TrustServerCertificate = True; ");
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (openedMiniForm is saleMiniForm) (openedMiniForm as saleMiniForm).saleMiniForm_FormClosing(null, null);
        }

        private void productsNavBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {

            OpenMiniForm(new productMiniForm());
        }

        private void customersNavBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenMiniForm(new customerMiniForm());
        }

        private void salesBarItem_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenMiniForm(new saleMiniForm());
        }

        private void OpenMiniForm(Form newMiniForm)
        {
            if (openedMiniForm != null) openedMiniForm.Close();

            openedMiniForm = newMiniForm;
            openedMiniForm.MdiParent = this;
            openedMiniForm.Dock = DockStyle.Fill;
            openedMiniForm.TopLevel = false;
            openedMiniForm.TopMost = true;
            openedMiniForm.FormBorderStyle = FormBorderStyle.None;
            openedMiniForm.Show();
            mainMiniFormPanel.Controls.Add(newMiniForm);
        }
    }
}

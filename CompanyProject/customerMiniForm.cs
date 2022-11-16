using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace CompanyProject
{
    public partial class customerMiniForm : XtraForm
    {
        public customerMiniForm()
        {
            InitializeComponent();
        }

        private void customerMiniForm_Load(object sender, EventArgs e)
        {
            GridRefresh();
        }

        private void customerCreateButton_Click(object sender, EventArgs e)
        {
            CustomerEditPanelOpen(true, false);
        }

        private void customerReadButton_Click(object sender, EventArgs e)
        {
            CustomerEditPanelOpen(false, true);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            GridRefresh();
        }

        private void customerUpdateButton_Click(object sender, EventArgs e)
        {
            CustomerEditPanelOpen(false, false);
        }

        private void customerDeleteButton_Click(object sender, EventArgs e)
        {
            CustomerDelete();
        }

        private void customerOkButton_Click(object sender, EventArgs e)
        {
            ProductOk();
        }

        private void customerCancelButton_Click(object sender, EventArgs e)
        {
            CustomerEditPanelClose();
        }

        private void GridRefresh()
        {
            DataTable dt = SqlConnector.ExQuery("SELECT ID,Name,(CustomerTable.Debt + dbo.IF_FUNC(dbo.CUSTOMER_FULL_AMOUNT_PAID(ID),0,dbo.CUSTOMER_FULL_AMOUNT_PAID(ID))-dbo.IF_FUNC(dbo.CUSTOMER_FULL_PRICE(ID),0,dbo.CUSTOMER_FULL_PRICE(ID))) AS [Debt],(dbo.IF_FUNC(dbo.CUSTOMER_FULL_AMOUNT_PAID(ID),0,dbo.CUSTOMER_FULL_AMOUNT_PAID(ID)) + CustomerTable.AmountPaid) AS [AmountPaid] FROM CustomerTable");

            customerGridControl.DataSource = dt;
        }

        //check if all necessary fields was filled
        private bool DataTest()
        {
            if (customerNameTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Name", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customerIDTextBox.Text == "")
            {
                XtraMessageBox.Show("Input ID", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customerDebtTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Debt", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customerAmountPaidTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Amount Paid", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customerEmailTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Email", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customerPhoneTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Phone", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (customerCompanyNameTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Company Name or none", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void NewCustomerPanelFilling(string fillingCustomerID)
        {
            DataTable dt = SqlConnector.ExQuery($"SELECT ct.ID, ct.Name, ct.Debt, ct.AmountPaid, cpdt.Email, cpdt.Phone, cpdt.CompanyName FROM CustomerTable ct JOIN CustomerPersonalDataTable cpdt ON ct.ID = cpdt.CustomerID WHERE ct.ID = {fillingCustomerID}");

            customerIDTextBox.Text = dt.Rows[0][0].ToString();
            customerNameTextBox.Text = dt.Rows[0][1].ToString();
            customerDebtTextBox.Text = dt.Rows[0][2].ToString();
            customerAmountPaidTextBox.Text = dt.Rows[0][3].ToString();
            customerEmailTextBox.Text = dt.Rows[0][4].ToString();
            customerPhoneTextBox.Text = dt.Rows[0][5].ToString();
            customerCompanyNameTextBox.Text = dt.Rows[0][6].ToString();
        }

        private void NewCustomerPanelClear()
        {
            customerNameTextBox.Text = "";
            customerIDTextBox.Text = "New";
            customerEmailTextBox.Text = "";
            customerPhoneTextBox.Text = "";
            customerCompanyNameTextBox.Text = "";
            customerDebtTextBox.Text = "";
            customerAmountPaidTextBox.Text = "";
        }

        private void CustomerDelete()
        {
            //return if grid is empty
            if (customerGridView.FocusedRowHandle < 0) return;

            if (SqlConnector.ExQuery($"SELECT ID FROM CheckTable WHERE CustomerID = {customerGridView.GetFocusedRowCellValue("ID")}").Rows.Count != 0)
            {
                XtraMessageBox.Show("Cannot Delete This Customer Because This ID Used In Sales", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            SqlConnector.ExNonQuery($"DELETE FROM CustomerTable WHERE ID = {customerGridView.GetFocusedRowCellValue("ID")};DELETE FROM CustomerPersonalDataTable WHERE CustomerID = {customerGridView.GetFocusedRowCellValue("ID")};");

            GridRefresh();
        }

        private void ProductOk() 
        {
            //if readmode true then exit behave like a cancel button
            if (customerNameTextBox.ReadOnly) customerCancelButton_Click(null, null);

            //if all testing is succesed
            bool isProcessSuccesed;

            if (customerIDTextBox.Text == "New") isProcessSuccesed = CustomerCreate();//if is create mode
            else isProcessSuccesed = CustomerUpdate();//if is update mode

            if (isProcessSuccesed) CustomerEditPanelClose();
        }

        //return false if something is wrong for creating
        private bool CustomerCreate()
        {
            if (!DataTest()) return false;

            DataTable dt = SqlConnector.ExQuery("SELECT ID FROM CustomerTable");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == customerIDTextBox.Text)
                {
                    XtraMessageBox.Show("This ID already exist", "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            //find new id from table and use it for inserting new customed data
            SqlConnector.ExNonQuery($"DECLARE @NEWID AS INT;SELECT @NEWID = MAX(ID) FROM CustomerTable;IF (@NEWID IS NULL) BEGIN SELECT @NEWID = 0 END ELSE BEGIN SELECT @NEWID = @NEWID + 1 END;INSERT INTO CustomerTable VALUES(@NEWID,'{customerNameTextBox.Text}', {customerDebtTextBox.Text},{customerAmountPaidTextBox.Text});INSERT INTO CustomerPersonalDataTable VALUES(@NEWID,'{customerEmailTextBox.Text}','{customerPhoneTextBox.Text}','{customerCompanyNameTextBox.Text}');");

            GridRefresh();

            customerGridView.FocusedRowHandle = customerGridView.RowCount - 1;

            return true;
        }

        //return false if something is wrong for updating
        private bool CustomerUpdate()
        {
            if (!DataTest()) return false;

            SqlConnector.ExNonQuery($"UPDATE CustomerTable SET Name = '{customerNameTextBox.Text}',Debt = {customerDebtTextBox.Text}, AmountPaid = {customerAmountPaidTextBox.Text} WHERE ID = {customerIDTextBox.Text};UPDATE CustomerPersonalDataTable SET Email = '{customerEmailTextBox.Text}',Phone = '{customerPhoneTextBox.Text}',CompanyName = '{customerCompanyNameTextBox.Text}' WHERE CustomerID = {customerIDTextBox.Text};");

            GridRefresh();

            return true;
        }

        private void CustomerEditPanelOpen(bool isCreateMood, bool isReadMode)
        {
            //return if grid empty and it is not create mode
            if (!((customerGridView.FocusedRowHandle >= 0 && !isCreateMood) || isCreateMood)) return;

            customerNameTextBox.ReadOnly = isReadMode;
            customerDebtTextBox.ReadOnly = isReadMode;
            customerAmountPaidTextBox.ReadOnly = isReadMode;
            customerEmailTextBox.ReadOnly = isReadMode;
            customerPhoneTextBox.ReadOnly = isReadMode;
            customerCompanyNameTextBox.ReadOnly = isReadMode;

            customerButtonsDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            customerFormDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;

            customerGridControl.Visible = false;

            if (isCreateMood) NewCustomerPanelClear();

            else NewCustomerPanelFilling(customerGridView.GetFocusedRowCellValue("ID").ToString());
        }

        private void CustomerEditPanelClose()
        {
            customerButtonsDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            customerFormDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

            customerGridControl.Visible = true;
        }
    }
}
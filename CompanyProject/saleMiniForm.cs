using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace CompanyProject
{
    public enum EditMode
    { 
        CheckView,
        CheckEdit,
        SaleEdit
    }

    public partial class saleMiniForm : XtraForm
    {
        public EditMode editMode = EditMode.CheckView;
        
        public saleMiniForm()
        {
            InitializeComponent();

            DataTable dt1 = SqlConnector.ExQuery("SELECT * FROM ProductTable");
            DataTable dt2 = SqlConnector.ExQuery("SELECT Measure FROM ConstMeasureTable");
            DataTable dt3 = SqlConnector.ExQuery("SELECT * FROM CustomerTable");
            //saleProductIDLookUp.Properties.DataSource = dt1;
            for (int i = 0; i < dt1.Rows.Count; i++) saleProductIDLookUp.EditValue = dt1;
            for (int i = 0; i < dt2.Rows.Count; i++) saleProductCurrentMeasureComboBox.Properties.Items.Add(dt2.Rows[i][0].ToString());
            for (int i = 0; i < dt3.Rows.Count; i++) saleCustomerIDLookUp.EditValue = dt3;
        }

        private void saleForm_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'productDataSet.ProductTable' table. You can move, or remove it, as needed.
            this.productTableTableAdapter.Fill(this.productDataSet.ProductTable);
            // TODO: This line of code loads data into the 'saleCustomerDataSet.CustomerTable' table. You can move, or remove it, as needed.
            this.customerTableTableAdapter.Fill(this.saleCustomerDataSet.CustomerTable);
            GridRefresh();
        }

        public void saleMiniForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SqlConnector.ExNonQuery($"DELETE FROM SaleTable WHERE CheckID = -1");
        }

        private void saleCreateButton_Click(object sender, EventArgs e)
        {
            SaleEditPanelOpen(true, false);
        }

        private void saleCheckReadButton_Click(object sender, EventArgs e)
        {
            SaleEditPanelOpen(false, true);
        }

        private void saleRefreshButton_Click(object sender, EventArgs e)
        {
            GridRefresh();
        }

        private void saleUpdateButton_Click(object sender, EventArgs e)
        {
            SaleEditPanelOpen(false, false);
        }

        private void saleDeleteButton_Click(object sender, EventArgs e)
        {
            SaleDelete();
        }

        private void SaleOkButton_Click(object sender, EventArgs e)
        {
            SaleOk();
        }

        private void SaleCancelButton_Click(object sender, EventArgs e)
        {
            if (editMode == EditMode.CheckEdit) SqlConnector.ExNonQuery($"DELETE FROM SaleTable WHERE CheckID = -1");

            SaleEditPanelClose();
        }

        private void saleCustomerIDComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (saleCustomerIDLookUp.EditValue.ToString() == null || saleCustomerIDLookUp.EditValue.ToString() == "") return;

            DataTable dt = SqlConnector.ExQuery($"SELECT Name FROM CustomerTable WHERE ID = {saleCustomerIDLookUp.EditValue}");

            saleCustomerNameTextBox.Text = dt.Rows[0][0].ToString();
        }

        private void saleProductIDComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            ProductFieldsUpdate();
        }

        private void GridRefresh()
        {
            //refresh check grid
            if (editMode == EditMode.CheckView)
            {
                if (saleCheckIDTextBox.Text != "New")
                {
                    DataTable dt = SqlConnector.ExQuery("SELECT cht.ID, cst.Name, cht.ActionTime, cht.AmountPaid FROM CheckTable cht JOIN CustomerTable cst ON cht.customerId = cst.ID");

                    saleCheckGridControl.DataSource = dt;
                }
            }

            //refresh sale grid
            else
            {
                DataTable dt;
                
                dt = SqlConnector.ExQuery($"SELECT st.ID, st.CheckID, pt.Name, cmt.Measure, st.ProductUnitCount FROM SaleTable st JOIN ConstMeasureTable cmt ON cmt.ID = st.CurrentMeasureID JOIN ProductTable pt ON st.ProductID = pt.ID WHERE CheckID = {(saleCheckIDTextBox.Text == "New" ? (-1).ToString() : saleCheckIDTextBox.Text)}");

                saleGridControl.DataSource = dt;

                dt = SqlConnector.ExQuery($"SELECT CustomerID FROM CheckTable WHERE ID = {(saleCheckIDTextBox.Text == "New" ? (-1).ToString() : saleCheckIDTextBox.Text)}");

                if (dt.Rows.Count > 0) saleCustomerIDLookUp.ItemIndex = saleCustomerIDLookUp.Properties.GetDataSourceRowIndex("ID", dt.Rows[0][0].ToString());

                saleAmountPriceTextBox.Text = SqlConnector.ExQuery($"SELECT dbo.CUSTOMER_AMOUNT_PRICE_ALL_SALES_BY_CHECK({(saleCheckIDTextBox.Text == "New" ? (-1).ToString() : saleCheckIDTextBox.Text)})").Rows[0][0].ToString();
            }
        }

        //return false if something is wrong for creating
        private bool SaleCreate()
        {
            if (!DataTest()) return false;

            //create check if all neccessary data field filled and created one or more sales
            if (editMode == EditMode.CheckEdit)
            {
                if (SqlConnector.ExQuery("SELECT * FROM SaleTable WHERE CheckID = -1").Rows.Count == 0)
                {
                    XtraMessageBox.Show("You Must Create 1 or More Sales in Check", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                saleCheckIDTextBox.Text = SqlConnector.ExQuery($"DECLARE @NEWID AS INT;SELECT @NEWID = MAX(ID) FROM CheckTable;IF (@NEWID IS NULL) BEGIN SELECT @NEWID = 0 END ELSE BEGIN SELECT @NEWID = @NEWID + 1 END;Update SaleTable Set CheckID = @NEWID WHERE CheckID = -1;INSERT INTO CheckTable VALUES(@NEWID, '{saleCustomerIDLookUp.EditValue}','{DateTime.Now}',{saleCustomerAmountPaidTextBox.Text});SELECT @NEWID").Rows[0][0].ToString();

                XtraMessageBox.Show($"Created Check\nPrice : {saleAmountPriceTextBox.Text}\nCustomer Name : {saleCustomerNameTextBox.Text}\nCreating Date : {DateTime.Now}");
            }
            
            //create sale if all neccessery fields filled
            else
            {
                if (saleIDTextBox.Text != "New")
                {
                    XtraMessageBox.Show("Input New ID", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }

                string newCheckID;

                if (saleCheckIDTextBox.Text != "New") newCheckID = saleCheckIDTextBox.Text;
                else newCheckID = (-1).ToString();

                SqlConnector.ExNonQuery($"DECLARE @NEWID AS INT;SELECT @NEWID = MAX(ID) FROM SaleTable;IF (@NEWID IS NULL) BEGIN SELECT @NEWID = 0 END ELSE BEGIN SELECT @NEWID = @NEWID + 1 END;INSERT INTO SaleTable VALUES(@NEWID,{newCheckID},'{saleProductIDLookUp.EditValue}',{saleProductCurrentMeasureComboBox.SelectedIndex},{saleUnitCountTextBox.Text})");

                saleCheckGridView.FocusedRowHandle = saleCheckGridView.RowCount - 1;
            }

            return true;
        }

        //return false if something is wrong for updating
        private bool SaleUpdate()
        {
            if (!DataTest()) return false;

            if (editMode == EditMode.CheckEdit) SqlConnector.ExNonQuery($"UPDATE CheckTable SET CustomerID = {saleCustomerIDLookUp.EditValue}, AmountPaid = {saleCustomerAmountPaidTextBox.Text}  WHERE ID = {saleCheckIDTextBox.Text};");

            else SqlConnector.ExNonQuery($"UPDATE SaleTable SET ProductID = '{saleProductIDLookUp.EditValue}', CurrentMeasureID = '{saleProductCurrentMeasureComboBox.SelectedIndex}', ProductUnitCount = {saleUnitCountTextBox.Text} WHERE ID = {saleIDTextBox.Text};");

            return true;
        }

        private void SaleDelete()
        {
            if (editMode == EditMode.CheckView)
            {
                if (saleCheckGridView.FocusedRowHandle < 0) return;

                SqlConnector.ExNonQuery($"DELETE FROM CheckTable WHERE ID = {saleCheckGridView.GetFocusedRowCellValue("ID")};DELETE FROM SaleTable WHERE CheckID = {saleCheckGridView.GetFocusedRowCellValue("ID")}");
            }
            //deleted last sale in early created check then check also delete
            else
            {
                if (saleGridView.FocusedRowHandle < 0) return;

                //delete sale and check if it was last sale in this check and return value 
                if (SqlConnector.ExQuery($"DELETE FROM SaleTable WHERE CheckID = {saleGridView.GetFocusedRowCellValue("CheckID")} AND ID = {saleGridView.GetFocusedRowCellValue("ID")};DECLARE @ROWCOUNT AS INT;SELECT @ROWCOUNT = COUNT(ID) FROM SaleTable WHERE CheckID = {saleGridView.GetFocusedRowCellValue("CheckID")};IF (@ROWCOUNT = 0)BEGIN DELETE FROM CheckTable WHERE ID = {saleGridView.GetFocusedRowCellValue("CheckID")};SELECT 0;END").Rows.Count > 0)
                {
                    if (saleCheckIDTextBox.Text != "New") SaleEditPanelClose();
                }
            }

            GridRefresh();
        }

        private void SaleOk()
        {
            //if readmode true then exit behave like a cancel button
            if ((editMode == EditMode.CheckEdit && saleCustomerIDLookUp.ReadOnly) || (editMode == EditMode.SaleEdit && saleProductIDLookUp.ReadOnly))
            {
                SaleCancelButton_Click(null, null);

                return;
            }

            if (editMode == EditMode.CheckEdit)
            {
                if (saleCustomerIDLookUp.EditValue.ToString() == "")
                {
                    XtraMessageBox.Show("Input Customer ID", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (saleCustomerAmountPaidTextBox.Text == "")
                {
                    XtraMessageBox.Show("Input Customer Amount Paid", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //if all testing is succesed
            bool isProcessSuccesed;

            //if 
            if (editMode == EditMode.CheckEdit)
            {
                if (saleCheckIDTextBox.Text == "New") isProcessSuccesed = SaleCreate();//if is create mode
                else isProcessSuccesed = SaleUpdate();//if is update mode

                if (isProcessSuccesed) SaleEditPanelClose();
            }

            else
            {
                if (saleIDTextBox.Text == "New") isProcessSuccesed = SaleCreate();//if is create mode
                else isProcessSuccesed = SaleUpdate();//if is update mode

                if (isProcessSuccesed) SaleEditPanelClose();
            }
        }

        //check if all neccesery fields filled
        private bool DataTest()
        {
            if (editMode == EditMode.SaleEdit)
            {
                if (saleIDTextBox.Text == "")
                {
                    XtraMessageBox.Show("Input ID", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (saleCheckIDTextBox.Text == "")
                {
                    XtraMessageBox.Show("Input Check ID", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (saleProductIDLookUp.EditValue.ToString() == "")
                {
                    XtraMessageBox.Show("Input Product ID", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (saleProductCurrentMeasureComboBox.Text == "")
                {
                    XtraMessageBox.Show("Input Current Measure", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (saleUnitCountTextBox.Text == "")
                {
                    XtraMessageBox.Show("Input Unit Count", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }

        private void ProductFieldsUpdate()
        {
            if (saleProductIDLookUp.EditValue.ToString() == null || saleProductIDLookUp.EditValue.ToString() == "") return;

            DataTable dt = SqlConnector.ExQuery($"SELECT pt.ID, cmt.Measure, pt.Name, pt.Price, pmct.ConvertValue0,pmct.ConvertValue1,pmct.ConvertValue2,pmct.ConvertValue3 FROM ProductTable pt JOIN ConstMeasureTable cmt ON cmt.ID = pt.MainMeasureID JOIN ProductMeasureConvertTable pmct ON pt.ID = pmct.ProductID WHERE pt.ID = {saleProductIDLookUp.EditValue}");

            saleProductMainMeasureTextBox.Text = dt.Rows[0][1].ToString();
            saleProductNameTextBox.Text = dt.Rows[0][2].ToString();
            saleProductunitPriceTextBox.Text = dt.Rows[0][3].ToString();
            saleFromKGTextBox.Text = dt.Rows[0][4].ToString();
            saleFromLTextBox.Text = dt.Rows[0][5].ToString();
            saleFromBOXTextBox.Text = dt.Rows[0][6].ToString();
            saleFromBOX_X2TextBox.Text = dt.Rows[0][7].ToString();
        }

        private void NewSalePanelFilling(string ID, string checkID)
        {
            saleIDTextBox.Text = ID;

            DataTable dt = SqlConnector.ExQuery($"SELECT pt.ID, cmt1.Measure, st.ProductUnitCount, cmt2.Measure, pt.Name, pt.Price, pmct.ConvertValue0,pmct.ConvertValue1,pmct.ConvertValue2,pmct.ConvertValue3 FROM SaleTable st  JOIN ProductTable pt  ON st.ProductID = pt.ID JOIN ConstMeasureTable cmt1 ON cmt1.ID = st.CurrentMeasureID JOIN ConstMeasureTable cmt2 ON cmt2.ID = pt.MainMeasureID JOIN ProductMeasureConvertTable pmct ON pt.ID = pmct.ProductID WHERE st.ID = {ID} AND st.CheckID = {checkID}");

            saleProductIDLookUp.ItemIndex = saleProductIDLookUp.Properties.GetDataSourceRowIndex("ID", dt.Rows[0][0].ToString());
            saleProductCurrentMeasureComboBox.Text = dt.Rows[0][1].ToString();
            saleUnitCountTextBox.Text = dt.Rows[0][2].ToString();
            saleProductMainMeasureTextBox.Text = dt.Rows[0][3].ToString();
            saleProductNameTextBox.Text = dt.Rows[0][4].ToString();
            saleProductunitPriceTextBox.Text = dt.Rows[0][5].ToString();
            saleFromKGTextBox.Text = dt.Rows[0][6].ToString();
            saleFromLTextBox.Text = dt.Rows[0][7].ToString();
            saleFromBOXTextBox.Text = dt.Rows[0][8].ToString();
            saleFromBOX_X2TextBox.Text = dt.Rows[0][9].ToString();
        }

        private void NewCheckPanelFilling(string checkID)
        {
            saleCheckIDTextBox.Text = checkID;

            DataTable dt = SqlConnector.ExQuery($"SELECT cht.CustomerID, dbo.CUSTOMER_AMOUNT_PRICE_ALL_SALES_BY_CHECK(cht.ID) AS [Amount Price], cht.AmountPaid AS [Amount Paid], cst.Name FROM CheckTable cht JOIN CustomerTable cst ON cht.CustomerID = cst.ID WHERE cht.ID = {checkID}");

            saleCustomerIDLookUp.ItemIndex = saleCustomerIDLookUp.Properties.GetDataSourceRowIndex("ID", dt.Rows[0][0].ToString());
            saleAmountPriceTextBox.Text = dt.Rows[0][1].ToString();
            saleCustomerAmountPaidTextBox.Text = dt.Rows[0][2].ToString();
            saleCustomerNameTextBox.Text = dt.Rows[0][3].ToString();
        }

        private void EditPanelClear()
        {
            saleCheckIDTextBox.Text = "New";
            saleAmountPriceTextBox.Text = "";
            saleCustomerAmountPaidTextBox.Text = "";
            saleIDTextBox.Text = "New";
            saleProductCurrentMeasureComboBox.Text = "";
            saleUnitCountTextBox.Text = "";
        }

        private void SaleEditPanelOpen(bool isCreateMode, bool isReadMode)
        {
            //return if grid empty and it is not create mode
            if (!isCreateMode)
            {
                if (editMode == EditMode.CheckView && saleCheckGridView.FocusedRowHandle < 0) return;
                if (editMode == EditMode.CheckEdit && saleGridView.FocusedRowHandle < 0) return;
            }

            if (editMode == EditMode.CheckView)
            {
                saleCustomerIDLookUp.ReadOnly = isReadMode;
                saleCustomerAmountPaidTextBox.ReadOnly = isReadMode;
            }

            else
            {
                saleProductIDLookUp.ReadOnly = isReadMode;
                saleUnitCountTextBox.ReadOnly = isReadMode;
                saleProductCurrentMeasureComboBox.ReadOnly = isReadMode;
            }

            if (editMode == EditMode.CheckView)
            {
                saleButtonsDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                saleEditSaleDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                saleEditCheckDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;

                saleCheckGridControl.Visible = false;
                saleGridControl.Visible = true;

                if (isCreateMode) EditPanelClear();

                else NewCheckPanelFilling(saleCheckGridView.GetFocusedRowCellValue("ID").ToString());

                editMode = EditMode.CheckEdit;
            }

            else
            {
                if (isCreateMode && !DataTest()) return;

                saleButtonsDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                saleEditSaleDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                saleEditCheckDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

                saleCheckGridControl.Visible = false;
                saleGridControl.Visible = false;

                if (isCreateMode) EditPanelClear();

                else NewSalePanelFilling(saleGridView.GetFocusedRowCellValue("ID").ToString(), saleGridView.GetFocusedRowCellValue("CheckID").ToString());

                editMode = EditMode.SaleEdit;
            }

            GridRefresh();
        }

        private void SaleEditPanelClose()
        {
            if (editMode == EditMode.CheckEdit)
            {
                saleButtonsDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                saleEditCheckDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

                saleCheckGridControl.Visible = true;
                saleGridControl.Visible = false;

                editMode = EditMode.CheckView;
            }

            else
            {
                saleButtonsDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                saleEditSaleDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                saleEditCheckDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;

                saleCheckGridControl.Visible = false;
                saleGridControl.Visible = true;

                editMode = EditMode.CheckEdit;
            }

            GridRefresh();
        }
    }
}
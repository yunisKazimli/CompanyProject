using DevExpress.XtraEditors;
using System;
using System.Data;
using System.Windows.Forms;

namespace CompanyProject
{
    public partial class productMiniForm : XtraForm
    {
        public productMiniForm()
        {
            InitializeComponent();

            productMainMeasureTextBox.Properties.Items.Clear();

            DataTable dt = SqlConnector.ExQuery("SELECT Measure FROM ConstMeasureTable");

            for(int i = 0; i < dt.Rows.Count; i++) productMainMeasureTextBox.Properties.Items.Add(dt.Rows[i][0].ToString());

            GridRefresh();
        }

        private void productsMiniForm_Load(object sender, EventArgs e)
        {
        }

        private void productCreateButton_Click(object sender, EventArgs e)
        {
            ProductEditPanelOpen(true ,false);
        }

        private void productUpdateButton_Click(object sender, EventArgs e)
        {
            ProductEditPanelOpen(false, false);
        }

        private void productRefreshButton_Click(object sender, EventArgs e)
        {
            GridRefresh();
        }

        private void productReadButton_Click(object sender, EventArgs e)
        {
            ProductEditPanelOpen(false, true);
        }

        private void productDeleteButton_Click(object sender, EventArgs e)
        {
            ProductDelete();
        }

        private void productOkButton_Click(object sender, EventArgs e)
        {
            ProductOk();
        }

        private void productCancelButton_Click(object sender, EventArgs e)
        {
            ProductEditPanelClose();
        }

        private void productMainMeasureTextBox_SelectedValueChanged(object sender, EventArgs e)
        {
            MeasureConvertFieldsRefresh();
        }

        private void GridRefresh()
        {
            DataTable dt = SqlConnector.ExQuery("SELECT pt.Name,  pt.ID, cmt.Measure AS [Main Measure], pt.Price AS [Unit Price] FROM ProductTable pt JOIN ConstMeasureTable cmt ON pt.MainMeasureID = cmt.ID");

            productGridControl.DataSource = dt;
        }

        //check if all necessary fields was filled
        private bool DataTest()
        {
            if (productNameTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Name", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (productIDTextBox.Text.ToString() == "")
            {
                XtraMessageBox.Show("Input ID", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (productMainMeasureTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Main Measure", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (productUnitPriceTextBox.Text == "")
            {
                XtraMessageBox.Show("Input Unit Price", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (productFromKGTextBox.Text == "")
            {
                XtraMessageBox.Show("Input From KG", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (productFromLTextBox.Text == "")
            {
                XtraMessageBox.Show("Input From L", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (productFromBOXTextBox.Text == "")
            {
                XtraMessageBox.Show("Input From BOX", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (productFromBOX_X2TextBox.Text == "")
            {
                XtraMessageBox.Show("Input From BOX_X2", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void NewProductPanelFilling(string fillingProductID)
        {
            DataTable dt = SqlConnector.ExQuery($"SELECT pt.Name, pt.ID, cmt.Measure, pt.Price, pmct.ConvertValue0,pmct.ConvertValue1,pmct.ConvertValue2,pmct.ConvertValue3 FROM ProductTable pt JOIN ConstMeasureTable cmt ON pt.MainMeasureID = cmt.ID JOIN ProductMeasureConvertTable pmct ON pt.ID = pmct.ProductID WHERE pt.ID = {fillingProductID}");

            if (dt.Rows.Count == 0) return;

            productNameTextBox.Text = dt.Rows[0][0].ToString();
            productIDTextBox.Text = dt.Rows[0][1].ToString();
            productMainMeasureTextBox.Text = dt.Rows[0][2].ToString();
            productUnitPriceTextBox.Text = dt.Rows[0][3].ToString();
            productFromKGTextBox.Text = dt.Rows[0][4].ToString();
            productFromLTextBox.Text = dt.Rows[0][5].ToString();
            productFromBOXTextBox.Text = dt.Rows[0][6].ToString();
            productFromBOX_X2TextBox.Text = dt.Rows[0][7].ToString();

            MeasureConvertFieldsRefresh();
        }

        private void NewProductPanelClear()
        {
            productUnitPriceTextBox.Text = "";
            productMainMeasureTextBox.Text = "";
            productIDTextBox.Text = "New";
            productNameTextBox.Text = "";
            productFromKGTextBox.Text = "";
            productFromLTextBox.Text = "";
            productFromBOXTextBox.Text = "";
            productFromBOX_X2TextBox.Text = "";
        }

        //return false if something is wrong for creating
        private bool ProductCreate()
        {
            if (!DataTest()) return false;

            //find new id from table and use it for inserting new customed data
            if (SqlConnector.ExQuery($"SELECT * FROM ProductTable pt JOIN ProductMeasureConvertTable pmct ON pt.ID = pmct.ProductID JOIN ConstMeasureTable cmt ON pt.MainMeasureID = cmt.ID WHERE pt.Name = '{productNameTextBox.Text}' AND pt.Price = {productUnitPriceTextBox.Text} AND pmct.ConvertValue0 = {productFromKGTextBox.Text} AND pmct.ConvertValue1 = {productFromLTextBox.Text} AND pmct.ConvertValue2 = {productFromBOXTextBox.Text} AND pmct.ConvertValue3 = {productFromBOX_X2TextBox.Text} AND cmt.Measure = '{productMainMeasureTextBox.Text}'").Rows.Count != 0)
            {
                XtraMessageBox.Show("Input Unique Product", "Has Found Same Product", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            SqlConnector.ExNonQuery($"DECLARE @NEWID AS INT;SELECT @NEWID = MAX(ID) FROM ProductTable;IF (@NEWID IS NULL) BEGIN SELECT @NEWID = 0 END ELSE BEGIN SELECT @NEWID = @NEWID + 1 END;INSERT INTO ProductTable VALUES(@NEWID,{productMainMeasureTextBox.SelectedIndex},'{productNameTextBox.Text}',{productUnitPriceTextBox.Text});INSERT INTO ProductMeasureConvertTable VALUES(@NEWID,'{productFromKGTextBox.Text}','{productFromLTextBox.Text}','{productFromBOXTextBox.Text}','{productFromBOX_X2TextBox.Text}')");

            GridRefresh();

            productGridView.FocusedRowHandle = productGridView.RowCount - 1;

            return true;
        }

        private void ProductOk()
        {
            //if readmode true then exit behave like a cancel button
            if (productNameTextBox.ReadOnly)
            {
                productCancelButton_Click(null, null);
                return;
            }
            //if all testing is succesed
            bool isProcessSuccesed;

            if (productIDTextBox.Text == "New") isProcessSuccesed = ProductCreate();//if is create mode
            else isProcessSuccesed = ProductUpdate();//if is update mode

            if (isProcessSuccesed) ProductEditPanelClose();
        }

        //return false if something is wrong for updating
        private bool ProductUpdate()
        {
            if (!DataTest()) return false;

            SqlConnector.ExNonQuery($"UPDATE ProductTable SET Name = '{productNameTextBox.Text}',MainMeasureID = {productMainMeasureTextBox.SelectedIndex}, Price = {productUnitPriceTextBox.Text} WHERE ProductTable.ID = {productIDTextBox.Text};UPDATE ProductMeasureConvertTable SET ConvertValue0 = {productFromKGTextBox.Text},ConvertValue1 = {productFromLTextBox.Text},ConvertValue2 = {productFromBOXTextBox.Text},ConvertValue3 = {productFromBOX_X2TextBox.Text} WHERE ProductID = {productIDTextBox.Text}");

            GridRefresh();

            return true;
        }

        private void ProductDelete()
        {
            //return if grid is empty
            if (productGridView.FocusedRowHandle < 0) return;

            if (SqlConnector.ExQuery($"SELECT * FROM SaleTable WHERE ProductID = {productGridView.GetFocusedRowCellValue("ID")}").Rows.Count != 0)
            {
                XtraMessageBox.Show("Cannot Delete This Product Because This ID Used In Sales", "Invalid Field", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            SqlConnector.ExNonQuery($"DELETE FROM ProductTable WHERE ID = {productGridView.GetFocusedRowCellValue("ID")};DELETE FROM ProductMeasureConvertTable WHERE ProductID = {productGridView.GetFocusedRowCellValue("ID")}") ;

            GridRefresh();
        }

        private void MeasureConvertFieldsRefresh()
        {
            DataTable dt = SqlConnector.ExQuery($"SELECT cmt.Measure, pmct.convertValue0,pmct.convertValue1,pmct.convertValue2,pmct.convertValue3 FROM ProductTable pt JOIN ProductMeasureConvertTable pmct ON pt.ID = pmct.ProductID JOIN ConstMeasureTable cmt ON pt.MainMeasureID = cmt.ID WHERE pt.ID = {(productIDTextBox.Text == "New" ? "-1" : productIDTextBox.Text)}");
             
            if (dt.Rows.Count == 0)
            {
                productFromKGTextBox.Text = "1";
                productFromLTextBox.Text = "1";
                productFromBOXTextBox.Text = "1";
                productFromBOX_X2TextBox.Text = "1";

                productFromKGTextBox.ReadOnly = productMainMeasureTextBox.Text == "KG";
                productFromLTextBox.ReadOnly = productMainMeasureTextBox.Text == "L";
                productFromBOXTextBox.ReadOnly = productMainMeasureTextBox.Text == "BOX";
                productFromBOX_X2TextBox.ReadOnly = productMainMeasureTextBox.Text == "BOX_X2";

                return;
            }

            if (productMainMeasureTextBox.Text == dt.Rows[0][0].ToString())
            {
                productFromKGTextBox.Text = dt.Rows[0][1].ToString();
                productFromLTextBox.Text = dt.Rows[0][2].ToString();
                productFromBOXTextBox.Text = dt.Rows[0][3].ToString();
                productFromBOX_X2TextBox.Text = dt.Rows[0][4].ToString();
            }

            else
            {
                productFromKGTextBox.Text = "1";
                productFromLTextBox.Text = "1";
                productFromBOXTextBox.Text = "1";
                productFromBOX_X2TextBox.Text = "1";

                productFromKGTextBox.ReadOnly = productMainMeasureTextBox.Text == "KG";
                productFromLTextBox.ReadOnly = productMainMeasureTextBox.Text == "L";
                productFromBOXTextBox.ReadOnly = productMainMeasureTextBox.Text == "BOX";
                productFromBOX_X2TextBox.ReadOnly = productMainMeasureTextBox.Text == "BOX_X2";
            }
        }

        private void ProductEditPanelOpen(bool isCreateMood, bool isReadMode)
        {
            //return if grid empty and it is not create mode
            if (!((productGridView.FocusedRowHandle >= 0 && !isCreateMood) || isCreateMood)) return;

            productNameTextBox.ReadOnly = isReadMode;
            productMainMeasureTextBox.ReadOnly = isReadMode;
            productUnitPriceTextBox.ReadOnly = isReadMode;
            productFromKGTextBox.ReadOnly = isReadMode;
            productFromLTextBox.ReadOnly = isReadMode;
            productFromBOXTextBox.ReadOnly = isReadMode;
            productFromBOX_X2TextBox.ReadOnly = isReadMode;

            productButtonsDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
            productFormDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;

            productGridControl.Visible = false;
            
            if (isCreateMood) NewProductPanelClear();

            else NewProductPanelFilling(productGridView.GetFocusedRowCellValue("ID").ToString());
        }

        private void ProductEditPanelClose()
        {
            productButtonsDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            productFormDockPanel.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;

            productGridControl.Visible = true;
        }
    }
}
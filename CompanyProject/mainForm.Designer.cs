
namespace CompanyProject
{
    partial class mainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.productsNavBarGroup = new DevExpress.XtraNavBar.NavBarGroup();
            this.productsNavBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.customersNavBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.salesBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.measuresNavBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.measureConvertingBarItem = new DevExpress.XtraNavBar.NavBarItem();
            this.hideContainerLeft = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.dockPanel2_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.mainDockManager = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.autoHideContainer1 = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.mainGridsNavBarDockControl = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer2 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.mainGridsNavBarControl = new DevExpress.XtraNavBar.NavBarControl();
            this.controlContainer3 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.mainMiniFormPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.mainDockManager)).BeginInit();
            this.autoHideContainer1.SuspendLayout();
            this.mainGridsNavBarDockControl.SuspendLayout();
            this.controlContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainGridsNavBarControl)).BeginInit();
            this.SuspendLayout();
            // 
            // productsNavBarGroup
            // 
            this.productsNavBarGroup.Caption = "Product Grids";
            this.productsNavBarGroup.Expanded = true;
            this.productsNavBarGroup.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("productsNavBarGroup.ImageOptions.SvgImage")));
            this.productsNavBarGroup.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.productsNavBarItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.customersNavBarItem),
            new DevExpress.XtraNavBar.NavBarItemLink(this.salesBarItem)});
            this.productsNavBarGroup.Name = "productsNavBarGroup";
            // 
            // productsNavBarItem
            // 
            this.productsNavBarItem.Caption = "Products";
            this.productsNavBarItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("productsNavBarItem.ImageOptions.SvgImage")));
            this.productsNavBarItem.Name = "productsNavBarItem";
            this.productsNavBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.productsNavBarItem_LinkClicked);
            // 
            // customersNavBarItem
            // 
            this.customersNavBarItem.Caption = "Customers";
            this.customersNavBarItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("customersNavBarItem.ImageOptions.SvgImage")));
            this.customersNavBarItem.Name = "customersNavBarItem";
            this.customersNavBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.customersNavBarItem_LinkClicked);
            // 
            // salesBarItem
            // 
            this.salesBarItem.Caption = "Sales";
            this.salesBarItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("salesBarItem.ImageOptions.SvgImage")));
            this.salesBarItem.Name = "salesBarItem";
            this.salesBarItem.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.salesBarItem_LinkClicked);
            // 
            // measuresNavBarItem
            // 
            this.measuresNavBarItem.Caption = "Measures";
            this.measuresNavBarItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("measuresNavBarItem.ImageOptions.SvgImage")));
            this.measuresNavBarItem.Name = "measuresNavBarItem";
            // 
            // measureConvertingBarItem
            // 
            this.measureConvertingBarItem.Caption = "Measure Converting";
            this.measureConvertingBarItem.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("measureConvertingBarItem.ImageOptions.SvgImage")));
            this.measureConvertingBarItem.Name = "measureConvertingBarItem";
            // 
            // hideContainerLeft
            // 
            this.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.hideContainerLeft.Location = new System.Drawing.Point(0, 0);
            this.hideContainerLeft.Name = "hideContainerLeft";
            this.hideContainerLeft.Size = new System.Drawing.Size(21, 568);
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(193, 539);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // controlContainer1
            // 
            this.controlContainer1.Location = new System.Drawing.Point(0, 0);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(944, 539);
            this.controlContainer1.TabIndex = 0;
            // 
            // dockPanel2_Container
            // 
            this.dockPanel2_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel2_Container.Name = "dockPanel2_Container";
            this.dockPanel2_Container.Size = new System.Drawing.Size(193, 539);
            this.dockPanel2_Container.TabIndex = 0;
            // 
            // mainDockManager
            // 
            this.mainDockManager.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.autoHideContainer1});
            this.mainDockManager.Form = this;
            this.mainDockManager.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // autoHideContainer1
            // 
            this.autoHideContainer1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.autoHideContainer1.Controls.Add(this.mainGridsNavBarDockControl);
            this.autoHideContainer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.autoHideContainer1.Location = new System.Drawing.Point(0, 0);
            this.autoHideContainer1.Name = "autoHideContainer1";
            this.autoHideContainer1.Size = new System.Drawing.Size(21, 574);
            // 
            // mainGridsNavBarDockControl
            // 
            this.mainGridsNavBarDockControl.Controls.Add(this.controlContainer2);
            this.mainGridsNavBarDockControl.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.mainGridsNavBarDockControl.ID = new System.Guid("8ef0f085-c37c-4c02-9136-7b6556535ea8");
            this.mainGridsNavBarDockControl.Location = new System.Drawing.Point(0, 0);
            this.mainGridsNavBarDockControl.Name = "mainGridsNavBarDockControl";
            this.mainGridsNavBarDockControl.Options.ShowAutoHideButton = false;
            this.mainGridsNavBarDockControl.Options.ShowCloseButton = false;
            this.mainGridsNavBarDockControl.OriginalSize = new System.Drawing.Size(200, 200);
            this.mainGridsNavBarDockControl.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.mainGridsNavBarDockControl.SavedIndex = 0;
            this.mainGridsNavBarDockControl.Size = new System.Drawing.Size(200, 574);
            this.mainGridsNavBarDockControl.Text = "Grids";
            this.mainGridsNavBarDockControl.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // controlContainer2
            // 
            this.controlContainer2.Controls.Add(this.mainGridsNavBarControl);
            this.controlContainer2.Location = new System.Drawing.Point(3, 26);
            this.controlContainer2.Name = "controlContainer2";
            this.controlContainer2.Size = new System.Drawing.Size(193, 545);
            this.controlContainer2.TabIndex = 0;
            // 
            // mainGridsNavBarControl
            // 
            this.mainGridsNavBarControl.ActiveGroup = this.productsNavBarGroup;
            this.mainGridsNavBarControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainGridsNavBarControl.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.productsNavBarGroup});
            this.mainGridsNavBarControl.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.salesBarItem});
            this.mainGridsNavBarControl.Location = new System.Drawing.Point(0, 0);
            this.mainGridsNavBarControl.Name = "mainGridsNavBarControl";
            this.mainGridsNavBarControl.OptionsNavPane.ExpandedWidth = 193;
            this.mainGridsNavBarControl.Size = new System.Drawing.Size(193, 545);
            this.mainGridsNavBarControl.TabIndex = 0;
            this.mainGridsNavBarControl.Text = "navBarControl1";
            // 
            // controlContainer3
            // 
            this.controlContainer3.Location = new System.Drawing.Point(0, 0);
            this.controlContainer3.Name = "controlContainer3";
            this.controlContainer3.Size = new System.Drawing.Size(765, 539);
            this.controlContainer3.TabIndex = 0;
            // 
            // mainMiniFormPanel
            // 
            this.mainMiniFormPanel.AutoSize = true;
            this.mainMiniFormPanel.BackColor = System.Drawing.SystemColors.Control;
            this.mainMiniFormPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainMiniFormPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMiniFormPanel.Location = new System.Drawing.Point(21, 0);
            this.mainMiniFormPanel.Name = "mainMiniFormPanel";
            this.mainMiniFormPanel.Size = new System.Drawing.Size(950, 574);
            this.mainMiniFormPanel.TabIndex = 6;
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 574);
            this.Controls.Add(this.mainMiniFormPanel);
            this.Controls.Add(this.autoHideContainer1);
            this.IsMdiContainer = true;
            this.Name = "mainForm";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.mainDockManager)).EndInit();
            this.autoHideContainer1.ResumeLayout(false);
            this.mainGridsNavBarDockControl.ResumeLayout(false);
            this.controlContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mainGridsNavBarControl)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraNavBar.NavBarGroup productsNavBarGroup;
        private DevExpress.XtraNavBar.NavBarItem productsNavBarItem;
        private DevExpress.XtraNavBar.NavBarItem customersNavBarItem;
        private DevExpress.XtraNavBar.NavBarItem measuresNavBarItem;
        private DevExpress.XtraNavBar.NavBarItem measureConvertingBarItem;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerLeft;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel2_Container;
        private DevExpress.XtraBars.Docking.DockPanel mainGridsNavBarDockControl;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer2;
        private DevExpress.XtraBars.Docking.DockManager mainDockManager;
        private DevExpress.XtraNavBar.NavBarControl mainGridsNavBarControl;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer3;
        private DevExpress.XtraNavBar.NavBarItem salesBarItem;
        private DevExpress.XtraBars.Docking.AutoHideContainer autoHideContainer1;
        private System.Windows.Forms.Panel mainMiniFormPanel;
    }
}


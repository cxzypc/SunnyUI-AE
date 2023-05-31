namespace cs
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode29 = new System.Windows.Forms.TreeNode("MXD文件");
            System.Windows.Forms.TreeNode treeNode30 = new System.Windows.Forms.TreeNode("矢量数据");
            System.Windows.Forms.TreeNode treeNode31 = new System.Windows.Forms.TreeNode("栅格数据");
            System.Windows.Forms.TreeNode treeNode32 = new System.Windows.Forms.TreeNode("数据添加", new System.Windows.Forms.TreeNode[] {
            treeNode29,
            treeNode30,
            treeNode31});
            System.Windows.Forms.TreeNode treeNode33 = new System.Windows.Forms.TreeNode("保存地图");
            System.Windows.Forms.TreeNode treeNode34 = new System.Windows.Forms.TreeNode("地图另存为");
            System.Windows.Forms.TreeNode treeNode35 = new System.Windows.Forms.TreeNode("出图大小");
            System.Windows.Forms.TreeNode treeNode36 = new System.Windows.Forms.TreeNode("保存", new System.Windows.Forms.TreeNode[] {
            treeNode33,
            treeNode34,
            treeNode35});
            System.Windows.Forms.TreeNode treeNode37 = new System.Windows.Forms.TreeNode("指北针");
            System.Windows.Forms.TreeNode treeNode38 = new System.Windows.Forms.TreeNode("比例尺");
            System.Windows.Forms.TreeNode treeNode39 = new System.Windows.Forms.TreeNode("图例");
            System.Windows.Forms.TreeNode treeNode40 = new System.Windows.Forms.TreeNode("图名");
            System.Windows.Forms.TreeNode treeNode41 = new System.Windows.Forms.TreeNode("导出地图");
            System.Windows.Forms.TreeNode treeNode42 = new System.Windows.Forms.TreeNode("地图三要素", new System.Windows.Forms.TreeNode[] {
            treeNode37,
            treeNode38,
            treeNode39,
            treeNode40,
            treeNode41});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.uiNavBar1 = new Sunny.UI.UINavBar();
            this.uiPanel1 = new Sunny.UI.UIPanel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.uiPanel2 = new Sunny.UI.UIPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.axTOCControl1 = new ESRI.ArcGIS.Controls.AxTOCControl();
            this.uiTabControl1 = new Sunny.UI.UITabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.axMapControl1 = new ESRI.ArcGIS.Controls.AxMapControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.axPageLayoutControl1 = new ESRI.ArcGIS.Controls.AxPageLayoutControl();
            this.uiContextMenuStrip1 = new Sunny.UI.UIContextMenuStrip();
            this.缩放至图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移除图层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.查看属性表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.图层渲染ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.axToolbarControl1 = new ESRI.ArcGIS.Controls.AxToolbarControl();
            this.axLicenseControl1 = new ESRI.ArcGIS.Controls.AxLicenseControl();
            this.uiContextMenuStrip2 = new Sunny.UI.UIContextMenuStrip();
            this.栅格添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.矢量添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.移动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.测试3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scale测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ttToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getWHToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.uiPanel1.SuspendLayout();
            this.uiPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).BeginInit();
            this.uiTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).BeginInit();
            this.uiContextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).BeginInit();
            this.uiContextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // uiNavBar1
            // 
            this.uiNavBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiNavBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiNavBar1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiNavBar1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiNavBar1.Location = new System.Drawing.Point(0, 35);
            this.uiNavBar1.MenuHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.uiNavBar1.MenuSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.uiNavBar1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiNavBar1.Name = "uiNavBar1";
            this.uiNavBar1.NodeAlignment = System.Drawing.StringAlignment.Near;
            this.uiNavBar1.NodeInterval = 0;
            treeNode29.Name = "节点1";
            treeNode29.Text = "MXD文件";
            treeNode30.Name = "节点2";
            treeNode30.Text = "矢量数据";
            treeNode31.Name = "节点3";
            treeNode31.Text = "栅格数据";
            treeNode32.Name = "节点0";
            treeNode32.Text = "数据添加";
            treeNode33.Name = "节点1";
            treeNode33.Text = "保存地图";
            treeNode34.Name = "节点2";
            treeNode34.Text = "地图另存为";
            treeNode35.Name = "节点0";
            treeNode35.Text = "出图大小";
            treeNode36.Name = "节点0";
            treeNode36.Text = "保存";
            treeNode37.Name = "节点4";
            treeNode37.Text = "指北针";
            treeNode38.Name = "节点5";
            treeNode38.Text = "比例尺";
            treeNode39.Name = "节点0";
            treeNode39.Text = "图例";
            treeNode40.Name = "节点0";
            treeNode40.Text = "图名";
            treeNode41.Name = "节点0";
            treeNode41.Text = "导出地图";
            treeNode42.Name = "节点0";
            treeNode42.Text = "地图三要素";
            this.uiNavBar1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode32,
            treeNode36,
            treeNode42});
            this.uiNavBar1.Size = new System.Drawing.Size(944, 46);
            this.uiNavBar1.Style = Sunny.UI.UIStyle.Custom;
            this.uiNavBar1.TabIndex = 1;
            this.uiNavBar1.Text = "uiNavBar1";
            this.uiNavBar1.MenuItemClick += new Sunny.UI.UINavBar.OnMenuItemClick(this.uiNavBar1_MenuItemClick);
            // 
            // uiPanel1
            // 
            this.uiPanel1.Controls.Add(this.uiLabel1);
            this.uiPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.uiPanel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel1.Location = new System.Drawing.Point(0, 745);
            this.uiPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel1.Name = "uiPanel1";
            this.uiPanel1.Size = new System.Drawing.Size(944, 29);
            this.uiPanel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel1.TabIndex = 3;
            this.uiPanel1.Text = null;
            this.uiPanel1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // uiLabel1
            // 
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiLabel1.Location = new System.Drawing.Point(0, 3);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(737, 23);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 0;
            this.uiLabel1.Text = "坐标值";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiPanel2
            // 
            this.uiPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiPanel2.Controls.Add(this.splitContainer1);
            this.uiPanel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiPanel2.Location = new System.Drawing.Point(0, 109);
            this.uiPanel2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiPanel2.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPanel2.Name = "uiPanel2";
            this.uiPanel2.Size = new System.Drawing.Size(944, 636);
            this.uiPanel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiPanel2.TabIndex = 4;
            this.uiPanel2.Text = null;
            this.uiPanel2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.axTOCControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.uiTabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(944, 636);
            this.splitContainer1.SplitterDistance = 203;
            this.splitContainer1.TabIndex = 0;
            // 
            // axTOCControl1
            // 
            this.axTOCControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axTOCControl1.Location = new System.Drawing.Point(0, 0);
            this.axTOCControl1.Name = "axTOCControl1";
            this.axTOCControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTOCControl1.OcxState")));
            this.axTOCControl1.Size = new System.Drawing.Size(203, 636);
            this.axTOCControl1.TabIndex = 0;
            this.axTOCControl1.OnMouseDown += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseDownEventHandler(this.axTOCControl1_OnMouseDown);
            this.axTOCControl1.OnMouseUp += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnMouseUpEventHandler(this.axTOCControl1_OnMouseUp);
            this.axTOCControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.ITOCControlEvents_Ax_OnDoubleClickEventHandler(this.axTOCControl1_OnDoubleClick);
            // 
            // uiTabControl1
            // 
            this.uiTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.uiTabControl1.Controls.Add(this.tabPage1);
            this.uiTabControl1.Controls.Add(this.tabPage2);
            this.uiTabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControl1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiTabControl1.ItemSize = new System.Drawing.Size(150, 40);
            this.uiTabControl1.Location = new System.Drawing.Point(0, 0);
            this.uiTabControl1.MainPage = "";
            this.uiTabControl1.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiTabControl1.Multiline = true;
            this.uiTabControl1.Name = "uiTabControl1";
            this.uiTabControl1.SelectedIndex = 0;
            this.uiTabControl1.Size = new System.Drawing.Size(737, 636);
            this.uiTabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControl1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTabControl1.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.uiTabControl1.TabIndex = 0;
            this.uiTabControl1.TabSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.uiTabControl1.TabUnSelectedForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.uiTabControl1.SelectedIndexChanged += new System.EventHandler(this.uiTabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.axMapControl1);
            this.tabPage1.Location = new System.Drawing.Point(0, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(737, 596);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "数据视图";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // axMapControl1
            // 
            this.axMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axMapControl1.Location = new System.Drawing.Point(0, 0);
            this.axMapControl1.Name = "axMapControl1";
            this.axMapControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axMapControl1.OcxState")));
            this.axMapControl1.Size = new System.Drawing.Size(737, 596);
            this.axMapControl1.TabIndex = 0;
            this.axMapControl1.OnMouseMove += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMouseMoveEventHandler(this.axMapControl1_OnMouseMove);
            this.axMapControl1.OnViewRefreshed += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnViewRefreshedEventHandler(this.axMapControl1_OnViewRefreshed);
            this.axMapControl1.OnAfterScreenDraw += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnAfterScreenDrawEventHandler(this.axMapControl1_OnAfterScreenDraw);
            this.axMapControl1.OnMapReplaced += new ESRI.ArcGIS.Controls.IMapControlEvents2_Ax_OnMapReplacedEventHandler(this.axMapControl1_OnMapReplaced);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.axPageLayoutControl1);
            this.tabPage2.Location = new System.Drawing.Point(0, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(737, 596);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "布局视图";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // axPageLayoutControl1
            // 
            this.axPageLayoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.axPageLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.axPageLayoutControl1.Name = "axPageLayoutControl1";
            this.axPageLayoutControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axPageLayoutControl1.OcxState")));
            this.axPageLayoutControl1.Size = new System.Drawing.Size(737, 596);
            this.axPageLayoutControl1.TabIndex = 0;
            this.axPageLayoutControl1.OnMouseDown += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnMouseDownEventHandler(this.axPageLayoutControl1_OnMouseDown);
            this.axPageLayoutControl1.OnDoubleClick += new ESRI.ArcGIS.Controls.IPageLayoutControlEvents_Ax_OnDoubleClickEventHandler(this.axPageLayoutControl1_OnDoubleClick);
            // 
            // uiContextMenuStrip1
            // 
            this.uiContextMenuStrip1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiContextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.uiContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.缩放至图层ToolStripMenuItem,
            this.移除图层ToolStripMenuItem,
            this.查看属性表ToolStripMenuItem,
            this.图层渲染ToolStripMenuItem});
            this.uiContextMenuStrip1.Name = "uiContextMenuStrip1";
            this.uiContextMenuStrip1.Size = new System.Drawing.Size(185, 132);
            // 
            // 缩放至图层ToolStripMenuItem
            // 
            this.缩放至图层ToolStripMenuItem.Name = "缩放至图层ToolStripMenuItem";
            this.缩放至图层ToolStripMenuItem.Size = new System.Drawing.Size(184, 32);
            this.缩放至图层ToolStripMenuItem.Text = "缩放至图层";
            this.缩放至图层ToolStripMenuItem.Click += new System.EventHandler(this.缩放至图层ToolStripMenuItem_Click);
            // 
            // 移除图层ToolStripMenuItem
            // 
            this.移除图层ToolStripMenuItem.Name = "移除图层ToolStripMenuItem";
            this.移除图层ToolStripMenuItem.Size = new System.Drawing.Size(184, 32);
            this.移除图层ToolStripMenuItem.Text = "移除图层";
            this.移除图层ToolStripMenuItem.Click += new System.EventHandler(this.移除图层ToolStripMenuItem_Click);
            // 
            // 查看属性表ToolStripMenuItem
            // 
            this.查看属性表ToolStripMenuItem.Name = "查看属性表ToolStripMenuItem";
            this.查看属性表ToolStripMenuItem.Size = new System.Drawing.Size(184, 32);
            this.查看属性表ToolStripMenuItem.Text = "查看属性表";
            this.查看属性表ToolStripMenuItem.Click += new System.EventHandler(this.查看属性表ToolStripMenuItem_Click);
            // 
            // 图层渲染ToolStripMenuItem
            // 
            this.图层渲染ToolStripMenuItem.Name = "图层渲染ToolStripMenuItem";
            this.图层渲染ToolStripMenuItem.Size = new System.Drawing.Size(184, 32);
            this.图层渲染ToolStripMenuItem.Text = "图层渲染";
            this.图层渲染ToolStripMenuItem.Click += new System.EventHandler(this.图层渲染ToolStripMenuItem_Click);
            // 
            // axToolbarControl1
            // 
            this.axToolbarControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.axToolbarControl1.Location = new System.Drawing.Point(0, 81);
            this.axToolbarControl1.Name = "axToolbarControl1";
            this.axToolbarControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axToolbarControl1.OcxState")));
            this.axToolbarControl1.Size = new System.Drawing.Size(944, 28);
            this.axToolbarControl1.TabIndex = 2;
            // 
            // axLicenseControl1
            // 
            this.axLicenseControl1.Enabled = true;
            this.axLicenseControl1.Location = new System.Drawing.Point(786, 35);
            this.axLicenseControl1.Name = "axLicenseControl1";
            this.axLicenseControl1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axLicenseControl1.OcxState")));
            this.axLicenseControl1.Size = new System.Drawing.Size(32, 32);
            this.axLicenseControl1.TabIndex = 0;
            // 
            // uiContextMenuStrip2
            // 
            this.uiContextMenuStrip2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.uiContextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.uiContextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.栅格添加ToolStripMenuItem,
            this.矢量添加ToolStripMenuItem,
            this.测试ToolStripMenuItem,
            this.测试2ToolStripMenuItem,
            this.移动ToolStripMenuItem,
            this.测试3ToolStripMenuItem,
            this.scale测试ToolStripMenuItem,
            this.ttToolStripMenuItem,
            this.getWHToolStripMenuItem});
            this.uiContextMenuStrip2.Name = "uiContextMenuStrip1";
            this.uiContextMenuStrip2.Size = new System.Drawing.Size(165, 292);
            // 
            // 栅格添加ToolStripMenuItem
            // 
            this.栅格添加ToolStripMenuItem.Name = "栅格添加ToolStripMenuItem";
            this.栅格添加ToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.栅格添加ToolStripMenuItem.Text = "栅格添加";
            this.栅格添加ToolStripMenuItem.Click += new System.EventHandler(this.栅格添加ToolStripMenuItem_Click);
            // 
            // 矢量添加ToolStripMenuItem
            // 
            this.矢量添加ToolStripMenuItem.Name = "矢量添加ToolStripMenuItem";
            this.矢量添加ToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.矢量添加ToolStripMenuItem.Text = "矢量添加";
            this.矢量添加ToolStripMenuItem.Click += new System.EventHandler(this.矢量添加ToolStripMenuItem_Click);
            // 
            // 测试ToolStripMenuItem
            // 
            this.测试ToolStripMenuItem.Name = "测试ToolStripMenuItem";
            this.测试ToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.测试ToolStripMenuItem.Text = "测试";
            this.测试ToolStripMenuItem.Click += new System.EventHandler(this.测试ToolStripMenuItem_Click);
            // 
            // 测试2ToolStripMenuItem
            // 
            this.测试2ToolStripMenuItem.Name = "测试2ToolStripMenuItem";
            this.测试2ToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.测试2ToolStripMenuItem.Text = "测试2";
            this.测试2ToolStripMenuItem.Click += new System.EventHandler(this.测试2ToolStripMenuItem_Click);
            // 
            // 移动ToolStripMenuItem
            // 
            this.移动ToolStripMenuItem.Name = "移动ToolStripMenuItem";
            this.移动ToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.移动ToolStripMenuItem.Text = "移动";
            this.移动ToolStripMenuItem.Click += new System.EventHandler(this.移动ToolStripMenuItem_Click);
            // 
            // 测试3ToolStripMenuItem
            // 
            this.测试3ToolStripMenuItem.Name = "测试3ToolStripMenuItem";
            this.测试3ToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.测试3ToolStripMenuItem.Text = "测试3";
            this.测试3ToolStripMenuItem.Click += new System.EventHandler(this.测试3ToolStripMenuItem_Click);
            // 
            // scale测试ToolStripMenuItem
            // 
            this.scale测试ToolStripMenuItem.Name = "scale测试ToolStripMenuItem";
            this.scale测试ToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.scale测试ToolStripMenuItem.Text = "修改比例";
            this.scale测试ToolStripMenuItem.Click += new System.EventHandler(this.scale测试ToolStripMenuItem_Click);
            // 
            // ttToolStripMenuItem
            // 
            this.ttToolStripMenuItem.Name = "ttToolStripMenuItem";
            this.ttToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.ttToolStripMenuItem.Text = "tt";
            this.ttToolStripMenuItem.Click += new System.EventHandler(this.ttToolStripMenuItem_Click);
            // 
            // getWHToolStripMenuItem
            // 
            this.getWHToolStripMenuItem.Name = "getWHToolStripMenuItem";
            this.getWHToolStripMenuItem.Size = new System.Drawing.Size(164, 32);
            this.getWHToolStripMenuItem.Text = "getW/H";
            this.getWHToolStripMenuItem.Click += new System.EventHandler(this.getWHToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(944, 774);
            this.Controls.Add(this.uiPanel2);
            this.Controls.Add(this.uiPanel1);
            this.Controls.Add(this.axToolbarControl1);
            this.Controls.Add(this.uiNavBar1);
            this.Controls.Add(this.axLicenseControl1);
            this.ForeColor = System.Drawing.Color.Silver;
            this.Name = "Form1";
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.uiPanel1.ResumeLayout(false);
            this.uiPanel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTOCControl1)).EndInit();
            this.uiTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axMapControl1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axPageLayoutControl1)).EndInit();
            this.uiContextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axToolbarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axLicenseControl1)).EndInit();
            this.uiContextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ESRI.ArcGIS.Controls.AxLicenseControl axLicenseControl1;
        private Sunny.UI.UINavBar uiNavBar1;
        private ESRI.ArcGIS.Controls.AxToolbarControl axToolbarControl1;
        private Sunny.UI.UIPanel uiPanel1;
        private Sunny.UI.UIPanel uiPanel2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private ESRI.ArcGIS.Controls.AxTOCControl axTOCControl1;
        private Sunny.UI.UITabControl uiTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ESRI.ArcGIS.Controls.AxMapControl axMapControl1;
        private ESRI.ArcGIS.Controls.AxPageLayoutControl axPageLayoutControl1;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UIContextMenuStrip uiContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 缩放至图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移除图层ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 查看属性表ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 图层渲染ToolStripMenuItem;
        private Sunny.UI.UIContextMenuStrip uiContextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 栅格添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 矢量添加ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 移动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 测试3ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scale测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ttToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem getWHToolStripMenuItem;
    }
}


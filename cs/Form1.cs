using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SpatialAnalystTools;
using ESRI.ArcGIS.SpatialAnalyst;
using ESRI.ArcGIS.Geoprocessor;
using ESRI.ArcGIS.AnalysisTools;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.DisplayUI;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using System.Runtime.InteropServices;


using Sunny.UI;

namespace cs
{
    public partial class Form1 : UIForm
    {
        IToolbarMenu m_toolbarMenu;
        IToolbarMenu m_toolbarMenu_temp;
        IToolbarMenu m_toolbarMenu_temp1;
        private INorthArrow m_NorthArrrow;
        private IScaleBar2 m_ScaleBar;
        private ITextSymbol m_Title;
        private IElement m_Element;
        private IElement m_ScaleBarElement;
        private IElement m_LegnedElement;
        private IElement m_TitleElement;

        IElement element1;

        IElement pEle;//看边界的
        IEnvelope pEnvelope;

        private string operation;
        private string tab1;
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.Desktop);
            IAoInitialize aoInitialize = new AoInitialize();
            esriLicenseStatus licenseStatus = esriLicenseStatus.esriLicenseUnavailable;
            licenseStatus = aoInitialize.Initialize(esriLicenseProductCode.esriLicenseProductCodeAdvanced);
            if (licenseStatus == esriLicenseStatus.esriLicenseNotInitialized)
            {
                MessageBox.Show("没有esriLicenseProductCodeArcInfo许可！");
                Application.Exit();

                
            }
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mTOCControl = axTOCControl1.Object as ITOCControl;
            FullExtentCommand();

            //axPageLayoutControl控件中右键菜单
            m_toolbarMenu = new ToolbarMenuClass();
            //m_toolbarMenu.AddItem("esriControls.ControlsEditingSketchContextMenu", 0, 0, false, esriCommandStyles.esriCommandStyleTextOnly);
            m_toolbarMenu.AddItem("esriControls.ControlsGroupCommand", 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem("esriControls.ControlsUngroupCommand", 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new ConvertToGraphics(), 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.AddItem(new FitToMargins(), 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu.SetHook(axPageLayoutControl1);

            m_toolbarMenu_temp = new ToolbarMenuClass();
            m_toolbarMenu_temp.AddItem("esriControls.ControlsGroupCommand", 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu_temp.AddItem("esriControls.ControlsUngroupCommand", 0, 0, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu_temp.SetHook(axPageLayoutControl1);

            m_toolbarMenu_temp1 = new ToolbarMenuClass();
            m_toolbarMenu_temp1.AddItem("esriControlTools.ControlsPageZoomWholePageCommand", 0, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu_temp1.AddItem("esriControlTools.ControlsPageZoomPageToLastExtentBackCommand", 0, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu_temp1.AddItem("esriControlTools.ControlsPageZoomPageToLastExtentForwardCommand", 0, -1, false, esriCommandStyles.esriCommandStyleIconAndText);
            m_toolbarMenu_temp1.SetHook(axPageLayoutControl1);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            Geoprocessor gp = new Geoprocessor { OverwriteOutput = true };
            ESRI.ArcGIS.AnalysisTools.Buffer buffer = new ESRI.ArcGIS.AnalysisTools.Buffer();

            buffer.in_features = @"C:\Users\Administrator\Desktop\大周镇斜坡单元 (1)\dzxpdy.shp";           //必须
            buffer.out_feature_class = @"C:\Users\Administrator\Desktop\大周镇斜坡单元 (1)\hhh.shp";
            buffer.buffer_distance_or_field = "200 Meters";
            object sev = null;
            try     //可以找出GP运行时的错误在哪里
            {
                // Execute the tool.
                gp.Execute(buffer, null);                             //必须输入地图描述
                Console.WriteLine(gp.GetMessages(ref sev));
            }
            catch (Exception ex)
            {
                // Print geoprocessing execution error messages.
                UIMessageBox.ShowWarning(gp.GetMessages(ref sev), false);
            }
            UIMessageBox.ShowSuccess("完成", false);
        }

        private void uiNavBar1_MenuItemClick(string itemText, int menuIndex, int pageIndex)
        {
            switch (menuIndex)
            {
                case 0:
                    switch (itemText)
                    {
                        case "MXD文件":
                            mxdload();
                            break;
                        case "矢量数据":
                            shpload();
                            break;
                        case "栅格数据":
                            tifload();
                            break;
                    }
                    break;
                case 1:
                    switch (itemText)
                    {
                        case "保存地图":
                            SaveMap();
                            break;
                        case "地图另存为":
                            SaveAsMap();
                            break;
                        case "出图大小":
                            MapSize();
                            break;
                    }
                    break;
                case 2:
                    switch (itemText)
                    {
                        case "指北针":
                            if (tab1 == "布局视图")
                                NorthArrow();
                            break;
                        case "比例尺":
                            if (tab1 == "布局视图")
                                ScaleBar();
                            break;
                        case "图例":
                            if (tab1 == "布局视图")
                                Legend();
                            break;
                        case "图名":
                            if (tab1 == "布局视图")
                                Title();
                            break;
                        case "导出地图":
                            if (tab1 == "布局视图")
                                ExportImage();
                            break;
                    }
                    break;
                //else if (itemText == "栅格数据") { shpload(); break; }
                //case 1:
                //case 2:

                //case 3:
            }
            //MessageBox.Show(itemText);
        }
        public void mxdload()
        {
            OpenFileDialog pOpenFileDialog1 = new OpenFileDialog();
            pOpenFileDialog1.Title = "添加文件";
            pOpenFileDialog1.Filter = "Map Doucument(*.mxd)|*.mxd;|ArcMap模板(*.mxt)|*.mxt;|发布地图文件(*.pmf)|*.pmf;|所有地图格式(*.mxd;*.mxt;*.pmf)|*.mxd;*.mxt;*.pmf";//筛选器语句
            pOpenFileDialog1.ShowDialog();
            axToolbarControl1.SetBuddyControl(axMapControl1);  //选择显示控制的为axMapControl1接口
            //axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
            axTOCControl1.SetBuddyControl(axMapControl1);
            string strFileName = pOpenFileDialog1.FileName;
            if (strFileName == "")
                return;
            if (axMapControl1.CheckMxFile(strFileName))
            {
                axMapControl1.LoadMxFile(strFileName);
            }
        }
        public void shpload()
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.Title = "添加文件";
            pOpenFileDialog.Filter = "Shape文件(*.shp)|*.shp";
            if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFileName = pOpenFileDialog.FileName;
                int pIndex = strFileName.LastIndexOf("\\");
                string pFilePath = strFileName.Substring(0, pIndex); //获取文件路径
                string pFileName = strFileName.Substring(pIndex + 1); //获取文件名 
                axTOCControl1.SetBuddyControl(axMapControl1);
                axToolbarControl1.SetBuddyControl(axMapControl1);
                //axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
                
                if (strFileName == "")
                    return;
                IWorkspaceFactory pWorkspaceFactory = new ShapefileWorkspaceFactory();
                //IWorkspaceFactory需要引用ESRI.ArcGIS.Geodatabase      ShapefileWorkspaceFactory需要引用ArcGIS.DataSourcesFile
                IFeatureWorkspace pFeatureWorkspace = (IFeatureWorkspace)pWorkspaceFactory.OpenFromFile(pFilePath, 0);
                IFeatureClass pFeatureClass = pFeatureWorkspace.OpenFeatureClass(pFileName);
                IFeatureLayer pFeatureLayer = new FeatureLayer();         //注意是FeatureLayer而不是IFeatureLayer;
                pFeatureLayer.FeatureClass = pFeatureClass;
                pFeatureLayer.Name = pFeatureClass.AliasName;
                axMapControl1.Map.AddLayer(pFeatureLayer);
                axMapControl1.ActiveView.Refresh();
            }
        }
        public void tifload()
        {
            OpenFileDialog pOpenFileDialog = new OpenFileDialog();
            pOpenFileDialog.Title = "添加文件";
            pOpenFileDialog.Filter = "栅格文件 (*.*)|*.bmp;*.tif;*.jpg;*.img|(*.bmp)|*.bmp|(*.tif)|*.tif|(*.jpg)|*.jpg|(*.img)|*.img";
            if (pOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                string strFileName11 = pOpenFileDialog.FileName;
                string pFilePath11 = System.IO.Path.GetDirectoryName(strFileName11);
                string pFileName11 = System.IO.Path.GetFileName(strFileName11);
                axTOCControl1.SetBuddyControl(axMapControl1);
                axToolbarControl1.SetBuddyControl(axMapControl1);
                //axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
                if (strFileName11 == "")
                    return;
                Bitmap curBitmap = (Bitmap)Image.FromFile(strFileName11);
                IWorkspaceFactory pWorkspaceFactory = new RasterWorkspaceFactory();
                IRasterWorkspace pRasterWorkspace = (IRasterWorkspace)pWorkspaceFactory.OpenFromFile(pFilePath11, 0);
                IRasterDataset pRasterDataset = pRasterWorkspace.OpenRasterDataset(pFileName11);
                IRasterPyramid pRasterPyramid = pRasterDataset as IRasterPyramid;
                if (pRasterPyramid != null)
                {
                    if (!(pRasterPyramid.Present))
                        pRasterPyramid.Create();
                }
                //因为无法使用 pRasterLayer.RasterDataset=pRasterDataset;   所以使用以下方法：
                IRaster pRaster = pRasterDataset.CreateDefaultRaster();   //定义一个栅格格式，用pRasterDataset创建一个栅格格式
                IRasterLayer pRasterLayer = new RasterLayer();
                pRasterLayer.CreateFromRaster(pRaster);      //在栅格图层中创建栅格图层，加载pRaster
                axMapControl1.Map.AddLayer(pRasterLayer);    //在主窗体内加入图层
                axMapControl1.ActiveView.Refresh();
            }
        }
        public void SaveMap()
        {
            string sMxdFileName = axMapControl1.DocumentFilename;
            IMapDocument pMapDocument = new MapDocumentClass();
            if (sMxdFileName != null && axMapControl1.CheckMxFile(sMxdFileName))
            {
                if (pMapDocument.get_IsReadOnly(sMxdFileName))
                {
                    UIMessageBox.ShowWarning("此地图为只读，不能保存",false);
                    pMapDocument.Close();
                    return;
                }
            }
            else
            {
                SaveFileDialog pSaveFileDialog = new SaveFileDialog();
                pSaveFileDialog.Title = "请选择保存路径";
                pSaveFileDialog.Filter = "ArcMap文档(*.mxd)|*.mxd;|ArcMap模板(*.mxt)|*.mxt";
                if (pSaveFileDialog.ShowDialog() == DialogResult.OK)     //控制保存的地图格式为.mxd或者.mxt，点击弹出窗口的确定
                    sMxdFileName = pSaveFileDialog.FileName;             //这个if...else一定要写，否则会报错
                else
                    return;
            }
            pMapDocument.New(sMxdFileName);
            pMapDocument.ReplaceContents(axMapControl1.Map as IMxdContents);

            IDocumentInfo2 pDocInfo = pMapDocument as IDocumentInfo2;     //修改Map Properties的接口
            pDocInfo.Comments = "hh";                                     //title-DocumentTitle;  author-Author  ;       

            pMapDocument.Save(pMapDocument.UsesRelativePaths, true);
            pMapDocument.Close();
            UIMessageBox.ShowSuccess("地图保存成功！", false);  //MXD地图生成成功
        }
        public void SaveAsMap()
        {
            string sMxdFileName = axMapControl1.DocumentFilename;
            IMapDocument pMapDocument = new MapDocumentClass();
            if (sMxdFileName != null && axMapControl1.CheckMxFile(sMxdFileName))
            {
                if (pMapDocument.get_IsReadOnly(sMxdFileName))
                {
                    UIMessageBox.ShowWarning("此地图为只读，不能保存",false);
                    pMapDocument.Close();
                    return;
                }
                else
                {
                    SaveFileDialog pSaveFileDialog = new SaveFileDialog();
                    pSaveFileDialog.Title = "请选择保存路径";
                    pSaveFileDialog.Filter = "ArcMap文档(*.mxd)|*.mxd;|ArcMap模板(*.mxt)|*.mxt";
                    if (pSaveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        sMxdFileName = pSaveFileDialog.FileName;
                        pMapDocument.New(sMxdFileName);
                        pMapDocument.ReplaceContents(axMapControl1.Map as IMxdContents);
                        pMapDocument.Save(true, true);
                        pMapDocument.Close();
                        UIMessageBox.ShowSuccess("地图保存成功！",false);
                    }
                    else
                        return;
                }
            }
        }
        public void MapSize() {
            MapPageSize MPS = new MapPageSize(axPageLayoutControl1);
            MPS.Visible = true;
        }

        public IRgbColor getRGB(int r, int g, int b)
        {
            IRgbColor pcolor = new RgbColorClass();
            pcolor.Red = r;
            pcolor.Green = g;
            pcolor.Blue = b;
            return pcolor;
        }
        public void NorthArrow() { 
            FrmNorthArrow frmNorthArrow = new FrmNorthArrow();
            frmNorthArrow.OnQueryNorthArrow += pNorthArrow => m_NorthArrrow = pNorthArrow;
            frmNorthArrow.ShowDialog();

            axPageLayoutControl1.CurrentTool = null;
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            operation = "添加指北针";
        }
        public void ScaleBar()
        {
            FrmScaleBar frmScaleBar = new FrmScaleBar();
            frmScaleBar.OnQueryScaleBar += pScaleBar => m_ScaleBar = pScaleBar;
            frmScaleBar.ShowDialog();

            axPageLayoutControl1.CurrentTool = null;
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            operation = "添加比例尺";
        }
        public void Legend(){
            axPageLayoutControl1.CurrentTool = null;
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            operation = "添加图例";
        }

        public void Title()
        {
            FrmTitle frmScaleBar = new FrmTitle();
            frmScaleBar.OnQueryTitle += pTitle => m_Title = pTitle;
            frmScaleBar.ShowDialog();

            axPageLayoutControl1.CurrentTool = null;
            axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerCrosshair;
            operation = "添加图名";
        }
        
        //还要修改，没有做到交互，结合csdn上面的方法
        public void ExportImage() {
            IActiveView pActiveView;
            pActiveView = axPageLayoutControl1.ActiveView;
            ESRI.ArcGIS.Output.IExport pExport = new ESRI.ArcGIS.Output.ExportBMPClass();
            pExport.ExportFileName = @"D:\cscs.tif";
            pExport.Resolution = 96;
            ESRI.ArcGIS.esriSystem.tagRECT pExportRECT;
            pExportRECT.left = 0;
            pExportRECT.top = 0;
            pExportRECT.right = pActiveView.ExportFrame.right * (96 / 96);
            pExportRECT.bottom = pActiveView.ExportFrame.bottom * (96 / 96);

            ESRI.ArcGIS.Geometry.IEnvelope pEnvelope = new ESRI.ArcGIS.Geometry.EnvelopeClass();
            pEnvelope.PutCoords(pExportRECT.left, pExportRECT.top, pExportRECT.right, pExportRECT.bottom);
            pExport.PixelBounds = pEnvelope;

            System.Int32 hDC = pExport.StartExporting();

            pActiveView.Output(hDC, (System.Int16)pExport.Resolution, ref pExportRECT, null, null);
            pExport.FinishExporting();
            pExport.Cleanup();
            UIMessageBox.ShowSuccess("导出完成!",false);
        }
        
        //FullExtentCommand
        public void FullExtentCommand()
        {
            string sProgID = "esriControlTools.ControlsMapFullExtentCommand";
            axToolbarControl1.AddItem(sProgID, -1, 5, false, -1, esriCommandStyles.esriCommandStyleIconOnly);

            //ICommand command = new ControlsMapFullExtentCommandClass();
            //axToolbarControl1.AddItem(command, -1, 5, false, -1, esriCommandStyles.esriCommandStyleIconOnly);
        }

        public string GetMapUnit(esriUnits _esriMapUnit)
        {
            string sMapUnits = string.Empty;
            switch (_esriMapUnit)
            {
                case esriUnits.esriCentimeters:
                    sMapUnits = "厘米";
                    break;
                case esriUnits.esriDecimalDegrees:
                    sMapUnits = "十进制";
                    break;
                case esriUnits.esriDecimeters:
                    sMapUnits = "分米";
                    break;
                case esriUnits.esriFeet:
                    sMapUnits = "尺";
                    break;
                case esriUnits.esriInches:
                    sMapUnits = "英寸";
                    break;
                case esriUnits.esriKilometers:
                    sMapUnits = "千米";
                    break;
                case esriUnits.esriMeters:
                    sMapUnits = "米";
                    break;
                case esriUnits.esriMiles:
                    sMapUnits = "英里";
                    break;
                case esriUnits.esriMillimeters:
                    sMapUnits = "毫米";
                    break;
                case esriUnits.esriNauticalMiles:
                    sMapUnits = "海里";
                    break;
                case esriUnits.esriPoints:
                    sMapUnits = "点";
                    break;
                case esriUnits.esriUnitsLast:
                    sMapUnits = "UnitsLast";
                    break;
                case esriUnits.esriUnknownUnits:
                    sMapUnits = "未知单位";
                    break;
                case esriUnits.esriYards:
                    sMapUnits = "码";
                    break;
                default:
                    break;
            }
            return sMapUnits;
        }

        //private IPoint pMovePt = null; 

        private void axMapControl1_OnMouseMove(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseMoveEvent e)
        {
            string sMapUnits = GetMapUnit(axMapControl1.Map.MapUnits);
            IPoint pMovePt = (axMapControl1.Map as IActiveView).ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y); //计算对应于设备点的地图坐标中的点
            uiLabel1.Text = String.Format("当前坐标为：X = {0:#.###} Y = {1:#.###} {2}", e.mapX, e.mapY, sMapUnits);
            //toolStripStatusLabel1.Text = String.Format("当前坐标为：X = {0:#.###} Y = {1:#.###} {2}", e.mapX, e.mapY, sMapUnits);
        }

        IFeatureLayer pTocFeatureLayer = null;
        IRasterLayer pTocRasterLayer = null;
        IMap pTocMap = null;
        public ITOCControl mTOCControl;
        public ILayer pMoveLayer;
        public int toIndex;
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        TimeSpan timeSpan;  //判断是否执行图层拖动前后层关系  的时间间隔，设置为200ms

        private void axTOCControl1_OnMouseDown(object sender, ESRI.ArcGIS.Controls.ITOCControlEvents_OnMouseDownEvent e)
        {
            //esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
            esriTOCControlItem pItem = new esriTOCControlItem();
            IBasicMap pMap = null;
            ILayer pLayer = null;
            object unk = null;
            object data = null;

            if (e.button == 1)
            {
                watch.Reset();
                watch.Start();
                pMoveLayer = null;
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                if (pItem == esriTOCControlItem.esriTOCControlItemLayer)
                {
                    if (pLayer is IAnnotationSublayer) return;//如果是注记图层则返回
                    else
                        pMoveLayer = pLayer;
                }
            }
            else if (e.button == 2)
            {
                pTocFeatureLayer = null;
                pTocRasterLayer = null;
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pMap, ref pLayer, ref unk, ref data);
                pTocFeatureLayer = pLayer as IFeatureLayer;
                pTocRasterLayer = pLayer as IRasterLayer;
                pTocMap = pMap as IMap;
                if (pItem == esriTOCControlItem.esriTOCControlItemMap)
                {
                    uiContextMenuStrip2.Show(Control.MousePosition);
                }
                else
                {
                    if (pItem == esriTOCControlItem.esriTOCControlItemLayer && (pTocFeatureLayer != null))
                    {
                        uiContextMenuStrip1.Show(Control.MousePosition);
                    }
                    if (pItem == esriTOCControlItem.esriTOCControlItemLayer && (pTocRasterLayer != null))
                    {
                        uiContextMenuStrip1.Show(Control.MousePosition);
                    }
                }

            }
        }
        private void axTOCControl1_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            esriTOCControlItem pItem = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap pBasicMap = null;
            ILayer pLayer = null;
            object unk = null;
            object data = null;

            if (e.button == 1) { watch.Stop(); timeSpan = watch.Elapsed; }

            if (e.button == 1 && timeSpan.TotalMilliseconds > 200)
            {
                axTOCControl1.HitTest(e.x, e.y, ref pItem, ref pBasicMap, ref pLayer, ref unk, ref data);
                if (pMoveLayer != pLayer)//如果是原图层则不用操作
                {
                    IMap pMap = axMapControl1.Map;
                    ILayer pTempLayer;
                    for (int i = 0; i < pMap.LayerCount; i++)
                    {
                        pTempLayer = pMap.get_Layer(i);
                        if (pTempLayer == pLayer)//获取移动后的图层索引
                            toIndex = i;
                    }
                    pMap.MoveLayer(pMoveLayer, toIndex);
                    axMapControl1.ActiveView.Refresh();
                    mTOCControl.Update();
                }
            }
        }

        private void 移除图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pTocRasterLayer == null && pTocFeatureLayer == null) return;
            if (pTocRasterLayer != null)
            {
                DialogResult result = MessageBox.Show("是否删除图层“" + pTocRasterLayer.Name + "”", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    axMapControl1.Map.DeleteLayer(pTocRasterLayer);
                }
            }
            else if (pTocFeatureLayer != null)
            {
                DialogResult result = MessageBox.Show("是否删除图层“" + pTocFeatureLayer.Name + "”", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                {
                    axMapControl1.Map.DeleteLayer(pTocFeatureLayer);
                }
            }

            axMapControl1.ActiveView.Refresh();
        }

        private void 缩放至图层ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pTocFeatureLayer == null && pTocRasterLayer == null) return;
            if (pTocFeatureLayer != null) {
                (axMapControl1.Map as IActiveView).Extent = pTocFeatureLayer.AreaOfInterest;
                (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            else if (pTocRasterLayer != null)
            {
                (axMapControl1.Map as IActiveView).Extent = pTocRasterLayer.AreaOfInterest;
                (axMapControl1.Map as IActiveView).PartialRefresh(esriViewDrawPhase.esriViewGeography, null, null);
            }
            
        }

        private void 图层渲染ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pTocRasterLayer != null) {
                RasterRenderer prasr = new RasterRenderer();
                prasr.Show();

                prasr.PTocRasterLayer = pTocRasterLayer;
                prasr.PTocControl = axTOCControl1;
                prasr.PMapControl = axMapControl1;
            }
            
            if (pTocFeatureLayer != null)
                SetupFeaturePropertySheet(pTocFeatureLayer as ILayer, axMapControl1.Map as IActiveView, axTOCControl1);
        }

        public bool SetupFeaturePropertySheet(ILayer layer, IActiveView activeview, ESRI.ArcGIS.Controls.AxTOCControl mTOCControl)
        {
            if (layer == null) return false;
            ESRI.ArcGIS.Framework.IComPropertySheet pComPropSheet;
            pComPropSheet = new ESRI.ArcGIS.Framework.ComPropertySheet();
            pComPropSheet.Title = layer.Name + " - 属性";

            ESRI.ArcGIS.esriSystem.UID pPPUID = new ESRI.ArcGIS.esriSystem.UIDClass();
            pComPropSheet.AddCategoryID(pPPUID);

            // General....
            ESRI.ArcGIS.Framework.IPropertyPage pGenPage = new ESRI.ArcGIS.CartoUI.GeneralLayerPropPageClass();
            pComPropSheet.AddPage(pGenPage);

            // Source
            ESRI.ArcGIS.Framework.IPropertyPage pSrcPage = new ESRI.ArcGIS.CartoUI.FeatureLayerSourcePropertyPageClass();
            pComPropSheet.AddPage(pSrcPage);

            // Selection...
            ESRI.ArcGIS.Framework.IPropertyPage pSelectPage = new ESRI.ArcGIS.CartoUI.FeatureLayerSelectionPropertyPageClass();
            pComPropSheet.AddPage(pSelectPage);

            // Display....
            ESRI.ArcGIS.Framework.IPropertyPage pDispPage = new ESRI.ArcGIS.CartoUI.FeatureLayerDisplayPropertyPageClass();
            pComPropSheet.AddPage(pDispPage);

            // Symbology....
            ESRI.ArcGIS.Framework.IPropertyPage pDrawPage = new ESRI.ArcGIS.CartoUI.LayerDrawingPropertyPageClass();
            pComPropSheet.AddPage(pDrawPage);

            // Fields... 
            ESRI.ArcGIS.Framework.IPropertyPage pFieldsPage = new ESRI.ArcGIS.CartoUI.LayerFieldsPropertyPageClass();
            pComPropSheet.AddPage(pFieldsPage);

            // Definition Query... 
            ESRI.ArcGIS.Framework.IPropertyPage pQueryPage = new ESRI.ArcGIS.CartoUI.LayerDefinitionQueryPropertyPageClass();
            pComPropSheet.AddPage(pQueryPage);

            // Labels....
            ESRI.ArcGIS.Framework.IPropertyPage pSelPage = new ESRI.ArcGIS.CartoUI.LayerLabelsPropertyPageClass();
            pComPropSheet.AddPage(pSelPage);

            // Joins & Relates....
            ESRI.ArcGIS.Framework.IPropertyPage pJoinPage = new ESRI.ArcGIS.ArcMapUI.JoinRelatePageClass();
            pComPropSheet.AddPage(pJoinPage);

            // Setup layer link
            ESRI.ArcGIS.esriSystem.ISet pMySet = new ESRI.ArcGIS.esriSystem.SetClass();
            pMySet.Add(layer);
            pMySet.Reset();

            // make the symbology tab active
            pComPropSheet.ActivePage = 4;

            // show the property sheet
            bool bOK = pComPropSheet.EditProperties(pMySet, 0);

            activeview.PartialRefresh(esriViewDrawPhase.esriViewGeography, null, activeview.Extent);
            mTOCControl.Update();


            //////更新图例
            //IMapSurround pMapSurround = null;
            //ILegend pLegend = null;

            //for (int i = 0; i < m_map.MapSurroundCount; i++)
            //{
            //    pMapSurround = m_map.get_MapSurround(i);
            //    if (pMapSurround is ILegend)
            //    {
            //        pLegend = pMapSurround as ILegend;
            //        pLegend.AutoVisibility = true;
            //        pLegend.Refresh();

            //    }
            //}

            return (bOK);
        }

        private void 查看属性表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (pTocRasterLayer == null && pTocFeatureLayer == null) return;
            if (pTocFeatureLayer != null) {
                AttributeTable pAT = new AttributeTable(pTocFeatureLayer, ref pTocMap);
                //pAT.PTocFeatureLayer = pTocFeatureLayer;
                //pTocFeatureLayer.FeatureClass.

                pAT.PTocMap = pTocMap;
                //pAT.InitUI();
                pAT.ShowDialog();
                //pAT.Show();

                
            }
            if (pTocRasterLayer != null)
            {
                AttributeTable pAT = new AttributeTable(pTocRasterLayer, ref pTocMap);
                //pAT.PTocRasterLayer = pTocRasterLayer;
                //pAT.InitRaster();
                pAT.ShowDialog();
            }
        }

        private void 矢量添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            shpload();
        }

        private void 栅格添加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tifload();
        }
        //PageLayoutControl的代码
        public static void CopyAndOverwriteMap(AxMapControl axMapControl, AxPageLayoutControl axPageLayoutControl)
        {
            IObjectCopy objectCopy = new ObjectCopyClass();
            object toCopyMap = axMapControl.Map;
            object copiedMap = objectCopy.Copy(toCopyMap);
            object overwriteMap = axPageLayoutControl.ActiveView.FocusMap;
            objectCopy.Overwrite(toCopyMap, ref overwriteMap);
        }

        private void axMapControl1_OnViewRefreshed(object sender, IMapControlEvents2_OnViewRefreshedEvent e)
        {
            Form1.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);
            axPageLayoutControl1.ActiveView.Refresh();
        }

        private void axMapControl1_OnMapReplaced(object sender, IMapControlEvents2_OnMapReplacedEvent e)
        {
            Form1.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);
            axPageLayoutControl1.ActiveView.Refresh();
        }

        private void axMapControl1_OnAfterScreenDraw(object sender, IMapControlEvents2_OnAfterScreenDrawEvent e)
        {
            Form1.CopyAndOverwriteMap(axMapControl1, axPageLayoutControl1);
            axPageLayoutControl1.ActiveView.Refresh();
        }

        private void axPageLayoutControl1_OnMouseDown(object sender, IPageLayoutControlEvents_OnMouseDownEvent e)
        {
            //if (operation == "添加指北针")
            //{
            //    ////画框放置指北针
            //    //IEnvelope pEnvelope = axPageLayoutControl1.TrackRectangle();
            //    //if (pEnvelope.IsEmpty || pEnvelope == null || pEnvelope.Width == 0 || pEnvelope.Height == 0)
            //    //{
            //    //    return;
            //    //}

            //    // 删除已有指北针
            //    IActiveView pActiveViewv = axPageLayoutControl1.PageLayout as IActiveView;
            //    IGraphicsContainer pGraphicsContainer = pActiveViewv.GraphicsContainer;
            //    if (m_Element != null)
            //    {
            //        pGraphicsContainer.DeleteElement(m_Element);
            //        pActiveViewv.Refresh();
            //    }

            //    // 获取框架元素
            //    IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveViewv.FocusMap) as IMapFrame;
            //    IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrame() as IMapSurroundFrame;
            //    pMapSurroundFrame.MapFrame = pMapFrame;
            //    pMapSurroundFrame.MapSurround = m_NorthArrrow as IMapSurround;
            //    double siz = m_NorthArrrow.Size;

            //    //
            //    //

            //    //我自己写的
            //    IEnvelope pEnvelope = new EnvelopeClass();
            //    IPoint pt = new PointClass();
            //    pt = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x - Convert.ToInt32(siz / 5), e.y - Convert.ToInt32(siz / 5));
            //    IPoint pt2 = new PointClass();
            //    pt2 = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x + Convert.ToInt32(siz / 5), e.y + Convert.ToInt32(siz / 5));
            //    pEnvelope.PutCoords(pt.X, pt.Y, pt2.X, pt2.Y);
            //    //我自己写的

            //    // 添加指北针
            //    m_Element = pMapSurroundFrame as IElement;
            //    m_Element.Geometry = pEnvelope;
            //    pGraphicsContainer.AddElement(m_Element, 0);
            //    pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //}
            //if (operation == "添加指北针")
            //{
            //    //////画框放置指北针
            //    //IEnvelope pEnvelope = axPageLayoutControl1.TrackRectangle();
            //    //if (pEnvelope.IsEmpty || pEnvelope == null || pEnvelope.Width == 0 || pEnvelope.Height == 0)
            //    //{
            //    //    return;
            //    //}

            //    // 删除已有指北针
            //    IActiveView pActiveViewv = axPageLayoutControl1.PageLayout as IActiveView;
            //    IGraphicsContainer pGraphicsContainer = pActiveViewv.GraphicsContainer;
            //    if (m_Element != null)
            //    {
            //        pGraphicsContainer.DeleteElement(m_Element);
            //        pActiveViewv.Refresh();
            //    }

            //    // 获取框架元素
            //    IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveViewv.FocusMap) as IMapFrame;

            //    IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrame() as IMapSurroundFrame;
            //    pMapSurroundFrame.MapFrame = pMapFrame;
            //    pMapSurroundFrame.MapSurround = m_NorthArrrow as IMapSurround;
            //    double siz = m_NorthArrrow.Size;




            //    //我自己写的
            //    pEnvelope = new EnvelopeClass();
            //    IPoint pt = new PointClass();
            //    //pt = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x - Convert.ToInt32(siz / 5), e.y - Convert.ToInt32(siz / 5));
            //    pt = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x - Convert.ToInt32(siz / 2), e.y - Convert.ToInt32(siz / 2));
            //    IPoint pt2 = new PointClass();
            //    //pt2 = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x + Convert.ToInt32(siz / 5), e.y + Convert.ToInt32(siz / 5));
            //    pt2 = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x + Convert.ToInt32(siz / 2), e.y + Convert.ToInt32(siz / 2));
            //    pEnvelope.PutCoords(pt.X, pt.Y, pt2.X, pt2.Y);

            //    IPoint pt3 = new PointClass();
            //    pt3 = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            //    ISimpleMarkerSymbol pSimpleMarkerS = new SimpleMarkerSymbolClass();
            //    pSimpleMarkerS.Color = getRGB(102, 20, 103);
            //    pSimpleMarkerS.Style = esriSimpleMarkerStyle.esriSMSDiamond;
            //    IElement pointele;

            //    IMarkerElement pMarkerElement = new MarkerElementClass();
            //    pointele=pMarkerElement as IElement;



            //    pointele.Geometry = pt3;
            //    pMarkerElement.Symbol = pSimpleMarkerS;
            //    pGraphicsContainer.AddElement(pointele, 0);

            //    pEnvelope.CenterAt(pt3);

            //    if (pEle != null)
            //    {
            //        pGraphicsContainer.DeleteElement(pEle);
            //        pActiveViewv.Refresh();
            //    }

            //    IPoint p1 = new PointClass();
            //    IPoint p2 = new PointClass();
            //    IPoint p3 = new PointClass();
            //    IPoint p4 = new PointClass();
            //    p1 = pEnvelope.UpperLeft;
            //    p2 = pEnvelope.LowerLeft;
            //    p3 = pEnvelope.LowerRight;
            //    p4 = pEnvelope.UpperRight;
            //    IPointCollection pPointCollection = new PolygonClass();
            //    object missing = Type.Missing;
            //    pPointCollection.AddPoint(p1, ref missing, ref missing);
            //    pPointCollection.AddPoint(p2, ref missing, ref missing);
            //    pPointCollection.AddPoint(p3, ref missing, ref missing);
            //    pPointCollection.AddPoint(p4, ref missing, ref missing);
            //    IPolygon pPolygon = (IPolygon)pPointCollection;

            //    ISimpleFillSymbol pSimpleFillS = new SimpleFillSymbolClass();
            //    pSimpleFillS.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            //    pSimpleFillS.Color = getRGB(102, 200, 103);

            //    IFillShapeElement pPolygonEle;
            //    pPolygonEle = new PolygonElementClass();
            //    pPolygonEle.Symbol = pSimpleFillS;


            //    pEle = pPolygonEle as IElement;
            //    pEle.Geometry = pPolygon;
            //    pGraphicsContainer.AddElement(pEle, 0);




            //    //我自己写的

            //    // 添加指北针
            //    m_Element = pMapSurroundFrame as IElement;

            //    m_Element.Geometry = pEnvelope;
            //    pGraphicsContainer.AddElement(m_Element, 0);
            //    pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //}
            if (e.button == 1)
            {
                if (operation == "添加指北针")
                {

                    // 删除已有指北针
                    IActiveView pActiveViewv = axPageLayoutControl1.PageLayout as IActiveView;
                    IGraphicsContainer pGraphicsContainer = pActiveViewv.GraphicsContainer;
                    if (m_Element != null)
                    {
                        pGraphicsContainer.DeleteElement(m_Element);
                        pActiveViewv.Refresh();
                    }

                    // 获取框架元素
                    IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveViewv.FocusMap) as IMapFrame;
                    IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrame() as IMapSurroundFrame;
                    pMapSurroundFrame.MapFrame = pMapFrame;

                    pMapSurroundFrame.MapSurround = m_NorthArrrow as IMapSurround;
                    pMapSurroundFrame.MapSurround.Name = "图例";
                    double siz = m_NorthArrrow.Size;

                    //我自己写的
                    IEnvelope pEnvelope = new EnvelopeClass();
                    IPoint pt = new PointClass();
                    pt = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x - Convert.ToInt32(siz / 5), e.y - Convert.ToInt32(siz / 5));
                    IPoint pt2 = new PointClass();
                    pt2 = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x + Convert.ToInt32(siz / 5), e.y + Convert.ToInt32(siz / 5));
                    pEnvelope.PutCoords(pt.X, pt.Y, pt2.X, pt2.Y);

                    IEnvelope newEnvelope = new EnvelopeClass();
                    //通过IMapSurround的QueryBounds方法可以获得MapSurround对象的边界
                    pMapSurroundFrame.MapSurround.QueryBounds(pActiveViewv.ScreenDisplay as IDisplay, pEnvelope, newEnvelope);

                    //获取鼠标选取点为中心点
                    IPoint centerp = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                    newEnvelope.CenterAt(centerp);


                    //IPoint p5 = new PointClass();
                    //IPoint p6 = new PointClass();
                    //IPoint p7 = new PointClass();
                    //IPoint p8 = new PointClass();
                    //p5 = newEnvelope.UpperLeft;
                    //p6 = newEnvelope.LowerLeft;
                    //p7 = newEnvelope.LowerRight;
                    //p8 = newEnvelope.UpperRight;

                    //object missing = Type.Missing;
                    //IPointCollection pPointCollection2 = new PolygonClass();
                    //pPointCollection2.AddPoint(p5, ref missing, ref missing);
                    //pPointCollection2.AddPoint(p6, ref missing, ref missing);
                    //pPointCollection2.AddPoint(p7, ref missing, ref missing);
                    //pPointCollection2.AddPoint(p8, ref missing, ref missing);
                    //IPolygon pPolygon2 = (IPolygon)pPointCollection2;

                    //ISimpleFillSymbol pSimpleFillS2 = new SimpleFillSymbolClass();
                    //pSimpleFillS2.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
                    //pSimpleFillS2.Color = getRGB(10, 200, 103);

                    //IFillShapeElement pPolygonEle2;
                    //pPolygonEle2 = new PolygonElementClass();
                    //pPolygonEle2.Symbol = pSimpleFillS2;

                    //IElement clickEle2;//看点击后的边界
                    //clickEle2 = pPolygonEle2 as IElement;
                    //clickEle2.Geometry = pPolygon2;
                    //pGraphicsContainer.AddElement(clickEle2, 0);
                    ////我自己写的

                    // 添加指北针
                    m_Element = pMapSurroundFrame as IElement;
                    //m_Element.Geometry = pEnvelope;
                    m_Element.Geometry = newEnvelope;
                    pGraphicsContainer.AddElement(m_Element, 0);
                    pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);


                    //选中该元素
                    IGraphicsContainerSelect pGraphicsContainerSelect = pGraphicsContainer as IGraphicsContainerSelect;
                    pGraphicsContainerSelect.SelectElement(m_Element);
                    pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);



                    axPageLayoutControl1.CurrentTool = null;
                    axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                    operation = "";

                }
                else if (operation == "添加比例尺")
                {
                    IEnvelope pEnvelope = null;
                    if (pEnvelope.IsEmpty || pEnvelope == null || pEnvelope.Width == 0 || pEnvelope.Height == 0)
                    {
                        return;
                    }

                    // 删除已有比例尺
                    IActiveView pActiveViewv = axPageLayoutControl1.PageLayout as IActiveView;
                    IGraphicsContainer pGraphicsContainer = pActiveViewv.GraphicsContainer;
                    if (m_ScaleBarElement != null)
                    {
                        pGraphicsContainer.DeleteElement(m_ScaleBarElement);
                        pActiveViewv.Refresh();
                    }

                    // 获取框架元素
                    IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveViewv.FocusMap) as IMapFrame;
                    IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrame() as IMapSurroundFrame;
                    pMapSurroundFrame.MapFrame = pMapFrame;
                    pMapSurroundFrame.MapSurround = m_ScaleBar as IMapSurround;

                    // 添加指北针
                    m_ScaleBarElement = pMapSurroundFrame as IElement;
                    pEnvelope.PutCoords(2, 1.5, 10, 2.5);
                    m_ScaleBarElement.Geometry = pEnvelope;
                    pGraphicsContainer.AddElement(m_ScaleBarElement, 0);
                    pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);


                    axPageLayoutControl1.CurrentTool = null;
                    axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                    operation = "";
                }
                #region
                //else if (operation == "添加比例尺")
                //{
                //    IEnvelope pEnvelope = axPageLayoutControl1.TrackRectangle();
                //    if (pEnvelope.IsEmpty || pEnvelope == null || pEnvelope.Width == 0 || pEnvelope.Height == 0)
                //    {
                //        return;
                //    }

                //    // 删除已有比例尺
                //    IActiveView pActiveViewv = axPageLayoutControl1.PageLayout as IActiveView;
                //    IGraphicsContainer pGraphicsContainer = pActiveViewv.GraphicsContainer;
                //    if (m_ScaleBarElement != null)
                //    {
                //        pGraphicsContainer.DeleteElement(m_ScaleBarElement);
                //        pActiveViewv.Refresh();
                //    }

                //    // 获取框架元素
                //    IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveViewv.FocusMap) as IMapFrame;
                //    IMapSurroundFrame pMapSurroundFrame = new MapSurroundFrame() as IMapSurroundFrame;
                //    pMapSurroundFrame.MapFrame = pMapFrame;
                //    pMapSurroundFrame.MapSurround = m_ScaleBar as IMapSurround;

                //    //double BarHeight = m_ScaleBar.BarHeight;
                //    //IScaleLine m_ScaleLine = m_ScaleBar as IScaleLine;
                //    //double LineWidth = m_ScaleLine.LineSymbol.Width;
                //    //MessageBox.Show(BarHeight.ToString() + "," + LineWidth.ToString());


                //    ////////我自己写的
                //    ////IEnvelope pEnvelope = new EnvelopeClass();
                //    //////IPoint pt = new PointClass();
                //    //////pt = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x - Convert.ToInt32(siz) * 10, e.y - Convert.ToInt32(siz) * 10);
                //    //////IPoint pt2 = new PointClass();
                //    //////pt2 = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x + Convert.ToInt32(siz) * 10, e.y + Convert.ToInt32(siz) * 10);
                //    //////pEnvelope.PutCoords(pt.X, pt.Y, pt2.X, pt2.Y);
                //    ////pEnvelope.PutCoords(2, 3, 11, 4);
                //    //////我自己写的

                //    // 添加指北针
                //    m_ScaleBarElement = pMapSurroundFrame as IElement;

                //    m_ScaleBarElement.Geometry = pEnvelope;
                //    pGraphicsContainer.AddElement(m_ScaleBarElement, 0);
                //    pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);


                //    axPageLayoutControl1.CurrentTool = null;
                //    axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                //    operation = "";
                //}
                #endregion
                else if (operation == "添加图例")
                {
                    IEnvelope pEnvelope = axPageLayoutControl1.TrackRectangle();
                    if (pEnvelope.IsEmpty || pEnvelope == null || pEnvelope.Width == 0 || pEnvelope.Height == 0)
                    {
                        return;
                    }

                    // 删除已有比例尺
                    IActiveView pActiveViewv = axPageLayoutControl1.PageLayout as IActiveView;
                    IGraphicsContainer pGraphicsContainer = pActiveViewv.GraphicsContainer;
                    if (m_LegnedElement != null)
                    {
                        pGraphicsContainer.DeleteElement(m_LegnedElement);
                        pActiveViewv.Refresh();
                    }

                    // 获取框架元素
                    UID uID = new UIDClass();//创建UID作为该图例的唯一标识符，方便创建之后进行删除、移动等操作
                    uID.Value = "esriCarto.Legend";
                    IMapFrame pMapFrame = pGraphicsContainer.FindFrame(pActiveViewv.FocusMap) as IMapFrame;
                    IMapSurroundFrame pMapSurroundFrame = pMapFrame.CreateSurroundFrame(uID, null);
                    //pMapSurroundFrame.MapFrame = pMapFrame;
                    pMapSurroundFrame.MapSurround.Name = "图例";
                    //pMapSurroundFrame.MapSurround = m_ScaleBar as IMapSurround;
                    //double siz = m_ScaleBar.BarHeight;

                    // 添加指北针
                    m_LegnedElement = pMapSurroundFrame as IElement;
                    m_LegnedElement.Geometry = pEnvelope;
                    pGraphicsContainer.AddElement(m_LegnedElement, 0);
                    pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

                    axPageLayoutControl1.CurrentTool = null;
                    axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                    operation = "";


                }
                else if (operation == "添加图名")
                {
                    IActiveView pActiveViewv = axPageLayoutControl1.PageLayout as IActiveView;
                    IGraphicsContainer pGraphicsContainer = pActiveViewv.GraphicsContainer;
                    if (m_TitleElement != null)
                    {
                        pGraphicsContainer.DeleteElement(m_TitleElement);
                        pActiveViewv.Refresh();
                    }

                    ////
                    //ITextElement pTextElement = new TextElementClass();
                    //pTextElement.Text = m_Title.Text;
                    //pTextElement.Symbol = m_Title;
                    //m_TitleElement = pTextElement as IElement;
                    //double siz = m_Title.Size;

                    ////我自己写的
                    //IEnvelope pEnvelope = new EnvelopeClass();
                    //IPoint pt = new PointClass();
                    //pt = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x - Convert.ToInt32(siz / 2), e.y - Convert.ToInt32(siz / 2));
                    //IPoint pt2 = new PointClass();
                    //pt2 = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x + Convert.ToInt32(siz / 2), e.y + Convert.ToInt32(siz / 2));
                    //pEnvelope.PutCoords(pt.X, pt.Y, pt2.X, pt2.Y);
                    ////我自己写的

                    IPoint point = pActiveViewv.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                    ITextElement txtEL = new TextElementClass();
                    txtEL.Text = m_Title.Text;
                    txtEL.Symbol = m_Title;
                    m_TitleElement = txtEL as IElement;
                    m_TitleElement.Geometry = point;
                    pGraphicsContainer.AddElement(m_TitleElement, 0);

                    // 添加图名
                    //m_TitleElement.Geometry = pEnvelope;
                    //pGraphicsContainer.AddElement(m_TitleElement, 0);
                    pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

                    axPageLayoutControl1.CurrentTool = null;
                    axPageLayoutControl1.MousePointer = esriControlsMousePointer.esriPointerDefault;
                    operation = "";
                }
            }
            else if (e.button == 2)
            {
                //m_toolbarMenu.PopupMenu(e.x, e.y, axPageLayoutControl1.hWnd);
                IPoint centerp = axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
                IEnumElement pEnumElement = axPageLayoutControl1.ActiveView.GraphicsContainer.LocateElements(centerp, 0.01);

                if (pEnumElement != null)
                {
                    pEnumElement.Reset();
                    element1 = pEnumElement.Next();

                    while (element1 != null)
                    {
                        if (element1 is IMapSurroundFrame)
                        {
                            IMapSurround mapSurround = ((IMapSurroundFrame)element1).MapSurround;
                            if (mapSurround is ILegend)
                            {
                                //MessageBox.Show("ILegend");
                                m_toolbarMenu.PopupMenu(e.x, e.y, axPageLayoutControl1.hWnd);
                                break;
                            }
                            else if (mapSurround is IScaleBar)
                            {
                                MessageBox.Show("IScaleBar");
                                break;
                            }
                            else if (mapSurround is INorthArrow)
                            {
                                //MessageBox.Show("INorthArrow");
                                m_toolbarMenu_temp.PopupMenu(e.x, e.y, axPageLayoutControl1.hWnd);
                                break;
                            }
                        }
                        else if (element1 is ITextElement)
                        {
                            MessageBox.Show("ITextElement");
                            break;
                        }
                        else if(element1 is IMapFrame)
                        {
                            //MessageBox.Show("IAMHERE");
                            m_toolbarMenu_temp1.PopupMenu(e.x, e.y, axPageLayoutControl1.hWnd);
                            break;
                        }
                            
                        element1 = pEnumElement.Next();
                    }
                }
            }
        }

        private void uiTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UITabControl TabControl = sender as UITabControl;
            switch (TabControl.SelectedTab.Text)
            {
                case "数据视图":
                    tab1 = "数据视图";
                    axToolbarControl1.SetBuddyControl(axMapControl1);  //选择显示控制的为axMapControl1接口
                    break;
                case "布局视图":
                    tab1 = "布局视图";
                    axToolbarControl1.SetBuddyControl(axPageLayoutControl1);
                    break;
            }
        }

        private void 测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //m_NorthArrrow.Size = 72;
            IActiveView pActiveViewv = axPageLayoutControl1.PageLayout as IActiveView;
            IGraphicsContainer pGraphicsContainer = pActiveViewv.GraphicsContainer;

            //IPoint pt = new PointClass();
            //pt.X = 0;
            //pt.Y = 0;
            //pEnvelope.CenterAt(pt);
            bool change = false;

            IMapSurround tempSurround=m_NorthArrrow as IMapSurround;
            IEnvelope tempEnvelope = new EnvelopeClass();
            //tempEnvelope.PutCoords(2,2,15,15);
            tempEnvelope.PutCoords(2, 2, 10, 15);

            IPoint p1 = new PointClass();
            IPoint p2 = new PointClass();
            IPoint p3 = new PointClass();
            IPoint p4 = new PointClass();
            p1 = tempEnvelope.UpperLeft;
            p2 = tempEnvelope.LowerLeft;
            p3 = tempEnvelope.LowerRight;
            p4 = tempEnvelope.UpperRight;
            IPointCollection pPointCollection = new PolygonClass();
            object missing = Type.Missing;
            pPointCollection.AddPoint(p1, ref missing, ref missing);
            pPointCollection.AddPoint(p2, ref missing, ref missing);
            pPointCollection.AddPoint(p3, ref missing, ref missing);
            pPointCollection.AddPoint(p4, ref missing, ref missing);
            IPolygon pPolygon = (IPolygon)pPointCollection;

            ISimpleFillSymbol pSimpleFillS = new SimpleFillSymbolClass();
            pSimpleFillS.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            pSimpleFillS.Color = getRGB(10, 200, 103);

            IFillShapeElement pPolygonEle;
            pPolygonEle = new PolygonElementClass();
            pPolygonEle.Symbol = pSimpleFillS;

            IElement clickEle;//看点击后的边界
            clickEle = pPolygonEle as IElement;
            clickEle.Geometry = pPolygon;
            pGraphicsContainer.AddElement(clickEle, 0);

            //通过IMapSurround的FitToBound方法可以设置一个MapSurround对象的大小
            tempSurround.FitToBounds(pActiveViewv.ScreenDisplay as IDisplay, tempEnvelope, out change);
            

            IEnvelope newEnvelope = new EnvelopeClass();
            //通过IMapSurround的QueryBounds方法可以获得MapSurround对象的边界
            tempSurround.QueryBounds(pActiveViewv.ScreenDisplay as IDisplay, tempEnvelope, newEnvelope);
            MessageBox.Show(newEnvelope.Width.ToString() + "," + newEnvelope.Height.ToString());
            //IPoint centerp = new PointClass();
            //centerp.X = (newEnvelope.LowerLeft.X + newEnvelope.UpperRight.X) / 2;
            //centerp.Y = (newEnvelope.LowerLeft.Y + newEnvelope.UpperRight.Y+10) / 2;
            //newEnvelope.CenterAt(centerp);

            //IPoint p5 = new PointClass();
            //IPoint p6 = new PointClass();
            //IPoint p7 = new PointClass();
            //IPoint p8 = new PointClass();
            //p5 = newEnvelope.UpperLeft;
            //p6 = newEnvelope.LowerLeft;
            //p7 = newEnvelope.LowerRight;
            //p8 = newEnvelope.UpperRight;
            //IPointCollection pPointCollection2 = new PolygonClass();
            //pPointCollection2.AddPoint(p5, ref missing, ref missing);
            //pPointCollection2.AddPoint(p6, ref missing, ref missing);
            //pPointCollection2.AddPoint(p7, ref missing, ref missing);
            //pPointCollection2.AddPoint(p8, ref missing, ref missing);
            //IPolygon pPolygon2 = (IPolygon)pPointCollection2;

            //ISimpleFillSymbol pSimpleFillS2 = new SimpleFillSymbolClass();
            //pSimpleFillS2.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            //pSimpleFillS2.Color = getRGB(10, 20, 103);

            //IFillShapeElement pPolygonEle2;
            //pPolygonEle2 = new PolygonElementClass();
            //pPolygonEle2.Symbol = pSimpleFillS2;

            //IElement clickEle2;//看点击后的边界
            //clickEle2 = pPolygonEle2 as IElement;
            //clickEle2.Geometry = pPolygon2;
            //pGraphicsContainer.AddElement(clickEle2, 0);

            newEnvelope.Expand(1, 1, false);
            IPoint p5 = new PointClass();
            IPoint p6 = new PointClass();
            IPoint p7 = new PointClass();
            IPoint p8 = new PointClass();
            p5 = newEnvelope.UpperLeft;
            p6 = newEnvelope.LowerLeft;
            p7 = newEnvelope.LowerRight;
            p8 = newEnvelope.UpperRight;
            IPointCollection pPointCollection2 = new PolygonClass();
            pPointCollection2.AddPoint(p5, ref missing, ref missing);
            pPointCollection2.AddPoint(p6, ref missing, ref missing);
            pPointCollection2.AddPoint(p7, ref missing, ref missing);
            pPointCollection2.AddPoint(p8, ref missing, ref missing);
            IPolygon pPolygon2 = (IPolygon)pPointCollection2;

            ISimpleFillSymbol pSimpleFillS2 = new SimpleFillSymbolClass();
            pSimpleFillS2.Style = esriSimpleFillStyle.esriSFSDiagonalCross;
            pSimpleFillS2.Color = getRGB(10, 20, 103);

            IFillShapeElement pPolygonEle2;
            pPolygonEle2 = new PolygonElementClass();
            pPolygonEle2.Symbol = pSimpleFillS2;

            IElement clickEle2;//看点击后的边界
            clickEle2 = pPolygonEle2 as IElement;
            clickEle2.Geometry = pPolygon2;
            pGraphicsContainer.AddElement(clickEle2, 0);
            
           
            pActiveViewv.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private void axPageLayoutControl1_OnDoubleClick(object sender, IPageLayoutControlEvents_OnDoubleClickEvent e)
        {
            #region
            ////与下面的组合、分解代码冲突了，需要注释掉
            //IPoint pPoint = axPageLayoutControl1.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(e.x, e.y);
            //IGraphicsContainer pGraphicsContainer = axPageLayoutControl1.PageLayout as IGraphicsContainer;
            //IGraphicsContainerSelect pGraphicsContainerSelect = axPageLayoutControl1.PageLayout as IGraphicsContainerSelect;

            //IEnumElement pEnumElement = pGraphicsContainer.LocateElements(pPoint, 1);

            //pEnumElement.Reset();
            //IElement pElement = pEnumElement.Next();
            //while (pElement != null)
            //{
            //    if (pElement is IMapFrame)
            //    {
            //        pElement = pEnumElement.Next();
            //        continue;
            //    }

            //    if (pElement is IMapSurroundFrame)     //解决从模板中删除IMapSurround对象（图例、指北针、比例尺等）时，报内存错误的情况。 
            //    {
            //        IMapSurroundFrame pMapSf = pElement as IMapSurroundFrame;
            //        IMapSurround pMapSurround = pMapSf.MapSurround;
            //        //if (pMapSurround is IMarkerNorthArrow || pMapSurround is ILegend || pMapSurround is IScaleBar || pMapSurround is IScaleText)
            //        //{
            //        //    //删除操作
            //        //    IMap pMap = axPageLayoutControl1.ActiveView.FocusMap;
            //        //    pMapSurround.Map = pMap;
            //        //    pMap.DeleteMapSurround(pMapSurround);
            //        //    axPageLayoutControl1.Refresh(esriViewDrawPhase.esriViewGraphics, null, null);

                        
            //        //}
            //        if (pElement != null)
            //        {
            //            //pGraphicsContainer.DeleteElement(pElement);
            //            pGraphicsContainerSelect.SelectElement(pElement);
            //            axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphicSelection, null, null);
            //        }
            //    }
            //    else
            //    {
            //        pGraphicsContainer.DeleteElement(pElement);
            //    }
            //    //pGraphicsContainer.AddElement(pNewElement, 0);
            //    //pGraphicsContainerSelect.SelectElement(pNewElement);
            //    axPageLayoutControl1.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            //    break;
            //}
            #endregion

            IElement pelement, selectElement = null;

            IGraphicsContainerSelect pGraphicsContainerSelect = axPageLayoutControl1.PageLayout as IGraphicsContainerSelect;
            IGraphicsContainer pGraphicsContainer = axPageLayoutControl1.PageLayout as IGraphicsContainer;
            pGraphicsContainer.Reset();
            pelement = pGraphicsContainer.Next();

            while (pelement != null)
            {
                if (pelement.HitTest(e.pageX, e.pageY, 0.1))
                {
                    if (pelement is ITextElement)
                    {
                        selectElement = pelement;
                    }
                }

                pelement = pGraphicsContainer.Next();
            }

            if (selectElement is ITextElement)
            {
                Txt propertiesForm = new Txt(axPageLayoutControl1, selectElement as ITextElement);
                propertiesForm.Visible = true;
            }

            pGraphicsContainer.Reset();
            pelement = pGraphicsContainer.Next();
            while (pelement != null)
            {
                if (pelement.HitTest(e.pageX, e.pageY, 0.1))
                {
                    if (pelement is IMapSurroundFrame)
                    {
                        IMapSurround mapSurround = ((IMapSurroundFrame)pelement).MapSurround;
                        if (mapSurround is IScaleBar || mapSurround is IScaleBar2)
                        {
                            selectElement = pelement;
                        }
                    }
                }
                pelement = pGraphicsContainer.Next();
            }

            if (selectElement is IMapSurroundFrame)
            {
                IMapSurround mapSurround = ((IMapSurroundFrame)selectElement).MapSurround;
                if (mapSurround is IScaleBar || mapSurround is IScaleBar2)
                {
                    Bar propertiesForm = new Bar(axPageLayoutControl1, selectElement);
                    propertiesForm.Visible = true;
                }
            }
        }

        private void 测试2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new Command1();
            command.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = command as ITool;
        }

        private void axTOCControl1_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            #region 需要安装ArcGIS
            esriTOCControlItem toccItem = esriTOCControlItem.esriTOCControlItemNone;
            ILayer layer = null; IBasicMap basicMap = null; object unk = null; object data = null;//定义HitTest函数所需的参数
            if (e.button == 1)
            {
                axTOCControl1.HitTest(e.x, e.y, ref toccItem, ref basicMap, ref layer, ref unk, ref data);
                {
                    if (toccItem == esriTOCControlItem.esriTOCControlItemLegendClass)
                    {
                        ESRI.ArcGIS.Carto.ILegendClass pLC = new LegendClassClass();//用户点击的图例
                        ESRI.ArcGIS.Carto.ILegendGroup PLG = new LegendGroupClass();//用户点击的图例组
                        if (unk is ILegendGroup)
                        {
                            PLG = (ILegendGroup)unk;
                        }//获取图例组
                        pLC = PLG.Class[(int)data];//获取图例组中点击的具体图例
                        ESRI.ArcGIS.Display.ISymbol pSym;
                        pSym = pLC.Symbol;
                        ISymbolSelector pSS = new SymbolSelectorClass();//实例化符号选择器
                        bool bOK = false;
                        pSS.AddSymbol(pSym);//添加符号的目录
                        bOK = pSS.SelectSymbol(0);
                        if (bOK)
                        {
                            pLC.Symbol = pSS.GetSymbolAt(0);//利用0索引检索选中的符号
                        }
                        this.axMapControl1.ActiveView.Refresh();
                        this.axTOCControl1.Refresh();
                    }
                }
            }
            #endregion 需要安装ArcGIS

        }

        private void 移动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new Tool1();
            command.OnCreate(axMapControl1.Object);
            //command.OnCreate(axPageLayoutControl1.Object);
            axMapControl1.CurrentTool = command as ITool;
            //axPageLayoutControl1.CurrentTool = command as ITool;
        }

        private void 测试3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new Tool2();
            command.OnCreate(axMapControl1.Object);
            axMapControl1.CurrentTool = command as ITool;
            
        }

        private void scale测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ICommand command = new Tool3();
            //command.OnCreate(axMapControl1.Object);
            //axMapControl1.CurrentTool = command as ITool;
            command.OnCreate(axPageLayoutControl1.Object);
            axPageLayoutControl1.CurrentTool = command as ITool;
        }

        private void ttToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //// Convert legend to graphics 

            IPageLayout pageLayout = axPageLayoutControl1.PageLayout;
            IActiveView activeView = (IActiveView)pageLayout;
            IGraphicsContainer graphicsContainer = pageLayout as IGraphicsContainer;
            graphicsContainer.Reset();


            IElement element = graphicsContainer.Next();
            while (element != null)
            {
                if (element is IMapSurroundFrame)
                {
                    IMapSurround mapSurround = ((IMapSurroundFrame)element).MapSurround;
                    if (mapSurround is ILegend)
                    {
                        ILegend legend = (ILegend)mapSurround;
                        IGraphicsComposite graphComp = (IGraphicsComposite)legend;
                        IEnumElement enumElem = graphComp.get_Graphics(activeView.ScreenDisplay, element.Geometry.Envelope);

                        IElement pElement = enumElem.Next();
                        while (pElement != null)
                        {
                            graphicsContainer.AddElement(pElement, 0);
                            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
                            pElement = enumElem.Next();
                            //break;
                        }
                        //graphicsContainer.AddElement(pElement, 0);
                        //activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);  
                    }
                }
                element = graphicsContainer.Next();
            }

            if (m_LegnedElement != null)
            {
                graphicsContainer.DeleteElement(m_LegnedElement);
                activeView.Refresh();
            }
            activeView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

        }

        private void getWHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            double W = axPageLayoutControl1.PageLayout.Page.PrintableBounds.Width;
            double H = axPageLayoutControl1.PageLayout.Page.PrintableBounds.Height;
            axPageLayoutControl1.PageLayout.Page.PutCustomSize(10,10);
            axPageLayoutControl1.PageLayout.Page.Orientation = 1;
            esriUnits Units=axPageLayoutControl1.PageLayout.Page.Units;
            
            //FrmScaleBar FSB = new FrmScaleBar();
            //int num = FSB.GetIndex(Units);
            //MessageBox.Show(num.ToString());
            //IUnitConverte.ConvertUnits
            IUnitConverter tt = new UnitConverterClass();
            double nn = tt.ConvertUnits(10, Units, esriUnits.esriPoints);
            MessageBox.Show(nn.ToString());
        }

        private void gPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //////IFeatureClass tt = null;
            ////IFeatureLayer pFeatureLayer = new FeatureLayerClass();
            //////pFeatureLayer.FeatureClass = tt;
            //object hh=null;
            //Geoprocessor gp = new Geoprocessor { OverwriteOutput = true };
            //ESRI.ArcGIS.AnalysisTools.Buffer buffer = new ESRI.ArcGIS.AnalysisTools.Buffer();

            //buffer.in_features = @"C:\Users\Administrator\Desktop\大周镇斜坡单元 (1)\dzxpdy.shp";           //必须
            ////buffer.out_feature_class = @"C:\Users\Administrator\Desktop\大周镇斜坡单元 (1)\hhh.shp";
            //buffer.out_feature_class = hh;
            
            //buffer.buffer_distance_or_field = "200 Meters";
            //object sev = null;
            //try     //可以找出GP运行时的错误在哪里
            //{
            //    // Execute the tool.
            //    gp.Execute(buffer, null);                             //必须输入地图描述
            //    Console.WriteLine(hh);
            //    Console.WriteLine(gp.GetMessages(ref sev));
            //}
            //catch (Exception ex)
            //{
            //    // Print geoprocessing execution error messages.
            //    UIMessageBox.ShowWarning(gp.GetMessages(ref sev), false);
            //}
        }
    }
}

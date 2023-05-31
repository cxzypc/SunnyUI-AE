using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using stdole;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.DataSourcesRaster;
using ESRI.ArcGIS.esriSystem;

namespace cs
{
    public partial class RasterRenderer : Form
    {
        IRasterLayer pTocRasterLayer = null;

        public IRasterLayer PTocRasterLayer
        {
            get { return pTocRasterLayer; }
            set { pTocRasterLayer = value; }
        }

        ESRI.ArcGIS.Controls.AxTOCControl pTocControl = null;

        public ESRI.ArcGIS.Controls.AxTOCControl PTocControl
        {
            get { return pTocControl; }
            set { pTocControl = value; }
        }

        ESRI.ArcGIS.Controls.AxMapControl pMapControl = null;

        public ESRI.ArcGIS.Controls.AxMapControl PMapControl
        {
            get { return pMapControl; }
            set { pMapControl = value; }
        }
        private ISymbologyStyleClass pSymbologyStyleClass;
        private Dictionary<int, IColorRamp> colorRampDictionary;
        
        public RasterRenderer()
        {
            InitializeComponent();
            InitSymbologyControl();
            InitColorRampCombobox();
            InitDictionary();
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 0;
            comboBox5.SelectedIndex = 2;
            tabControl1.SelectedIndex = 1;
            //comboBox1.SelectedIndex = 0;
            //pictureBox1.Image = comboBox1.SelectedItem as Image;
        }
        #region 生成颜色带

         // 初始化符号库
        private void InitSymbologyControl()
        {
            //this.axSymbologyControl1.LoadStyleFile(Application.StartupPath + "\\ESRI.ServerStyle");
            //对应Engine的安装路径
            this.axSymbologyControl1.LoadStyleFile(@"E:\Program Files (x86)\ArcGIS\Engine10.4\Styles\ESRI.ServerStyle");
            this.axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassColorRamps;
            this.pSymbologyStyleClass = axSymbologyControl1.GetStyleClass(esriSymbologyStyleClass.esriStyleClassColorRamps);
        }

        // 初始化色带下拉框
        private void InitColorRampCombobox()
        {
            this.comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;

            this.comboBox4.DrawMode = DrawMode.OwnerDrawFixed;
            this.comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;

            this.comboBox6.DrawMode = DrawMode.OwnerDrawFixed;
            this.comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;

            for (int i = 0; i < pSymbologyStyleClass.ItemCount; i++)
            {
                IStyleGalleryItem pStyleGalleryItem = pSymbologyStyleClass.GetItem(i);
                IPictureDisp pPictureDisp = pSymbologyStyleClass.PreviewItem(pStyleGalleryItem, comboBox1.Width, comboBox1.Height);
                Image image = Image.FromHbitmap(new IntPtr(pPictureDisp.Handle));
                comboBox1.Items.Add(image);

                IPictureDisp pPictureDisp4 = pSymbologyStyleClass.PreviewItem(pStyleGalleryItem, comboBox4.Width, comboBox4.Height);
                Image image4 = Image.FromHbitmap(new IntPtr(pPictureDisp4.Handle));
                comboBox4.Items.Add(image4);

                IPictureDisp pPictureDisp6 = pSymbologyStyleClass.PreviewItem(pStyleGalleryItem, comboBox6.Width, comboBox6.Height);
                Image image6 = Image.FromHbitmap(new IntPtr(pPictureDisp6.Handle));
                comboBox6.Items.Add(image6);
            }
            comboBox1.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox6.SelectedIndex = 0;
        }

        // 初始化字典
        private void InitDictionary()
        {
            this.colorRampDictionary = new Dictionary<int, IColorRamp>();
            for (int i = 0; i < pSymbologyStyleClass.ItemCount; i++)
            {
                IStyleGalleryItem pStyleGalleryItem = pSymbologyStyleClass.GetItem(i);
                IColorRamp pColorRamp = pStyleGalleryItem.Item as IColorRamp;
                colorRampDictionary.Add(i, pColorRamp);
            }
        }
        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();//绘制背景
            e.DrawFocusRectangle();//绘制焦点框
            //绘制图例
            //Rectangle iRectangle = new Rectangle(e.Bounds.Left, e.Bounds.Top, 215, 27);
            ////Bitmap getBitmap = new Bitmap(imageList1.Images[e.Index]);
            e.Graphics.DrawImage(comboBox1.Items[e.Index] as Image, e.Bounds);
        }
        private void comboBox4_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();//绘制背景
            e.DrawFocusRectangle();//绘制焦点框
            e.Graphics.DrawImage(comboBox4.Items[e.Index] as Image, e.Bounds);
        }
        private void comboBox6_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();//绘制背景
            e.DrawFocusRectangle();//绘制焦点框
            e.Graphics.DrawImage(comboBox6.Items[e.Index] as Image, e.Bounds);
        }
        // 创建色带
        private IColorRamp CreateColorRamp(int size) //分级渲染
        {
            bool ok = false;
            IColorRamp pColorRamp = colorRampDictionary[comboBox1.SelectedIndex];
            pColorRamp.Size = size;
            pColorRamp.CreateRamp(out ok);
            return pColorRamp;
        }
        private IColorRamp getColorRamp(int size) //唯一值渲染
        {
            bool ok = false;
            IColorRamp pColorRamp = colorRampDictionary[comboBox6.SelectedIndex];
            pColorRamp.Size = size;
            pColorRamp.CreateRamp(out ok);
            return pColorRamp;
        }

        // 创建符号
        private ISymbol CreateSymbol(esriGeometryType geomerryType, IColor pColor)
        {
            ISymbol pSymbol = null;
            if (geomerryType == esriGeometryType.esriGeometryPoint)
            {
                ISimpleMarkerSymbol pSimpleMarkerSymbol = new SimpleMarkerSymbol();
                pSimpleMarkerSymbol.Color = pColor;
                pSymbol = pSimpleMarkerSymbol as ISymbol;
            }
            else if (geomerryType == esriGeometryType.esriGeometryPolyline)
            {
                ISimpleLineSymbol pSimpleLineSymbol = new SimpleLineSymbol();
                pSimpleLineSymbol.Color = pColor;
                pSymbol = pSimpleLineSymbol as ISymbol;
            }
            else
            {
                ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbol();
                pSimpleFillSymbol.Color = pColor;
                pSymbol = pSimpleFillSymbol as ISymbol;
            }
            return pSymbol;
        }

        #endregion 
        

        private void comboBox1_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //pictureBox1.Image = comboBox1.SelectedItem as Image;
        }
        

        private void button1_Click(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    IRaster raster0= pTocRasterLayer.Raster;
                    IRasterBandCollection rasterbandcollection0 = raster0 as IRasterBandCollection;
                    IRasterBand rasterband0 = rasterbandcollection0.Item(0);
                    IRasterDataset rasterdataset0 = rasterband0 as IRasterDataset;
                    UnqueValueRenderer(rasterdataset0);
                    break;
                case 1:
                    IRaster raster1= pTocRasterLayer.Raster;
                    IRasterBandCollection rasterbandcollection1 = raster1 as IRasterBandCollection;
                    IRasterBand rasterband1 = rasterbandcollection1.Item(0);
                    IRasterDataset rasterdataset1 = rasterband1 as IRasterDataset;
                    ClassifyRenderer(rasterdataset1);
                    break;
                case 2:
                    StretchRenderer(pTocRasterLayer);
                    break;
            }
        }
        public void ClassifyRenderer(IRasterDataset rasterDataset)  //分级渲染 没有Value
        {
            try
            {
                //Create the classify renderer.
                IRasterClassifyColorRampRenderer classifyRenderer = new
                  RasterClassifyColorRampRendererClass();
                IRasterRenderer rasterRenderer = (IRasterRenderer)classifyRenderer;

                int breaknum = Convert.ToInt32(comboBox2.Text);

                //Set up the renderer properties.
                IRaster raster = rasterDataset.CreateDefaultRaster();
                IRasterBandCollection rasterbandcollection2 = raster as IRasterBandCollection;
                IRasterBand rasterband2 = rasterbandcollection2.Item(0);
                if (rasterband2.Histogram == null)
                {
                    rasterband2.ComputeStatsAndHist();
                }

                rasterRenderer.Raster = raster;
                rasterRenderer.Update();

                //分类方法
                IClassify classify = null;

                switch (comboBox3.Text)
                {
                    case "等间距分级":
                        classify = new EqualIntervalClass();
                        break;
                    case "自然断点分级":
                        classify = new NaturalBreaksClass();
                        break;
                    case "分位数":
                        classify = new QuantileClass();
                        break;
                    case "几何间断":
                        classify = new GeometricalIntervalClass();
                        break;
                }

                classify.Classify(breaknum);
                double[] Classes = classify.ClassBreaks as double[];
                UID pUid = classify.ClassID;
                IRasterClassifyUIProperties rasClassifyUI = classifyRenderer as IRasterClassifyUIProperties;
                rasClassifyUI.ClassificationMethod = pUid;
                classifyRenderer.ClassCount = breaknum;
                

                //Set the color ramp for the symbology.

                IColorRamp pColorsRamp = CreateColorRamp(breaknum);
                //Create the symbol for the classes.
                IFillSymbol fillSymbol = new SimpleFillSymbolClass();
                ////for (int i = 0; i < classifyRenderer.ClassCount; i++)
                ////{
                ////    fillSymbol.Color = pColorsRamp.get_Color(i);
                ////    classifyRenderer.set_Symbol(i, (ISymbol)fillSymbol);
                ////    //classify后的label
                ////    //classifyRenderer.set_Label(i, Convert.ToString(i));
                ////    //classifyRenderer.set_Label(i, Classes[i].ToString("0.000") + "-" + Classes[i + 1].ToString("0.000"));
                ////}

                int gap = pColorsRamp.Size / (breaknum - 1);
                for (int i = 0; i < classifyRenderer.ClassCount; i++)
                {
                    int index;
                    if (i < classifyRenderer.ClassCount - 1)
                    {
                        index = i * gap;
                    }
                    else
                    {
                        index = pColorsRamp.Size - 1;
                    }
                    fillSymbol.Color = pColorsRamp.get_Color(index);
                    classifyRenderer.set_Symbol(i, fillSymbol as ISymbol);
                    classifyRenderer.set_Label(i, classifyRenderer.get_Break(i).ToString("0.00") + "-" + classifyRenderer.get_Break(i + 1).ToString("0.00"));
                }
                //
                pTocRasterLayer.Renderer = rasterRenderer;
                pTocControl.Update();
                pMapControl.Refresh();
                ////this.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void StretchRenderer(IRasterLayer pTocRasterLayer)   //拉伸着色
        {
            try
            {
                IRaster raster2 = pTocRasterLayer.Raster;
                IRasterBandCollection rasterbandcollection2 = raster2 as IRasterBandCollection;
                IRasterBand rasterband2 = rasterbandcollection2.Item(0);
                if (rasterband2.Histogram == null)
                {
                    rasterband2.ComputeStatsAndHist();
                }
                IRasterDataset rasterDataset = rasterband2 as IRasterDataset;

                //Create a stretch renderer.
                IRasterStretchColorRampRenderer stretchRenderer = new
                  RasterStretchColorRampRendererClass();
                IRasterRenderer rasterRenderer = (IRasterRenderer)stretchRenderer;
                //Set the renderer properties.
                IRaster raster = rasterDataset.CreateDefaultRaster();
                rasterRenderer.Raster = raster;
                rasterRenderer.Update();

                stretchRenderer.BandIndex = comboBox4.SelectedIndex;
                stretchRenderer.ColorRamp = colorRampDictionary[comboBox4.SelectedIndex];

                ////Set the stretch type.
                IRasterStretch stretchType = rasterRenderer as IRasterStretch;
                switch (comboBox5.Text)
                {
                    case "无": //None
                        stretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_NONE;
                        break;
                    case "自定义":
                        stretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_Custom;
                        break;
                    case "标准差":
                        stretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_StandardDeviations;
                        stretchType.StandardDeviationsParam = 2.5;
                        break;
                    case "直方图均衡化":
                        stretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_HistogramEqualize;
                        break;
                    case "最值":
                        stretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_MinimumMaximum;
                        break;
                    case "百分比截断":
                        stretchType.StretchType = esriRasterStretchTypesEnum.esriRasterStretch_PercentMinimumMaximum;
                        break;
                }
                rasterRenderer.Update();
                pTocRasterLayer.Renderer = rasterRenderer;
                pTocControl.Update();
                pMapControl.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public void UnqueValueRenderer(IRasterDataset rasterDataset) //唯一值
        {
            try
            {
                //Get the raster attribute table and the size of the table.
                IRaster2 raster = (IRaster2)rasterDataset.CreateDefaultRaster();
                ITable rasterTable = raster.AttributeTable;
                if (rasterTable == null)
                {
                    IRaster pRaster = rasterDataset.CreateDefaultRaster();   //定义一个栅格格式，用pRasterDataset创建一个栅格格式
                    IRasterLayer pRasterLayer = new RasterLayer();
                    pRasterLayer.CreateFromRaster(pRaster);
                    rasterTable = AttributeTable.BuildRasterTable(pRasterLayer as ILayer);
                }

                int tableRows = rasterTable.RowCount(null);

                IColorRamp colorRamp = getColorRamp(tableRows);

                //Create a unique value renderer.
                IRasterUniqueValueRenderer uvRenderer = new RasterUniqueValueRendererClass();
                IRasterRenderer rasterRenderer = (IRasterRenderer)uvRenderer;
                rasterRenderer.Raster = rasterDataset.CreateDefaultRaster();
                rasterRenderer.Update();

                //Set the renderer properties.
                uvRenderer.HeadingCount = 1;
                uvRenderer.set_Heading(0, "Value");
                uvRenderer.set_ClassCount(0, tableRows);
                uvRenderer.Field = "Value"; //Or any other field in the table.
                IRow row;
                ISimpleFillSymbol fillSymbol;
                for (int i = 0; i < tableRows; i++)
                {
                    row = rasterTable.GetRow(i);
                    uvRenderer.AddValue(0, i, Convert.ToInt16(row.get_Value(1)));
                    // Assuming the raster is 8-bit.
                    uvRenderer.set_Label(0, i, Convert.ToString(row.get_Value(1)));
                    fillSymbol = new SimpleFillSymbolClass();
                    fillSymbol.Color = colorRamp.get_Color(i);
                    uvRenderer.set_Symbol(0, i, (ISymbol)fillSymbol);
                }
                rasterRenderer.Update();
                pTocRasterLayer.Renderer = rasterRenderer;
                pTocControl.Update();

                pMapControl.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                MessageBox.Show("唯一值数量已达到限制（65536）");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
    }
}

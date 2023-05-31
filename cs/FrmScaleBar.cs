using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sunny.UI;

using stdole;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;

namespace cs
{
    public partial class FrmScaleBar : UIForm
    {
        // 定义事件
        public event Action<IScaleBar2> OnQueryScaleBar;

        // 样式变量
        private ISymbologyStyleClass m_SymbologyStyleClass;
        private IStyleGalleryItem m_StyleGalleryItem;
        private IScaleBar2 m_ScaleBar;
        public FrmScaleBar()
        {
            InitializeComponent();
        }

        private void FrmNorthArrow_Load(object sender, EventArgs e)
        {
            axSymbologyControl1.LoadStyleFile(@"E:\Program Files (x86)\ArcGIS\Engine10.4\Styles\ESRI.ServerStyle");
            
            axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassScaleBars;

            // 选择符号
            m_SymbologyStyleClass = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);
            m_SymbologyStyleClass.SelectItem(0);

            // 预览符号
            PriviewSymbol();
            uiIntegerUpDown1.Value = (int)m_ScaleBar.Divisions;
            uiIntegerUpDown2.Value = (int)m_ScaleBar.Subdivisions;
            this.Text = "比例尺设置项";

            uiComboBox1.Items.Add("Centimeters");
            uiComboBox1.Items.Add("Decimal Degrees");
            uiComboBox1.Items.Add("Decimeters");
            uiComboBox1.Items.Add("Feet");
            uiComboBox1.Items.Add("Inches");
            uiComboBox1.Items.Add("Kilometers");
            uiComboBox1.Items.Add("Meters");
            uiComboBox1.Items.Add("Miles");
            uiComboBox1.Items.Add("Millimeters");
            uiComboBox1.Items.Add("Nautical Miles");
            uiComboBox1.Items.Add("Points");
            uiComboBox1.Items.Add("Unknown Units");
            uiComboBox1.Items.Add("Yards");
            uiComboBox1.SelectedIndex = GetIndex(m_ScaleBar.Units);
        }
         // 符号预览 
        private void PriviewSymbol()
        {
            IPictureDisp pPictureDisp = m_SymbologyStyleClass.PreviewItem(m_StyleGalleryItem, pictureBox1.Width, pictureBox1.Height);
            Image priviewImage = Image.FromHbitmap(new IntPtr(pPictureDisp.Handle));
            pictureBox1.Image = priviewImage;
        }

        private void axSymbologyControl1_OnItemSelected(object sender, ISymbologyControlEvents_OnItemSelectedEvent e)
        {
            m_StyleGalleryItem = e.styleGalleryItem as IStyleGalleryItem;
            m_ScaleBar = m_StyleGalleryItem.Item as IScaleBar2;

            // 
            PriviewSymbol();
            uiIntegerUpDown1.Value = (int)m_ScaleBar.Divisions;
            uiIntegerUpDown2.Value = (int)m_ScaleBar.Subdivisions;
            uiButton4.FillColor = Utility.ConvertToColor(m_ScaleBar.BarColor);
            uiButton4.FillHoverColor = Utility.ConvertToColor(m_ScaleBar.BarColor);
            uiButton4.FillPressColor = Utility.ConvertToColor(m_ScaleBar.BarColor);
            
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (OnQueryScaleBar != null)
            {
                m_ScaleBar.BarHeight *= 3;
               
                OnQueryScaleBar(m_ScaleBar);
                this.Close();
                this.Dispose();
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void uiIntegerUpDown1_ValueChanged(object sender, int value)
        {
            m_ScaleBar.Divisions = (short)uiIntegerUpDown1.Value;
            PriviewSymbol();
        }

        private void uiButton4_Click(object sender, EventArgs e)
        {
            Control tt = sender as Control;
            //tt.PointToScreen(tt.Location).X;

            IColor pColor = new RgbColor();
            pColor.RGB = 255;
            int a = tt.Location.X;
            tagRECT pTag = new tagRECT();

            pTag.left = tt.PointToScreen(tt.Location).X;
            pTag.bottom = tt.PointToScreen(tt.Location).Y;
            IColorPalette pColorPalette = new ColorPalette();
            bool b = pColorPalette.TrackPopupMenu(ref pTag, pColor, false, 0);
            if (b)
            {
                pColor = pColorPalette.Color;
                uiButton4.FillColor = Utility.ConvertToColor(pColor);
                uiButton4.FillHoverColor = Utility.ConvertToColor(pColor);
                uiButton4.FillPressColor = Utility.ConvertToColor(pColor);
                m_ScaleBar.BarColor = pColor;
            }
        }

        private void uiIntegerUpDown2_ValueChanged(object sender, int value)
        {
            m_ScaleBar.Subdivisions = (short)uiIntegerUpDown2.Value;
            PriviewSymbol();
        }

        public int GetIndex(esriUnits Units)
        {
            int inx = -1;
            switch (Units)
            {
                case esriUnits.esriCentimeters:
                    inx = 0;
                    break;
                case esriUnits.esriDecimalDegrees:
                    inx = 1;
                    break;
                case esriUnits.esriDecimeters:
                    inx = 2;
                    break;
                case esriUnits.esriFeet:
                    inx = 3;
                    break;
                case esriUnits.esriInches:
                    inx = 4;
                    break;
                case esriUnits.esriKilometers:
                    inx = 5;
                    break;
                case esriUnits.esriMeters:
                    inx = 6;
                    break;
                case esriUnits.esriMiles:
                    inx = 7;
                    break;
                case esriUnits.esriMillimeters:
                    inx = 8;
                    break;
                case esriUnits.esriNauticalMiles:
                    inx = 9;
                    break;
                case esriUnits.esriPoints:
                    inx = 10;
                    break;
                case esriUnits.esriUnknownUnits:
                    inx = 11;
                    break;
                case esriUnits.esriYards:
                    inx = 12;
                    break;
            }
            return inx;
        }

        private void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (uiComboBox1.SelectedIndex)
            {
                case 0:
                    m_ScaleBar.Units = esriUnits.esriCentimeters;
                    break;
                case 1:
                    m_ScaleBar.Units = esriUnits.esriDecimalDegrees;
                    break;
                case 2:
                    m_ScaleBar.Units = esriUnits.esriDecimeters;
                    break;
                case 3:
                    m_ScaleBar.Units = esriUnits.esriFeet;
                    break;
                case 4:
                    m_ScaleBar.Units = esriUnits.esriInches;
                    break;
                case 5:
                    m_ScaleBar.Units = esriUnits.esriKilometers;
                    break;
                case 6:
                    m_ScaleBar.Units = esriUnits.esriMeters;
                    break;
                case 7:
                    m_ScaleBar.Units = esriUnits.esriMiles;
                    break;
                case 8:
                    m_ScaleBar.Units = esriUnits.esriMillimeters;
                    break;
                case 9:
                    m_ScaleBar.Units = esriUnits.esriNauticalMiles;
                    break;
                case 10:
                    m_ScaleBar.Units = esriUnits.esriPoints;
                    break;
                case 11:
                    m_ScaleBar.Units = esriUnits.esriUnknownUnits;
                    break;
                case 12:
                    m_ScaleBar.Units = esriUnits.esriYards;
                    break;
            }
            PriviewSymbol();
        }
    }
}

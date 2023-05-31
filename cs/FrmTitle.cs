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
    public partial class FrmTitle : UIForm
    {
        public event Action<ITextSymbol> OnQueryTitle;
        private ITextSymbol m_Title=new TextSymbolClass();
        public FrmTitle()
        {
            InitializeComponent();
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
                m_Title.Color = pColor;
            }
        }

        private void uiIntegerUpDown1_ValueChanged(object sender, int value)
        {
            m_Title.Size = uiIntegerUpDown1.Value;
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (OnQueryTitle != null)
            {
                if (uiTextBox1.Text != null)
                    m_Title.Text = uiTextBox1.Text;
                else m_Title.Text ="默认图名";
                OnQueryTitle(m_Title);
                this.Close();
                this.Dispose();
            }
        }

        private void FrmTitle_Load(object sender, EventArgs e)
        {
            m_Title.Color = GetRgbColor(0, 0, 0);
            uiButton4.FillColor = Utility.ConvertToColor(m_Title.Color);
            uiButton4.FillHoverColor = Utility.ConvertToColor(m_Title.Color);
            uiButton4.FillPressColor = Utility.ConvertToColor(m_Title.Color);
            m_Title.Size = uiIntegerUpDown1.Value = 30;
            this.Text = "图名设置项";
        }

        public IRgbColor GetRgbColor(int intR, int intG, int intB)
        {
            IRgbColor pRgbColor = null;
            if (intR < 0 || intR > 255 || intG < 0 || intG > 255 || intB < 0 || intB > 255)
            {
                return pRgbColor;
            }
            pRgbColor = new RgbColorClass();
            pRgbColor.Red = intR;
            pRgbColor.Green = intG;
            pRgbColor.Blue = intB;
            return pRgbColor;
        }
    }
}

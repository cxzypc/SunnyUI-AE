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
    public partial class FrmNorthArrow : UIForm
    {
        // 定义事件
        public event Action<INorthArrow> OnQueryNorthArrow;

        // 样式变量
        private ISymbologyStyleClass m_SymbologyStyleClass;
        private IStyleGalleryItem m_StyleGalleryItem;
        private INorthArrow m_NorthArrow;

        public FrmNorthArrow()
        {
            InitializeComponent();
        }

        private void FrmNorthArrow_Load(object sender, EventArgs e)
        {
            axSymbologyControl1.LoadStyleFile(@"E:\Program Files (x86)\ArcGIS\Engine10.4\Styles\ESRI.ServerStyle");
            
            axSymbologyControl1.StyleClass = esriSymbologyStyleClass.esriStyleClassNorthArrows;

            // 选择符号
            m_SymbologyStyleClass = axSymbologyControl1.GetStyleClass(axSymbologyControl1.StyleClass);
            m_SymbologyStyleClass.SelectItem(0);

            // 预览符号
            PriviewSymbol();
            uiIntegerUpDown1.Value = (int)m_NorthArrow.Size;
            this.Text = "指北针设置项";

            //uiComboBox1.SelectedColor = Utility.ConvertToColor(m_NorthArrow.Color);
            
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
            m_NorthArrow = m_StyleGalleryItem.Item as INorthArrow;

            // 
            PriviewSymbol();
            uiIntegerUpDown1.Value = (int)m_NorthArrow.Size;
            uiButton4.FillColor = Utility.ConvertToColor(m_NorthArrow.Color);
            uiButton4.FillHoverColor = Utility.ConvertToColor(m_NorthArrow.Color);
            uiButton1.FillPressColor = Utility.ConvertToColor(m_NorthArrow.Color);
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            if (OnQueryNorthArrow != null)
            {
                //m_NorthArrow.Size *= 3;
                OnQueryNorthArrow(m_NorthArrow);
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
            m_NorthArrow.Size = uiIntegerUpDown1.Value;
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
            //pTag.left = 250;
            //pTag.bottom = 300;

            pTag.left = tt.PointToScreen(tt.Location).X;
            pTag.bottom = tt.PointToScreen(tt.Location).Y;
            IColorPalette pColorPalette = new ColorPalette();
            bool b = pColorPalette.TrackPopupMenu(ref pTag, pColor, false, 0);
            if (b)
            {
                pColor = pColorPalette.Color;
                uiButton4.FillColor = Utility.ConvertToColor(pColor);
                uiButton4.FillHoverColor = Utility.ConvertToColor(pColor);
                uiButton1.FillPressColor = Utility.ConvertToColor(m_NorthArrow.Color);
                m_NorthArrow.Color = pColor;
            }
        }
    }
}

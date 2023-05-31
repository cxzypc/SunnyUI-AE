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

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.DataSourcesRaster;

namespace cs
{
    public partial class Txt : UIForm
    {
        //IPageLayoutControlDefault pPageLayout = null;
        AxPageLayoutControl pPageLayout = null;
        ITextElement pTextElement = null;
        ITextSymbol pTextSymbol = null;
        ICharacterOrientation pCharacterOrientation = null;
        IGraphicsContainer pGraphicsContainer = null;
        public Txt(AxPageLayoutControl pageLayout, ITextElement textElement)
        {
            InitializeComponent();
            pPageLayout = pageLayout;
            pTextElement = textElement;
            pTextSymbol = pTextElement.Symbol;
            pGraphicsContainer = pageLayout.ActiveView.GraphicsContainer;
        }

        private void Txt_Load(object sender, EventArgs e)
        {
            uiTextBox1.Text = pTextElement.Text;
            uiTextBox2.Text = pTextSymbol.Font.Name + "  " + pTextSymbol.Font.Size.ToString();
            uiDoubleUpDown1.Value = pTextSymbol.Angle;
            uiCheckBox1.Checked = pTextSymbol.Font.Bold;
            uiCheckBox2.Checked = pTextSymbol.Font.Italic;
            uiCheckBox3.Checked = pTextSymbol.Font.Underline;
            
            pCharacterOrientation = pTextSymbol as ICharacterOrientation;
            uiCheckBox6.Checked = pCharacterOrientation.CJKCharactersRotation;
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Font selectFont = fontDialog.Font;
                uiTextBox2.Text = selectFont.Name + "  " + selectFont.Size.ToString();
                stdole.IFontDisp pFont = ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(selectFont) as stdole.IFontDisp;

                pTextSymbol.Font = pFont;
            }
        }

        private void uiButton2_Click(object sender, EventArgs e)
        {
            pCharacterOrientation = pTextSymbol as ICharacterOrientation;
            pCharacterOrientation.CJKCharactersRotation = uiCheckBox6.Checked;

            stdole.IFontDisp pFont = pTextSymbol.Font;
            pFont.Bold = uiCheckBox1.Checked;
            pFont.Underline = uiCheckBox3.Checked;
            pFont.Italic = uiCheckBox2.Checked;
            pTextSymbol.Font = pFont;
            pTextSymbol.Angle = uiDoubleUpDown1.Value;

            pTextElement.Text = uiTextBox1.Text;
            pTextElement.Symbol = pTextSymbol;

            pGraphicsContainer.UpdateElement(pTextElement as IElement);
            pPageLayout.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            this.Close();
            this.Dispose();
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}

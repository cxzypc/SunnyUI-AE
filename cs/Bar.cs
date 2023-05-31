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
using ESRI.ArcGIS.esriSystem;

namespace cs
{
    public partial class Bar : UIForm
    {
        AxPageLayoutControl pPageLayout = null;
        ITextSymbol pTextSymbol = null;
        IGraphicsContainer pGraphicsContainer = null;
        IScaleBar2 pScaleBar = null;
        IGraphicElements tt = null;
        
        IElement pElement = null;
        public Bar(AxPageLayoutControl pageLayout, IElement selectElement)
        {
            InitializeComponent();
            pPageLayout = pageLayout;
            pGraphicsContainer = pageLayout.ActiveView.GraphicsContainer;
            IMapSurround mapSurround = ((IMapSurroundFrame)selectElement).MapSurround;
            pScaleBar = mapSurround as IScaleBar2;
            pElement = selectElement;
        }

        private void Bar_Load(object sender, EventArgs e)
        {
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
            uiComboBox1.SelectedIndex = GetIndex(pScaleBar.Units);
            uiIntegerUpDown1.Value = (int)pScaleBar.Divisions;
            uiIntegerUpDown2.Value = (int)pScaleBar.Subdivisions;
            pTextSymbol = pScaleBar.UnitLabelSymbol;
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (uiComboBox1.SelectedIndex)
            {
                case 0:
                    uiTextBox2.Text = "Centimeters";
                    break;
                case 1:
                    uiTextBox2.Text = "Decimal Degrees";
                    break;
                case 2:
                    uiTextBox2.Text = "Decimeters";
                    break;
                case 3:
                    uiTextBox2.Text = "Feet";
                    break;
                case 4:
                    uiTextBox2.Text = "Inches";
                    break;
                case 5:
                    uiTextBox2.Text = "Kilometers";
                    break;
                case 6:
                    uiTextBox2.Text = "Meters";
                    break;
                case 7:
                    uiTextBox2.Text = "Miles";
                    break;
                case 8:
                    uiTextBox2.Text = "Millimeters";
                    break;
                case 9:
                    uiTextBox2.Text = "Nautical Miles";
                    break;
                case 10:
                    uiTextBox2.Text = "Points";
                    break;
                case 11:
                    uiTextBox2.Text = "Unknown Units";
                    break;
                case 12:
                    uiTextBox2.Text = "Yards";
                    break;
            }
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

        public esriUnits GetUnits(int index) {
            esriUnits Units = new esriUnits();
            switch (index)
            {
                case 0:
                    Units = esriUnits.esriCentimeters;
                    break;
                case 1:
                    Units = esriUnits.esriDecimalDegrees;
                    break;
                case 2:
                    Units = esriUnits.esriDecimeters;
                    break;
                case 3:
                    Units = esriUnits.esriFeet;
                    break;
                case 4:
                    Units = esriUnits.esriInches;
                    break;
                case 5:
                    Units = esriUnits.esriKilometers;
                    break;
                case 6:
                    Units = esriUnits.esriMeters;
                    break;
                case 7:
                    Units = esriUnits.esriMiles;
                    break;
                case 8:
                    Units = esriUnits.esriMillimeters;
                    break;
                case 9:
                    Units = esriUnits.esriNauticalMiles;
                    break;
                case 10:
                    Units = esriUnits.esriPoints;
                    break;
                case 11:
                    Units = esriUnits.esriUnknownUnits;
                    break;
                case 12:
                    Units = esriUnits.esriYards;
                    break;
            }
            return Units;
        }
        private void uiButton2_Click(object sender, EventArgs e)
        {
            pScaleBar.Divisions = (short)uiIntegerUpDown1.Value;
            pScaleBar.Subdivisions = (short)uiIntegerUpDown2.Value;
            pScaleBar.Units = GetUnits(uiComboBox1.SelectedIndex);

            stdole.IFontDisp pFont = pTextSymbol.Font;
            pTextSymbol.Font = pFont;
            pScaleBar.UnitLabelSymbol = pTextSymbol;
            pScaleBar.UnitLabel = uiTextBox2.Text;

            pGraphicsContainer.UpdateElement(pElement as IElement);
            pPageLayout.ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
            this.Close();
            this.Dispose();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Font selectFont = fontDialog.Font;
                stdole.IFontDisp pFont = ESRI.ArcGIS.ADF.COMSupport.OLE.GetIFontDispFromFont(selectFont) as stdole.IFontDisp;

                pTextSymbol.Font = pFont;
            }
        }
    }
}

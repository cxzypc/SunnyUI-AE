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

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.esriSystem;

namespace cs
{
    public partial class MapPageSize : UIForm
    {
        AxPageLayoutControl pPageLayout = null;
        string[] Units = new string[4] { "Points", "Inches", "Centimeters", "Millimeters" };
        public MapPageSize(AxPageLayoutControl pageLayout)
        {
            InitializeComponent();
            pPageLayout = pageLayout;
        }

        private void uiButton3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void MapPageSize_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Units.Length; i++)
            {
                uiComboBox1.Items.Add(Units[i]);
                uiComboBox2.Items.Add(Units[i]);
            }
            uiTextBox1.Text = pPageLayout.PageLayout.Page.PrintableBounds.Width.ToString();
            uiTextBox2.Text = pPageLayout.PageLayout.Page.PrintableBounds.Height.ToString();
            uiComboBox1.SelectedIndex = GetIndex(pPageLayout.PageLayout.Page.Units);
            uiComboBox2.SelectedIndex = GetIndex(pPageLayout.PageLayout.Page.Units);

            if (pPageLayout.PageLayout.Page.Orientation == 1) uiRadioButton1.Checked = true;
            else if (pPageLayout.PageLayout.Page.Orientation == 2) uiRadioButton2.Checked = true;
        }

        public int GetIndex(esriUnits Units)
        {
            int inx = -1;
            switch (Units)
            {
                case esriUnits.esriPoints:
                    inx = 0;
                    break;
                case esriUnits.esriInches:
                    inx = 1;
                    break;
                case esriUnits.esriCentimeters:
                    inx = 2;
                    break;
                case esriUnits.esriMillimeters:
                    inx = 3;
                    break;
            }
            return inx;
        }
    }
}

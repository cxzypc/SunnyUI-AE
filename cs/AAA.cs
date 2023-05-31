using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.SystemUI;

namespace cs
{
    class AAA : IMenuDef
    {
        public string Caption
        {
            get
            {
                return "Navigation";
            }
        }
        public void GetItemInfo(int pos, IItemDef itemDef)      //POS为序列号
        {
            switch (pos)
            {
                case 0:
                    //itemDef.ID = "esriControls.ControlsMapZoomInFixedCommand";     //就是ESRI.ArcGIS.Controls下的类的名称
                    itemDef.ID = "cs.TurnAllLayersOnCmd";
                    break;
                case 1:
                    itemDef.ID = "esriControls.ControlsMapZoomOutFixedCommand";
                    break;
                case 2:
                    itemDef.ID = "esriControls.ControlsMapFullExtentCommand";
                    itemDef.Group = true;
                    break;
                case 3:
                    itemDef.ID = "esriControls.ControlsMapZoomToLastExtentBackCommand";
                    break;
                case 4:
                    itemDef.ID = "esriControls.ControlsMapZoomToLastExtentForwardCommand";
                    break;
            }
        }

        public int ItemCount
        {
            get
            {
                return 5;
            }
        }

        public string Name
        {
            get { return "Navigation"; }
        }
    }
}

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.DataSourcesFile;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Output;
using ESRI.ArcGIS.SystemUI;
using System.Windows.Forms;

namespace cs
{
    /// <summary>
    /// Summary description for Tool2.
    /// </summary>
    [Guid("d0e91e6a-1d47-4eed-a8e1-c1c0e0682a38")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("cs.Tool2")]
    public sealed class Tool2 : BaseTool
    {
        #region COM Registration Function(s)
        [ComRegisterFunction()]
        [ComVisible(false)]
        static void RegisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration(registerType);

            //
            // TODO: Add any COM registration code here
            //
        }

        [ComUnregisterFunction()]
        [ComVisible(false)]
        static void UnregisterFunction(Type registerType)
        {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration(registerType);

            //
            // TODO: Add any COM unregistration code here
            //
        }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Register(regKey);
            ControlsCommands.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);
            ControlsCommands.Unregister(regKey);
        }

        #endregion
        #endregion

        private IHookHelper m_hookHelper = null;
 
        private ISymbol _symbol;
        private bool _rotateTrackerTrigger = false;
        private IMovePointFeedback _pointFeedback;
        private IRotateTracker _rotateTracker;
 
        public IActiveView ActiveView { get; set; }

        public Tool2()
        {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text 
            base.m_caption = "";  //localizable text 
            base.m_message = "This should work in ArcMap/MapControl/PageLayoutControl";  //localizable text
            base.m_toolTip = "";  //localizable text
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_MyTool")
            try
            {
                //
                // TODO: change resource name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
                base.m_cursor = new System.Windows.Forms.Cursor(GetType(), GetType().Name + ".cur");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overridden Class Methods

        /// <summary>
        /// Occurs when this tool is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            try
            {
                m_hookHelper = new HookHelperClass();
                m_hookHelper.Hook = hook;
                ActiveView = m_hookHelper.ActiveView;
                if (m_hookHelper.ActiveView == null)
                {
                    m_hookHelper = null;
                }
            }
            catch
            {
                m_hookHelper = null;
            }

            if (m_hookHelper == null)
                base.m_enabled = false;
            else
                base.m_enabled = true;

            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this tool is clicked
        /// </summary>
        public override void OnClick()
        {
            IMarkerSymbol ms = new SimpleMarkerSymbol();
            ms.Size = 100;
            ISimpleMarkerSymbol sms = ms as ISimpleMarkerSymbol;
            sms.Style = esriSimpleMarkerStyle.esriSMSCross;
            _symbol = ms as ISymbol;
            _symbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
 
            _rotateTracker = new EngineRotateTracker();
            _rotateTracker.Display = ActiveView.ScreenDisplay;
            _rotateTracker.ClearGeometry();
 
            IPoint curPoint = new PointClass();
            curPoint.PutCoords(0, 0);
            _rotateTracker.OnMouseMove(curPoint);
            _rotateTracker.OnMouseUp();
 
            base.OnClick();
        }

        public override void OnMouseDown(int Button, int Shift, int X, int Y)
        {
             IPoint curPoint = ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            if (Button == 1)
            {
                if (!_rotateTrackerTrigger)
                {
                    if (_pointFeedback != null)
                    {
                        _pointFeedback.Stop();
                        _pointFeedback = null;
                    }
 
                    _rotateTracker.ClearGeometry();
                    _rotateTracker.Origin = curPoint;
                    _rotateTracker.AddPoint(curPoint, _symbol as IMarkerSymbol);
                    _rotateTracker.OnMouseDown();
                    
 
                    _rotateTrackerTrigger = true;
                }
                else
                {
                    if (_rotateTracker != null && _rotateTrackerTrigger)
                    {
                        bool rst = _rotateTracker.OnMouseUp();
                        if(rst)
                            CreateElement(_rotateTracker.Origin, _rotateTracker.Angle);
 
                        _rotateTrackerTrigger = false;
                    }
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add Tool2.OnMouseMove implementation
             IPoint curPoint = ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            if (!_rotateTrackerTrigger)
            {
                if (_pointFeedback == null)
                {
                    _pointFeedback = new MovePointFeedbackClass();
                    _pointFeedback.Display = ActiveView.ScreenDisplay;
                    _pointFeedback.Symbol = _symbol;
                    _pointFeedback.Start(curPoint, curPoint);
                }
                _pointFeedback.MoveTo(curPoint);
            }
            else
            {
                if(_rotateTracker != null)
                    _rotateTracker.OnMouseMove(curPoint);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add Tool2.OnMouseUp implementation
        }

        public override void Refresh(int hDC)
        {
            if (_pointFeedback != null)
            {
                _pointFeedback.Refresh(hDC);
            }

            if (_rotateTracker != null && _rotateTrackerTrigger)
            {
                _rotateTracker.Refresh();
            }
            base.Refresh(hDC);
        }
        #endregion

        private void CreateElement(IGeometry pGeometry, double angle)
        {
            IMarkerSymbol ms = ((IClone)_symbol).Clone() as IMarkerSymbol;
            ms.Angle = angle * (180 / Math.PI);//180/¦Ð¡Á»¡¶È
 
            IMarkerElement pMarkerElement = new MarkerElementClass();
            pMarkerElement.Symbol = ms;
 
            IElement pElement = pMarkerElement as IElement;
            pElement.Geometry = pGeometry;
 
            ActiveView.GraphicsContainer.AddElement(pElement, 0);
 
            IEnvelope pEnvelope = new EnvelopeClass();
            pElement.QueryBounds(ActiveView.ScreenDisplay, pEnvelope);
            ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, pEnvelope);
        }
    }
}

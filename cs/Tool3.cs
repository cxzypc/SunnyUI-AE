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
using ESRI.ArcGIS.CartoUI;
using System.Windows.Forms;

namespace cs
{
    /// <summary>
    /// Summary description for Tool3.
    /// </summary>
    [Guid("d74362cc-e599-42cf-a327-fdfa906ddd63")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("cs.Tool3")]
    public sealed class Tool3 : BaseTool
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
        private IElement pElement;
        private ISimpleFillSymbol pSimpleFillSymbol;

        private IScaleTracker pScaleTracker;
        public IActiveView ActiveView { get; set; }

        public Tool3()
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

            IRgbColor pRgbColor = new RgbColor();
            pRgbColor.Red = 255;
            pRgbColor.Green = 100;
            pRgbColor.Blue = 0;

            // 创建符号
            pSimpleFillSymbol = new SimpleFillSymbol();
            pSimpleFillSymbol.Color = pRgbColor;
            pSimpleFillSymbol.Style = esriSimpleFillStyle.esriSFSCross;


            IGraphicsContainer pGraphicsContainer = m_hookHelper.ActiveView.GraphicsContainer;
            IGraphicsContainerSelect pGraphicsContainerSelect = pGraphicsContainer as IGraphicsContainerSelect;

            // 判断是否有元素被选中
            IEnumElement pEnumElement = pGraphicsContainerSelect.SelectedElements;
            pEnumElement.Reset();
            pElement = pEnumElement.Next();
            if (pElement == null)
            {
                MessageBox.Show("当前没有被选中的元素");
                return;
            }

            pScaleTracker = new ScaleTrackerClass();
            pScaleTracker.Display = ActiveView.ScreenDisplay;
            pScaleTracker.ClearGeometry();

            IPoint curPoint = new PointClass();
            curPoint.PutCoords(0, 0);
            pScaleTracker.OnMouseMove(curPoint);
            pScaleTracker.OnMouseUp();

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

                    pScaleTracker.ClearGeometry();
                    pScaleTracker.Origin = curPoint;
                    pScaleTracker.AddGeometry(pElement.Geometry);
                    pScaleTracker.OnMouseDown();

                    _rotateTrackerTrigger = true;
                }
                else
                {
                    if (pScaleTracker != null && _rotateTrackerTrigger)
                    {
                        bool rst = pScaleTracker.OnMouseUp();
                        if (rst) {
                            CreateElement(pScaleTracker.Origin, pScaleTracker.ScaleFactor);
                            //(m_hookHelper.Hook as IPageLayoutControl).CurrentTool = null;
                        }
                        _rotateTrackerTrigger = false;
                    }
                }
            }
        }

        public override void OnMouseMove(int Button, int Shift, int X, int Y)
        {
            IPoint curPoint = ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y);
            if (!_rotateTrackerTrigger)
            {
                if (_pointFeedback == null)
                {
                    _pointFeedback = new MovePointFeedbackClass();
                    _pointFeedback.Display = ActiveView.ScreenDisplay;
                    //_pointFeedback.Symbol = _symbol;
                    _pointFeedback.Start(curPoint, curPoint);
                }
                _pointFeedback.MoveTo(curPoint);
            }
            else
            {
                if (pScaleTracker != null)
                    pScaleTracker.OnMouseMove(curPoint);
            }
        }

        public override void OnMouseUp(int Button, int Shift, int X, int Y)
        {
            // TODO:  Add Tool3.OnMouseUp implementation
        }
        #endregion

        private void CreateElement(IPoint point, double angle)
        {
            IElement pScaleElement = ((IClone)pElement).Clone() as IElement;
            IGeometry ttGeometry = Telescopic(pScaleElement.Geometry, angle, point);

            //IFillShapeElement pFillShapeElement = new PolygonElement() as IFillShapeElement;
            //pFillShapeElement.Symbol = pSimpleFillSymbol;

            //IElement Element = pFillShapeElement as IElement;
            //Element.Geometry = ttGeometry;

            //ActiveView.GraphicsContainer.AddElement(Element, 0);

            //IEnvelope pEnvelope = new EnvelopeClass();
            //pElement.QueryBounds(ActiveView.ScreenDisplay, pEnvelope);
            //ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);

            IMapSurroundFrame pMapSf = pElement as IMapSurroundFrame;
            IMapSurround pMapSurround = pMapSf.MapSurround;
            bool tt;
            pMapSurround.FitToBounds(ActiveView.ScreenDisplay, ttGeometry.Envelope, out tt);

            ActiveView.PartialRefresh(esriViewDrawPhase.esriViewGraphics, null, null);
        }

        private IGeometry Telescopic(IGeometry pGeometry, double size,IPoint point)
        {
            IEnvelope pEnvelope = pGeometry.Envelope;

            // 返回结果
            ITransform2D pTransform2D = pGeometry as ITransform2D;
            pTransform2D.Scale(point, size, size);
            return pTransform2D as IGeometry;
        }


        public override void Refresh(int hDC)
        {
            if (_pointFeedback != null)
            {
                _pointFeedback.Refresh(hDC);
            }

            if (pScaleTracker != null && _rotateTrackerTrigger)
            {
                pScaleTracker.Refresh();
            }
            base.Refresh(hDC);
        }

        public IRgbColor getRGB(int r, int g, int b)
        {
            IRgbColor pcolor = new RgbColorClass();
            pcolor.Red = r;
            pcolor.Green = g;
            pcolor.Blue = b;
            return pcolor;
        }
    }
}

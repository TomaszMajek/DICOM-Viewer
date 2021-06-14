using System.Collections.ObjectModel;

namespace DICOM_Viewer_v2
{
    public class DicomChart
    {
        public ObservableCollection<DicomChartItem> ChartItems { get; set; } = new ObservableCollection<DicomChartItem>();
    }
    public class DicomChartItem
    {
        public float _Value { get; set; }

        public DicomChartItem(float Value)
        {
            _Value = Value;
        }
    }
}

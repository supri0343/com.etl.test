using System;

namespace Com.Danliris.ETL.Service.ExcelModels.DashboardKoranLaborat
{
    public class DashboardRnDTestResult
    {
        public DashboardRnDTestResult(){

        }
        public DashboardRnDTestResult(string id, int no, DateTime date, string sOFSPP, string construction, decimal? tegewa, decimal? fabricPHDrop, decimal? absorbtionDrop, string capillary, decimal? whiteness, string shading31, string shading14, string shading42, string capillarityLeft, string capillarityCenter, string capillarityRight):this()
        {
            this.Id = id;
            this.No = no;
            this.Date = date;
            this.SOFSPP = sOFSPP;
            this.Construction = construction;
            this.Tegewa = tegewa;
            this.FabricPHDrop = fabricPHDrop;
            this.AbsorbtionDrop = absorbtionDrop;
            this.Capillary = capillary;
            this.Whiteness = whiteness;
            this.Shading31 = shading31;
            this.Shading14 = shading14;
            this.Shading42 = shading42;
            this.CapillarityLeft = capillarityLeft;
            this.CapillarityCenter = capillarityCenter;
            this.CapillarityRight = capillarityRight;

        }
        public string Id { get; set; }
        public int No { get; set; }
        public DateTime Date { get; set; }
        public string SOFSPP { get; set; }
        public string Construction { get; set; }
        public decimal? Tegewa { get; set; }
        public decimal? FabricPHDrop { get; set; }
        public decimal? AbsorbtionDrop { get; set; }
        public string Capillary { get; set; }
        public decimal? Whiteness { get; set; }
        public string Shading31 { get; set; }
        public string Shading14 { get; set; }
        public string Shading42 { get; set; }
        public string CapillarityLeft { get; set; }
        public string CapillarityCenter { get; set; }
        public string CapillarityRight { get; set; }
    }
}
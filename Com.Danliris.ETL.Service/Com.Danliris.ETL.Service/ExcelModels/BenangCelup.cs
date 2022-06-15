namespace Com.Danliris.ETL.Service.ExcelModels
{
    public class BenangCelup
    {
        public BenangCelup(string nosp, string color, string referenceNo)
        {
            this.nosp = nosp;
            this.color = color;
            this.referenceNo = referenceNo;
        }
        public string nosp {get; set;}
        public string color {get; set;}
        public string referenceNo {get; set;}
    }
}
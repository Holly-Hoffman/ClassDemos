using System.Text;

namespace PrsWeb.Models
{
    public class RequestForm
    {
        public int UserId { get; set; }
        public string Description { get; set; }
        public string Justification { get; set; }
        public DateOnly? DateNeeded { get; set; }
        public string DeliveryMode { get; set; }
        //public string RequestNumber { get; set; }

        //public string RNum(DateOnly today)
        //{
        //    StringBuilder requestNumber = new StringBuilder("");


        //    int counter = 0;

        //    counter++;
        //    today = DateOnly.FromDateTime(DateTime.Now);

        //    string rNumerals = String.Format("{0:yyMMdd}", today);

        //    requestNumber.Append("R");
        //    requestNumber.Append(rNumerals);
        //    requestNumber.Append(counter.ToString().PadLeft(4, '0'));

        //    int lastReq = Int32.Parse(rNumerals);
        //    var maxReq = context.Requests.Max(r => r.RequestNumber);


        //    return requestNumber.ToString();
        //}
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoAPIDSalesDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        public static List<string> regions = new List<string>(); //{ "North", "South", "East", "West"}

        [HttpGet]
        public List<string> GetAll()
        {
            if (regions.Count == 0)
            {
                return new List<string> {"No regions in list yet."};
            }
            else
            {return regions;}
        }

        [HttpGet("{idx}")]
        public string GetRegionByIdx(int idx)
        {
            if (!ValidIndex(idx)) { return "No region at that index position. Try again."; }
            else
            {
                return regions[idx];
            }
        }

        [HttpPost]
        public string AddRegion(string region)
        {
            regions.Add(region);
            return "Region added";
        }

        [HttpDelete("{idx}")]
        public string RemoveRegion(int idx)
        {
            regions.RemoveAt(idx);
            return "Region removed.";
        }

        [HttpPut]
        public string UpdateRegion(int idx, string uRegion)
        {
            if (!ValidIndex(idx)) { return "No region at that index position. Try again."; }
            else
            {
                regions[idx] = uRegion;
                return "Region updated.";
            }
        }

        private bool ValidIndex(int idx)
        {
            bool isValid = true;
            if (idx < 0 || idx >= regions.Count)
            {
                isValid = false;
            }
            return isValid;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NexxAPI.Controllers
{
    public class TransferController : ApiController
    {
        public static List<Transfer> reports = new List<Transfer>
        {
            new Transfer
            {
                Id = 1,
                User_id = 1,
                Acc_onwer = "Mario",
                Acc_onwer_bank = "Santander",
                Acc_onwer_agency = "1111",
                Acc_onwer_number = "15588",
                Reciver_name = "Luigi",
                Reciver_bank = "Real",
                Reciver_agency = "1111",
                Reciver_acc = "12345",
                Transaction_type = "TED",
                Value = 1000.00,
                Status = "OK"
            }
         };

        [HttpGet]
        // GET api/<controller>
        public List<Transfer> Get()
        {
            return reports;
        }

        [HttpGet]
        // GET api/<controller>/5
        public Transfer Get(int id)
        {
            return reports.Find((r) => r.Id == id);
        }

        [HttpPost]
        // POST api/<controller>
        public bool Post(Transfer transfer)
        {
            try
            {
                reports.Add(transfer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        // DELETE api/<controller>/5
        public bool Delete(int id)
        {
            try
            {
                var itemToRemove = reports.Find((r) => r.Id == id);
                reports.Remove(itemToRemove);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

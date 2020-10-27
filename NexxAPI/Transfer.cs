using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NexxAPI
{
    public class Transfer
    {
        public int Id { get; set; }

        public int User_id { get; set; }

        public string Acc_onwer { get; set; }

        public string Acc_onwer_bank { get; set; }

        public string Acc_onwer_agency { get; set; }

        public string Acc_onwer_number { get; set; }

        public string Reciver_name { get; set;}

        public string Reciver_bank { get; set; }

        public string Reciver_acc { get; set; }

        public string Reciver_agency { get; set; }

        public double Value { get; set; }

        public string Transaction_type { get; set; }

        public string Status { get; set; }
    }
}
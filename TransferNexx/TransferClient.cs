using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TransferNexx
{
    class TransferClient
    {

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter Action:");
                string id = Console.ReadLine();

                GetRequest(id).Wait();
                Console.ReadKey();
                Console.Clear();
            }
        }

        static async Task GetRequest(string ID)
        {
            bool isvalied = true;

            switch (ID)
            {
                //Get Request    
                case "Get":
                    Console.WriteLine("Enter id:");
                    int id = Convert.ToInt32(Console.ReadLine());
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:38104/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response;

                        //id == 0 means select all records    
                        if (id == 0)
                        {
                            response = await client.GetAsync("api/Transfer");
                            if (response.IsSuccessStatusCode)
                            {
                                TransferClient[] reports = await response.Content.ReadAsAsync<TransferClient[]>();
                                foreach (var report in reports)
                                {
                                    Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}", report.Id, report.User_id, report.Acc_onwer, report.Acc_onwer_bank, report.Acc_onwer_agency, report.Acc_onwer_number, report.Reciver_name, report.Reciver_bank, report.Reciver_agency, report.Reciver_acc, report.Transaction_type, report.Value, report.Status);
                                }
                            }
                        }
                        else
                        {
                            response = await client.GetAsync("api/Weather/" + id);
                            if (response.IsSuccessStatusCode)
                            {
                                TransferClient report = await response.Content.ReadAsAsync<TransferClient>();
                                Console.WriteLine("\n{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}", report.Id, report.User_id, report.Acc_onwer, report.Acc_onwer_bank, report.Acc_onwer_agency, report.Acc_onwer_number, report.Reciver_name, report.Reciver_bank, report.Reciver_agency, report.Reciver_acc, report.Transaction_type, report.Value, report.Status);
                            }
                        }
                    }
                    break;

                //Post Request    
                case "Post":
                    TransferClient newReport = new TransferClient();
                    Console.WriteLine("O Id da transação:");
                    newReport.Id = Console.ReadLine();
                    Console.WriteLine("O Id do dono da conta:");
                    newReport.User_id = Console.ReadLine();
                    Console.WriteLine("O nome do dono da conta:");
                    newReport.Acc_onwer = Console.ReadLine();
                    Console.WriteLine("O nome do banco da conta:");
                    newReport.Acc_onwer_bank = Console.ReadLine();
                    Console.WriteLine("o numero da agencia da conta:");
                    newReport.Acc_onwer_agency = Console.ReadLine();
                    Console.WriteLine("O numero da conta:");
                    newReport.Acc_onwer_number = Console.ReadLine();
                    Console.WriteLine("O nome de quem vai receber:");
                    newReport.Reciver_name = Console.ReadLine();
                    Console.WriteLine("O nome do banco que vai receber:");
                    newReport.Reciver_bank = Console.ReadLine();
                    Console.WriteLine("O numero da agencia que vai receber:");
                    newReport.Reciver_agency = Console.ReadLine();
                    Console.WriteLine("O numero da conta que vai receber:");
                    newReport.Reciver_acc = Console.ReadLine();

                    Console.WriteLine("O valor da transacao:");
                    newReport.Value = Console.ReadLine();

                    do
                    {
                        if (newReport.Value > 100000)
                        {
                            Console.WriteLine("O valor da transacao nao pode ser maior que 100000");
                            Console.WriteLine("Digite o novo valor da transacao:");
                            newReport.Value = Console.ReadLine();
                        }
                        else
                        {
                            isvalied = true;
                        }
                    } while (isvalied == true);

                    Console.WriteLine("O tipo da transacao:");
                    newReport.Transaction_type = Console.ReadLine();

                    do
                    {
                        if (newReport.Transaction_type == "TED" && newReport.Value >= 5000)
                        {
                            Console.WriteLine("O Devido ao valor da transacao a operacao nao pode ser TED");
                            newReport.Transaction_type = "DOC";
                        }
                        else
                        {
                            isvalied = true;
                        }

                        if (newReport.Transaction_type == "CC" && newReport.Reciver_acc != newReport.Acc_onwer_number)
                        {
                            Console.WriteLine("O Devido ao valor da transacao a operacao nao pode ser CC");
                            Console.WriteLine("O tipo da transacao:");
                            newReport.Transaction_type = Console.ReadLine();
                        }
                        else
                        {
                            isvalied = true;
                        }
                    } while (isvalied == true);


                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:38104/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.PostAsJsonAsync("api/Transfer", newReport);

                        if (response.IsSuccessStatusCode)
                        {
                            bool result = await response.Content.ReadAsAsync<bool>();
                            if (result)
                                Console.WriteLine("Report Submitted");
                            else
                                Console.WriteLine("An error has occurred");
                        }
                    }

                    break;

                //Delete Request    
                case "Delete":
                    Console.WriteLine("Enter id:");
                    int delete = Convert.ToInt32(Console.ReadLine());
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:38104/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.DeleteAsync("api/Weather/" + delete);

                        if (response.IsSuccessStatusCode)
                        {
                            bool result = await response.Content.ReadAsAsync<bool>();
                            if (result)
                                Console.WriteLine("Report Deleted");
                            else
                                Console.WriteLine("An error has occurred");
                        }
                    }
                    break;
            }

        }
    }
}


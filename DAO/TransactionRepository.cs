
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
namespace DAO
{
    public class TransactionRepository
    {
        private readonly string _cnx;

        public TransactionRepository(IConfiguration configuration)
        {
            _cnx = configuration.GetConnectionString("DevConection");
        }
        public async Task<IEnumerable<TransactionModel>> Select()
        {
            List<TransactionModel> lstTransaction = new List<TransactionModel>();
            using (SqlConnection con = new SqlConnection(_cnx))
            {
                SqlCommand cmd = new SqlCommand("Wsp_ConsultaTransaction", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read())
                {
                    TransactionModel transaction = new TransactionModel();
                    transaction.TransactionId = (int)reader["TransactionId"];
                     transaction.AccountNumber = reader["AccountNumber"].ToString();
                    transaction.BeneficiaryName = reader["BeneficiaryName"].ToString();
                    transaction.BankName = (string)reader["BankName"];
                    transaction.SWIFTCode = (string)reader["SWIFTCode"];
                    transaction.Date = (DateTime)reader["Date"];
                }
                con.Close();
            }
            return lstTransaction.ToList();
        }







        public class TransactionModel
        {
            public int TransactionId { get; set; }
            public string AccountNumber { get; set; }
            public string BeneficiaryName { get; set; }
            public string BankName { get; set; }
            public string SWIFTCode { get; set; }
            public int Amount { get; set; }
            public DateTime Date { get; set; }

        }

    }

}

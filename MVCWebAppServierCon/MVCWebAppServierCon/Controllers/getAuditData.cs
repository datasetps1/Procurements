using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCWebAppServierCon.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using MVCWebAppServierCon.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using MVCWebAppServierCon.Helpers;

namespace MVCWebAppServierCon.Controllers
{
    public class getAuditData
    {




        public List<CostsViewModel> projLoad(string tblName, SqlConnection connection, string connect_with)
        {// use sql command to make new query to get data from cost table that needed in the order


            connection.Open();
            SqlCommand command = new SqlCommand();
            if (connect_with == Constants.audit)
            {
                command = new SqlCommand("SELECT Code,Name FROM " + tblName + " Where freeze=0;", connection);
            }
            else if (connect_with == Constants.finpack)
            {
                command = new SqlCommand("SELECT Code,Name FROM " + tblName + " Where Status=1;", connection);
            }

            var reader = command.ExecuteReader();
            List<CostsViewModel> costLst = new List<CostsViewModel>();
            while (reader.Read())
            {
                CostsViewModel costs = new CostsViewModel();
                costs.costCode = reader.GetValue(0).ToString();
                costs.costName = reader.GetValue(1).ToString();
                costs.costBudget = -1; //float.Parse(reader.GetValue(2).ToString());
                costLst.Add(costs);

                // do something with 'value'
            }
            connection.Close();
            return costLst;
        }


        public string getTblCodeName(string tblName, string code, SqlConnection connection)
        {// use sql command to make new query to get data from cost table that needed in the order
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT Name FROM " + tblName + " Where Code= '" + code + "';", connection);
            var reader = command.ExecuteReader();
            String name = "";
            while (reader.Read())
            {
                name = reader.GetValue(0).ToString();

                // do something with 'value'
            }
            connection.Close();
            return name;
        }
        public int GetPriceQouteAmountForSupplier(string Suppliercode, SqlConnection connection)
        {// use sql command to make new query to get data from cost table that needed in the order
            connection.Open();
            SqlCommand command = new SqlCommand("SELECT CreditLimit FROM Extra " + " Where Code= '" + Suppliercode + "';", connection);
            var reader = command.ExecuteReader();
            int PriceQouteAmount = 0;
            while (reader.Read())
            {
                PriceQouteAmount = (int)reader.GetValue(0);

                // do something with 'value'
            }
            connection.Close();
            return PriceQouteAmount;
        }
        public List<CodeNameModel> getTableData(string tblName, string condition, SqlConnection connection)
        {
            connection.Open();

            SqlCommand command = new SqlCommand("SELECT Code,Name FROM " + tblName + condition + "  ;", connection);
            var reader = command.ExecuteReader();
            List<CodeNameModel> Lst = new List<CodeNameModel>();
            while (reader.Read())
            {
                CodeNameModel obj = new CodeNameModel();
                obj.Code = reader.GetValue(0).ToString();
                obj.Name = reader.GetValue(1).ToString();

                Lst.Add(obj);

                // do something with 'value'
            }
            connection.Close();
            return Lst;
        }

        public string getCodeByName(string tblName, string name, SqlConnection connection)
        {
            ConnectionState state = connection.State;
            if (state == ConnectionState.Closed)
            {
                connection.Open();
            }

            SqlCommand command = new SqlCommand("SELECT code FROM " + tblName + " Where name= @Name", connection);
            command.Parameters.AddWithValue("@Name", name);
            var reader = command.ExecuteReader();
            String code = "";
            while (reader.Read())
            {
                code = reader.GetValue(0).ToString();

                // do something with 'value'
            }
            connection.Close();
            return code;
        }


        public float GetRate(string CurrencyCode, SqlConnection connection, string connect_with)
        {
            connection.Open();
            var date = DateTime.Now.ToString("yyyy-MM-dd");  //'2020-02-24'

            //decide finpack or audit

            SqlCommand command = new SqlCommand();
            if (connect_with == Constants.audit)
            {
                command = new SqlCommand("SELECT Rate FROM Rate WHERE (Code = '" + CurrencyCode + "' and RateDate = '" + date + "');", connection); // + "' and RateDate = '" + DateTime.Today.Date.ToString("yyyy-MM-dd")
            }
            else if (connect_with == Constants.finpack)
            {
                command = new SqlCommand("SELECT Rate FROM " + Constants_Finpack.rateTable + " WHERE (Code = '" + CurrencyCode + "' and RDate = '" + date + "');", connection); // + "' and RateDate = '" + DateTime.Today.Date.ToString("yyyy-MM-dd")
            }


            var reader = command.ExecuteReader();
            float rate = 0;
            while (reader.Read())
            {
                rate = float.Parse(reader.GetValue(0).ToString());
            }
            connection.Close();
            return rate;
        }
    }
}

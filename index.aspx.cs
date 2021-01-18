using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;

namespace Worles_Po_Status
{
    public partial class index : System.Web.UI.Page
    {
        string copyvalue;
        DataTable datatable = new DataTable();
        string connectionString = ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CheckBox1.Checked = true;
                if (CheckBox1.Checked == true)
                {
                    getData();
                }
            }
            
        }

        public void getData()
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(connectionString);
                List<String> worlespolist = new List<String>();
                List<long> sortedlist = new List<long>();
                List<long> finallist = new List<long>();
                List<String> dateList = new List<String>();

                OleDbCommand command = new OleDbCommand("SELECT WorlesPo from [Pedidos de clientes] where WorlesPo IS Not Null Order By WorlesPo Desc");
                command.Connection = conn;
                conn.Open();
                OleDbDataReader dr = command.ExecuteReader();

                while (dr.Read())
                {
                    string worlespo = (dr["WorlesPo"].ToString()).Trim();

                    long outPut;
                    bool nummm = long.TryParse(worlespo, out outPut);



                    if (worlespo != "")
                    {
                        if (worlespo.Contains("e") || worlespo.Contains("E") || worlespo.Contains("S") || worlespo.Contains("A") || worlespo.Contains("V") || worlespo.Contains("u") || worlespo.Contains("T") || worlespo.Contains("P"))
                        {

                        }
                        else if (worlespo.Contains("-"))
                        {

                            if (copyvalue != worlespo)
                            {
                                copyvalue = worlespo;
                            }
                        }
                        else if (worlespo.Length == 5)
                        {
                            worlespolist.Add((worlespo));
                        }
                    }
                }

                for (int i = 0; i < worlespolist.Count; i++)
                {
                    for (int j = i + 1; j < worlespolist.Count; j++)
                    {
                        long first = Convert.ToInt64(worlespolist[i]);
                        long last = Convert.ToInt64(worlespolist[j]);
                        long result = first - last;

                        if (last != 0)
                        {
                            if (result > 1)
                            {
                                for (long k = last + 1; k < first; k++)
                                {
                                    sortedlist.Add(k);
                                }
                            }
                        }
                        break;
                    }
                }
                sortedlist.Sort();
                sortedlist.Reverse();

                for (int i = 0; i < 20; i++)
                {
                    finallist.Add(sortedlist[i]);

                    string presentWorlesPo = (sortedlist[i] + 1).ToString();

                    OleDbCommand com = new OleDbCommand("Select Top 1 WorlesPo, PedPed from[Pedidos de clientes] Where WorlesPo = '" + presentWorlesPo + "'", conn);
                    OleDbDataReader dataReader = com.ExecuteReader();

                    if (dataReader.HasRows == true)
                    {
                        while (dataReader.Read())
                        {
                            if (CheckBox1.Checked == true)
                            {
                                if (dataReader["PedPed"].ToString() != "")
                                {
                                    string pedped = dataReader["PedPed"].ToString();
                                    int index = pedped.LastIndexOf(' ');
                                    string date = pedped.Substring(index + 1);
                                    DateTime today = DateTime.Today;

                                    int days = ((Convert.ToDateTime(date) - today).Duration()).Days;

                                    if (days <= 10)
                                    {
                                        string worlesPo = dataReader["WorlesPo"].ToString();
                                        dateList.Add("Po " + worlesPo + " " + date);
                                    }
                                }
                                else
                                {
                                    dateList.Add("--");
                                }
                            }
                            else
                            {
                                if (dataReader["PedPed"].ToString() != "")
                                {
                                    string pedped = dataReader["PedPed"].ToString();
                                    int index = pedped.LastIndexOf(' ');
                                    string date = pedped.Substring(index + 1);
                                    string worlesPo = dataReader["WorlesPo"].ToString();
                                    dateList.Add("Po " + worlesPo + " " + date);
                                }
                                else
                                {
                                    dateList.Add("--");
                                }
                            }
                        }
                    }
                    else
                    {
                        dateList.Add("--");
                    }
                }


                datatable.Columns.Add("S.No.");
                datatable.Columns.Add("Missing");
                datatable.Columns.Add("Date");


                if (CheckBox1.Checked == true)
                {
                    for (int i = 0; i < dateList.Count(); i++)
                    {
                        datatable.Rows.Add(i + 1, finallist[i], dateList[i]);
                    }
                    Session["Last10DaysMissingPoData"] = datatable;
                }
                else
                {
                    for (int i = 0; i < finallist.Count(); i++)
                    {
                        datatable.Rows.Add(i + 1, finallist[i], dateList[i]);
                    }
                }

                GridView1.DataSource = datatable;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            getData();
        }
    }
}
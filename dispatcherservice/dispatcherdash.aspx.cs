using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Web.Caching;

namespace dispatcherservice
{
    /// <summary>
    /// 
    /// </summary>
    public partial class dispatcherdash : System.Web.UI.Page
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {

            if (Cache["fdeData"] != null)
            {
                grdFDE.DataSource = Cache["fdeData"];
                grdFDE.DataBind();

                lblStatus.Text = "Fuel Delivery Event data retrieved from cache at " + DateTime.Now.ToString();
            }
            else
            {
                string cs = ConfigurationManager.ConnectionStrings["efc_testConnectionString"].ConnectionString;

                System.Web.Caching.SqlCacheDependencyAdmin.EnableNotifications(cs);
                System.Web.Caching.SqlCacheDependencyAdmin.EnableTableForNotifications(cs, "fuel_delivery_event");

                SqlConnection con = new SqlConnection(cs);
                //SqlDataAdapter sda = new SqlDataAdapter("select * from fuel_delivery_event", con);

                SqlDataAdapter sda = new SqlDataAdapter(@"SELECT 

                       s.stop AS 'Current Stop'
                      , f2.[current_fuel_level] AS 'Previous Fuel Level'
                      , f.[current_fuel_level] AS 'Current Fuel Level'
                      , (f2.[current_fuel_level] - f.[current_fuel_level]) AS 'Fuel Consumed at Stop'
                      , o.region AS 'Region'
                      , t.truck AS 'Truck'
                      , f.[stop_datetime] AS 'Date'
                  FROM[dbo].[fuel_delivery_event] f CROSS JOIN[dbo].[fuel_delivery_event] f2

                  JOIN[stops] s ON f.current_stop = s.pkey
                  JOIN[operating_regions] o ON s.operating_region = o.pkey
                  JOIN[trucks] t ON s.operating_region = t.operating_region

                  WHERE f.current_stop = f2.next_stop

                  AND truck != 'All_Trucks'

                    ", con);

                //AND Region = '" + DropDownList1.SelectedValue + "'", con);

                DataSet ds = new DataSet();
                sda.Fill(ds);

                SqlCacheDependency sqlDependency = new SqlCacheDependency("efc_test", "fuel_delivery_event");
                Cache.Insert("fdeData", ds, sqlDependency);

                grdFDE.DataSource = ds;
                grdFDE.DataBind();
                lblStatus.Text = "Fuel Delivery Event data retrieved at " + DateTime.Now.ToString();
            }
        }
    }
}
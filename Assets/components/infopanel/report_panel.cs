using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using System.Drawing;
using System.Globalization;

public class report_panel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMPro.TMP_Text from_time;
    [SerializeField] TMPro.TMP_Text to_time;
    [SerializeField] TMPro.TMP_Text salepoint;
    [SerializeField] TMPro.TMP_Text winpoint;
    [SerializeField] TMPro.TMP_Text commipoint;
    [SerializeField] TMPro.TMP_Text ntppoint;
    [SerializeField] TMPro.TMP_Text operatorpoint;
    [SerializeField] CalendarController tocalender;
    [SerializeField] CalendarController fromcalender;
    public int mode=0;
    public void setdata()
    {
        int ppoint = 0;
        int wpoint = 0;
        int epoint = 0;
        int npoint = 0;
        int ppoints = 0;

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        try
        {
            sqlCmnd.CommandTimeout = 60;
            sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
            sqlCmnd.CommandType = CommandType.Text;
            sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as ppoint" +
                ",ISNULL(sum(clm),0) as wpoint,ISNULL(sum(qty),0)-ISNULL(sum(clm),0) as epoint," +
                "ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as npoint," +
                "ISNULL(sum(qty),0)*g.comm/100 as ppoints from bet16 n, " +
                "g_master g where g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and g_date between '" + fromcalender.datetimeyear + "' and  '" + tocalender.datetimeyear + "' and term_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id +
                "'group by n.ter_id,g.term_name,g.comm;";
            //
            sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
            //(sqlCmnd.CommandText);
            if (sqlData.Read())
            {


                if (sqlData["ppoint"] != null || sqlData["ppoint"].ToString().Trim() != string.Empty)
                {
                    ppoint += Convert.ToInt32(sqlData["ppoint"].ToString());
                }
                if (sqlData["wpoint"] != null || sqlData["wpoint"].ToString().Trim() != string.Empty)
                {
                    wpoint += Convert.ToInt32(sqlData["wpoint"].ToString());
                }
                if (sqlData["epoint"] != null || sqlData["epoint"].ToString().Trim() != string.Empty)
                {
                    epoint += Convert.ToInt32(sqlData["epoint"].ToString());
                }
                if (sqlData["npoint"] != null || sqlData["npoint"].ToString().Trim() != string.Empty)
                {
                    npoint += Convert.ToInt32(sqlData["npoint"].ToString());
                }
                if (sqlData["ppoints"] != null || sqlData["ppoints"].ToString().Trim() != string.Empty)
                {
                    ppoints += Convert.ToInt32(sqlData["ppoints"].ToString());
                }

            }
            sqlData.Close();
            sqlData.DisposeAsync();
            sqlCmnd = new SqlCommand();
            sqlData = null;
            sqlCmnd.CommandTimeout = 60;
            sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
            sqlCmnd.CommandType = CommandType.Text;
            sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as ppoint" +
                ",ISNULL(sum(clm),0) as wpoint,ISNULL(sum(qty),0)-ISNULL(sum(clm),0) as epoint," +
                "ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as npoint," +
                "ISNULL(sum(qty),0)*g.comm/100 as ppoints from tengp n, " +
                "g_master g where g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and g_date between '" + fromcalender.datetimeyear + "' and  '" + tocalender.datetimeyear + "' and term_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id +
                "'group by n.ter_id,g.term_name,g.comm;";
            //
            sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
            //(sqlCmnd.CommandText);
            if (sqlData.Read())
            {


                if (sqlData["ppoint"] != null || sqlData["ppoint"].ToString().Trim() != string.Empty)
                {
                    ppoint += Convert.ToInt32(sqlData["ppoint"].ToString());
                }
                if (sqlData["wpoint"] != null || sqlData["wpoint"].ToString().Trim() != string.Empty)
                {
                    wpoint += Convert.ToInt32(sqlData["wpoint"].ToString());
                }
                if (sqlData["epoint"] != null || sqlData["epoint"].ToString().Trim() != string.Empty)
                {
                    epoint += Convert.ToInt32(sqlData["epoint"].ToString());
                }
                if (sqlData["npoint"] != null || sqlData["npoint"].ToString().Trim() != string.Empty)
                {
                    npoint += Convert.ToInt32(sqlData["npoint"].ToString());
                }
                if (sqlData["ppoints"] != null || sqlData["ppoints"].ToString().Trim() != string.Empty)
                {
                    ppoints += Convert.ToInt32(sqlData["ppoints"].ToString());
                }

            }
            sqlData.Close();
            sqlData.DisposeAsync();
            sqlCmnd = new SqlCommand();
            sqlData = null;
            sqlCmnd.CommandTimeout = 60;
            sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
            sqlCmnd.CommandType = CommandType.Text;
            sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as ppoint" +
                ",ISNULL(sum(clm),0) as wpoint,ISNULL(sum(qty),0)-ISNULL(sum(clm),0) as epoint," +
                "ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as npoint," +
                "ISNULL(sum(qty),0)*g.comm/100 as ppoints from tasp n, " +
                "g_master g where g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and g_date between '" + fromcalender.datetimeyear + "' and  '" + tocalender.datetimeyear + "' and term_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id +
                "'group by n.ter_id,g.term_name,g.comm;";
            //
            sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
            //(sqlCmnd.CommandText);
            if (sqlData.Read())
            {


                if (sqlData["ppoint"] != null || sqlData["ppoint"].ToString().Trim() != string.Empty)
                {
                    ppoint += Convert.ToInt32(sqlData["ppoint"].ToString());
                }
                if (sqlData["wpoint"] != null || sqlData["wpoint"].ToString().Trim() != string.Empty)
                {
                    wpoint += Convert.ToInt32(sqlData["wpoint"].ToString());
                }
                if (sqlData["epoint"] != null || sqlData["epoint"].ToString().Trim() != string.Empty)
                {
                    epoint += Convert.ToInt32(sqlData["epoint"].ToString());
                }
                if (sqlData["npoint"] != null || sqlData["npoint"].ToString().Trim() != string.Empty)
                {
                    npoint += Convert.ToInt32(sqlData["npoint"].ToString());
                }
                if (sqlData["ppoints"] != null || sqlData["ppoints"].ToString().Trim() != string.Empty)
                {
                    ppoints += Convert.ToInt32(sqlData["ppoints"].ToString());
                }
                salepoint.text = ppoint.ToString();
                winpoint.text = wpoint.ToString();
                commipoint.text = npoint.ToString();
                ntppoint.text = ppoints.ToString();
                operatorpoint.text = epoint.ToString();
                //(sqlData["plyid"].ToString());
            }
            sqlData.Close();
            sqlData.DisposeAsync();
        }
        catch
        {
            sqlData.Close();
            sqlData.DisposeAsync();
        }
    }

}

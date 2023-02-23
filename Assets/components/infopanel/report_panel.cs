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
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        if (mode == 0)
        {
            sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as sale_point,ISNULL(sum(clm),0) as win_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0) as operator_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as ntp_point,ISNULL(sum(qty),0)*g.comm/100 as commision_points from tasp n, g_master g where g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and g_date between '" + fromcalender.datetimeyear + "' and  '" + tocalender.datetimeyear + "'group by n.ter_id,g.term_name,g.comm";
        }
        if (mode == 1)
        {
            sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as sale_point,ISNULL(sum(clm),0) as win_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0) as operator_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as ntp_point,ISNULL(sum(qty),0)*g.comm/100 as commision_points from bet16 n, g_master g where g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and g_date between '" + fromcalender.datetimeyear + "' and  '" + tocalender.datetimeyear + "'group by n.ter_id,g.term_name,g.comm";
        }
        if (mode == 2)
        {
            sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as sale_point,ISNULL(sum(clm),0) as win_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0) as operator_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as ntp_point,ISNULL(sum(qty),0)*g.comm/100 as commision_points from tengp n, g_master g where g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and g_date between '" + fromcalender.datetimeyear + "' and  '" + tocalender.datetimeyear + "'group by n.ter_id,g.term_name,g.comm";
        }
        //
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        print(sqlCmnd.CommandText);
        if (sqlData.Read())
        {
            from_time.text = fromcalender.datetimeyear;
            to_time.text = tocalender.datetimeyear;
            salepoint.text = sqlData["sale_point"].ToString();
            winpoint.text = sqlData["win_point"].ToString();
            commipoint.text = sqlData["operator_point"].ToString();
            operatorpoint.text = sqlData["ntp_point"].ToString();
            if (Convert.ToInt32(sqlData["ntp_point"].ToString()) < 0)
            {
                int x = Convert.ToInt32(sqlData["operator_point"].ToString()) - Convert.ToInt32(sqlData["ntp_point"].ToString());
                ntppoint.text = x.ToString();
            }
            else if (Convert.ToInt32(sqlData["ntp_point"].ToString()) > 0)
            {
                ntppoint.text = sqlData["commision_points"].ToString();
            }

            print(sqlData["plyid"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();
    }

}

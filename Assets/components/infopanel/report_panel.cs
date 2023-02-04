using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class report_panel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]CalendarController fromdate;
    [SerializeField] CalendarController todate;
    [SerializeField] TMPro.TMP_Text pname;
    [SerializeField] TMPro.TMP_Text play;
    [SerializeField] TMPro.TMP_Text win_text;
    [SerializeField] TMPro.TMP_Text claim_text;
    [SerializeField] TMPro.TMP_Text unclalim_text;
    [SerializeField] TMPro.TMP_Text end_text;
    [SerializeField] TMPro.TMP_Text commi_text;
    [SerializeField] TMPro.TMP_Text ntp_text;



    public void onreportinfoclicked()
    {

        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "select distinct g.term_name,n.ter_id plyid,ISNULL(sum(qty),0) as sale_point,ISNULL(sum(clm),0) as win_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0) as operator_point,ISNULL(sum(qty),0)-ISNULL(sum(clm),0)-(ISNULL(sum(qty),0)*g.comm/100) as ntp_point,ISNULL(sum(qty),0)*g.comm/100 as commision_points from tasp n, g_master g where g.term_id=n.ter_id and n.id is not null and n.status not in('Canceled') and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and g_date between '" +fromdate.datetimeyear+ "' and  '" + todate.datetimeyear + "'group by n.ter_id,g.term_name,g.comm";


        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        print(sqlCmnd.CommandText);
        if (sqlData.Read())
        {
            pname.text = sqlData["plyid"].ToString();
            play.text = sqlData["sale_point"].ToString();
            win_text.text = sqlData["win_point"].ToString();
            commi_text.text = sqlData["operator_point"].ToString();
         //   operatorpoint.text = sqlData["ntp_point"].ToString();
            if (Convert.ToInt32(sqlData["ntp_point"].ToString()) < 0)
            {
                int x = Convert.ToInt32(sqlData["operator_point"].ToString()) - Convert.ToInt32(sqlData["ntp_point"].ToString());
                ntp_text.text = x.ToString();
            }
            else if (Convert.ToInt32(sqlData["ntp_point"].ToString()) > 0)
            {
                ntp_text.text = sqlData["commision_points"].ToString();
            }

            print(sqlData["plyid"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class historypanel : MonoBehaviour
{
   [SerializeField] GameObject content;

    [SerializeField] GameObject prefabitem;
    public string barcode;
   
    public void onResultsbuttonClicked()
    {
        print("report button clicked");
        foreach (Transform tf in content.GetComponentInChildren<Transform>())
        {
            if (tf != content.transform)
            {
                Destroy(tf.gameObject);
            }
        }
        
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT [taas].[dbo].[tasp].id,[taas].[dbo].[tasp].bar,[taas].[dbo].[tasp].g_id,[taas].[dbo].[tasp].g_date,[taas].[dbo].[tasp].ter_id,[taas].[dbo].[tasp].clm,[taas].[dbo].[tasp].tot,[taas].[dbo].[tasp].status,[taas].[dbo].[tasp].g_time,[taas].[dbo].[tasp].p_time,[taas].[dbo].[resultsTaa].result as gameresult FROM [taas].[dbo].[tasp],[taas].[dbo].[resultsTaa] WHERE resultsTaa.g_date=tasp.g_date and resultsTaa.g_time=tasp.g_time and ter_id='"+GameObject.FindObjectOfType<userManager>().getUserData().id+"' and [taas].[dbo].[tasp].g_date='"+GameObject.FindObjectOfType<SQL_manager>().server_day+"' order by g_id desc";
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        print(sqlCmnd.CommandText);
       
        //
        while (sqlData.Read())
        {
             GameObject gb = (GameObject)Instantiate(prefabitem);
             gb.transform.SetParent(content.transform);
             gb.transform.position = Vector3.zero;
             gb.GetComponent<historyInfoSetter>().setdata(sqlData["id"].ToString(), sqlData["bar"].ToString(), sqlData["tot"].ToString(), sqlData["clm"].ToString(), sqlData["gameresult"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();

    }
}

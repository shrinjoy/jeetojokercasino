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
    public int mode = 0;
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
        if (mode == 0)
        {
            sqlCmnd.CommandText = "SELECT [taas].[dbo].[tasp].id,[taas].[dbo].[tasp].bar,[taas].[dbo].[tasp].g_id,[taas].[dbo].[tasp].g_date,[taas].[dbo].[tasp].ter_id,[taas].[dbo].[tasp].clm,[taas].[dbo].[tasp].tot,[taas].[dbo].[tasp].status,[taas].[dbo].[tasp].g_time,[taas].[dbo].[tasp].p_time,[taas].[dbo].[resultsTaa].result as gameresult FROM [taas].[dbo].[tasp],[taas].[dbo].[resultsTaa] WHERE resultsTaa.g_date=tasp.g_date and resultsTaa.g_time=tasp.g_time and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[tasp].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' order by g_id desc";
        
        }
        if (mode == 1)
        {
            sqlCmnd.CommandText = "SELECT [taas].[dbo].[bet16].id,[taas].[dbo].[bet16].bar,[taas].[dbo].[bet16].g_id,[taas].[dbo].[bet16].g_date,[taas].[dbo].[bet16].ter_id,[taas].[dbo].[bet16].clm,[taas].[dbo].[bet16].tot,[taas].[dbo].[bet16].status,[taas].[dbo].[bet16].g_time,[taas].[dbo].[bet16].p_time,[taas].[dbo].[results16].result as gameresult FROM [taas].[dbo].[bet16],[taas].[dbo].[results16] WHERE results16.g_date=bet16.g_date and results16.g_time=bet16.g_time and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[bet16].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' order by g_id desc";

        }
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        print(sqlCmnd.CommandText);
       
        ////
        while (sqlData.Read())
        {
             GameObject gb = (GameObject)Instantiate(prefabitem);
             gb.transform.SetParent(content.transform,false);
             
             gb.GetComponent<historyInfoSetter>().setdata(sqlData["id"].ToString(), sqlData["bar"].ToString(), sqlData["tot"].ToString(), sqlData["clm"].ToString(), converttoresult(sqlData["gameresult"].ToString()));
        }
        sqlData.Close();
        sqlData.DisposeAsync();

    }
    public string converttoresult(string result)
    {
        if (mode == 0) { 
        if (result == "NR00")
        {
            return "JC";
        }
        else if (result == "NR01")
        {
            return "JD";
        }
        else if (result == "NR02")
        {
            return  "JS";
        }
        //
        else if (result == "NR03")
        {
            return "JH";
        }
        //
        else if (result == "NR04")
        {
            return "QC";
        }
        else if (result == "NR05")
        {
            return "QD";
        }
        else if (result == "NR06")
        {
            return "QS";
        }
        else if (result == "NR07")
        {
            return "QH";
        }
        //
        else if (result == "NR08")
        {
            return "KC";
        }
        else if (result == "NR09")
        {
            return "KD";
        }
        else if (result == "NR10")
        {
            return "KS";
        }
        else if (result == "NR11")
        {
            return "KH";
        }
        return "Null";
            }
        if (mode == 1)
        {
            if (result == "NR00")
            {
                return "AC";
            }
            else if (result == "NR01")
            {
                return "AD";
            }
            else if (result == "NR02")
            {
                return "AS";
            }
            //
            else if (result == "NR03")
            {
                return "AH";
            }
            //
            else if (result == "NR04")
            {
                return "KC";
            }
            else if (result == "NR05")
            {
                return "KD";
            }
            else if (result == "NR06")
            {
                return "KS";
            }
            else if (result == "NR07")
            {
                return "KH";
            }
            //
            else if (result == "NR08")
            {
                return "QC";
            }
            else if (result == "NR09")
            {
                return "QD";
            }
            else if (result == "NR10")
            {
                return "QS";
            }
            else if (result == "NR11")
            {
                return "QH";
            }
            //
            else if (result == "NR12")
            {
                return "JC";
            }
            else if (result == "NR13")
            {
                return "JD";
            }
            else if (result == "NR14")
            {
                return "JS";
            }
            else if (result == "NR15")
            {
                return "JH";
            }
           
        }
        return "Null";
    }
}

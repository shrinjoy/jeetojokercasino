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
    public bool canremove = false;
    public int mode = 0;
    public void onResultsbuttonClicked()
    {
        //print("report button clicked");
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
        //'" + GameObject.FindObjectOfType<userManager>().getUserData().id + "'
        //'" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "'
        if (mode == 0)
        {
            //tasp
            //resultsTaa
            sqlCmnd.CommandText = "SELECT [taas].[dbo].[tasp].status,[taas].[dbo].[tasp].clm_tm,ISNULL([taas].[dbo].[tasp].st_point,0) as st_point,[taas].[dbo].[tasp].id,[taas].[dbo].[tasp].bar,[taas].[dbo].[tasp].g_id,[taas].[dbo].[tasp].g_date,[taas].[dbo].[tasp].ter_id,ISNULL([taas].[dbo].[tasp].clm,0) as clm,[taas].[dbo].[tasp].tot,[taas].[dbo].[tasp].status,[taas].[dbo].[tasp].g_time,[taas].[dbo].[tasp].p_time,[taas].[dbo].[resultsTaa].result as gameresult FROM [taas].[dbo].[tasp],[taas].[dbo].[resultsTaa] WHERE resultsTaa.g_date=tasp.g_date and resultsTaa.g_time=tasp.g_time and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[tasp].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' union all SELECT [taas].[dbo].[tasp].status,[taas].[dbo].[tasp].clm_tm,ISNULL([taas].[dbo].[tasp].st_point,0) as st_point,[taas].[dbo].[tasp].id,[taas].[dbo].[tasp].bar,[taas].[dbo].[tasp].g_id,[taas].[dbo].[tasp].g_date,[taas].[dbo].[tasp].ter_id,ISNULL([taas].[dbo].[tasp].clm,0) as clm,[taas].[dbo].[tasp].tot,[taas].[dbo].[tasp].status,[taas].[dbo].[tasp].g_time,[taas].[dbo].[tasp].p_time,'' as gameresult FROM [taas].[dbo].[tasp] WHERE   ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[tasp].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' and tasp.id >(SELECT isnull(max(n.id),0) as id  FROM tasp n, resultsTaa r  where n.g_date=r.g_date and n.g_time=r.g_time  and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' ) order by g_id desc";


                }
        if (mode == 1)
        {
            //bet16
            //results16
            sqlCmnd.CommandText = "SELECT [taas].[dbo].[bet16].status,[taas].[dbo].[bet16].clm_tm,ISNULL([taas].[dbo].[bet16].st_point,0) as st_point,[taas].[dbo].[bet16].id,[taas].[dbo].[bet16].bar,[taas].[dbo].[bet16].g_id,[taas].[dbo].[bet16].g_date,[taas].[dbo].[bet16].ter_id,ISNULL([taas].[dbo].[bet16].clm,0) as clm,[taas].[dbo].[bet16].tot,[taas].[dbo].[bet16].status,[taas].[dbo].[bet16].g_time,[taas].[dbo].[bet16].p_time,[taas].[dbo].[results16].result as gameresult FROM [taas].[dbo].[bet16],[taas].[dbo].[results16] WHERE results16.g_date=bet16.g_date and results16.g_time=bet16.g_time and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[bet16].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' union all SELECT [taas].[dbo].[bet16].status,[taas].[dbo].[bet16].clm_tm,ISNULL([taas].[dbo].[bet16].st_point,0) as st_point,[taas].[dbo].[bet16].id,[taas].[dbo].[bet16].bar,[taas].[dbo].[bet16].g_id,[taas].[dbo].[bet16].g_date,[taas].[dbo].[bet16].ter_id,ISNULL([taas].[dbo].[bet16].clm,0) as clm,[taas].[dbo].[bet16].tot,[taas].[dbo].[bet16].status,[taas].[dbo].[bet16].g_time,[taas].[dbo].[bet16].p_time,'' as gameresult FROM [taas].[dbo].[bet16] WHERE   ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[bet16].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' and bet16.id >(SELECT isnull(max(n.id),0) as id  FROM bet16 n, results16 r  where n.g_date=r.g_date and n.g_time=r.g_time  and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' ) order by g_id desc";
        }
        if (mode == 2)
        {
            //tengp
            //results
            sqlCmnd.CommandText = "SELECT [taas].[dbo].[tengp].status,[taas].[dbo].[tengp].clm_tm,ISNULL([taas].[dbo].[tengp].st_point,0) as st_point,[taas].[dbo].[tengp].id,[taas].[dbo].[tengp].bar,[taas].[dbo].[tengp].g_id,[taas].[dbo].[tengp].g_date,[taas].[dbo].[tengp].ter_id,ISNULL([taas].[dbo].[tengp].clm,0) as clm,[taas].[dbo].[tengp].tot,[taas].[dbo].[tengp].status,[taas].[dbo].[tengp].g_time,[taas].[dbo].[tengp].p_time,[taas].[dbo].[results].result as gameresult FROM [taas].[dbo].[tengp],[taas].[dbo].[results] WHERE results.g_date=tengp.g_date and results.g_time=tengp.g_time and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[tengp].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' union all SELECT [taas].[dbo].[tengp].status,[taas].[dbo].[tengp].clm_tm,ISNULL([taas].[dbo].[tengp].st_point,0) as st_point,[taas].[dbo].[tengp].id,[taas].[dbo].[tengp].bar,[taas].[dbo].[tengp].g_id,[taas].[dbo].[tengp].g_date,[taas].[dbo].[tengp].ter_id,ISNULL([taas].[dbo].[tengp].clm,0) as clm,[taas].[dbo].[tengp].tot,[taas].[dbo].[tengp].status,[taas].[dbo].[tengp].g_time,[taas].[dbo].[tengp].p_time,'' as gameresult FROM [taas].[dbo].[tengp] WHERE   ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[tengp].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' and tengp.id >(SELECT isnull(max(n.id),0) as id  FROM tengp n, results r  where n.g_date=r.g_date and n.g_time=r.g_time  and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' ) order by g_id desc";
        }

        if (mode == 3)
        {
            sqlCmnd.CommandText = "SELECT [taas].[dbo].[doup].status,[taas].[dbo].[doup].clm_tm,ISNULL([taas].[dbo].[doup].st_point,0) as st_point,[taas].[dbo].[doup].id,[taas].[dbo].[doup].bar,[taas].[dbo].[doup].g_id,[taas].[dbo].[doup].g_date,[taas].[dbo].[doup].ter_id,ISNULL([taas].[dbo].[doup].clm,0) as clm,ISNULL([taas].[dbo].[doup].sclm,0) as sclm,[taas].[dbo].[doup].tot,[taas].[dbo].[doup].status,[taas].[dbo].[doup].g_time,[taas].[dbo].[doup].p_time,[taas].[dbo].[resultsDou].result as gameresult FROM [taas].[dbo].[doup],[taas].[dbo].[resultsDou] WHERE resultsDou.g_date=doup.g_date and resultsDou.g_time=doup.g_time and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[doup].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' union all SELECT [taas].[dbo].[doup].status,[taas].[dbo].[doup].clm_tm,ISNULL([taas].[dbo].[doup].st_point,0) as st_point,[taas].[dbo].[doup].id,[taas].[dbo].[doup].bar,[taas].[dbo].[doup].g_id,[taas].[dbo].[doup].g_date,[taas].[dbo].[doup].ter_id,ISNULL([taas].[dbo].[doup].clm,0) as clm,ISNULL([taas].[dbo].[doup].sclm,0) as sclm,[taas].[dbo].[doup].tot,[taas].[dbo].[doup].status,[taas].[dbo].[doup].g_time,[taas].[dbo].[doup].p_time,'' as gameresult FROM [taas].[dbo].[doup] WHERE   ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' and [taas].[dbo].[doup].g_date='" + GameObject.FindObjectOfType<SQL_manager>().server_day.ToString("dd-MMM-yyyy") + "' and doup.id >(SELECT isnull(max(n.id),0) as id  FROM doup n, resultsDou r  where n.g_date=r.g_date and n.g_time=r.g_time  and n.ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "' ) order by g_id desc";

        }
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        print(sqlCmnd.CommandText);

        ////
        while (sqlData.Read())
        {
            if (sqlData["status"].ToString().ToUpper() != "CANCELED")
            {
                GameObject gb = (GameObject)Instantiate(prefabitem);
                gb.transform.SetParent(content.transform, false);
                if (mode < 3)
                {
                    gb.GetComponent<historyInfoSetter>().setdata(sqlData["id"]?.ToString(), sqlData["bar"]?.ToString(), sqlData["tot"]?.ToString(), sqlData["clm"]?.ToString(), converttoresult(sqlData["gameresult"].ToString()), sqlData["status"].ToString(), sqlData["st_point"].ToString(), sqlData["clm_tm"].ToString());
                }
                if (mode == 3)
                {
                    string totalclm = (Convert.ToInt32(sqlData["clm"]?.ToString()) + Convert.ToInt32(sqlData["sclm"]?.ToString())).ToString();


                    gb.GetComponent<historyInfoSetter>().setdata(sqlData["id"]?.ToString(), sqlData["bar"]?.ToString(), sqlData["tot"]?.ToString(), totalclm, converttoresult(sqlData["gameresult"]?.ToString()), sqlData["status"]?.ToString(), sqlData["st_point"]?.ToString(), sqlData["clm_tm"]?.ToString());

                }
            }
        }
        sqlData.Close();
        sqlData.DisposeAsync();

    }
    public string converttoresult(string result)
    {
        if (mode == 0)
        {
            if (result == "NR00")
            {
                return "JH";
            }
            else if (result == "NR01")
            {
                return "JS";
            }
            else if (result == "NR02")
            {
                return "JD";
            }
            //
            else if (result == "NR03")
            {
                return "JC";
            }
            //
            else if (result == "NR04")
            {
                return "QH";
            }
            else if (result == "NR05")
            {
                return "QS";
            }
            else if (result == "NR06")
            {
                return "QD";
            }
            else if (result == "NR07")
            {
                return "QC";
            }
            //
            else if (result == "NR08")
            {
                return "KH";
            }
            else if (result == "NR09")
            {
                return "KS";
            }
            else if (result == "NR10")
            {
                return "KD";
            }
            else if (result == "NR11")
            {
                return "KC";
            }
            return "Null";
        }
        if (mode == 1)
        {
            if (result == "NR00")
            {
                return "AH";
            }
            else if (result == "NR01")
            {
                return "AS";
            }
            else if (result == "NR02")
            {
                return "AD";
            }
            //
            else if (result == "NR03")
            {
                return "AC";
            }
            //
            else if (result == "NR04")
            {
                return "KH";
            }
            else if (result == "NR05")
            {
                return "KS";
            }
            else if (result == "NR06")
            {
                return "KD";
            }
            else if (result == "NR07")
            {
                return "KC";
            }
            //
            else if (result == "NR08")
            {
                return "QH";
            }
            else if (result == "NR09")
            {
                return "QS";
            }
            else if (result == "NR10")
            {
                return "QD";
            }
            else if (result == "NR11")
            {
                return "QC";
            }
            //
            else if (result == "NR12")
            {
                return "JH";
            }
            else if (result == "NR13")
            {
                return "JS";
            }
            else if (result == "NR14")
            {
                return "JD";
            }
            else if (result == "NR15")
            {
                return "JC";
            }
            //
        }
        if (mode == 2)
        {
            if (result == "NR00")
            {
                return "0";
            }
            else if (result == "NR01")
            {
                return "1";
            }
            else if (result == "NR02")
            {
                return "2";
            }
            //
            else if (result == "NR03")
            {
                return "3";
            }
            //
            else if (result == "NR04")
            {
                return "4";
            }
            else if (result == "NR05")
            {
                return "5";
            }
            else if (result == "NR06")
            {
                return "6";
            }
            else if (result == "NR07")
            {
                return "7";
            }
            //
            else if (result == "NR08")
            {
                return "8";
            }
            else if (result == "NR09")
            {
                return "9";
            }

            return "Null";
        }
        if (mode == 3)
        {
            print(result);
            if (result.ToUpper() == "NULL" || result.Trim() == string.Empty)
            {
                return "NULL";
            }
            else
            {
                return result.Substring(2, 2);
            }
        }
        return "Null";
    }
}

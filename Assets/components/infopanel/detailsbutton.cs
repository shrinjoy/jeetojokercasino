using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class detailsbutton : MonoBehaviour
{
    [SerializeField] GameObject detailsprefab;
    [SerializeField] GameObject content;
    [SerializeField] GameObject detailspanel;
    [SerializeField]int mode = 0;

    public void ShowDetails()
    {
        detailspanel.SetActive(true);
        try
        {
            print("mode:" + mode);
            if (GameObject.FindObjectOfType<historypanel>().barcode != null)
            {
                foreach (Transform t in content.GetComponentsInChildren<Transform>())
                {
                    if (t.transform != content.transform)
                    {
                        Destroy(t.gameObject);
                    }
                }
                string cmnd = "";

                if (mode == 0)
                {
                    // cmnd = "select 'JH' nam, a00 ply, CASE WHEN r.result='NR00' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='"+ GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'JS' nam, a01 ply, CASE WHEN r.result='NR01' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'JD' nam, a02 ply, CASE WHEN r.result='NR02' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'JC' nam, a03 ply, CASE WHEN r.result='NR03' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='"+ GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'QH' nam, a04 ply, CASE WHEN r.result='NR04' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='"+ GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'QS' nam, a05 ply, CASE WHEN r.result='NR05' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'QD' nam, a06 ply, CASE WHEN r.result='NR06' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'QC' nam, a07 ply, CASE WHEN r.result='NR07' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'KH' nam, a08 ply, CASE WHEN r.result='NR08' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'KS' nam, a09 ply, CASE WHEN r.result='NR09' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'KD' nam, a10 ply, CASE WHEN r.result='NR10' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "'";
                    cmnd = "select 'JH' nam, a00 ply, CASE WHEN r.result='NR00' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'JS' nam, a01 ply, CASE WHEN r.result='NR01' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'JD' nam, a02 ply, CASE WHEN r.result='NR02' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'JC' nam, a03 ply, CASE WHEN r.result='NR03' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'QH' nam, a04 ply, CASE WHEN r.result='NR04' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'QS' nam, a05 ply, CASE WHEN r.result='NR05' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'QD' nam, a06 ply, CASE WHEN r.result='NR06' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'QC' nam, a07 ply, CASE WHEN r.result='NR07' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'KH' nam, a08 ply, CASE WHEN r.result='NR08' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'KS' nam, a09 ply, CASE WHEN r.result='NR09' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'KD' nam, a10 ply, CASE WHEN r.result='NR10' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select 'KC' nam, a11 ply, CASE WHEN r.result='NR11' THEN  t.clm ELSE  '0' END as win from tasp t,resultsTaa r  where t.g_date=r.g_date and t.g_time=r.g_time and bar= ' " + GameObject.FindObjectOfType<historypanel>().barcode + "'";
                }
                else if (mode == 2)
                {
                    cmnd = "select '0' nam, a00 ply, CASE WHEN r.result='NR00' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '1' nam, a01 ply, CASE WHEN r.result='NR01' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '2' nam, a02 ply, CASE WHEN r.result='NR02' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '3' nam, a03 ply, CASE WHEN r.result='NR03' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '4' nam, a04 ply, CASE WHEN r.result='NR04' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '5' nam, a05 ply, CASE WHEN r.result='NR05' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '6' nam, a06 ply, CASE WHEN r.result='NR06' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '7' nam, a07 ply, CASE WHEN r.result='NR07' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '8' nam, a08 ply, CASE WHEN r.result='NR08' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "' union all select '9' nam, a09 ply, CASE WHEN r.result='NR09' THEN  t.clm ELSE  '0' END as win from tengp t,results r  where t.g_date=r.g_date and t.g_time=r.g_time and bar='" + GameObject.FindObjectOfType<historypanel>().barcode + "'";
                }
                print(cmnd);
                SqlCommand sqlCmnd = new SqlCommand();
                SqlDataReader sqldata = null;
                sqlCmnd.CommandTimeout = 60;
                sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
                sqlCmnd.CommandType = CommandType.Text;
                sqlCmnd.CommandText = cmnd;
                sqldata = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);

                while (sqldata.Read())
                {
                    GameObject gb = GameObject.Instantiate(detailsprefab, content.transform.position, Quaternion.identity, content.transform);
                    gb.GetComponent<betdetails_prefab>().SetData(sqldata["nam"].ToString(), sqldata["ply"].ToString(), sqldata["win"].ToString(), mode);

                }
                sqldata.Close();
                sqldata.DisposeAsync();
            }
        }
        catch
        {
            ShowDetails();
        }
    }
    public void hidedetailspanel()
    {
        detailspanel.SetActive(false);
    }
}

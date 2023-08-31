using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class cancel : MonoBehaviour
{
    int mode = 0;
    string finalcommand;
    private void Start()
    {
    }
    public void cancelpressed()
    {
        if (GameObject.FindObjectOfType<historypanel>().canremove == true)
        {
            int totalbetplaced = 0;
            mode = FindObjectOfType<historypanel>().mode;

            if (mode == 0)
            {

                string command = "UPDATE [taas].[dbo].[tasp] set status='Canceled' WHERE status='Print'   and bar='" + FindObjectOfType<historypanel>().barcode + "'   and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "';";
                string command2 = "SELECT [tot] from [taas].[dbo].[tasp] where status='Canceled' and bar='" + FindObjectOfType<historypanel>().barcode + "'  and   ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "';";
                finalcommand = command + command2;

            }

            if (mode == 1)
            {

                string command = "UPDATE [taas].[dbo].[bet16] set status='Canceled' WHERE status='Print'   and bar='" + FindObjectOfType<historypanel>().barcode + "'   and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "';";
                string command2 = "SELECT [tot] from [taas].[dbo].[bet16] where status='Canceled' and bar='" + FindObjectOfType<historypanel>().barcode + "'  and   ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "';";
                finalcommand = command + command2;

            }
            if (mode == 2)
            {

                string command = "UPDATE [taas].[dbo].[tengp] set status='Canceled' WHERE status='Print'   and bar='" + FindObjectOfType<historypanel>().barcode + "'   and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "';";
                string command2 = "SELECT [tot] from [taas].[dbo].[tengp] where status='Canceled' and bar='" + FindObjectOfType<historypanel>().barcode + "'  and   ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "';";

                finalcommand = command + command2;

            }
            if (mode == 3)
            {

                string command = "UPDATE [taas].[dbo].[doup] set status='Canceled' WHERE status='Print'   and bar='" + FindObjectOfType<historypanel>().barcode + "'   and ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "';";
                string command2 = "SELECT [tot] from [taas].[dbo].[doup] where status='Canceled' and bar='" + FindObjectOfType<historypanel>().barcode + "'  and   ter_id='" + GameObject.FindObjectOfType<userManager>().getUserData().id + "';";

                finalcommand = command + command2;

            }

            SqlCommand sqlCmnd = new SqlCommand();
            SqlDataReader sqlData = null;
            sqlCmnd.CommandTimeout = 60;
            sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
            sqlCmnd.CommandType = CommandType.Text;
            sqlCmnd.CommandText = finalcommand;//this is the sql command we use to get data about user
            sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
            if (sqlData.Read())
            {
                print("claimed:" + sqlData["tot"]);
                totalbetplaced = Convert.ToInt32(sqlData["tot"].ToString());
            }

            sqlData.Close();
            sqlData.Dispose();
            print(totalbetplaced);
            GameObject.FindObjectOfType<SQL_manager>().addubalanceindatabase(GameObject.FindObjectOfType<userManager>().getUserData().id, totalbetplaced);
            if (mode == 0)
            {
                StartCoroutine(GameObject.FindObjectOfType<jeetoJoker_GAMEMANAGER>().UpdateBalanceAndInfo());
            }
            if (mode == 1)
            {
                StartCoroutine(GameObject.FindObjectOfType<bet16>().UpdateBalanceAndInfo());

            }
            if (mode == 2)
            {
              GameObject.FindObjectOfType<spin2win_manager>().UpdateBalanceAndInfo();

            }
            if (mode == 3)
            {
                StartCoroutine(GameObject.FindObjectOfType<doublechance_gamemanager>().UpdateBalanceAndInfo());

            }
            FindObjectOfType<historypanel>().onResultsbuttonClicked();
        }
    }
}

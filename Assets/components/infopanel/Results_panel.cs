using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class Results_panel : MonoBehaviour
{
    public GameObject resultsprefab;
    public CalendarController cal;
    public GameObject content;
   


   
   public void onResultsbuttonClicked()
    {
        if (content.GetComponentInChildren<Transform>().childCount > 1)
        {
            foreach (Transform tf in content.GetComponentInChildren<Transform>())
            {
                if (tf != content.transform)
                {
                    Destroy(tf.gameObject);
                }
            }
        }
        print("result button clicked");
        DateTime dt = new DateTime();
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "SELECT  * FROM [taas].[dbo].[resultsTaa] WHERE g_date='"+(cal.datetimeyear)+"'";//this is the sql command we use to get data about user
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);

        print(sqlCmnd.CommandText);
        while (sqlData.Read())
        {
           GameObject gb = (GameObject)Instantiate(resultsprefab);
           gb.transform.SetParent(content.transform,false);
          
           gb.GetComponent<Results_object_info>().setResult(sqlData["result"].ToString(), sqlData["g_time"].ToString());
        }
        sqlData.Close();
        sqlData.DisposeAsync();
      
    }
}
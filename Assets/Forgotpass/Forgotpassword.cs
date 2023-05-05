using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Forgotpassword : MonoBehaviour
{//
    [SerializeField] GameObject forgotpassword;
    
    [SerializeField] TMPro.TMP_InputField username;
    [SerializeField] TMPro.TMP_InputField oldpassword;
    [SerializeField] TMPro.TMP_InputField newpassword;
    public void setforgotpassword()
    {
        SqlCommand sqlCmnd = new SqlCommand();
        SqlDataReader sqlData = null;
        sqlCmnd.CommandTimeout = 60;
        sqlCmnd.Connection = GameObject.FindObjectOfType<SQL_manager>().SQLconn;
        sqlCmnd.CommandType = CommandType.Text;
        sqlCmnd.CommandText = "UPDATE [taas].[dbo].[g_master] set pass = '" + newpassword.text + "' where term_id='" + username.text + "' and pass='" + oldpassword.text + "'";
        print(sqlCmnd.CommandText);
        sqlData = sqlCmnd.ExecuteReader(CommandBehavior.SingleResult);
        if(sqlData.Read())
        {

        }
        sqlData.Close();
        sqlData.DisposeAsync();
        SceneManager.LoadScene(0);
    }
    public void Openpasswordpanel()
    {
        forgotpassword.SetActive(true);
    }

    public void Closepasswordpanel()
    {
        forgotpassword.SetActive(false);
    }

}

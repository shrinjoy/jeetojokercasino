using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;

public class CasinoAPI : MonoBehaviour
{
    HttpClient client = new HttpClient();
    public async Task<LoginDataResponse> canlogin(string username, string password)
    {
        LoginDataRequested data = new LoginDataRequested
        {
            username = username,
            password = password
        };
        string jsonpayload = JsonConvert.SerializeObject(data);
        StringContent stringcontent = new StringContent(jsonpayload, Encoding.UTF8, "application/json");

        HttpResponseMessage message = await client.PostAsync("http://191.101.3.139:3000/s2w/loginuser/", stringcontent);
        if (message.StatusCode == HttpStatusCode.OK)
        {
            LoginDataResponse res = JsonConvert.DeserializeObject<LoginDataResponse>(await message.Content.ReadAsStringAsync());
            return res;
        }
        else
        {

            return null;
        }
    }
    public async Task<PlayerdataResponse>getuserdata(string username,string password)
    {
        LoginDataRequested data = new LoginDataRequested
        {
            username = username,
            password = password
        };
        string jsonpayload = JsonConvert.SerializeObject(data);
        StringContent stringcontent = new StringContent(jsonpayload, Encoding.UTF8, "application/json");

        HttpResponseMessage message = await client.PostAsync("http://191.101.3.139:3000/s2w/getplayerdata/", stringcontent);
        if (message.StatusCode == HttpStatusCode.OK)
        {
            PlayerdataResponse pdrs = JsonConvert.DeserializeObject<PlayerdataResponse>(await message.Content.ReadAsStringAsync());
            return pdrs;
        }
        else
        {
            return null;
        }
     }

    public async Task<string> getresultbyid(int id)
    {
        gameresultbyidRequest data = new gameresultbyidRequest {gameid=id };
       
        string jsonpayload = JsonConvert.SerializeObject(data);
        
        
        StringContent stringcontent = new StringContent(jsonpayload, Encoding.UTF8, "application/json");

        HttpResponseMessage message = await client.PostAsync("http://191.101.3.139:3000/s2w/getresult/", stringcontent);
        if (message.IsSuccessStatusCode)
        {
            gameresultbyidResponse pdrs = JsonConvert.DeserializeObject<gameresultbyidResponse>(await message.Content.ReadAsStringAsync());
            print("result status:"+message.StatusCode);

            print("result :" +await message.Content.ReadAsStringAsync());
            print("PDRS:" + pdrs.result);
            return pdrs.result;
        }
        else
        {
            return null;
        }
    }


    public async Task<int> gettimeleft(string timeroute)
    {
        HttpResponseMessage message = await client.GetAsync(timeroute);
        if (message.StatusCode == HttpStatusCode.OK)
        {
            gametimeresponse res = JsonConvert.DeserializeObject<gametimeresponse>(await message.Content.ReadAsStringAsync());
            return res.time;
        }
        else
        {
            return 999999;
        }
    }
    public async Task<gamedata> getgamedata(string gamedataroute)
    {
       // //http://191.101.3.139:3000/s2w/getgameinfo/
        HttpResponseMessage message = await client.GetAsync(gamedataroute);
        if (message.StatusCode == HttpStatusCode.OK)
        {
            gamedata res = JsonConvert.DeserializeObject<gamedata>(await message.Content.ReadAsStringAsync());
            return res;
        }
        else
        {
            return null;
        }
    }
    public async Task<last10result> getlast10result(string route)
    {
        HttpResponseMessage message = await client.GetAsync(route);
        if (message.StatusCode == HttpStatusCode.OK)
        {
            last10result res = JsonConvert.DeserializeObject <last10result>(await message.Content.ReadAsStringAsync());
            return res;
        }
        else
        {
            return null;
        }
    }
    

    public async Task<int> getwinamount(string username,int gameid)
    {
        getwinbyIDRequest data = new getwinbyIDRequest
        {
            username= username,
            gameid= gameid
        };
        string jsonpayload = JsonConvert.SerializeObject(data);


      

        StringContent stringcontent = new StringContent(jsonpayload, Encoding.UTF8, "application/json");

        HttpResponseMessage message = await client.PostAsync("http://191.101.3.139:3000/s2w/getresult/", stringcontent);
        if (message.IsSuccessStatusCode)
        {
            getwinbyIDResponse pdrs = JsonConvert.DeserializeObject<getwinbyIDResponse>(await message.Content.ReadAsStringAsync());
            return pdrs.claim;
        }
        else
        {
            return 0;
        }
    }

}

public class setbetRequest
{
    public int NR0,NR1, NR2,NR3,NR4,NR5,NR6,NR7,NR8,NR9,totalbet;
    public string username;
}

public class gamedata
{
    public int gameid;
    public int serverbalance;
    public string currentgametime;
    public string currentgamedrawtime;
}
public class getwinbyIDRequest
{
    public string username;
    public int gameid;
}
public class getwinbyIDResponse
{
   public int claim;
}
public class gameresultbyidRequest
{
    public int gameid;
}
public class gameresultbyidResponse
{
    public string result;
}
public class PlayerdataResponse
{
   public string status;
   public string username;
   public int balance;
}
public class gametimeresponse
{
   public int time;
}
public class LoginDataRequested
{
    public string username;
    public string password;
}
public class LoginDataResponse
{
    public string status;
    public string username;
    public string password;
    public string UUID;
    public string type;
    public int balance;

}
public class last10ResultItem
{
    public string Time { get; set; }
    public string Result { get; set; }
}

public class last10result
{
    public int Index { get; set; }
    public List<last10ResultItem> Results { get; set; }
}
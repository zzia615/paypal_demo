using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;


public class PaypalUtil
{
    string url;
    string client_id;
    string secret;
    private readonly PaypalDbContext dbContext;

    public PaypalUtil(PaypalDbContext dbContext)
    {
        url = "https://api.sandbox.paypal.com";
        client_id="ASh-RLhu7ISDX5MnFGQEJv6S35bUgGM9LrRMZh_uK-EUf5BVOqabNPD_olCkPOejaBOaXECCWOhs9lf3";
        secret="EO5WDe46uu7WqE9uGLMzhOwiDZcjoHMLmB2cKUgzQBKfNosCZEq2LFsUMl3TI1IWVo39eHV7hNEzUO7u";
        this.dbContext = dbContext;
    }
    public PaypalToken GetAccessToken()
    {
        var token = new PaypalToken();
        if(dbContext.PaypalToken.Count()<=0)
        {
            token = GetAccessTokenFromServer();
            
        }else{
            token = dbContext.PaypalToken.OrderByDescending(a=>a.id).SingleOrDefault();
            
            var d_time = token.date.AddSeconds(token.expires_in);

            if(DateTime.Now.AddMinutes(-10)>d_time)
            {
                token = GetAccessTokenFromServer();
            }
        }
        return token;
    }

    PaypalToken GetAccessTokenFromServer()
    {
            string url = $"{this.url}/v1/oauth2/token";
            Dictionary<string,string> data = new Dictionary<string, string>();
            data.Add("grant_type","client_credentials");

            Action<HttpWebRequest> action = new Action<HttpWebRequest>((request)=>{
                request.Headers.Add("Authorization",$"Basic {getAuth()}");
                request.ContentType="application/x-www-form-urlencoded";
            });
            string result = HttpUtil.PostUrl(url,data,action);
            var token = JsonConvert.DeserializeObject<PaypalToken>(result);
            dbContext.PaypalToken.Add(token);
            dbContext.SaveChanges();
            return token;
    }

    public  string  CreateOrder(){
        /*
        'https://api.sandbox.paypal.com/v2/checkout/orders' \
        -H 'accept: application/json' \
        -H 'accept-language: en_US' \
        -H 'authorization: Bearer A21AAFJ9eoorbnbVH3fTJrCTl2o7-P_1T6q8vdYB_QwBB9Ais5ZZmJD4BsNjIiOh8j8OyOcfzLO1BKcgKe0pK-mntpk6jOm-' \
        -H 'content-type: application/json' \
        -d '{
        "intent": "CAPTURE",
        "purchase_units": [
            {
            "reference_id": "PUHF",
            "amount": {
                "currency_code": "USD",
                "value": "100.00"
            }
            }
        ],
        "application_context": {
            "return_url": "",
            "cancel_url": ""
        }
        }
        */
        string url = $"{this.url}/v2/checkout/orders";
        var token = GetAccessToken();
        Action<HttpWebRequest> action = new Action<HttpWebRequest>((request)=>{
            request.Headers.Add("accept","application/json");
            request.Headers.Add("accept-language","en-US");
            request.Headers.Add("authorization",$"{token.token_type} {token.access_token}");
            request.ContentType="application/json";
        });
        var obj = new{
            intent="TEST",
            purchase_units = new[]{
                new{
                    reference_id="111",
                    amount=new{
                        currency_code="USD",
                        value="1"
                    }
                }
            },
            application_context=new{
                return_url="http://ubuntu.free.jaylosy.com:280/Order/Accept",
                cancel_url="http://ubuntu.free.jaylosy.com:280/Order/Cancel"
            }
        };
        string data = JsonConvert.SerializeObject(obj);
        string result = HttpUtil.PostUrl(url,data,action);
        return string.Empty;

    }

    string getAuth(){
        string auth=Convert.ToBase64String(Encoding.UTF8.GetBytes($"{client_id}:{secret}"));
        return auth;
    }

    

}

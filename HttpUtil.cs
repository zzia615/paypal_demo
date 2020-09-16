using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

public class HttpUtil
{
    public static string PostUrl(string url,Dictionary<string,string> data,Action<HttpWebRequest> SetHeader=null,Func<string,string> ParseResult=null)
    {
        string result="";
        string s_data="";
        s_data=getformData(data);

        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Method="POST";
        byte[] bytes = Encoding.UTF8.GetBytes(s_data);
        using(var sr = request.GetRequestStream()){
            sr.Write(bytes,0,bytes.Length);
        }
        if(SetHeader!=null) SetHeader(request);

        using(var sr = request.GetResponse().GetResponseStream()){
            var reader = new StreamReader(sr);
            result = reader.ReadToEnd();
        }

        if(ParseResult!=null) return ParseResult(result);

        return result;
    }

    public static string PostUrl(string url,string data,Action<HttpWebRequest> SetHeader=null,Func<string,string> ParseResult=null)
    {
        string result="";
        string s_data="";
        s_data=data;

        HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
        request.Method="POST";
        byte[] bytes = Encoding.UTF8.GetBytes(s_data);
        using(var sr = request.GetRequestStream()){
            sr.Write(bytes,0,bytes.Length);
        }
        if(SetHeader!=null) SetHeader(request);

        using(var sr = request.GetResponse().GetResponseStream()){
            var reader = new StreamReader(sr);
            result = reader.ReadToEnd();
        }

        if(ParseResult!=null) return ParseResult(result);

        return result;
    }

    static string getformData(Dictionary<string,string> data)
    {
        StringBuilder s_json = new StringBuilder();
        foreach(var t in data)
        {
            if(s_json.Length>0){
                s_json.Append("&");
            }
            s_json.Append($"{t.Key}={t.Value}");
        }
        return s_json.ToString();
    }
    static string getJsonData(Dictionary<string,string> data)
    {
        StringBuilder s_json = new StringBuilder();
        foreach(var t in data)
        {
            if(s_json.Length>0){
                s_json.Append(",");
            }
            s_json.Append($"\"{t.Key}\":\"{t.Value}\"");
        }
        return "{"+s_json.ToString()+"}";
    }
    
}
using System;
using System.Collections;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;

public class CertificateWhore : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}

public class WebManager
{
    public string BaseUrl { get; set; }
    public string ip = "127.0.0.1";
    public int port = 8080;

    public void Init()
    {
        IPAddress ipv4 = Utils.GetIpv4Address(ip);
        if (ipv4 == null)
        {
            Debug.LogError("WebServer IPv4 Failed");
            return;
        }

        BaseUrl = $"http://{ipv4.ToString()}:{port}";
        Debug.Log($"WebServer BaseUrl : {BaseUrl}");
    }

    public void SendPostRequest<T>(string url, object obj, Action<T> res)
    {
        Managers.Instance.StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbPOST, obj, res));
    }

    public void SendGetRequest<T>(string url, object obj, Action<T> res)
    {
        Managers.Instance.StartCoroutine(CoSendWebRequest(url, UnityWebRequest.kHttpVerbGET, obj, res));
    }

    IEnumerator CoSendWebRequest<T>(string url, string method, object obj, Action<T> res)
    {
        if (string.IsNullOrEmpty(BaseUrl))
            Init();

        string sendUrl = $"{BaseUrl}/{url}";

        byte[] jsonBytes = null;
        if (obj != null)
        {
            string jsonStr = JsonConvert.SerializeObject(obj);
            jsonBytes = Encoding.UTF8.GetBytes(jsonStr);
        }

        using (var uwr = new UnityWebRequest(sendUrl, method))
        {
            uwr.uploadHandler = new UploadHandlerRaw(jsonBytes);
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.certificateHandler = new CertificateWhore();
            uwr.SetRequestHeader("Content-Type", "application/json");

            yield return uwr.SendWebRequest();

            if (uwr.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log($"CoSendWebRequest Failed : {uwr.error}");
            }
            else
            {
                Debug.Log($"CoSendWebRequest Failed : {uwr.error}");
                T resObj = JsonUtility.FromJson<T>(uwr.downloadHandler.text);
                res.Invoke(resObj);
            }
        }
    }

    public void SendPostRequestForm<T>(string url, WWWForm formData, Action<T> res)
    {
        Managers.Instance.StartCoroutine(CoSendWebRequestForm(url, formData, res));
    }

    IEnumerator CoSendWebRequestForm<T>(string url, WWWForm formData, Action<T> res)
    {
        if (string.IsNullOrEmpty(BaseUrl))
            Init();
        string sendUrl = $"{BaseUrl}/{url}";
        using (UnityWebRequest uwr = UnityWebRequest.Post(sendUrl, formData))
        {
            yield return uwr.SendWebRequest();
            if (uwr.result == UnityWebRequest.Result.ConnectionError || uwr.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError($"Error: {uwr.error}");
                Debug.LogError($"Response: {uwr.downloadHandler.text}");
            }
            else
            {
                Debug.Log($"Success response: {uwr.downloadHandler.text}");
                T resObj = JsonUtility.FromJson<T>(uwr.downloadHandler.text);
                res.Invoke(resObj);
            }
        }
    }
}

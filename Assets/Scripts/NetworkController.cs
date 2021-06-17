using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkController 
{
    public static async Task GetRequest(string url, Action<string> callback)
    {
        var getRequest = UnityWebRequest.Get(url);
        await getRequest.SendWebRequest();
        var result = getRequest.downloadHandler.text;

        callback.Invoke(result);
    }
}
public static class ExtensionMethods
{
    public static TaskAwaiter GetAwaiter(this AsyncOperation asyncOp)
    {
        var tcs = new TaskCompletionSource<object>();
        asyncOp.completed += obj => { tcs.SetResult(null); };
        return ((Task)tcs.Task).GetAwaiter();
    }
}
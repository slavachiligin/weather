using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Networking;

public static class WebRequestSender
{
    public static async Task<string> SendWeatherRequest(string url, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL cannot be null or empty.", nameof(url));
        
        using var request = UnityWebRequest.Get(url);
        var operation = request.SendWebRequest();
        
        while (!operation.isDone)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                request.Abort();
                throw new OperationCanceledException(cancellationToken);
            }

            await Task.Yield();
        }

        if (request.result != UnityWebRequest.Result.Success)
        {
            throw new Exception($"Web request error: {request.error}");
        }

        return request.downloadHandler.text;
    }
}
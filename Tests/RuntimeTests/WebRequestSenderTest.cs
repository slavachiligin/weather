using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Samples.Scripts;
using UnityEngine.TestTools;
using WeatherServices;

namespace RuntimeTests
{
    public class WebRequestSenderTest
    {
        [Test]
        public void SendWeatherRequest_ThrowsException_OnEmptyUrl()
        {
            Assert.ThrowsAsync<ArgumentException>(() =>
                WebRequestSender.SendWeatherRequest("", CancellationToken.None));
        }

        [Test]
        public async Task SendWeatherRequest_ThrowsOperationCanceledException_OnCancellation()
        {
            var cts = new CancellationTokenSource();
            cts.Cancel();

            var url = new OpenMeteoWeatherService().BuildUrl(ExampleConstants.Latitude, ExampleConstants.Longitude);
            
            var exception = Assert.ThrowsAsync<TaskCanceledException>(async () =>
                await WebRequestSender.SendWeatherRequest(url, cts.Token));
        }
        
        [UnityTest]
        public IEnumerator SendWeatherRequest_AbortsRequest_OnCancellation()
        {
            var cts = new CancellationTokenSource();
            var url = new OpenMeteoWeatherService().BuildUrl(ExampleConstants.Latitude, ExampleConstants.Longitude);

            var task = WebRequestSender.SendWeatherRequest(url, cts.Token);
            cts.Cancel();

            while (!task.IsCompleted)
            {
                yield return null;
            }

            Assert.IsTrue(task.IsCanceled, "Task should be canceled.");
            Assert.ThrowsAsync<TaskCanceledException>(async () => await task);
        }
    }
}
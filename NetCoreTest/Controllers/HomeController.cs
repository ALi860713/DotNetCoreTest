using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreTest.Models;
using Newtonsoft.Json;

namespace NetCoreTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            JsonPostAsync();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<APIResult> JsonPostAsync()
        {
            APIResult fooAPIResult;
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    try
                    {
                        var test2 = "";
                        string FooUrl = $"http://vulcanwebapi.azurewebsites.net/api/values";
                        HttpResponseMessage response = null;
                        
                        var fooFullUrl = $"{FooUrl}";
                        
                        //client.DefaultRequestHeaders.Accept.TryParseAdd("application/json");
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        // Content-Type 用於宣告遞送給對方的文件型態
                        client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");

                        response = await client.GetAsync(fooFullUrl);

                        #region 處理呼叫完成 Web API 之後的回報結果
                        if (response != null)
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                // 取得呼叫完成 API 後的回報內容
                                String strResult = await response.Content.ReadAsStringAsync();
                                fooAPIResult = JsonConvert.DeserializeObject<APIResult>(strResult, new JsonSerializerSettings { MetadataPropertyHandling = MetadataPropertyHandling.Ignore });
                                var test = JsonConvert.SerializeObject(fooAPIResult);
                            }
                            else
                            {
                                fooAPIResult = new APIResult
                                {
                                    Success = false,
                                    Message = string.Format("Error Code:{0}, Error Message:{1}", response.StatusCode, response.RequestMessage),
                                    Payload = null,
                                };
                            }
                        }
                        else
                        {
                            fooAPIResult = new APIResult
                            {
                                Success = false,
                                Message = "應用程式呼叫 API 發生異常",
                                Payload = null,
                            };
                        }
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        fooAPIResult = new APIResult
                        {
                            Success = false,
                            Message = ex.Message,
                            Payload = null,
                        };
                    }
                }
            }

            return fooAPIResult;
        }

        //private async Task<APIResult> JsonPostAsync(APIData apiData)
        //{
        //    APIResult api;
        //    using (HttpClientHandler handler = new HttpClientHandler())
        //    {
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            try
        //            {
        //                string FooUrl = $"http://vulcanwebapi.azurewebsites.net/api/Values";
        //                HttpResponseMessage response = null;
        //                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //            }
        //            catch
        //            {

        //            }
        //        }
        //    }
        //}
    }
}

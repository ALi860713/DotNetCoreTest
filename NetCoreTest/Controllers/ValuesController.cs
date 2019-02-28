using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreTest.Models;

namespace NetCoreTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public APIResult Get()
        {
            APIResult foo = new APIResult()
            {
                Success = true,
                Message = "成功得所有資料集合",
                Payload = new List<APIData>()
                {
                    new APIData()
                    {
                        Id =777,
                        Name = "Vulcan01"
                    },
                    new APIData()
                    {
                        Id =234,
                        Name ="Vulcan02"
                    }
                }
            };
            return foo;
        }

        [HttpGet("QueryStringGet")]
        public APIResult QueryStringGet([FromQuery] APIData value)
        {
            APIResult foo;
            if (value.Id == 777)
            {
                foo = new APIResult()
                {
                    Success = true,
                    Message = "透過 Get 方法，接收到 Id=777",
                    Payload = new List<APIData>()
                    {
                        new APIData()
                        {
                            Id = 777,
                            Name = "Vulcan by QueryStringGet"
                        }
                    }
                };
            }
            else
            {
                foo = new APIResult()
                {
                    Success = false,
                    Message = "無法發現到指定的 ID",
                    Payload = null
                };
            }
            return foo;
        }
    }
}
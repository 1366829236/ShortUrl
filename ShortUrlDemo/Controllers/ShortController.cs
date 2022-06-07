using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShortUrlDemo.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShortUrlDemo.Controllers
{
    [Route("[controller]/[action]")]
    public class ShortController : Controller
    {
        private readonly dbContext _dbContext;
        private readonly IHttpContextAccessor _accessor;

        public ShortController(dbContext dbContext, IHttpContextAccessor accessor)
        {
            _dbContext = dbContext;
            _accessor = accessor;
        }
        /// <summary>
        /// 将原Url存入数据库，然后把该链接变为短链接编码，并重定向到原链接
        /// </summary>
        /// <param name="url"></param>
        [HttpGet]
        [Obsolete]
        public void SetUrl(string url)
        {
            bool yn = true;
            var model = new Short();
            while (yn)
            {
                var code = ShortCode();
                model.Url = url;

                var list = _dbContext.Short.Where(s => s.Code == code).ToList();
                if (list.Count() > 0)
                {
                    code = model.Code = ShortCode();
                }
                else
                {
                    yn = false;
                    model.Code = code;
                }
            }
            _dbContext.Short.Add(model);
            _dbContext.SaveChanges();
            Response.Redirect(url);
        }

        /// <summary>
        /// 根据短链编码重定向到原链接
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        public void GetUrl(string code)
        {
            var model = _dbContext.Short.FirstOrDefault(u => u.Code == code);
            _accessor.HttpContext.Response.Redirect(model?.Url);
            //return model?.Url;
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        //[Obsolete]
        //private long ConvertDateTimeToInt(DateTime time)
        //{
        //    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0));
        //    long t = (time.Ticks - startTime.Ticks) / 100000000000;   //除100000000000调整为6位      
        //    return t;
        //}

        //随机数编码
        static string ShortCode()
        {
            char[] eng = { 'a','b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
                            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z',
                                '0','1','2','3','4','5','6','7','8','9'};

            Random rad = new Random();
            string s = "";
            for (int i = 0; i < 6; i++)
            {
                int a = rad.Next(0, 61);
                s += eng[a];
            }

            return s;
        }
    }
}

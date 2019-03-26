using System;
using System.Net;
using DDD.Core.Sample.WebApi.ResultModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace DDD.Core.Sample.WebApi.Filter
{
    /// <summary>
    /// 异常拦截器
    /// </summary>
    public class ExceptionAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 发生异常时
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnException(ExceptionContext filterContext)
        {
            //获取异常信息 
            Exception error = filterContext.Exception;
            string message = error.Message;//错误信息
            filterContext.ExceptionHandled = true;
            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Error(error);
            MessageBase result = new MessageBase();

            result.Success = false;
#if DEBUG
            result.Message = message;
#else
            result.Message = "系统繁忙，请稍后再试！";
#endif
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.Result = new JsonResult(result);

            base.OnException(filterContext);
        }
    }
}
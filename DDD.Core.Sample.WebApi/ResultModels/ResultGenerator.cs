using System.Net;
using DDD.Core.Sample.Application;

namespace DDD.Core.Sample.WebApi.ResultModels;

/// <summary>
/// 返回结果生成器
/// </summary>
public class ResultGenerator
{
    /// <summary>
    /// 生成错误返回
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <returns></returns>
    public static BaseResult GenErrorResult(String message)
    {
        BaseResult result = new ()
        {
            Code = HttpStatusCode.InternalServerError,
            Success = false,
            Message = string.IsNullOrWhiteSpace(message) ? "ERROR" : message
        };
        return result;
    }
    /// <summary>
    /// 生成失败返回
    /// </summary>
    /// <param name="message">失败消息</param>
    /// <returns></returns>
    public static BaseResult GenFailResult(string message)
    {
        BaseResult result = new ()
        {
            Code = HttpStatusCode.BadRequest,
            Success = false,
            Message = string.IsNullOrWhiteSpace(message) ? "FAIL" : message
        };
        return result;
    }

    /// <summary>
    /// 生成成功返回
    /// </summary>
    /// <param name="data">数据</param>
    /// <returns></returns>
    public static BaseResult GenSuccessResult(object data)
    {
        BaseResult result;
        

        if (data.GetType().IsGenericType && data.GetType().GetGenericTypeDefinition().FullName == typeof(Pager<>).FullName)
        {
            var pager = data as dynamic;
            result = new ()
            {
                Code = HttpStatusCode.OK,
                Success = true,
                Data = pager.Content,
                NextPage = pager.NextPage ?? 0,
                PageCount = pager.PageCount ?? 0,
                PageIndex = pager.PageIndex ?? 0,
                PageSize = pager.PageSize ?? 0,
                PrevPage = pager.PrevPage ?? 0,
                TotalRecord = pager.TotalRecord ?? 0
            };
        }
        else
        {
            result = new ()
            {
                Code = HttpStatusCode.OK,
                Success = true,
                Data = data
            };
        }
        
        return result;
    }
}
using System.Net;

namespace DDD.Core.Sample.WebApi.ResultModels;
/// <summary>
/// 返回结果
/// </summary>
public class BaseResult
{
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; } = true;
    /// <summary>
    /// 错误代码
    /// </summary>
    public HttpStatusCode Code { get; set; } = HttpStatusCode.OK;
    /// <summary>
    /// 错误信息
    /// </summary>
    public object Message { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    public object Data { get; set; }

    /// <summary>
    /// 当前页
    /// </summary>
    public int PageIndex { get; set; }
    /// <summary>
    /// 每页数量
    /// </summary>
    public int PageSize { get; set; }
    /// <summary>
    /// 数据总数
    /// </summary>
    public int TotalRecord { get; set; }
    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount { get; set; }
    /// <summary>
    /// 上一页
    /// </summary>
    public int PrevPage { get; set; }
    /// <summary>
    /// 下一页
    /// </summary>
    public int NextPage { get; set; }
}
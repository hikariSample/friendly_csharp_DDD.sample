using System.Linq;
using System.Reflection;
using DDD.Core.Sample.Web.ResultModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DDD.Core.Sample.Web.Filter
{
    /// <summary>
    /// ModelState验证过滤器
    /// </summary>
    public class ModelStateValidationAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 方法运行后
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var v = !((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)filterContext.ActionDescriptor).MethodInfo.GetCustomAttributes(typeof(NotAjaxRequestAttribute)).Any();

            if (v && filterContext.HttpContext.Request.Headers != null && filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (!filterContext.ModelState.IsValid)
                {
                    var buf = filterContext.ModelState.Where(x => x.Value.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid);
                    var list = buf.ToDictionary(x => x.Key, x => string.Join(',', x.Value.Errors.Select(y => y.ErrorMessage)));
                    filterContext.HttpContext.Response.StatusCode = 412;  //未满足前提条件
                    filterContext.Result = new JsonResult(list);
                }
                else
                {
                    if (filterContext.Result == null || filterContext.Result.GetType() != typeof(JsonResult))
                    {
                        MessageBase result = new MessageBase();
                        result.Success = true;
                        filterContext.Result = new JsonResult(result);
                    }

                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}
﻿using EticaretAPI.Application.Abstraction.Services;
using EticaretAPI.Application.CustomAttribute;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Reflection;

namespace EticaretAPI.Presentation.Filters
{
    public class RolePermissionFilter : IAsyncActionFilter
    {
        readonly IUserService _userService;

        public RolePermissionFilter(IUserService userService)
        {
            _userService = userService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var name= context.HttpContext.User.Identity?.Name;
            if(!string.IsNullOrEmpty(name)&& name!="seen")
            {
               var descriptor= context.ActionDescriptor as ControllerActionDescriptor;
               var attribute= descriptor.MethodInfo.GetCustomAttribute(typeof(AuthorizeDefitionAttribute)) as AuthorizeDefitionAttribute  ;
                var httpAttribute = descriptor.MethodInfo.GetCustomAttribute(typeof(HttpMethodAttribute)) as HttpMethodAttribute;
                var code = $"{(httpAttribute !=null ? httpAttribute.HttpMethods.First(): HttpMethods.Get)}.{attribute.ActionType}.{attribute.Definition.Replace(" ","")}";

                var hasRole = await _userService.HasRolePermissionToEndPointAsync(name,code);
                if (!hasRole)
                    context.Result = new UnauthorizedResult();                
                else
                    await next();
            }
            await next();
        }
    }
}

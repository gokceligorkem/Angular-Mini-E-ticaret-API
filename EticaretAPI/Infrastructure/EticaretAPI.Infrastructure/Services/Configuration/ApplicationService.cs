using EticaretAPI.Application.Abstraction.Services.Configurations;
using EticaretAPI.Application.CustomAttribute;
using EticaretAPI.Application.DTOs.Configuration;
using EticaretAPI.Application.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Infrastructure.Services.Configuration
{
    public class ApplicationService : IApplicationService
    {
        public List<Menu> GetAuthorizeDefinitionEndpoints(Type type)
        {
            Assembly assembly = Assembly.GetAssembly(type);
           var controllers= assembly.GetTypes().Where(t=>t.IsAssignableTo(typeof(ControllerBase)));
            List<Menu> menus= new List<Menu>();
            if (controllers != null) {
            foreach(var controller in controllers)
            {
              var actions=  controller.GetMethods().Where(m => m.IsDefined(typeof(AuthorizeDefitionAttribute)));
                    if (actions != null)
                    {
                        foreach (var action in actions)
                        {
                          var attributes=  action.GetCustomAttributes(true);
                            if(attributes != null)
                            {
                                Menu menu = null;
                              var authorizeDefitionAttribute= attributes.FirstOrDefault(a => a.GetType() == typeof(AuthorizeDefitionAttribute)) as AuthorizeDefitionAttribute;
                                if (!menus.Any(m => m.Name == authorizeDefitionAttribute.Menu))
                                {
                                    menu = new() { Name = authorizeDefitionAttribute.Menu };
                                    menus.Add(menu);
                                }
                                else
                                    menu = menus.FirstOrDefault(m => m.Name == authorizeDefitionAttribute.Menu);
                                Application.DTOs.Configuration.Action _action = new()
                                {
                                    ActionType=Enum.GetName(typeof(ActionType), authorizeDefitionAttribute.ActionType),
                                    Definition=authorizeDefitionAttribute.Definition,
                                    
                                };
                               var httpAttribute= attributes.FirstOrDefault(f => f.GetType().IsAssignableTo(typeof(HttpMethodAttribute))) as HttpMethodAttribute;
                                if (httpAttribute != null)
                                    _action.HttpType = httpAttribute.HttpMethods.First();

                                else
                                    _action.HttpType = HttpMethods.Get;

                                _action.Code = $"{_action.HttpType}.{_action.ActionType}." +
                                    $"{_action.Definition.Replace(" ","")}";
                                menu.Actions.Add(_action);
                            }
                        }
                    }
            }
            }
            return menus;
        }
    }
}

using AutoMapper;
using PreventionAdvisor.Models;
using PreventionAdvisor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Config
{
    public static class MapperConfig
    {
        public static void CreateMappings() {
            Mapper.Initialize(cfg => {
                cfg.CreateMap<AppUser, UserViewModel>();
            });
        }
    }
}

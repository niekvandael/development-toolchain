using AutoMapper;
using PreventionAdvisor.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreventionAdvisor.Models
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

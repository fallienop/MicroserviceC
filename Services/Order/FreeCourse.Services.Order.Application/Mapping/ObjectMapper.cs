using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Mapping
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(x =>
            {
                x.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();   
        }); 
        public static IMapper Mapper=>lazy.Value;
    }
}

using Microsoft.Extensions.DependencyInjection;
namespace Service.Extensions.DependencyInjection
{
    public static class AutoMapperExt
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperExt));

        }
    }
}

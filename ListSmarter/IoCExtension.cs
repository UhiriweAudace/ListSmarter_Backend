using FluentValidation;
using ListSmarter.Common;
using ListSmarter.Models;
using ListSmarter.Models.Validators;
using ListSmarter.Repositories;
using ListSmarter.Repositories.Interfaces;
using ListSmarter.Services;
using ListSmarter.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter
{
    public static class IoCExtension
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IUserService,UserService>();
            services.AddTransient<IBucketService, BucketService>();
        }

        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBucketRepository, BucketRepository>();
        }

        public static void RegisterValidators(this IServiceCollection services)
        {

            services.AddScoped<IValidator<TaskDto>, TaskDtoValidator>();
            services.AddScoped<IValidator<UserDto>, UserDtoValidator>();
            services.AddScoped<IValidator<BucketDto>, BucketDtoValidator>();
        }
    }
}

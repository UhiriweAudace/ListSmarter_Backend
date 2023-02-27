﻿using AutoMapper;
using ListSmarter.Models;
using ListSmarter.Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListSmarter
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Repositories.Models.Task, TaskDto>().ReverseMap();

            CreateMap<Bucket, BucketDto>().ReverseMap();
        }
    }
}

﻿using EticaretAPI.Application.RequestParameters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    namespace EticaretAPI.Application.Feature_Özellikler_.Products.Queries
    {
        public class GetAllProductQueryRequest:IRequest<GetAllProductQueryResponse>
        {
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;
        }
    
    }

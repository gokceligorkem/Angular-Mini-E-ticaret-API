﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EticaretAPI.Application.DTOs.Configuration
{
    public class Menu
    {
        public string Name { get; set; }
        public List<Action> Actions { get; set; }= new List<Action>();
    }
}

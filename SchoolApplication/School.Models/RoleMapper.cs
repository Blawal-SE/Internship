﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Models
{
    public class RoleMapper
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
        public int RoleId { get; set; }
        public Role role { get; set; }

    }
}
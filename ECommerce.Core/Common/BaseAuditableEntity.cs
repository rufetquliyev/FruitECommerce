﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
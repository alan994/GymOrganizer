﻿using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Model
{
    public class OfficeQueue : QueueBase
    {
        public Guid? Id { get; set; }
        public Guid CityId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public ExistenceStatus Status { get; set; }
    }
}

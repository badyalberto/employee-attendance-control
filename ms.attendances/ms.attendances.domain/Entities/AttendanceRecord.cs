﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ms.attendances.domain.Entities
{
    public class AttendanceRecord
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public DateTime Date { get; set; }
        public bool Attendance { get; set; }
        public string Notes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace wellbeing_api.Data
{
    public class ApplicationDbContext
    {

        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string UserCollection { get; set; } = null!;
        public string WellbeingCollection { get; set; } = null!;
        public string DepartmentCollection { get; set; } = null!;
        public string ApplicationUserCollection { get; set; } = null!;

    }
}
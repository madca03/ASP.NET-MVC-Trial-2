using ContosoSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContosoSite.ViewModels
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public List<Enrollment> Enrollments { get; set; }
    }
}
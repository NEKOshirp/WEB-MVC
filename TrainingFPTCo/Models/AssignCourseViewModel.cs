using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace TrainingFPTCo.Models
{
    public class AssignCourseViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int RoleId { get; set; }
        public List<SelectListItem> TrainerList { get; set; }
    }
}

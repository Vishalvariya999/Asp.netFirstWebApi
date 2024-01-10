using System;
using System.Collections.Generic;

namespace Domain.Models;

public partial class ClassDetail
{
    public int ClassId { get; set; }

    public string? ClassName { get; set; }

    public virtual ICollection<StudentDetail> StudentDetails { get; set; } = new List<StudentDetail>();
}

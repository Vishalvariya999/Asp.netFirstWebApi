namespace Domain.Models;

public partial class StudentDetail
{
    public int StudentId { get; set; }

    public int? StudentRollNo { get; set; }

    public string? StudentName { get; set; }

    public string? StudentPhone { get; set; }

    public string? StudentEmail { get; set; }

    public int? ClassId { get; set; }

    public virtual ClassDetail? Class { get; set; }
}

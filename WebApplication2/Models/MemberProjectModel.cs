using System.ComponentModel.DataAnnotations;
namespace WebApplication2.Models
{ 
public class MemberProjectModel
{
    public int ProjectId { get; set; }

    [Required(ErrorMessage = "Member ID is required.")]
    public string MemberId { get; set; }
    public MemberProjectModel() {}
    public MemberProjectModel(string m) { MemberId = m; }
}
}
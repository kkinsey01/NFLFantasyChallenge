namespace NFLFantasyChallenge.API.DTOs.Admin.ManageUsers;

public class UserDTO
{
    public int UserID { get; set; }
    public string UserName { get; set; }
    public string? Password { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public double Balance { get; set; }
    public int RoleID { get; set; }
    public string RoleName { get; set; }
}

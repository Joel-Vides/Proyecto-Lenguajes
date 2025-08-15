namespace Terminal.Dtos.Security.User
{
    public class UserEditDto
    {
        public bool ChangePassword { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}

namespace TaskManagement.Communication.Responses;
public class ResponseUserJson
{
    public string Name {  get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

   //Não expor a senha para o usuário!
}

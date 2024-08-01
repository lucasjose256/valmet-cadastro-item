namespace valmet_cadastro_item.Repositories
{
    public interface ISmtpRepository
    {
    bool SendEmail(string email, string message, string subject);


    }
}

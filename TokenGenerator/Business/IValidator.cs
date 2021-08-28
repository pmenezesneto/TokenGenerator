namespace TokenGenerator.Business
{
    public interface IValidator
    {
        int CreateToken(Card card);
        bool ValidateToken(Card requestCard, Card savedCard);
    }
}
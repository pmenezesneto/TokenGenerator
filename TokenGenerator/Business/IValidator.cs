namespace TokenGenerator.Business
{
    /// <summary>
    /// IValidator interface.
    /// </summary>
    public interface IValidator
    {
        /// <summary>
        /// Create a token.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns>The token number.</returns>
        int CreateToken(Card card);

        /// <summary>
        /// Validate a token.
        /// </summary>
        /// <param name="requestCard">The request Card.</param>
        /// <param name="savedCard">The saved Card.</param>
        /// <returns></returns>
        bool ValidateToken(Card requestCard, Card savedCard);
    }
}

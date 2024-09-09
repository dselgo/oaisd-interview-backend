namespace OaisdInterviewBackend.Models
{
    /// <summary>
    /// An object containing the number value and its text description
    /// </summary>
    public class ViewModel
    {
        /// <example>1234</example>
        public long Num { get; set; }

        /// <example>one thousand two hundred thirty four</example>
        public required string Text { get; set; }
    }
}

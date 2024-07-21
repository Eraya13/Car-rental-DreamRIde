namespace Autopujcovna_DreamRide.Models
{
    /// <summary>
    /// Výchozí třída ErrorViewModelu - v mém projektu nedošlo k jeho implementaci
    /// </summary>
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}

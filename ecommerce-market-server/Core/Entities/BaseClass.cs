namespace Core.Entities
{
    /// <summary>
    /// Clase base que proporciona una propiedad de identificador único para las entidades.
    /// </summary>
    /// <remarks>
    /// Esta clase puede ser heredada por otras entidades para asegurar que todas tengan un identificador común.
    /// </remarks>
    public class BaseClass
    {
        public int Id { get; set; }
    }
}

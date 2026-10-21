namespace Domain.Common;

public class SucceededOrNotResponse
{
    public bool Succeeded { get; set; }
    public List<Error> Errors { get; set; } = new();

    // Propriété optionnelle pour les données
    public object? Data { get; set; }

    public SucceededOrNotResponse() {}

    public SucceededOrNotResponse(bool succeeded, Error? error = null)
    {
        Succeeded = succeeded;
        if (error != null)
            Errors.Add(error);
    }

    public SucceededOrNotResponse(bool succeeded, IEnumerable<Error> errors)
    {
        Succeeded = succeeded;
        Errors.AddRange(errors);
    }

    // Nouveau constructeur optionnel pour inclure Data
    public SucceededOrNotResponse(bool succeeded, IEnumerable<Error>? errors, object? data)
    {
        Succeeded = succeeded;
        if (errors != null)
            Errors.AddRange(errors);
        Data = data;
    }
}
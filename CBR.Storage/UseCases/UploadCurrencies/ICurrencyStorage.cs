namespace CBR.Storage.UseCases.UploadValutes;

public interface ICurrencyStorage
{
    Task SaveToStorage(CancellationToken cancellationToken);
}
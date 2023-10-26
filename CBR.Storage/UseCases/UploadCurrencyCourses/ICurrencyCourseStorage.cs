namespace CBR.Storage.UseCases.UploadCurrencyCourses;

public interface ICurrencyCourseStorage
{
    Task SaveToStorage(CancellationToken cancellationToken);
}

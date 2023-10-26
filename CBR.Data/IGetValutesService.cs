using CBR.Data.Models;

namespace CBR.Data;

public interface IGetValutesService
{
    Task<Valuta> GetValutes(CancellationToken cancellationToken);

    Task<ValCurs> GetValutesCoursesByDate(DateTime dateTime, CancellationToken cancellationToken);   
}

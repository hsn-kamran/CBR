using CBR.Data.Models;
using System.Text;
using System.Xml.Serialization;

namespace CBR.Data;

public class GetValutesService : IGetValutesService, IDisposable
{
    private readonly HttpClient _httpClient;
    public GetValutesService()
    {
        _httpClient = new HttpClient();

        // https://stackoverflow.com/questions/33579661/encoding-getencoding-cant-work-in-uwp-app
        // для работы с кириллицей в xml
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        Encoding.GetEncoding("windows-1251");
    }


    public async Task<Valuta> GetValutes(CancellationToken cancellationToken)
    {
        string url = $"https://cbr.ru/scripts/XML_val.asp?d=0";

        Valuta valuta = new Valuta();
        var response = await _httpClient.GetAsync(url, cancellationToken);

        var xmlSerializer = new XmlSerializer(typeof(Valuta));
        string sXml = await response.Content.ReadAsStringAsync(cancellationToken);

        using var reader = new StringReader(sXml);
        valuta = xmlSerializer.Deserialize(reader) as Valuta;

        return valuta!;
    }

    public async Task<ValCurs> GetValutesCoursesByDate(DateTime dateTime, CancellationToken cancellationToken)
    {
        string date_req = dateTime.ToString("dd/MM/yyyy");
        string url = $"http://www.cbr.ru/scripts/XML_daily.asp?date_req={date_req}";

        ValCurs valCurs = new ValCurs();
        var response = await _httpClient.GetAsync(url, cancellationToken);

        var xmlSerializer = new XmlSerializer(typeof(ValCurs));
        string sXml = await response.Content.ReadAsStringAsync(cancellationToken);

        using var reader = new StringReader(sXml);
        valCurs = xmlSerializer.Deserialize(reader) as ValCurs;

        return valCurs!;
    }

    public void Dispose()
    {
        _httpClient.Dispose();
    }
}

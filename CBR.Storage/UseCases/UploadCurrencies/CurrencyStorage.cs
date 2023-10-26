using AutoMapper;
using CBR.Data;
using CBR.Storage.Models;
using CBR.Storage.UseCases.UploadValutes;
using Microsoft.EntityFrameworkCore;

namespace CBR.Storage.UseCases.UploadCurrencies;

public class CurrencyStorage : ICurrencyStorage
{
    private readonly IMapper _mapper;
    private readonly CbrDbContext _context;
    private readonly IGetValutesService _getValutesService;

    public CurrencyStorage(CbrDbContext context, IGetValutesService getValutesService)
    {
        _context = context;
        _getValutesService = getValutesService;
        _mapper = MapperConfig.InitializeAutomapper();
    }
    
    public async Task SaveToStorage(CancellationToken cancellationToken)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            if (await _context.Currencies.AnyAsync())
                _context.Currencies.RemoveRange(_context.Currencies.ToList());

            var valuta = await _getValutesService.GetValutes(cancellationToken);
            var currencies = _mapper.Map<List<Currency>>(valuta.Item);

            await _context.Currencies.AddRangeAsync(currencies, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine(ex.Message);
        }
    }
}

using AutoMapper;
using CBR.Data;
using CBR.Storage.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CBR.Storage.UseCases.UploadCurrencyCourses;

public class CurrencyCourseMonthStorage : ICurrencyCourseStorage
{
    private readonly IMapper _mapper;
    private readonly CbrDbContext _context;
    private readonly IGetValutesService _getValutesService;

    public CurrencyCourseMonthStorage(CbrDbContext context, IGetValutesService getValutesService)
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
            string sqlDeleteCommand = "DELETE FROM \"CurrencyCourses\"";
            await _context.Database.ExecuteSqlRawAsync(sqlDeleteCommand);

            var currencyCourses = new List<CurrencyCourse>();

            var (startDate, endDate) = GetDateTimeRange();

            for (DateTime date = startDate; date <= endDate; date += TimeSpan.FromDays(1))
            {
                var valCrs = await _getValutesService.GetValutesCoursesByDate(date, cancellationToken);

                // если вдруг попался выходной день
                // странное поведение эндпоинта, по докам курс валюты не меняется в выходные дни, но
                // при запросе указав будний день (например 02.10.23) возвращает результат с последнего рабочего дня (т.е 30.09.23)
                // что означает: этот день нерабочий, т.к такое поведение установлено согласно докам
                if (DateTime.Parse(valCrs.Date).Day != date.Day)
                    continue;

                var mappedCurrencyCourses = _mapper.Map<List<CurrencyCourse>>(valCrs.Valute);
                mappedCurrencyCourses.ForEach(cc => cc.Date = DateTime.Parse(valCrs.Date));

                currencyCourses.AddRange(mappedCurrencyCourses);
            }

            await _context.CurrencyCourses.AddRangeAsync(currencyCourses, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            transaction.Commit();
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            Console.WriteLine(ex.Message);
        }        
    }

    private (DateTime start, DateTime end) GetDateTimeRange()
    {
        // TODO
        if (DateTime.Now.Day <= 10)
            return (new DateTime(DateTime.Now.Year, DateTime.Now.Month - 1, DateTime.Now.Day), DateTime.Now);


        return (new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), DateTime.Now);
    }
}

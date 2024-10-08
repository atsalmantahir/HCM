﻿using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.TaxSlabs.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Holidays.Queries.Get;

public record GetHolidayQuery(int id) : IRequest<HolidayVM>;

public class GetHolidayQueryHandler : IRequestHandler<GetHolidayQuery, HolidayVM>
{
    private readonly IHolidaysRepository repository;

    public GetHolidayQueryHandler(IHolidaysRepository repository)
    {
        this.repository = repository;
    }

    public async Task<HolidayVM> Handle(GetHolidayQuery request, CancellationToken cancellationToken)
    {
        var holiday = await this.repository.GetAsync(request.id);
        if (holiday is null) 
        {
            throw new Domain.Exceptions.NotFoundRequestException($"The holiday with identifier '{request.id}' is not found");
        }

        return holiday.ToDto();
    }
}

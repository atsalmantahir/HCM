using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Common.Models;
using HumanResourceManagement.Application.Holidays.Queries.Get;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Holidays.Queries.List;

public record GetHolidaysQuery : PaginatedQuery, IRequest<PaginatedList<HolidayVM>>;
public class GetHolidaysQueryHandler : IRequestHandler<GetHolidaysQuery, PaginatedList<HolidayVM>>
{
    private readonly IHolidaysRepository repository;

    public GetHolidaysQueryHandler(IHolidaysRepository repository)
    {
        this.repository = repository;
    }

    public async Task<PaginatedList<HolidayVM>> Handle(GetHolidaysQuery request, CancellationToken cancellationToken)
    {
        var holidays = this.repository.GetAll();
        var response = holidays.ToDto();

        return await PaginatedList<HolidayVM>.CreateAsync(response, request?.PageNumber, request?.PageSize);
    }
}

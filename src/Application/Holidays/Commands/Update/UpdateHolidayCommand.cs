using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.TaxSlabs.Commands.Update;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Holidays.Commands.Update;

public record UpdateHolidayCommand : IRequest<UpdateHolidayCommand>
{
    public string ExternalIdentifier { get; set; }

    [Required]
    public string HolidayName { get; set; }

    [Required]
    public DateTime HolidayDate { get; set; }
    public bool IsOfficial { get; set; }
    public bool IsActive { get; set; }
}

public class UpdateHolidayCommandHandler : IRequestHandler<UpdateHolidayCommand, UpdateHolidayCommand>
{
    private readonly IHolidaysRepository repository;

    public UpdateHolidayCommandHandler(IHolidaysRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UpdateHolidayCommand> Handle(UpdateHolidayCommand request, CancellationToken cancellationToken)
    {
        var holidays = this.repository.GetAll();
        if (holidays.Any(x => x.HolidayDate.Date == request.HolidayDate.Date))
        {
            throw new ConflictRequestException($"Holiday already exists for date '{request.HolidayDate.Date}' provided");
        }

        var holiday = await this.repository.GetAsync(request.ExternalIdentifier);

        if (holiday == null) 
        {
            return null;
        }

        var entity = request.ToEntity(holiday.HolidayId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}

public static class UpdateHolidayCommandExtention
{
    public static UpdateHolidayCommand StructureRequest(
        this UpdateHolidayCommand request,
        string externalIdentifier)
    {
        return new UpdateHolidayCommand
        {
            ExternalIdentifier = externalIdentifier,
            HolidayDate = request.HolidayDate,
            HolidayName = request.HolidayName,
            IsActive = request.IsActive,
            IsOfficial = request.IsOfficial 
        };
    }
}
using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Exceptions;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Holidays.Commands.Create;

public record CreateHolidayCommand : IRequest<CreateHolidayCommand>
{
    [Required]
    public string HolidayName { get; set; }

    [Required]
    public DateTime HolidayDate { get; set; }
    public bool IsOfficial { get; set; }
    public bool IsActive { get; set; }
}

public class CreateHolidayCommandHandler : IRequestHandler<CreateHolidayCommand, CreateHolidayCommand>
{
    private readonly IHolidaysRepository repository;

    public CreateHolidayCommandHandler(IHolidaysRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CreateHolidayCommand> Handle(CreateHolidayCommand request, CancellationToken cancellationToken)
    {
        var holidays = this.repository.GetAll();
        if (holidays.Any(x => x.HolidayDate.Date == request.HolidayDate.Date)) 
        {
            throw new ConflictRequestException($"Holiday already exists for date '{request.HolidayDate.Date}' provided");
        }

        var entity = request.ToEntity();

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}


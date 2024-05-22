using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Holidays.Commands;
using HumanResourceManagement.Domain.Entities;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Organisations.Commands.Create;

public record CreateOrganisationCommand : IRequest<CreateOrganisationCommand>
{

    [Required]
    public string OrganisationName { get; set; }

    public string Logo { get; set; }

    public string Address { get; set; }

    public TimeOnly TimeIn { get; set; }

    public TimeOnly TimeOut { get; set; }

    public decimal DailyWorkingHours { get; set; }

    public IList<string> WeekendHolidays { get; set; }

    public bool IsActive { get; set; }
}

public class CreateOrganisationCommandHandler : IRequestHandler<CreateOrganisationCommand, CreateOrganisationCommand>
{
    private readonly IOrganisationsRepository repository;

    public CreateOrganisationCommandHandler(IOrganisationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<CreateOrganisationCommand> Handle(CreateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var entity = request.ToCreateOrganisationEntity();

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.InsertAsync(entity, cancellationToken);

        return request;
    }
}

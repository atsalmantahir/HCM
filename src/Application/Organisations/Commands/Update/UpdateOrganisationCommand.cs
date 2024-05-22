using HumanResourceManagement.Application.Common.Mappings;
using HumanResourceManagement.Application.Organisations.Commands.Create;
using HumanResourceManagement.Domain.Repositories;
using System.ComponentModel.DataAnnotations;

namespace HumanResourceManagement.Application.Organisations.Commands.Update;

public record UpdateOrganisationCommand : IRequest<UpdateOrganisationCommand>
{
    [Required]
    public string ExternalIdentifier { get; set; }

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

public class UpdateOrganisationCommandCommandHandler : IRequestHandler<UpdateOrganisationCommand, UpdateOrganisationCommand>
{
    private readonly IOrganisationsRepository repository;

    public UpdateOrganisationCommandCommandHandler(IOrganisationsRepository repository)
    {
        this.repository = repository;
    }

    public async Task<UpdateOrganisationCommand> Handle(UpdateOrganisationCommand request, CancellationToken cancellationToken)
    {
        var organistaion = await this.repository.GetAsync(request.ExternalIdentifier);
        var entity = request.ToUpdateOrganisationEntity(organistaion.OrganisationId);

        // todo
        //entity.AddDomainEvent(new TodoItemCreatedEvent(entity));

        await repository.UpdateAsync(entity, cancellationToken);

        return request;
    }
}

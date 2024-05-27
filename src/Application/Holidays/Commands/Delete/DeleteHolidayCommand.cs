using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.Holidays.Commands.Delete;

public record DeleteHolidayCommand(int id) : IRequest<DeleteHolidayCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteHolidayCommandHandler : IRequestHandler<DeleteHolidayCommand, DeleteHolidayCommand>
{
    private readonly IHolidaysRepository repository;

    public DeleteHolidayCommandHandler(IHolidaysRepository repository)
    {
        this.repository = repository;
    }

    public async Task<DeleteHolidayCommand> Handle(DeleteHolidayCommand request, CancellationToken cancellationToken)
    {
        var holiday = await this.repository.GetAsync(request.id);
        if (holiday is null)
        {
            throw new Domain.Exceptions.NotFoundRequestException($"The holiday with identifier '{request.id}' is not found");
        }

        await this.repository.DeleteAsync(holiday, new CancellationToken());

        return new DeleteHolidayCommand(request.id)
        {
            IsDeleted = true,
        };
    }
}

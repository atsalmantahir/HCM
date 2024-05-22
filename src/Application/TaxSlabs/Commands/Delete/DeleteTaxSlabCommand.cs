﻿using HumanResourceManagement.Application.TaxSlabs.Commands.Update;
using HumanResourceManagement.Domain.Repositories;

namespace HumanResourceManagement.Application.TaxSlabs.Commands.Delete;

public record DeleteTaxSlabCommand(string externalIdentifier) : IRequest<DeleteTaxSlabCommand>
{
    public bool IsDeleted { get; set; }
}

public class DeleteTaxSlabCommandHandler : IRequestHandler<DeleteTaxSlabCommand, DeleteTaxSlabCommand>
{
    private readonly ITaxSlabsRepository taxSlabsRepository;

    public DeleteTaxSlabCommandHandler(ITaxSlabsRepository taxSlabsRepository)
    {
        this.taxSlabsRepository = taxSlabsRepository;
    }

    public async Task<DeleteTaxSlabCommand> Handle(DeleteTaxSlabCommand request, CancellationToken cancellationToken)
    {
        var taxSlab = await this.taxSlabsRepository.GetAsync(request.externalIdentifier);
        if (taxSlab == null) 
        {
            return null;
        }

        await this.taxSlabsRepository.DeleteAsync(taxSlab, new CancellationToken());

        return new DeleteTaxSlabCommand(request.externalIdentifier)
        {
            IsDeleted = true,
        };
    }
}
﻿
using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        :ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    public class UpdateCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("Product ID is required");
               
            RuleFor(command => command.Name)
                .NotEmpty().WithMessage("Product Name is required")
                .Length(2,250).WithMessage("Name must be between 2 and 150 characters");

            RuleFor(command => command.Price)
                .GreaterThan(0).WithMessage("Product's Price must be greater then 0");
        }
    }
    internal class UpdateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);

            if (product == null) 
            {
                throw new ProductNotFoundException(command.Id);
            }

            product.Name = command.Name; 
            product.Category = command.Category;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile  = command.ImageFile;

            session.Update(product);
            await session.SaveChangesAsync();

            return new UpdateProductResult(true);
        }
    }
}

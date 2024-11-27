
namespace Basket.Api.Basket.DeleteBasket;

public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketCommandResult>;

public record DeleteBasketCommandResult (bool IsSuccess);

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand> 
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty().WithMessage("The username shound not be empty!");
    }
}
public class DeleteBasketCommandHandler : ICommandHandler<DeleteBasketCommand, DeleteBasketCommandResult>
{
    public async Task<DeleteBasketCommandResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
    {
        //todo : delete basket from db and cache
        //
        return new DeleteBasketCommandResult(true);
    }
}

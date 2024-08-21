using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderResult>;

public record DeleteOrderResult(bool IsSuccess);

public class DeleteCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
    }
}
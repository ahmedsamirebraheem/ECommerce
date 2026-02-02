using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Service.Exceptions;

public abstract class NotFoundException(string message) : Exception(message)
{

}
public sealed class ProductNotFoundException(int id)
    : NotFoundException($"Product with Id {id} was not found.")
{
}

public sealed class BasketNotFoundException(string id)
    : NotFoundException($"Basket with Id {id} was not found.")
{
}

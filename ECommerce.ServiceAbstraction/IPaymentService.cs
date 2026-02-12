using ECommerce.Shared.BasketDtos;
using ECommerce.Shared.Common_Result;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.ServiceAbstraction;

public interface IPaymentService
{
    Task<Result<BasketDTO>> CreatePaymentAsync(string basketId);
    
}

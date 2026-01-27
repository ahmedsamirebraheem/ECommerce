using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Domain.Contracts;

public interface IDataInitializer
{
    Task InitializeAsync();
}

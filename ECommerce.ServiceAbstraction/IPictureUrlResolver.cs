using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.ServiceAbstraction;

public interface IPictureUrlResolver
{
    string Resolve(string sourceUrl);
}

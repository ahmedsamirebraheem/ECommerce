using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.BasketDtos;

public record BasketDTO(string Id,ICollection<BasketItemDTO> Items );

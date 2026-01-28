using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared.BasketDtos;

public record BasketItemDTO(int Id, 
                            string ProductName, 
                            string PictureUrl, 
                            decimal Price, 
                            int Quantity);

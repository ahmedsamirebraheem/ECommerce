using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Shared;

public class ProductQueryParams
{
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public string? Search { get; set; }
    public ProductSortingOptions Sort { get; set; }
    private int _pageIndex = 1;
    public int PageIndex 
    { 
        get { return _pageIndex; } 
        set 
        { 
            _pageIndex = (value<=0)?1:value; 
        } 
    }

    private const int _DefaultPageSize = 5;
    private const int _maxPageSize = 10;
    private  int _pageSize = _DefaultPageSize;
    public int PageSize { 
        get { return _pageSize; }
        set 
        {
            if (value <= 0)
            {
                _pageSize = _DefaultPageSize;
            }
            else if (value > _maxPageSize)
            {
                _pageSize = _maxPageSize;
            }
            else
            {
                _pageSize = value;
            }
        } }
}

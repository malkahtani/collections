using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISE_TEST_V1.Models
{
    public interface IPagedList
    {
       int CurrentPage { get; }   
        bool HasNextPage { get; }   
        bool HasPreviousPage { get; }  
        int ItemsPerPage { get; } 
        int TotalItems { get; }  
        int TotalPages { get; }  
    }
}

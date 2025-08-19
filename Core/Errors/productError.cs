using Core.Results;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Errors
{
    public static class productError
    {
        public static Error ProductNotFound = new Error("ProductNotFound", "Product not found", StatusCodes.Status404NotFound);
   
    }
}

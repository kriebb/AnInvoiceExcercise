using System;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.ErrorManagement.Impl
{
    internal class ErrorService : IErrorService
    {
        public ActionResult Catch(Exception ex)
        {
            return new ObjectResult(ex) { StatusCode = 500 };
        }
    }
}
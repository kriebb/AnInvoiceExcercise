using System;
using Microsoft.AspNetCore.Mvc;

namespace Backend.API.ErrorManagement
{
    public interface IErrorService
    {
        ActionResult Catch(Exception ex);
    }
}
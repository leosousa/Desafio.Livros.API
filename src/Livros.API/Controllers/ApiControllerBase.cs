﻿using Microsoft.AspNetCore.Mvc;

namespace Livros.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
}
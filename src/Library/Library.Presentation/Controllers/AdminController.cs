using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Presentation.Controllers;

[Authorize(Policy = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{

}

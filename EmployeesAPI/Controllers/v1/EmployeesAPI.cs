using Microsoft.AspNetCore.Mvc;

namespace EmployeesAPI.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class EmployeesAPI : ControllerBase
    {
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReactRio.Utils.Api;

[ApiController]
[Authorize]
public abstract class BaseController : ControllerBase
{ }

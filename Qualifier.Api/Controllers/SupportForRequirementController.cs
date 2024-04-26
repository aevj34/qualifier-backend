﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Qualifier.Application.Database.SupportForRequirement.Commands.CreateSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Commands.DeleteSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Commands.UpdateSupportForRequirement;
using Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementById;
using Qualifier.Application.Database.SupportForRequirement.Queries.GetSupportForRequirementsByDocumentationId;
using Qualifier.Common.Api;
using Qualifier.Common.Application.Dto;

namespace Qualifier.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportForRequirementController : ControllerBase
    {

        [HttpGet()]
        public async Task<IActionResult> Get(int skip, int pageSize, string? search, int documentationId, [FromServices] IGetSupportForRequirementsByDocumentationIdQuery query)
        {
            if (search == null)
                search = string.Empty;

            var res = await query.Execute(skip, pageSize, search, documentationId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(res);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, [FromServices] IGetSupportForRequirementByIdQuery getSupportForRequirementByIdQuery)
        {
            var res = await getSupportForRequirementByIdQuery.Execute(id);
            if (res.GetType() == typeof(BaseErrorResponseDto))

                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromBody] CreateSupportForRequirementDto model, [FromServices] ICreateSupportForRequirementCommand createSupportForRequirementCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            int companyId;
            bool success2 = int.TryParse(JwtTokenProvider.GetCompanyIdFromToken(accessToken), out companyId);

            if (success)
                model.creationUserId = userId;

            if (success2)
                model.companyId = companyId;

            var res = await createSupportForRequirementCommand.Execute(model);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateSupportForRequirementDto model, int id, [FromServices] IUpdateSupportForRequirementCommand updateSupportForRequirementCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userId;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userId);

            if (success)
                model.updateUserId = userId;

            var res = await updateSupportForRequirementCommand.Execute(model, id);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> delete(int id, [FromServices] IDeleteSupportForRequirementCommand deleteSupportForRequirementCommand)
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            int userIdValue;

            bool success = int.TryParse(JwtTokenProvider.GetUserIdFromToken(accessToken), out userIdValue);

            int userId = 0;
            if (success)
                userId = userIdValue;

            var res = await deleteSupportForRequirementCommand.Execute(id, userId);
            if (res.GetType() == typeof(BaseErrorResponseDto))
                return BadRequest(res);
            else
                return Ok(new
                {
                    data = res
                });

        }

    }
}

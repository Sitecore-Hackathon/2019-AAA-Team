﻿namespace Hackathon.AAATeam.Feature.Navigation
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Sitecore.Commerce.Core;

    public class ApiController : CommerceController
    {
        public ApiController(IServiceProvider serviceProvider, CommerceEnvironment globalEnvironment) : base(serviceProvider, globalEnvironment)
        {
        }

        [HttpGet]
        [Route("api/GetBreadcrumb()")]
        [Microsoft.AspNetCore.OData.EnableQuery]
        public async Task<IActionResult> GetBreadcrumb()
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var process = Command<BizFxBreadcrumbCommand>()?.Process(string.Empty, CurrentContext);

            if (process == null)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var result = await process;
            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        [HttpGet]
        [Route("api/GetBreadcrumbByItemId(itemId={itemId})")]
        [Microsoft.AspNetCore.OData.EnableQuery]
        public async Task<IActionResult> GetBreadcrumbByItemId(string itemId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var process = Command<BizFxBreadcrumbCommand>()?.Process(itemId, CurrentContext);

            if (process == null)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var result = await process;
            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }

        [HttpGet]
        [Route("api/GetChildrenByItemId(itemId={itemId})")]
        [Microsoft.AspNetCore.OData.EnableQuery]
        public async Task<IActionResult> GetChildrenByItemId(string itemId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var process = Command<BizFxBreadcrumbCommand>()?.Process(itemId, CurrentContext);

            if (process == null)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var result = await process;
            if (result == null)
            {
                return NotFound();
            }

            return new ObjectResult(result);
        }
    }
}

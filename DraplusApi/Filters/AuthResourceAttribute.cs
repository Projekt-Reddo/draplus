using System.Security.Claims;
using DraplusApi.Data;
using DraplusApi.Dtos;
using DraplusApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Driver;
using static Constant;

namespace DraplusApi.Filters
{
    public class AuthResourceAttribute : ActionFilterAttribute
    {
        public AuthResourceType ResourceType { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ClaimsIdentity claimsIdentity = context.HttpContext.User!.Identity as ClaimsIdentity;
            var userIdInAccessToken = claimsIdentity!.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();

            var requestResourceId = context.ActionArguments["id"] as string;

            if (requestResourceId == null)
            {
                throw new Exception("Resource id is required");
            }

            switch (ResourceType)
            {
                case AuthResourceType.Board:
                    var _boardRepo = (IBoardRepo)context.HttpContext.RequestServices.GetService(typeof(IBoardRepo))!;

                    if (_boardRepo == null)
                    {
                        throw new Exception("BoardRepo is null");
                    }

                    var board = _boardRepo.GetByCondition(Builders<Board>.Filter.Eq("Id", requestResourceId)).Result;
                    if (board == null)
                    {
                        context.Result = new NotFoundObjectResult(new ResponseDto(404, "Board not found"));
                    }

                    if (board?.UserId != userIdInAccessToken?.Value)
                    {
                        context.Result = new BadRequestObjectResult(new ResponseDto(400, "You don't have permission to access this resource"));
                    }

                    break;

                case AuthResourceType.User:
                    if (userIdInAccessToken == null || userIdInAccessToken.Value != requestResourceId)
                    {
                        context.Result = new BadRequestObjectResult(new ResponseDto(400, "You don't have permission to access this resource"));
                    }

                    break;

                default:
                    context.Result = new BadRequestObjectResult(new ResponseDto(400, "ResourceType is required"));
                    return;
            }
        }
    }
}
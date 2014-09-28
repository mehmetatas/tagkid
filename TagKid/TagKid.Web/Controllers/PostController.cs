using System;
using System.Web.Http;
using Taga.Core.DynamicProxy;
using Taga.Core.IoC;
using TagKid.Lib.Exceptions;
using TagKid.Lib.Models.DTO.Messages;
using TagKid.Lib.Services;

namespace TagKid.Web.Controllers
{
    [Intercept]
    public class PostController : ApiController
    {
        private readonly IPostService _postService;

        public PostController()
        {
            _postService = ServiceProvider.Provider.GetOrCreate<IPostService>();
        }

        [HttpPost]
        [ActionName("save_post")]
        public Response SavePost([FromBody] PostRequest postRequest)
        {
            try
            {
                ControllerUtils.ValidateRequest(postRequest);
                var response = _postService.SavePost(postRequest);
                ControllerUtils.FinalizeResponse(postRequest, response);
                return response;
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseCode = -1,
                    ResponseMessage = ex.GetMessage()
                };
            }
        }

        [HttpPost]
        [ActionName("search_tags")]
        public Response SearchTags([FromBody] TagSearchRequest request)
        {
            try
            {
                return ServiceProvider.Provider.GetOrCreate<ITagService>().Search(request);
            }
            catch (Exception ex)
            {
                return new Response
                {
                    ResponseCode = -1,
                    ResponseMessage = ex.GetMessage()
                };
            }
        }
    }
}
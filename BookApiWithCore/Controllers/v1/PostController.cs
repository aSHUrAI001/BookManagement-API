using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiWithCore.Contracts;
using BookApiWithCore.Contracts.v1.Requests;
using BookApiWithCore.Contracts.v1.Response;
using BookApiWithCore.Domain;
using BookApiWithCore.Extension;
using BookApiWithCore.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookApiWithCore.Controllers.v1
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostController : Controller
    {
        private IPostService _Postservice;

        public PostController(IPostService postService)
        {
            _Postservice = postService;
        }

        [HttpGet(ApiRoutes.Posts.GetAllPost)]
        public async Task<IActionResult> GetAllPostAsync()
        {
            return Ok(await _Postservice.GetPosts());
        }

        [HttpGet(ApiRoutes.Posts.Get)]
        public async Task<IActionResult> GetPostAsync([FromRoute] Guid postId)
        {
            var post = await _Postservice.GetPostByID(postId);
            if (post == null)
                return NotFound();
            return Ok(post);
        }


        [HttpPost(ApiRoutes.Posts.Create)]
        public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequest postRequest)
        {
            
            
            var post = new Post {Name = postRequest.Name,UserId=HttpContext.GetUserId()};
            var opDT=await _Postservice.CreatePost(post);
            if (opDT)
            {
                var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
                var locationUri = baseUrl + "/" + ApiRoutes.Posts.Get.Replace("postId", post.Id.ToString());
                var response = new PostResponse { Id = post.Id };
                return Created(locationUri, post);
            }
            return NotFound();
            
        }

        [HttpPost(ApiRoutes.Posts.Update)]
        public async  Task<IActionResult> UpdatePostAsync([FromRoute] Guid postId,[FromBody] UpdatePostRequest postRequest)
        {
            var userOwnPost = await _Postservice.UserOwnPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnPost)
            {
                return BadRequest(new { error="You do not own this post"});
            }
            var post = await _Postservice.GetPostByID(postId);
            post.Name = postRequest.Name;
            
            bool isUpdated=await _Postservice.UpdatePost(post);
            if (isUpdated)
                return Ok(post);
            return NotFound();
 
        }

        [HttpDelete(ApiRoutes.Posts.Delete)]
        public async Task<IActionResult> DeletePostAsync([FromRoute]Guid postId)
        {
            var userOwnPost = await _Postservice.UserOwnPostAsync(postId, HttpContext.GetUserId());
            if (!userOwnPost)
            {
                return BadRequest(new { error = "You do not own this post" });
            }
            var isdeleted =await _Postservice.DeletePost(postId);
            if(isdeleted)
                return NoContent();
            return NotFound();

        }
    }
}
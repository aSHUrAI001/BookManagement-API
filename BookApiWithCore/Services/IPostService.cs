using BookApiWithCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApiWithCore.Services
{
    public interface IPostService
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPostByID(Guid postid);
        Task<bool> UpdatePost(Post postupdate);
        Task<bool> DeletePost(Guid postID);
        Task<bool> CreatePost(Post postcreate);
        Task<bool> UserOwnPostAsync(Guid postId, string getuserid);
    }
}

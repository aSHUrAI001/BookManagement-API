using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApiWithCore.Data;
using BookApiWithCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookApiWithCore.Services
{
    public class PostService : IPostService
    {
        
        private readonly BookApiWithCore.Data.DbContext _dbcontext;
        public PostService(BookApiWithCore.Data.DbContext dbcontext)
        {
           _dbcontext=dbcontext;
        }
        

        public async Task<bool> DeletePost(Guid postID)
        {
            var post = await _dbcontext.Posts.FindAsync(postID);
            if (post==null)
                return false;
            _dbcontext.Posts.Remove(post);
            await _dbcontext.SaveChangesAsync();
            return true;
        }

        public async Task<Post> GetPostByID(Guid postid)
        {
            
            return await _dbcontext.Posts.Where(x => x.Id == postid).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetPosts()
        {
            
            return await _dbcontext.Posts.ToListAsync();
        }

        public async Task<bool> UpdatePost(Post postupdate)
        {
            
            _dbcontext.Posts.Update(postupdate);
            await _dbcontext.SaveChangesAsync();
            return true;


        }
        public async Task<bool> CreatePost(Post postcreate)
        {
            var post = await _dbcontext.Posts.FindAsync(postcreate.Id);
            if (post != null)
                return false;
            _dbcontext.Posts.Add(postcreate);
            await _dbcontext.SaveChangesAsync();
            return true;


        }

        public async Task<bool> UserOwnPostAsync(Guid postId, string getuserid)
        {
            var post = await _dbcontext.Posts.AsNoTracking().SingleOrDefaultAsync(predicate: x => x.Id == postId);
            if (post == null)
            {
                return false;
            }
            if (post.UserId != getuserid)
            {
                return false;
            }
            return true;
        }
    }
}

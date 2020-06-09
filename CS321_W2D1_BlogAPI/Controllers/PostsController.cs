﻿using Microsoft.AspNetCore.Mvc;
using CS321_W2D1_BlogAPI.Models;
using System.Collections.Generic;
using System.Linq;
using CS321_W2D1_BlogAPI.Services;

namespace CS321_W2D1_BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        // Constructor
        // IPostService is automatically injected by the ASP.NET framework, if you've
        // configured it properly in Startup.ConfigureServices()
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // get all posts
        // GET api/posts
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_postService.GetAll());
        }

        // get specific post by id
        // GET api/posts/:id
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // look up post by id
            //XTODO: use _postsService to get post by id
            var post = _postService.Get(id);

            // if not found, return 404 NotFound 
            if (post == null) return NotFound();

            // otherwise return 200 OK and the Post
            return Ok(post);
        }

        // create a new post
        // POST api/posts
        [HttpPost]
        public IActionResult Post([FromBody] Post newPost)
        {
            // add the new post
            //XTODO: use _postService to add newPost
            _postService.Add(newPost);

            // return a 201 Created status. This will also add a "location" header
            // with the URI of the new post. E.g., /api/posts/99, if the new is 99
            return CreatedAtAction("Get", new { Id = newPost.Id }, newPost);
        }

        // update an existing post
        // PUT api/posts/:id
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Post updatedPost)
        {
            Post post;
            //XTODO: use _postService to update post. store returned Post in the post variable.
            post = _postService.Update(updatedPost);

            if (post == null) return NotFound();
            return Ok(post);
        }

        // delete an existing post
        // DELETE api/posts/:id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Post post;
            //XTODO: use _postService to get post by id

            post = _postService.Get(id);

            if (post == null) return NotFound();
            //XTODO: use _postService to update post

            _postService.Remove(post);

            return NoContent();
        }
    }
}

using Blog.Data;
using Blog.Extensions;
using Blog.Models;
using Blog.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers;
[ApiController]


public class CategoryController : ControllerBase


{
    #region ListAllCategories

    // GET List
    [HttpGet("v1/categories")]
    public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context )
    {
        try
        {
            var categories = await context.Categories.ToListAsync();
        
            return Ok(new ResultViewModel<List<Category>>(categories));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<List<Category>>("0x504 - Internal Server Error"));
        }
    }

    #endregion

    #region GetByID_FIND

    // GetById Find
    [HttpGet("v1/categories/{id:int}")]
    public async Task<IActionResult> GetAsyncById([FromRoute]int id, [FromServices] BlogDataContext context )
    {
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return StatusCode(500,new ResultViewModel<Category>("0x501 - Not Found"));
            return Ok(category);
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResultViewModel<List<Category>>("0x506 - Internal Server Error"));
        }
    }

    #endregion

    #region Create

    //post Create
    [HttpPost("v1/categories/")]
    public async Task<IActionResult> PostAsyncById([FromServices] BlogDataContext context,[FromBody] EditorCategoryViewModel model )
    {
        if (!ModelState.IsValid) return BadRequest(new ResultViewModel<Category>(ModelState.GetErrors()));
        var category = new Category
        {
            Id=0,
            Posts = null,
            Name = model.Name,
            Slug = model.Slug.ToLower(),
        };
        
        try
        {
       
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return StatusCode(200, new ResultViewModel<Category>(category));
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500,new ResultViewModel<Category>("0x502 - Internal Server Error"));
        }
        catch (Exception e)
        {
            return StatusCode(500,new ResultViewModel<Category>("0x501 - Internal Server Error"));
        }

    }

    #endregion

    #region UpdateCategory

    //Put UPDATE
    [HttpPut("v1/categories/{id:int}")]
    public async Task<IActionResult> PutAsyncById(
        [FromServices] BlogDataContext context, 
        [FromBody] EditorCategoryViewModel model,
        [FromRoute] int id)
    {     
   
 
        try
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound(new ResultViewModel<Category>("0x055 - Internal Server Error"));
            
            category.Name = model.Name;
            category.Slug = model.Slug.ToLower();

            context.Categories.Update(category);
            await context.SaveChangesAsync();
            return Ok(new ResultViewModel<Category>(category));
        }
        catch 
        {
            return StatusCode(500, new ResultViewModel<Category>("Internal Server Error"));
        }
    }
        #endregion

    #region DeleteByID

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id, [FromServices] BlogDataContext context )
        {
            try
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) return NotFound(new ResultViewModel<Category>("Not Found"));
            
                context.Remove(category);
                await context.SaveChangesAsync();
                return Accepted();
            }
            catch (Exception e)
            {
                return StatusCode(500, new ResultViewModel<Category>("Internal Server Error"));
            }
        }

        #endregion





}
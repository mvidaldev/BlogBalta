using System.ComponentModel.DataAnnotations;

namespace Blog.ViewModel;

public class EditorCategoryViewModel
{
  
    public required string Name { get; set; }
    
    public required string Slug { get; set; }

}
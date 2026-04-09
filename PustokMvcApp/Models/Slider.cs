using System.Reflection;
using PustokMvcApp.Models.Common;

namespace PustokMvcApp.Models
{
    public class Slider:BaseEntity
    {
        public string Title { get; set; }=null!;
        public string Description { get; set; }=null!;
        public string ImageUrl { get; set; }=null!;
        public string ButtonText { get; set; }=null!;
        public string ButtonUrl { get; set; }= null!;
    }
}

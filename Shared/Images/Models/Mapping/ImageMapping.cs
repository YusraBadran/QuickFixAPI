using AutoMapper;
using QuickFix.Shared.Images.Models.DTOs;

namespace QuickFix.Shared.Images.Models.Mapping
{
    public class ImageMapping:Profile
    {
       public ImageMapping()
        {
            CreateMap<Image,ImageDtos>();
        }
    }
}

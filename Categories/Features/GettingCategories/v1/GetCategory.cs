using AutoMapper;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Models.DTOs;
using QuickFix.Shared.Abstractions.Commands;

namespace QuickFix.Categories.Features.GettingCategories.v1;

public record GetCategory : ITxCommand<GetCategoryRespons>
{
}

public class GetCategoryHandler : ICommandHandler<GetCategory, GetCategoryRespons>
{
    private readonly ICategoryContext _context;
    private readonly IMapper _mapper;
    public GetCategoryHandler(ICategoryContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<GetCategoryRespons> Handle(GetCategory request, CancellationToken cancellationToken)
    {
        var category = await _context.FindAllCategory();
        var respons = _mapper.Map<IEnumerable<CategoryDTOs>>(category);
        List<CategoryDTOs> resoult = new();
        foreach (var item in respons)
        {
            var subCategory = respons.Where(c => c.Id == item.SubCategoryId).FirstOrDefault();
            var resoultDto = new CategoryDTOs
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                State = item.State,
                ServiceId = item.ServiceId,
                SubCategoryId = item.SubCategoryId,
                ServiceType = item.ServiceType
            };
            if (subCategory != null)
            {
                resoultDto.SubCategory = new LookUpCategoryRespons
                {
                    Id = subCategory.Id,
                    Name = subCategory.Name
                };
            }
            resoult.Add(resoultDto);

        }
        return new GetCategoryRespons(resoult);
    }
}
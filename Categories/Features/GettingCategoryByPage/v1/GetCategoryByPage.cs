using AutoMapper;
using QuickFix.Categories.Data;
using QuickFix.Categories.Extensions;
using QuickFix.Categories.Features.LookUpsCategory.v1;
using QuickFix.Categories.Models.DTOs;
using QuickFix.Shared.Abstractions.Queries;
using QuickFix.Shared.Core.Queries;

namespace QuickFix.Categories.Features.GettingCategoryByPage.v1;

public record GetCategoryByPage : ListQuery<GetCategoryByPageResponse>;
public class GetCategoryHandler : IQueryHandler<GetCategoryByPage, GetCategoryByPageResponse>
{
    private readonly ICategoryContext _category;
    private readonly IMapper _mapper;
    public GetCategoryHandler(ICategoryContext category, IMapper mapper)
    {
        _category = category;
        _mapper = mapper;
    }
    public async Task<GetCategoryByPageResponse> Handle(GetCategoryByPage request, CancellationToken cancellationToken)
    {
        var category = await _category.FindCategoryWithPageAsync<CategoryDTOs>(_mapper, request, cancellationToken);
        List<CategoryDTOs> resoult = new List<CategoryDTOs>();
        foreach (var item in category.Data)
        {
            var subCategory = category.Data.Where(c => c.Id == item.SubCategoryId).FirstOrDefault();
            var resoultDto = new CategoryDTOs
            {
                Id = item.Id,
                Name = item.Name,
                Logo = item.Logo,
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
        category.Data.Clear();
        category.Data.AddRange(resoult);
        return new GetCategoryByPageResponse(category);
    }
}
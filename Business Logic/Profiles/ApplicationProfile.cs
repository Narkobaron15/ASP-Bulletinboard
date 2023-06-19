using Business_Logic.DTO;
using DataAccess.Entities;
using File = DataAccess.Entities.File;

namespace Business_Logic.Profiles;

public class ApplicationProfile : AutoMapper.Profile
{
    public ApplicationProfile()
    {
        // Bulletin maps (map city and country names)
        this.CreateMap<Bulletin, BulletinDTO>();
        this.CreateMap<BulletinDTO, Bulletin>().ForMember(x => x.OwnerId, opt => opt.Ignore());

        // Category maps
        this.CreateMap<Category, CategoryDTO>();
        this.CreateMap<CategoryDTO, Category>();

        // City maps
        this.CreateMap<City, CityDTO>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.Name));
        this.CreateMap<CityDTO, City>();

        // Favorite maps
        this.CreateMap<Favorite, FavoriteDTO>();
        this.CreateMap<FavoriteDTO, Favorite>();

        this.CreateMap<File, FileDTO>();
        this.CreateMap<FileDTO, File>();
    }
}

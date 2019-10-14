using AdvertApi.Models;
using AutoMapper;

namespace AdevertApi.Services
{
	public class AdvertProfile : Profile
	{
		public AdvertProfile()
		{
			CreateMap<AdvertModel, AdvertDbModel>();
		}
	}
}

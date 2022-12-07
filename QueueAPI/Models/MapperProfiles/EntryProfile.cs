using QueueAPI.Database.Entities;

namespace QueueAPI.Models.MapperProfiles
{
    public class EntryProfile : AutoMapper.Profile
    {
        public EntryProfile()
        {
            CreateMap<Entry, EntryModel>();
            CreateMap<EntryModel, Entry>();
        }
    }
}

using QueueAPI.Models;

namespace QueueAPI.Providers.QueueEntryProvider
{
    public interface IQueueEntryProvider
    {
        Task<(bool IsSuccess, EntryModel? entry, string? Error)> GetEntry();
        Task<(bool IsSuccess, EntryModel? entry, string? Error)> CreateEntry(EntryModel entry);
        Task<IEnumerable<(bool IsSuccess, EntryModel entry, string? Error)>> CreateEntries(IEnumerable<EntryModel> entries);
    }
}

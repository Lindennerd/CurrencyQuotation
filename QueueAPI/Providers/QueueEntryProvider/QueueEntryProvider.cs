using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QueueAPI.Database;
using QueueAPI.Database.Entities;
using QueueAPI.Models;

namespace QueueAPI.Providers.QueueEntryProvider
{
    public class QueueEntryProvider : IQueueEntryProvider
    {
        private QueueContext Context { get; }
        private ILogger<QueueEntryProvider> Logger { get; }
        private IMapper Mapper { get; }

        private const string NothingFoundMessage = "Nothing Found";

        public QueueEntryProvider(QueueContext ctx, ILogger<QueueEntryProvider> logger, IMapper mapper)
        {
            this.Context = ctx;
            this.Logger = logger;
            this.Mapper = mapper;
        }

        private void SetEntryAsConsumed(Entry entry)
        {
            entry.WasConsumed = true;
            Context.SaveChanges();
        }


        public async Task<(bool IsSuccess, EntryModel? entry, string? Error)> GetEntry()
        {
            try
            {
                var entry = await Context.Entries
                    .OrderBy(entry => entry.CreatedAt)
                    .FirstOrDefaultAsync(entry => !entry.WasConsumed);

                if (entry == null) return (false, null, NothingFoundMessage);
                var entryModel = Mapper.Map<EntryModel>(entry);

                SetEntryAsConsumed(entry);

                return (true, entryModel, null);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, EntryModel? entry, string? Error)> CreateEntry(EntryModel entry)
        {
            try
            {
                var newEntry = await Context.Entries.AddAsync(Mapper.Map<Entry>(entry));
                Context.SaveChanges();

                return (true, Mapper.Map<EntryModel>(newEntry.Entity), null);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<IEnumerable<(bool IsSuccess, EntryModel entry, string? Error)>> CreateEntries(IEnumerable<EntryModel> entries)
        {
            var results = new List<(bool IsSuccess, EntryModel entry, string? Error)>();

            foreach (var entry in entries)
            {
                var result = await CreateEntry(entry);
                results.Add(result);
            }

            return results;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using QueueAPI.Models;
using QueueAPI.Providers.QueueEntryProvider;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QueueAPI.Controllers
{
    [Route("api/")]
    [ApiController]
    public class QueueController : ControllerBase
    {
        public IQueueEntryProvider QueueEntryProvider { get; set; }
        public ILogger<QueueController> Logger { get; }

        public QueueController(IQueueEntryProvider queueEntryProvider, ILogger<QueueController> logger)
        {
            this.QueueEntryProvider = queueEntryProvider;
            this.Logger = logger;
        }

        // GET: api/<QueueController>
        [HttpGet]
        [Route("GetItemFila")]
        public async Task<IActionResult> Get()
        {
            try
            {
                Logger.LogInformation("Getting the log entry to process");
                var result = await QueueEntryProvider.GetEntry();

                Logger.LogDebug("The entry fetched from the query", result.entry);
                if (!result.IsSuccess) return NoContent();
                return Ok(result.entry);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), ex);
                return BadRequest(ex.ToString());
            }
        }


        // POST api/<QueueController>
        [HttpPost]
        [Route("AddItemFila")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<EntryModel> entries)
        {
            try
            {
                Logger.LogInformation("Inserting new entries to the queue", entries);
                var result = await QueueEntryProvider.CreateEntries(entries);
                return Created("", result.Select(it => it.entry));
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString(), ex);
                return BadRequest(ex.ToString());
            }
        }
    }
}

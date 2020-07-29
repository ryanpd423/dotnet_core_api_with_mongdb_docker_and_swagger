using System.Collections.Generic;
using System.Threading.Tasks;
using MedicineCabinet_CRUD_API.Models;
using MedicineCabinet_CRUD_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MedicineCabinet_CRUD_API.Controllers
{
    // Try to perhaps wrap ControllerBase and inject it as a library 
    // instantiated interface for testability and DI?
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly ILogger<MedicineController> _logger;

        public MedicineController(IMedicineService medicineService, 
        ILogger<MedicineController> logger)
        {
            _medicineService = medicineService;
            _logger = logger;
        }

        // GET: api/Medicine
        [HttpGet]
        public async Task<ActionResult<List<Medicine>>> Get()
        {
            return await _medicineService.GetMedicines();
        }

        // GET: api/Medicine/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Medicine>> Get(string id)
        {
            var medicine = await _medicineService.GetMedicineById(id);
            if (medicine == null)
            {
                return NotFound();
            }
            return medicine;
        }

        // POST: api/Medicine
        [HttpPost]
        public async Task<ActionResult<Medicine>> Create([FromBody] Medicine med)
        {
            await _medicineService.CreateMedicine(med);
            return CreatedAtRoute("Get", new { id = med.Id.ToString() }, med);
        }

        // PUT: api/Medicine/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Medicine>> Put(string id, [FromBody] Medicine med)
        {
            var medicine = await _medicineService.GetMedicineById(id);
            if (medicine == null)
            {
                return NotFound();
            }
            med.Id = medicine.Id;

            await _medicineService.UpdateMedicine(med, id);
            return CreatedAtRoute("Get", new { id = med.Id.ToString() }, med);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Medicine>> Delete(string id)
        {
            var medicine = await _medicineService.GetMedicineById(id);
            if (medicine == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
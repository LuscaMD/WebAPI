using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;
using WebAPI.ViewModel;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger <EmployeeController> _logger;

        public EmployeeController(IEmployeeRepository employeeRepository, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //[Authorize]
        [HttpPost]
        public IActionResult Add([FromForm] EmployeeViewModel employeeView)
        {
            var filePath = string.Empty;
            if (employeeView.Photo != null)
            {
                filePath = Path.Combine("Storage", employeeView.Photo.FileName);

                using Stream fileStream = new FileStream(filePath, FileMode.Create);
                employeeView.Photo.CopyTo(fileStream);
            }

            var employee = new Employee(employeeView.Name, employeeView.Age, filePath); 

            _employeeRepository.Add(employee);
            _logger.LogInformation("Um funcionário foi cadastrado.");

            return Ok();
        }

        //[Authorize]
        [HttpPost]
        [Route("{id}/download")]
        public IActionResult DownloadPhoto(int id)
        {
            var employee = _employeeRepository.Get(id);
            var dataBytes = System.IO.File.ReadAllBytes(employee.photo);

            return File(dataBytes, "image/png");
        }

        //[Authorize]
        [HttpGet]
        public IActionResult Get(int pageNumber, int pageQuantity)
        {
            if(pageNumber == 0 || pageQuantity == 0)
            {
                throw new Exception("Não foi informado os parâmetros necessários.");
            }
            var employee = _employeeRepository.Get(pageNumber, pageQuantity);

            return Ok(employee);
        }
    }
}

using Bolao.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bolao.Controllers
{
    [Route("api/[controller]")]
    public class ClassificacaoController : Controller
    {
        private readonly IClassificacaoService _classificacaoService;

        public ClassificacaoController(IClassificacaoService classificacaoService)
        {
            _classificacaoService = classificacaoService;
        }

        [HttpGet]       
        public IActionResult Get()
        {
            var result = _classificacaoService.GetAll();
            return Json(result);
        }
    }
}

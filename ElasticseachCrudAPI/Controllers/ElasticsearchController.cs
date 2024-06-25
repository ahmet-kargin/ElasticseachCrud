using ElasticseachCrudAPI.Model;
using ElasticseachCrudAPI.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElasticseachCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticsearchController : ControllerBase
    {
        private readonly Services.IElasticsearchService<MyDocument> _elasticsearchService;

        public ElasticsearchController(IElasticsearchService<MyDocument> elasticsearchService) 
        {
            _elasticsearchService = elasticsearchService;
        }
        [EnableCors("AllowSpecificOrigin")]
        [HttpGet]
        public async Task<IActionResult> GetAllDocuments() 
        {
            var response = await _elasticsearchService.GetAllDocuments();

            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateDocument([FromBody] MyDocument document)
        {
            var result = await _elasticsearchService.CreateDocumentAsync(document);

            return Ok(result);
        }
        [HttpGet]
        [Route("read/{id}")]
        public async Task<IActionResult> GetDocument(int id)
        {
            var document = await _elasticsearchService.GetDocumentAsync(id);
            if (document == null)
                return NotFound();
            else
                return Ok(document);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDocument([FromBody] MyDocument document)
        {
            var result = await _elasticsearchService.UpdateDocumentAsync(document);
            return Ok(result);
        }
        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> DeleteDocument(int id)
        {
            var result = await _elasticsearchService.DeleteDocumentAsync(id);
            return Ok(result);
        }
    }
}

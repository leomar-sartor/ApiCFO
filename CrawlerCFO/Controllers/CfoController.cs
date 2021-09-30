using AngleSharp;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CrawlerCFO.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CfoController : ControllerBase
    {
        private string _url = $"https://website.cfo.org.br/profissionais-cadastrados/?cro=ESTADO&inscricao=CREDENCIAL";
        private Dentista _dentista;

        [HttpGet("{estado}/{credencial}")]
        public async Task<IActionResult> consultaDadoset(string estado, string credencial)
        {
            _url = _url.Replace("ESTADO", estado).Replace("CREDENCIAL", credencial);

            var context = BrowsingContext.New(
               Configuration.Default.WithDefaultLoader()
               .WithDefaultCookies()
               );

            var Page = await context.OpenAsync(_url);

            var Results = Page.GetElementsByClassName("entry-content").FirstOrDefault();
            Results.QuerySelector("div").Remove();
            Results.QuerySelector("script").Remove();
            Results.QuerySelector("h6").Remove();

            var nosComValor = Results.ChildNodes
                .Where(n => !String.IsNullOrWhiteSpace(n.TextContent) && n.NodeName != "HR" && n.NodeName != "A")
                .ToList();

            if (nosComValor.Count > 6)
            {
                nosComValor.RemoveAt(0);
                nosComValor.RemoveAt(nosComValor.Count - 1);
                nosComValor.RemoveAt(nosComValor.Count - 1);
            }

            if (nosComValor.Count == 6)
            {
                try
                {
                    _dentista = new Dentista(
                        nosComValor[0].TextContent.Split("-")[0],
                        nosComValor[0].TextContent.Split("-")[1].Split(":")[1],
                        nosComValor[1].TextContent,
                        nosComValor[2].TextContent.Split(":")[1],
                        nosComValor[3].TextContent.Split(":")[1],
                        nosComValor[4].TextContent.Split(":")[1].Trim(),
                        nosComValor[5].TextContent.Split(":")[1].Trim()
                    );

                    return Ok(_dentista);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }

            return NotFound("CFO não encontrado!");
        }
    }
}

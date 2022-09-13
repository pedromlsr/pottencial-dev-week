using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using src.Models;
using src.Persistence;

namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PessoaController : ControllerBase {

    private DatabaseContext _context { get; set; }

    public PessoaController(DatabaseContext context) {
        this._context = context;
    }

    [HttpGet]
    // public Pessoa Get() {
    public List<Pessoa> Get() {
        // Pessoa pessoa = new Pessoa("Pedro", 28, "11111111111");
        // Contrato novoContrato = new Contrato("abc123", 50.46);
        // pessoa.contratos.Add(novoContrato);\

        return _context.Pessoas.Include(p => p.contratos).ToList();
    }

    [HttpPost]
    public Pessoa Post([FromBody]Pessoa pessoa) {
        _context.Pessoas.Add(pessoa);
        _context.SaveChanges();

        return pessoa;
    }

    [HttpPut("{id}")]
    public string Update([FromRoute]int id, [FromBody]Pessoa pessoa) {
        // Console.WriteLine(id);
        // Console.WriteLine(pessoa);

        _context.Pessoas.Update(pessoa);
        _context.SaveChanges();

        return "Dados do id " + id + " atualizados";
    }

    [HttpDelete("{id}")]
    public string Delete([FromRoute]int id) {
        var result = _context.Pessoas.SingleOrDefault(e => e.Idade == id);

        _context.Pessoas.Remove(result);
        _context.SaveChanges();

        return "Deletada pessoa de id " + id;
    }
}

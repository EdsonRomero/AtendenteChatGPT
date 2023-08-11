using AtendenteChatGPT.Models;
using Microsoft.AspNetCore.Mvc;

namespace AtendenteChatGPT.Controllers;

public class ConversaController : Controller
{
    private readonly JsonFileService<conversa> _jsonfileService;

    public ConversaController(JsonFileService<conversa> jsonfileservice)
    {
        _jsonfileService = jsonfileservice;
    }

    public IActionResult Index()
    {
        var conversa = _jsonfileService.LerDados();
        return View(conversa);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(conversa novaConversa)
    {
        if (ModelState.IsValid)
        {
            var conversa = _jsonfileService.LerDados();
            novaConversa.Id= conversa.Count + 1;
            conversa.Add(novaConversa);
            _jsonfileService.GravarDados(conversa);
            return RedirectToAction("Index");
        }
        return View(novaConversa);
    }

    public IActionResult Edit(int id) 
    {
        var conversa = _jsonfileService.LerDados();
        var conversaEdit = conversa.FirstOrDefault(c => c.Id == id);
        if (conversaEdit == null)
        {
            return NotFound();
        }
        return View(conversaEdit);

    }

    [HttpPost]
    public IActionResult Edit(conversa atendenteEditado) 
    {
        if (ModelState.IsValid) { 
            var conversa = _jsonfileService.LerDados();
            var conversaExistente = conversa.FirstOrDefault(c => c.Id == atendenteEditado.Id);

            if(conversaExistente != null)
            {
                conversaExistente.atendente = atendenteEditado.atendente;
                conversaExistente.atendido = atendenteEditado.atendido;
                _jsonfileService.GravarDados(conversa);
                return RedirectToAction("Index");
            }
        }
        return View(atendenteEditado);

    }

    public IActionResult Delete(int id) 
    {
        var conversa = _jsonfileService.LerDados();
        var conversaDelete = conversa.FirstOrDefault(c => c.Id == id);
        if (conversaDelete == null)
        {
            return NotFound();
        }
        return View(conversaDelete);
    }

    [HttpPost]
    [ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var conversa = _jsonfileService.LerDados();
        var conversaDelete = conversa.FirstOrDefault(c => c.Id == id);
        if (conversaDelete != null)
        {
            conversa.Remove(conversaDelete);
            _jsonfileService.GravarDados(conversa);
        }
        return RedirectToAction("Index");
    }


}

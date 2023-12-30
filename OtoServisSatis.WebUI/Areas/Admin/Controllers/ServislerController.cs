using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers;
[Area("Admin")]
public class ServislerController : Controller
{
    private readonly IService<Servis> _service;

    public ServislerController(IService<Servis> service)
    {
        _service = service;
    }

    // GET: ServislerController
    public async Task<ActionResult> IndexAsync()
    {
        var model = await _service.GetAllAsync();
        return View(model);
    }

    // GET: ServislerController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: ServislerController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: ServislerController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(Servis servis)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _service.AddAsync(servis);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
        }
        return View(servis);
    }

    // GET: ServislerController/Edit/5
    public async Task<ActionResult> EditAsync(int id)
    {
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: ServislerController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(int id, Servis servis)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _service.Update(servis);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
        }
        return View(servis);
    }

    // GET: ServislerController/Delete/5
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: ServislerController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Servis servis)
    {
        try
        {
            _service.Delete(servis);
            _service.Save();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

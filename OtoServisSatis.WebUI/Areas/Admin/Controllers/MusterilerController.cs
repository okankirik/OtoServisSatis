using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers;

[Area("Admin")]
public class MusterilerController : Controller
{
    private readonly IService<Musteri> _service;
    private readonly IService<Arac> _serviceArac;

    public MusterilerController(IService<Musteri> service, IService<Arac> serviceArac)
    {
        _service = service;
        _serviceArac = serviceArac;
    }

    // GET: MusterilerController
    public async Task<ActionResult> IndexAsync()
    {
        var model = await _service.GetAllAsync();
        return View(model);
    }

    // GET: MusterilerController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: MusterilerController/Create
    public async Task<ActionResult> CreateAsync()
    {
        ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
        return View();
    }

    // POST: MusterilerController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(Musteri musteri)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _service.AddAsync(musteri);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("","Hata Oluştu!");
            }
        }
        ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
        return View(musteri);
    }

    // GET: MusterilerController/Edit/5
    public async Task<ActionResult> EditAsync(int id)
    {
        ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: MusterilerController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(int id, Musteri musteri)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _service.Update(musteri);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
        }
        ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
        return View(musteri);
    }

    // GET: MusterilerController/Delete/5
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: MusterilerController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Musteri musteri)
    {
        try
        {
            _service.Delete(musteri);
            _service.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

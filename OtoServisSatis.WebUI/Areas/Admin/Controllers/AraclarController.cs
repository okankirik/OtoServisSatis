using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers;
[Area("Admin")]
public class AraclarController : Controller
{
    private readonly IService<Arac> _service;
    private readonly IService<Marka> _serviceMarka;

    public AraclarController(IService<Arac> service, IService<Marka> serviceMarka)
    {
        _service = service;
        _serviceMarka = serviceMarka;
    }

    // GET: AraclarController
    public async Task<ActionResult> IndexAsync()
    {
        var model = await _service.GetAllAsync();
        return View(model);
    }

    // GET: AraclarController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: AraclarController/Create
    public async Task<ActionResult> CreateAsync()
    {
        ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id","Adi");
        return View();
    }

    // POST: AraclarController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(Arac arac)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _service.AddAsync(arac);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata oluştu!");
            }
        }
        ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");
        return View(arac);
    }

    // GET: AraclarController/Edit/5
    public async Task<ActionResult> EditAsync(int id)
    {
        ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: AraclarController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(int id, Arac arac)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _service.Update(arac);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata oluştu!");
            }
        }
        ViewBag.MarkaId = new SelectList(await _serviceMarka.GetAllAsync(), "Id", "Adi");
        return View(arac);
    }

    // GET: AraclarController/Delete/5
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: AraclarController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteAsync(int id, Arac arac)
    {
        try
        {
            _service.Delete(arac);
            await _service.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

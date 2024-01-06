using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers;
[Area("Admin"), Authorize]
public class KullanicilarController : Controller
{
    private readonly IService<Kullanici> _service;
    private readonly IService<Rol> _serviceRol;

    public KullanicilarController(IService<Kullanici> service, IService<Rol> serviceRol)
    {
        _service = service;
        _serviceRol = serviceRol;
    }

    // GET: KullanicilarController
    public async Task<ActionResult> IndexAsync()
    {
        var model = await _service.GetAllAsync();
        return View(model);
    }

    // GET: KullanicilarController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: KullanicilarController/Create
    public async Task<ActionResult> CreateAsync()
    {
        ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
        return View();
    }

    // POST: KullanicilarController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(Kullanici kullanici)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _service.AddAsync(kullanici);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
        }
        ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
        return View(kullanici);
    }

    // GET: KullanicilarController/Edit/5
    public async Task<ActionResult> EditAsync(int id)
    {
        var model = await _service.FindAsync(id);
        ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
        return View(model);
    }

    // POST: KullanicilarController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(int id, Kullanici kullanici)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _service.Update(kullanici);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
        }
        ViewBag.RolId = new SelectList(await _serviceRol.GetAllAsync(), "Id", "Adi");
        return View(kullanici);
    }

    // GET: KullanicilarController/Delete/5
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: KullanicilarController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Kullanici kullanici)
    {
        try
        {
            _service.Delete(kullanici);
            _service.Save();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

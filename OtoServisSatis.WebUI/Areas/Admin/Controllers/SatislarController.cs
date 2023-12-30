using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers;
[Area("Admin")]
public class SatislarController : Controller
{
    private readonly IService<Satis> _service;
    private readonly IService<Arac> _serviceArac;
    private readonly IService<Musteri> _serviceMusteri;

    public SatislarController(IService<Satis> service, IService<Musteri> serviceMusteri, IService<Arac> serviceArac)
    {
        _service = service;
        _serviceMusteri = serviceMusteri;
        _serviceArac = serviceArac;
    }

    // GET: SatisController
    public async Task<ActionResult> IndexAsync()
    {
        var model = await _service.GetAllAsync();
        return View(model);
    }

    // GET: SatisController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: SatisController/Create
    public async Task<ActionResult> CreateAsync()
    {
        ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
        ViewBag.MusteriId = new SelectList(await _serviceMusteri.GetAllAsync(), "Id", "Adi");
        return View();
    }

    // POST: SatisController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> CreateAsync(Satis satis)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _service.AddAsync(satis);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
        }
        ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
        ViewBag.MusteriId = new SelectList(await _serviceMusteri.GetAllAsync(), "Id", "Adi");
        return View(satis);
    }

    // GET: SatisController/Edit/5
    public async Task<ActionResult> EditAsync(int id)
    {
        ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
        ViewBag.MusteriId = new SelectList(await _serviceMusteri.GetAllAsync(), "Id", "Adi");
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: SatisController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> EditAsync(int id, Satis satis)
    {
        if (ModelState.IsValid)
        {
            try
            {
                 _service.Update(satis);
                await _service.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Hata Oluştu!");
            }
        }
        ViewBag.AracId = new SelectList(await _serviceArac.GetAllAsync(), "Id", "Modeli");
        ViewBag.MusteriId = new SelectList(await _serviceMusteri.GetAllAsync(), "Id", "Adi");
        return View(satis);
    }

    // GET: SatisController/Delete/5
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: SatisController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Satis satis)
    {
        try
        {
            _service.Delete(satis);
            _service.Save();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

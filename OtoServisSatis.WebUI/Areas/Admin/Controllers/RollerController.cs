﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OtoServisSatis.Entities;
using OtoServisSatis.Service.Abstract;

namespace OtoServisSatis.WebUI.Areas.Admin.Controllers;
[Area("Admin")]
public class RollerController : Controller
{
    private readonly IService<Rol> _service;

    public RollerController(IService<Rol> service)
    {
        _service = service;
    }

    // GET: RollerController
    public async Task<ActionResult> IndexAsync()
    {
        var model = await _service.GetAllAsync();
        return View(model);
    }

    // GET: RollerController/Details/5
    public ActionResult Details(int id)
    {
        return View();
    }

    // GET: RollerController/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: RollerController/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Rol rol)
    {
        try
        {
            _service.Add(rol);
            _service.Save();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: RollerController/Edit/5
    public async Task<ActionResult> EditAsync(int id)
    {
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: RollerController/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(int id, Rol rol)
    {
        try
        {
            _service.Update(rol);
            _service.Save();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }

    // GET: RollerController/Delete/5
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var model = await _service.FindAsync(id);
        return View(model);
    }

    // POST: RollerController/Delete/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, Rol rol)
    {
        try
        {
            _service.Delete(rol);
            _service.Save();
            return RedirectToAction(nameof(Index));
        }
        catch
        {
            return View();
        }
    }
}

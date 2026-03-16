using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementRepoPattern.Models;
using StudentManagementRepoPattern.UnitOfWorks;

namespace StudentManagementRepoPattern.Controllers;

public class DepartmentController : Controller
{
    private readonly IUnitOfWork _uow;

    public DepartmentController(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await _uow.Departments.Query()
            .AsNoTracking()
            .Include(d => d.Courses)
            .OrderBy(d => d.DepartmentName)
            .ToListAsync();

        return View(departments);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id is null) return NotFound();

        var department = await _uow.Departments.Query()
            .AsNoTracking()
            .Include(d => d.Courses)
            .Include(d => d.Students)
            .FirstOrDefaultAsync(d => d.DepartmentId == id.Value);

        return department is null ? NotFound() : View(department);
    }

    public IActionResult Create()
        => View(new Department());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Department department)
    {
        if (!ModelState.IsValid) return View(department);

        _uow.Departments.Insert(department);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int? id)
    {
        if (id is null) return NotFound();
        var department = _uow.Departments.GetById(id.Value);
        return department is null ? NotFound() : View(department);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Department department)
    {
        if (id != department.DepartmentId) return NotFound();
        if (!ModelState.IsValid) return View(department);

        _uow.Departments.Update(department);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null) return NotFound();

        var department = await _uow.Departments.Query()
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.DepartmentId == id.Value);

        return department is null ? NotFound() : View(department);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        _uow.Departments.Delete(id);
        await _uow.SaveAsync();
        return RedirectToAction(nameof(Index));
    }
}


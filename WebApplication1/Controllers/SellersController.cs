using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.ViewModels;
using SalesWebMvc.Services;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Services;
using System.Collections.Generic;
using WebApplication1.Services.Exceptions;
using System.Diagnostics;

namespace WebApplication1.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartamentService _departmentService;

        public SellersController(SellerService sellerService, DepartamentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departaments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error),new {message = "Id não encontrado"  });
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return  RedirectToAction(nameof(Error), new { message = "Id não encontrado" });

            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();

            }
            return View(obj);

        }


        public IActionResult Edit (int? id)
        {

            if(id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            var obj = _sellerService.FindById(id.Value);
            if(obj == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }

            List<Departament> departaments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departaments = departaments };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit (int id, Seller seller)
        {

            if(id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado" });
            }
            try
            {
                _sellerService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message});
            }
            


        }
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
        return View(viewModel); 
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using WebApplication7.Models;
using WebApplication7.Repositry.IRepositry;

namespace WebApplication7.Controllers
{
    public class PromotionController : Controller
    {
        private readonly IPromotion _promotionRepository;
        public PromotionController(IPromotion promotionRepository)
        {
            _promotionRepository = promotionRepository;
        }
        public IActionResult Index()
        {
            var promotions = _promotionRepository.GetAll();
            ViewBag.Promotions = promotions;
            return View(new Promotion());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Promotion promotion)
        {
            if (ModelState.IsValid)
            {
                _promotionRepository.Add(promotion);
                return RedirectToAction("Index");
            }
            var promotions = _promotionRepository.GetAll();
            ViewBag.Promotions = promotions;
            return View("Index", promotion);
        }
        public IActionResult Delete(int id)
        {
            _promotionRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SaaLItems.Models;

namespace SaaLItems.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index()
        {
            List<ItemModel> items = ItemRepository.GetItems();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ItemModel obj)
        {
            ItemRepository.AddObject(obj);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            ItemModel? obj = ItemRepository.GetItems().FirstOrDefault(o => o.Id == id);

            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(ItemModel item)
        {

            int id = ItemRepository.GetItems().FirstOrDefault(o => o.Id == item.Id).Id;

            ItemRepository.EditItem(id, item);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            ItemRepository.DeleteObject(id);
            return RedirectToAction("Index");
        }

        public ActionResult DeleteRelation(int id)
        {
            RelationshipRepository.DeleteRelation(id);
            return RedirectToAction("Index");
        }

        public ActionResult Search(string term)
        {
            List<ItemModel> matchedObjects = ItemRepository.GetItems().Where(o => o.Name.Contains(term)).ToList();
            return View("Index", matchedObjects);
        }

        public IActionResult Relationships(int itemId)
        {
            List<RelationModel> relationships = RelationshipRepository.GetItemRelationships().Where(r => r.ItemId1 == itemId || r.ItemId2 == itemId).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateRelationship(RelationModel model)
        {
            // Check if the model is valid
            if (ModelState.IsValid)
            {
                // Add the relationship to the repository
                RelationshipRepository.AddRelation(model);

                // Return a partial view with the updated relationships
                return PartialView("Relationships", RelationshipRepository.GetItemRelationships());
            }

            // If the model is not valid, return the view with errors
            return View("Relationships", model);
        }

    }
}

﻿using Microsoft.AspNetCore.Mvc;
using SaaLItems.Models;

namespace SaaLItems.Controllers
{
    public class ItemController : Controller
    {
        public ActionResult Index()
        {
            List<ItemModel> items = ItemRepository.GetItems();

            return View("~/Views/Home/Index.cshtml", items);
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
            return View("Relationships");
        }

        public ActionResult Search(string term)
        {
            // Assuming GetItems is a method to retrieve all items
            List<ItemModel> items = ItemRepository.GetItems();

            if (!string.IsNullOrEmpty(term))
            {
                // Filter items based on the search term
                items = items.Where(item =>
                    item.Name.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    item.Description.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    item.Type.Contains(term, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            return PartialView("ItemTablePartial", items);
        }

        public IActionResult Relationships(int itemId)
        {
            List<RelationModel> relationships = RelationshipRepository.GetItemRelationships().Where(r => r.ItemId1 == itemId || r.ItemId2 == itemId).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult CreateRelationship(RelationModel model)
        {

                // Add the relationship to the repository
                RelationshipRepository.AddRelation(model);

                // Return a partial view with the updated relationships
                return PartialView("RelationshipsTablePartial", RelationshipRepository.GetItemRelationships());

        }

    }
}

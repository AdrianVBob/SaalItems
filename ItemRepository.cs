using SaaLItems.Models;
using System.Text.Json;

namespace SaaLItems
{
    public static class ItemRepository
    {
        private static List<ItemModel> _items = [];

        /// <summary>
        /// Constructor
        /// </summary>
        static ItemRepository()
        {
            LoadObjects();
        }

        /// <summary>
        /// Retrieves all items. 
        /// </summary>
        /// <returns></returns>
        public static List<ItemModel> GetItems()
        {
            return _items;
        }

        /// <summary>
        /// Add item. 
        /// </summary>
        public static void AddObject(ItemModel item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            SaveObjects();
        }

        /// <summary>
        /// Delete item.
        /// </summary>
        /// <param name="objectId"></param>
        public static void DeleteObject(int objectId)
        {
            _items.RemoveAll(obj => obj.Id == objectId);
            SaveObjects();
        }

        public static void EditItem(int index, ItemModel item)
        {
            _items[index].Name = item.Name;
            _items[index].Description = item.Description;
            _items[index].Type = item.Type;
        }

        private static void LoadObjects()
        {
            try
            {
                string objectsJson = File.ReadAllText("items.json");
                _items = JsonSerializer.Deserialize<List<ItemModel>>(objectsJson) ?? [];
            }
            catch (FileNotFoundException)
            {
                // Handle file not found exception, create an empty list
                _items = [];
            }
        }

        private static void SaveObjects()
        {
            string objectsJson = JsonSerializer.Serialize(_items);
            File.WriteAllText("objects.json", objectsJson);
        }
    }
}

using SaaLItems.Models;
using System.Text.Json;

namespace SaaLItems
{
    public static class RelationshipRepository
    {
        private static List<ItemModel> _items = [];
        private static List<RelationModel> _relations = [];

        /// <summary>
        /// Constructor
        /// </summary>
        static RelationshipRepository()
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


        public static List<RelationModel> GetItemRelationships()
        {
            // Load relationships from the JSON file
            LoadRelationships();

            return _relations;
        }


        // Method to save relationships to a JSON file
        private static void SaveRelationships()
        {
            string relationshipsJson = JsonSerializer.Serialize(_relations);
            File.WriteAllText("relationships.json", relationshipsJson);
        }

        // Method to load relationships from a JSON file
        private static void LoadRelationships()
        {
            try
            {
                string relationshipsJson = File.ReadAllText("relationships.json");
                _relations = JsonSerializer.Deserialize<List<RelationModel>>(relationshipsJson) ?? new List<RelationModel>();
            }
            catch (FileNotFoundException)
            {
                // Handle file not found exception, create an empty list
                _relations = new List<RelationModel>();
            }
        }

        public static void AddRelation(RelationModel relation)
        {
            relation.Id = _relations.Count + 1;
            _relations.Add(relation);

            SaveRelationships();
        }

        public static void DeleteRelation(int relationId)
        {
            _relations.RemoveAll(rel => rel.Id == relationId);
            SaveRelationships();
        }
    }
}

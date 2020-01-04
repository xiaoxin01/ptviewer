using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MongoDB.Driver;
using PtViewer.Models;
using Supperxin.Linq;

namespace PtViewer.Services
{
    public class ItemService
    {
        private readonly IMongoCollection<Item> _Items;

        public ItemService(IItemstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _Items = database.GetCollection<Item>(settings.ItemsCollectionName);
        }

        public List<Item> Get(string search, int page, string source)
        {
            var pageSize = 100;
            var skip = (page - 1) * pageSize;
            Expression<Func<Item, bool>> filters = Item => true;
            if (null != source)
            {
                filters = filters.And<Item>(Item => Item.Source == source);
            }
            if (null != search)
            {
                filters = filters.And<Item>(Item => Item.Description.Contains(search));
            }

            return
            _Items.Find(filters).Skip(skip).Limit(pageSize).ToList();
        }

        public Item GetById(string id) =>
            _Items.Find<Item>(Item => Item.Id == id).FirstOrDefault();

        public Item Create(Item Item)
        {
            _Items.InsertOne(Item);
            return Item;
        }

        public void Update(string id, Item ItemIn) =>
            _Items.ReplaceOne(Item => Item.Id == id, ItemIn);

        public void Remove(Item ItemIn) =>
            _Items.DeleteOne(Item => Item.Id == ItemIn.Id);

        public void Remove(string id) =>
            _Items.DeleteOne(Item => Item.Id == id);
    }
}

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
        private readonly IMongoCollection<Item> _items;
        private readonly IMongoCollection<Hot> _hots;

        public ItemService(IItemstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _items = database.GetCollection<Item>(settings.ItemsCollectionName);
            _hots = database.GetCollection<Hot>(settings.HotsCollectionName);
        }

        public List<Item> GetItem(string search, int page, string source)
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
            _items.Find(filters).SortByDescending(item => item.Created).Skip(skip).Limit(pageSize).ToList();
        }

        public List<Item> GetFavorateItem(string search, int page, string source)
        {
            var pageSize = 100;
            var skip = (page - 1) * pageSize;
            Expression<Func<Item, bool>> filters = Item => Item.Favorators != null;
            if (null != source)
            {
                filters = filters.And<Item>(Item => Item.Source == source);
            }
            if (null != search)
            {
                filters = filters.And<Item>(Item => Item.Description.Contains(search));
            }

            return
            _items.Find(filters).SortByDescending(item => item.Created).Skip(skip).Limit(pageSize).ToList();
        }

        public List<Hot> GetHot(string search, int page)
        {
            var pageSize = 10;
            var skip = (page - 1) * pageSize;
            Expression<Func<Hot, bool>> filters = Hot => true;
            if (null != search)
            {
                filters = filters.And<Hot>(Hot => Hot.Id.Contains(search));
            }

            return
            _hots.Find(filters).SortByDescending(item => item.CreatedUnix).Skip(skip).Limit(pageSize).ToList();
        }

        public Item GetItemById(string id) =>
            _items.Find<Item>(Item => Item.Id == id).FirstOrDefault();

        public void Update(string id, Item ItemIn) =>
            _items.ReplaceOne(Item => Item.Id == id, ItemIn);

        // public Item Create(Item Item)
        // {
        //     _items.InsertOne(Item);
        //     return Item;
        // }

        // public void Remove(Item ItemIn) =>
        //     _items.DeleteOne(Item => Item.Id == ItemIn.Id);

        // public void Remove(string id) =>
        //     _items.DeleteOne(Item => Item.Id == id);
    }
}

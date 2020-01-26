using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Driver;
using PtViewer.Models;
using Supperxin.Linq;

namespace PtViewer.Services
{
    public class ItemService
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Item> _items;
        private readonly IMongoCollection<Hot> _hots;
        private readonly IMongoCollection<User> _users;
        private readonly IMongoCollection<Subscribe> _subscribe;

        public ItemService(IItemstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);

            _items = _database.GetCollection<Item>(settings.ItemsCollectionName);
            _hots = _database.GetCollection<Hot>(settings.HotsCollectionName);
            _users = _database.GetCollection<User>("users");
            _subscribe = _database.GetCollection<Subscribe>("subscribes");
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


        public User GetUserByUserName(string username) =>
            _users.Find<User>(user => user.UserName == username).FirstOrDefault();

        public User CreateUser(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public Subscribe[] GetSubscribes(int count)
        {
            count = count > 100 ? 100 : count;
            // var demoUser = _users.Find(u => true).FirstOrDefault();
            // if (null == demoUser)
            //     return null;

            // return demoUser.Subscribes.Take(count).ToArray();

            return _subscribe.Find(s => true).SortByDescending(s => s.Created).Limit(count).ToList().ToArray();
        }

        public string[] Subscribe(string sub)
        {
            var update = Builders<User>.Update.AddToSet("subscribes", sub);
            var demoUser = _users.FindOneAndUpdate<User>(u => true, update);
            if (null == demoUser)
                return null;

            var subscribe = _subscribe.UpdateOne<Subscribe>(s => s.Tag == sub,
                Builders<Subscribe>.Update.SetOnInsert("_id", sub).AddToSet("subscribers", demoUser.Id),
                new UpdateOptions() { IsUpsert = true });

            return demoUser.Subscribes;
        }

        public void RemoveSubscribe(string id)
        {
            var update = Builders<User>.Update.Pull("subscribes", id);
            var demoUser = _users.FindOneAndUpdate<User>(u => true, update);
            if (null == demoUser)
                return;

            _subscribe.DeleteOne(item => item.Tag == id);
        }

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

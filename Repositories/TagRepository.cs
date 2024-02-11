using NotasApi.Data;
using NotasApi.Models;
using NotasApi.Repositories.IRepositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NotasApi.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _db;
        public TagRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CreateTag(Tag tag)
        {
            _db.Tags.Add(tag);
            return Save();
        }

        public bool DeleteTag(Tag tag)
        {
            _db.Tags.Remove(tag);
            return Save();
        }

        public bool ExistTag(string name)
        {
            bool var = _db.Tags.Any(t => t.Name.ToLower().Trim() == name.ToLower().Trim());
            return var;
        }

        public bool ExistTag(int id)
        {
            var tag = _db.Tags.Find(id);
            return tag != null ? true : false;
        }

        public Tag GetTagById(int id)
        {
            return _db.Tags.FirstOrDefault(n => n.Id == id);
        }

        public List<Tag> GetTags()
        {
            return _db.Tags.OrderBy(n => n.Name).ToList();
        }

        public bool Save()
        {
            return _db.SaveChanges() >= 0 ? true : false;
        }

        public bool UpdateTag(Tag tag)
        {
            _db.Tags.Update(tag);
            return Save();
        }
    }
}

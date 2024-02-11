using NotasApi.Models;

namespace NotasApi.Repositories.IRepositories
{
    public interface ITagRepository
    {
        bool CreateTag(Tag tag);
        List<Tag> GetTags();
        Tag GetTagById(int id);
        bool UpdateTag(Tag tag);
        bool DeleteTag(Tag tag);
        bool Save();
        bool ExistTag(string name);
        bool ExistTag(int id);
    }
}

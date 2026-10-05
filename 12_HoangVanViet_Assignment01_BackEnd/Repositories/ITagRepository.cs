using System.Collections.Generic;
using _12_HoangVanViet_Assignment01_BackEnd.Models;

namespace _12_HoangVanViet_Assignment01_BackEnd.Repositories
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetTags();
        Tag? GetTagById(int id);
        void AddTag(Tag tag);
        void UpdateTag(Tag tag);
        void DeleteTag(Tag tag);
    }
}

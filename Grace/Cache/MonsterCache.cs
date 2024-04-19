using Grace.Model;
using Grace.Model.Repository;

namespace Grace.Cache;

public static class MonsterCache
{
    public static List<Monster> Cache = [];

    public static async Task Init()
    {
        Cache = await MonsterRepository.GetAll();
    }
}

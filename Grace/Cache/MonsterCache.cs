using Grace.Model;
using Grace.Model.Repository;

namespace Grace.Cache;

public class MonsterCache
{
    private readonly MonsterRepository _monsterRepository;
    public static List<Monster> Cache = [];

    public MonsterCache(MonsterRepository monsterRepository)
    {
        _monsterRepository = monsterRepository;
    }

    public async Task Init()
    {
        Cache = await _monsterRepository.GetAll();
    }
}

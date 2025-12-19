namespace Tests.ArrayAndString._5_Conclusion;

/// <summary>
/// Вставка, удаление, рандомное получение за O(1) https://leetcode.com/explore/learn/card/hash-table/187/conclusion-hash-table/1141/
/// </summary>
public class RandomizedSetTests
{
    [Fact]
    public void Test()
    {
        RandomizedSet randomizedSet = new RandomizedSet();
        Assert.True(randomizedSet.Insert(1)); // Inserts 1 to the set. Returns true as 1 was inserted successfully.
        Assert.False(randomizedSet.Remove(2)); // Returns false as 2 does not exist in the set.
        Assert.True(randomizedSet.Insert(2)); // Inserts 2 to the set, returns true. Set now contains [1,2].
        Assert.Contains(randomizedSet.GetRandom(), [1,2]); // getRandom() should return either 1 or 2 randomly.
        Assert.True(randomizedSet.Remove(1)); // Removes 1 from the set, returns true. Set now contains [2].
        Assert.False(randomizedSet.Insert(2)); // 2 was already in the set, so return false.
        Assert.Equal(2, randomizedSet.GetRandom()); // Since 2 is the only number in the set, getRandom() will always return 2.
    }
    
    [Fact]
    public void Test1()
    {
        RandomizedSet randomizedSet = new RandomizedSet();
        Assert.True(randomizedSet.Insert(1)); // Inserts 1 to the set. Returns true as 1 was inserted successfully.
        Assert.True(randomizedSet.Insert(10));
        Assert.True(randomizedSet.Insert(20));
        Assert.True(randomizedSet.Insert(30));
        Assert.Contains(randomizedSet.GetRandom(), [1,10,20,30]);
        Assert.Contains(randomizedSet.GetRandom(), [1,10,20,30]);
        Assert.Contains(randomizedSet.GetRandom(), [1,10,20,30]);
        Assert.Contains(randomizedSet.GetRandom(), [1,10,20,30]);
    }
    
    [Fact]
    public void Test2()
    {
//         ["insert","getRandom","getRandom","remove","getRandom","remove","insert","getRandom","insert","getRandom","getRandom","insert","remove","getRandom","insert","insert","getRandom","insert","remove","insert","getRandom","insert","insert","insert","insert","remove","getRandom","getRandom","insert","insert","getRandom","getRandom","insert","remove","insert","insert","remove","remove","getRandom","insert","insert","insert","remove","getRandom","remove","insert","getRandom","insert","insert","remove","remove","getRandom","insert","getRandom","remove","insert","getRandom","getRandom","insert","insert","insert","insert","remove","remove","insert","insert","getRandom","getRandom","insert","insert","insert","remove","remove","remove","remove","insert","remove","remove","getRandom","insert","getRandom","insert","getRandom","getRandom","insert","remove","getRandom","insert","remove","remove","getRandom","getRandom","getRandom","insert","getRandom","insert","insert","insert","getRandom","getRandom","insert","remove","remove","insert","getRandom","insert","getRandom","remove","getRandom","insert","insert","insert","insert","remove","insert","getRandom","getRandom","getRandom","getRandom","insert","insert","getRandom","getRandom","remove","remove","remove","getRandom","getRandom","insert","getRandom","insert","remove","insert","getRandom","insert","insert","insert","getRandom","insert","getRandom","getRandom","remove","insert","getRandom","insert","remove","remove","remove","remove","remove","insert","remove","remove","remove","getRandom","insert","insert","getRandom","insert","getRandom","remove","remove","insert","getRandom","remove","getRandom","insert","insert","remove","remove","remove","remove","remove","remove","remove","getRandom","getRandom","remove","remove","getRandom","remove","insert","remove","remove","getRandom","insert","insert","remove","insert","remove","remove","insert","remove","insert","remove","getRandom","insert","remove","remove","insert","insert","insert","insert","insert","insert","insert","getRandom","remove","getRandom","insert","getRandom","remove","insert","insert","remove","remove","getRandom","remove","remove","getRandom","getRandom","insert","insert","getRandom","getRandom","insert","getRandom","insert","remove","getRandom","insert","insert","remove","insert","insert","getRandom","remove","insert","getRandom","getRandom","getRandom","getRandom","getRandom","insert","remove","getRandom","insert","getRandom","insert","getRandom","insert","remove","insert","insert","insert","insert","remove","insert","insert","getRandom","insert","getRandom","getRandom","remove","insert","getRandom","getRandom","getRandom","insert","insert","getRandom","getRandom","insert","insert","getRandom","getRandom","remove","getRandom","insert","insert","remove","getRandom","remove","getRandom","remove","getRandom","insert","getRandom","insert","getRandom","remove","remove","getRandom","remove","insert","getRandom","remove","insert","remove","getRandom","getRandom","insert"]
// [[144],[],[],[-121],[],[144],[154],[],[-13],[],[],[16],[16],[],[-78],[44],[],[57],[154],[-25],[],[142],[142],[-84],[-84],[-78],[],[],[-115],[110],[],[],[26],[-13],[-122],[-14],[26],[-115],[],[-4],[-102],[-35],[44],[],[-84],[153],[],[-28],[-69],[-122],[-4],[],[138],[],[-102],[76],[],[],[133],[115],[31],[-59],[138],[-59],[147],[109],[],[],[84],[-35],[-113],[110],[147],[-25],[109],[66],[133],[84],[],[-71],[],[-19],[],[],[-138],[-138],[],[80],[-71],[31],[],[],[],[-31],[],[104],[104],[142],[],[],[55],[-35],[-69],[-92],[],[-91],[],[55],[],[-59],[104],[126],[14],[-91],[60],[],[],[],[],[135],[57],[],[],[60],[60],[-92],[],[],[-127],[],[-113],[-14],[-77],[],[79],[-20],[25],[],[100],[],[],[126],[-93],[],[128],[-59],[14],[57],[80],[128],[-60],[-60],[-28],[-19],[],[-131],[86],[],[-69],[],[-77],[-77],[11],[],[-31],[],[90],[-20],[76],[-20],[-20],[-93],[153],[25],[115],[],[],[-127],[104],[],[86],[-95],[-131],[-131],[],[47],[112],[90],[-105],[-69],[-69],[28],[-95],[67],[142],[],[118],[-105],[118],[149],[-113],[-8],[150],[150],[0],[0],[],[11],[],[35],[],[0],[76],[128],[-113],[-113],[],[66],[28],[],[],[111],[111],[],[],[50],[],[-76],[112],[],[46],[157],[150],[-36],[-123],[],[149],[134],[],[],[],[],[],[48],[128],[],[-135],[],[-133],[],[-127],[-36],[97],[97],[38],[38],[-127],[150],[75],[],[-75],[],[],[111],[63],[],[],[],[-107],[-107],[],[],[-42],[127],[],[],[-133],[],[62],[106],[135],[],[79],[],[35],[],[-32],[],[-47],[],[97],[-47],[],[-32],[-31],[],[75],[-118],[-107],[],[],[152]]
// [true,-121,144,true,144,true,true,154,true,154,-13,true,true,154,true,true,44,true,true,true,-25,true,false,true,false,true,-84,142,true,true,-84,-115,true,true,true,true,true,true,-122,true,true,true,true,-122,true,true,-102,true,true,true,true,-35,true,138,true,true,110,110,true,true,true,true,true,true,true,true,153,147,true,false,true,true,true,true,true,true,true,true,-35,true,-113,true,57,-14,true,true,153,true,true,true,66,-69,57,true,-69,true,false,false,-69,104,true,true,true,true,-92,true,115,true,115,true,false,true,true,true,true,153,-31,-59,14,true,false,126,104,true,false,true,-19,14,true,80,false,true,true,-113,true,true,true,57,true,-19,142,true,true,14,true,true,true,true,true,true,true,true,true,true,66,true,true,-31,true,76,true,false,true,76,true,-69,true,false,true,true,false,true,true,true,true,66,135,true,true,-69,true,true,true,false,-69,true,true,true,true,true,false,true,true,true,true,11,true,true,true,true,false,true,true,false,true,false,66,true,135,true,47,true,true,true,true,false,79,true,true,150,35,true,false,47,100,true,149,true,true,149,true,true,true,true,true,50,true,true,-76,128,100,67,-36,true,true,-8,true,50,true,111,true,true,true,false,true,false,true,true,true,79,true,47,135,true,true,38,-123,46,true,false,100,-123,true,true,75,67,true,76,true,true,true,100,true,106,true,-75,true,76,true,48,true,true,-107,true,true,134,true,true,true,-123,62,true]        
        RandomizedSet randomizedSet = new RandomizedSet();
        Assert.True(randomizedSet.Insert(-20));
        Assert.True(randomizedSet.Insert(-47));
        Assert.True(randomizedSet.Remove(-20));
        Assert.True(randomizedSet.Remove(-47));
        Assert.True(randomizedSet.Insert(-119));
        Assert.False(randomizedSet.Insert(-119));
        Assert.True(randomizedSet.Remove(-119));
        Assert.True(randomizedSet.Insert(-99));
        Assert.True(randomizedSet.Remove(-99));
        Assert.True(randomizedSet.Insert(-121));
        Assert.Equal(-121, randomizedSet.GetRandom());
        Assert.Equal(-121, randomizedSet.GetRandom());
        Assert.Equal(-121, randomizedSet.GetRandom());
    }
    
    public class RandomizedSet
    {
        private Dictionary<int, int> map;
        private List<int> list;
        private Random rand;

        public RandomizedSet()
        {
            map = new Dictionary<int, int>();
            list = new List<int>();
            rand = new Random();
        }

        public bool Insert(int val)
        {
            if (map.ContainsKey(val))
                return false;

            map[val] = list.Count;
            list.Add(val);
            return true;
        }

        public bool Remove(int val)
        {
            if (!map.ContainsKey(val))
                return false;

            int index = map[val];
            int lastElement = list[list.Count - 1];

            // Перемещаем последний элемент на место удаляемого
            list[index] = lastElement;
            map[lastElement] = index;

            // Удаляем последний элемент
            list.RemoveAt(list.Count - 1);
            map.Remove(val);

            return true;
        }

        public int GetRandom()
        {
            int randomIndex = rand.Next(list.Count);
            return list[randomIndex];
        }
    }
    
    // моя реализация, почти правильно
    public class RandomizedSet1
    {
        private HashSet<int> _hashSet = new HashSet<int>();
        private readonly Random _random = new Random();
        private readonly List<int> _list = new List<int>();

        public RandomizedSet1()
        {
            
        }
    
        public bool Insert(int val) {
            var res = _hashSet.Add(val);
            if (res)
            {
                _list.Add(val);
            }

            return res;
        }
    
        public bool Remove(int val)
        {
            _list.Remove(val); // удаление может быть долгим
            return _hashSet.Remove(val);
        }
    
        public int GetRandom()
        {
            var next = _random.Next(_list.Count);
            return _list[next];
        }
    }
}
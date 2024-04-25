using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using static Text.Program;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;

namespace Text
{
    public class Program
    {
        public class Player
        {
            public int Level {  get; set; }
            public string Jop {  get; set; }
            public int Atk { get; set; }
            public int Df { get; set; }
            public int Health { get; set; }
            public int Gold { get; set; }
            public Bag Bag { get; set; }
            public int EquipAtk { get; set; }
            public int EqUupDf { get; set; }

            private static Player instance;
            public static Player Instance
            {
                get 
                { 
                    if(instance == null)
                        instance = new Player();
                    return instance;
                }
            }

            private Player()
            {
                Level = 01;
                Jop = "전사";
                Atk = 10;
                Df = 5;
                Health = 100;
                Gold = 8500;
                Bag = new Bag();
                EquipAtk = 0;
                EqUupDf = 0;
            }

            public void Equip(int a) // 장비장착 칸에서 장비를 선택 하면 알맞게 장착 되게 했습니다.
            {
                if (Bag.items[a].ItemType == "공격력")
                {
                    Atk += Bag.items[a].Value;
                    EquipAtk += Bag.items[a].Value;
                }
                else if (Bag.items[a].ItemType == "방어력")
                {
                    Df += Bag.items[a].Value;
                    EqUupDf += Bag.items[a].Value;
                }
            }
        }


        public class Bag // 상점에서 구매 하면 추가 되게 했습니다.
        {
            public List<Item> items = new List<Item>();
            public void AddItem(Item item)
            {
                items.Add(item);
            }

            public void bagItemList()
            {
                foreach (Item item in items)
                {
                    item.explan2();
                }
            }
        }


        public class Item
        {
            public string ItemName { get; set; }
            public string ItemType { get; set; }
            public int Value { get; set; }
            public string Price { get; set; }
            public string Explanation { get; set; }
            public bool Install { get; set; }
            int count= 0;
            public string equip {  get; set; }

            public Item(string itemName, string itemType, int value, string price, string explanation, bool install)
            {
                ItemName = itemName;
                ItemType = itemType;
                Value = value;
                Price = price;
                Explanation = explanation;
                Install = install;
                equip = itemName;
            }
            public void explan()  // 처음에 썻던 함수들인데 중요 하지 않습니다.. 나중에 작업 하면서 함수들 썻던 거 지운 게 많습니다.
            {
                Console.WriteLine("- " + count + " " + ItemName + " " + ItemType + "+" + Value + " " + Explanation + " " + Price);
            }
            public void explan2()
            {
                Console.WriteLine(equip + "   | " + ItemType + "+" + Value + "     | " + Explanation  );
            }

            public void Equip() // 장비장착 칸에서 선택 되면 이 함수가 실행 되게 했습니다. 장비창에 표시 되는 것은 Name이 아닌 equip 입니다.
            {
                equip = $" [E]{ItemName}";
            }

            public void UnEquip()
            {
                equip = ItemName;
            }
        }


        public class ItemList
        {
            public List<Item> items = new List<Item>();
            public void addItem(string itemName, string itemType, int value, string price, string explanation, bool install)
            {
                Item newItem = new Item(itemName, itemType, value, price, explanation, install);
                items.Add(newItem);
            }

            public void itemList()
            {
                addItem("수련자 갑옷", "방어력", 5, "1000" , "수련에 도움을 주는 갑옷입니다.", false);
                addItem("무쇠갑옷", "방어력", 9, "2000", "무쇠로 만들어져 튼튼한 갑옷입니다.", false);
                addItem("스파르타의 갑옷", "방어력", 15, "3500", "스파르타의 전사들이 사용 했다는 전설의 갑옷입니다.", false);
                addItem("낡은 검","공격력",2,"600","쉽게 볼 수 있는 낡은 검 입니다.", false);
                addItem("청룡 도끼", "공격력", 5, "1500", "어디선가 사용 됐던 것 같은 도끼 입니다.", false );
                addItem("스파르타의 창", "공격력", 7, "3000", "스파르타의 전사들이 사용 했다는 전설의 창입니다.", false);
            }


            public void storeItemList()
            {
                Console.WriteLine();
            }
        }


            public class Choice // "매 화면에서 선택 하라"는 기능을 클래스로 만들었습니다.
            {
            public int select(int num)  // 오버로딩입니다. 변수 하나 입력해서 번호 선택이 한 칸인 경우에 실행 하게 합니다.
            {
                Console.WriteLine("원하시는 행동을 입력 해주세요.");
                Console.Write(">> ");
                int a;
                bool roop = true;

                do
                {
                    bool t = int.TryParse(Console.ReadLine(), out a);
                    if (t)
                    {
                        if (num == a)
                        {
                            roop = false;
                        }
                        else
                        {
                            Console.WriteLine("올바른 값을 입력 해주세요,");
                            Console.Write(">> ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력 해주세요,");
                        Console.Write(">> ");
                    }
                }
                while (roop);
                return a;
            }

            public int select(int min, int max) // 오버로딩입니다. 최소값과 최댓값을 넣어서 ex) 1~3의 값만 반환 합니다.
            {
                Console.WriteLine("원하시는 행동을 입력 해주세요.");
                Console.Write(">> ");
                int a;
                bool roop = true;
                do
                {
                    bool t = int.TryParse(Console.ReadLine(), out a);
                    if (t)
                    {
                        if (min <= a && max >= a)
                        {
                            roop = false;
                        }
                        else
                        {
                            Console.WriteLine("올바른 값을 입력 해주세요,");
                            Console.Write(">> ");
                        }
                    }
                    else
                    {
                        Console.WriteLine("숫자를 입력 해주세요,");
                        Console.Write(">> ");
                    }
                }
                while (roop);
                return a;
            }
        }


        public abstract class Screen : Choice // Choice 클래스를 상속 받아 추상 클래스로 만들고 번호선택만이 아닌 다른 것도 호출 되게 했습니다.
        {
            public abstract int screenSelect();
        }


        public class Home : Screen  // Screen을 호출 받아 추상클래스를 사용 할 수 있게 했습니다. 
        {
            public override int screenSelect() // 오버라이딩으로 추상 클래스를 사용 한 겁니다. 콘솔문구가 먼저 나오고 후에 번호선택 기능이 나오게 한겁니다.
            {
                ment();
                return select(1, 3);
            }
            public void ment()  
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영 합니다.");
                Console.WriteLine("이 곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리");
                Console.WriteLine("3. 상점");
                Console.WriteLine();
            }
        }

        public class inventory : Screen
        {
            List<Item> playerItems = Player.Instance.Bag.items; 
            public override int screenSelect() 
            {
                inventoryOpne();
                return 0;
            }

            public void inventoryOpne()
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.");
                Console.WriteLine();
                Console.WriteLine("아이템 목록\n");

                foreach (Item item in playerItems)   // 장비목록 출력
                {
                    item.explan2();
                }

                Console.WriteLine();
                Console.WriteLine("1. 장착 관리");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();

                int a = select(0, 1); // 선택 기능 함수를 여기서도 활용 했습니다. 
                if(a >0)
                {
                    bool roop = true;
                    while (roop)
                    {
                        Console.Clear();
                        Console.WriteLine("인벤토리");
                        Console.WriteLine("보유 중인 아이템을 관리 할 수 있습니다.");
                        Console.WriteLine();
                        Console.WriteLine("아이템 목록\n");
                        for (int i = 0; i < playerItems.Count; i++)
                        {
                            Console.WriteLine($"-{i + 1} {playerItems[i].equip}  |  {playerItems[i].ItemType} {playerItems[i].Value}  |  {playerItems[i].Explanation}");
                        }

                        Console.WriteLine();
                        Console.WriteLine("0. 나가기");
                        Console.WriteLine();
                        int b = select(0, playerItems.Count);
                        if (b > 0)
                        {
                            playerItems[b-1].Equip();
                            Player.Instance.Equip(b-1);
                        }
                        else
                            {
                                roop = false;
                            }
                        }
                }
            }
        }
        public class state : Screen
        {
            public override int screenSelect()
            {
                stateA();
                return select(0);
            }
            
            public void stateA()
            {
                string equipAtk = "";
                string equipDf = "";

                if (Player.Instance.EquipAtk > 0)       // 장착을 했으면 표시가 되게 하고 장착이 없으면 "" 표시 되게 했습니다. 기준은 공격력이 0이냐 아니냐
                {
                    int a = Player.Instance.EquipAtk;
                    equipAtk = "(+"+a.ToString()+")";
                }
                else if(Player.Instance.EquipAtk == 0)
                {
                    equipAtk = "";
                }
                if(Player.Instance.EqUupDf > 0)
                {
                    int a = Player.Instance.EqUupDf;
                    equipDf = "(+"+a.ToString()+")";
                }
                else if (Player.Instance.EquipAtk == 0)
                {
                    equipDf = "";
                }

                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시 됩니다.");
                Console.WriteLine();
                Console.Write("LV.");
                Console.WriteLine(Player.Instance.Level);
                Console.Write("Chad ");
                Console.WriteLine("("+Player.Instance.Jop+")");
                Console.Write("공격력 : ");
                Console.WriteLine($"{Player.Instance.Atk}{equipAtk}"); 
                Console.Write("방어력 : ");
                Console.WriteLine($"{Player.Instance.Df}{equipDf}");
                Console.Write("체력 : ");
                Console.WriteLine(Player.Instance.Health);
                Console.Write("Gold : ");
                Console.WriteLine(Player.Instance.Gold);
                Console.WriteLine();
                Console.WriteLine("0. 나가기");
                Console.WriteLine();
            }
        }


        public class Store : Screen
        {
            ItemList itemList = new ItemList();
            int a = 0;

            public override int screenSelect()
            {
                storeScreen();
                return 0;
            }

            void storeScreen()
            {
                if (a<1)
                {
                    a++;
                    itemList.itemList();
                }

                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                Console.WriteLine("[보유 골드]");
                Console.WriteLine(Player.Instance.Gold);
                Console.WriteLine();
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < itemList.items.Count; i++)
                {
                    Console.WriteLine($"- {itemList.items[i].equip}   | {itemList.items[i].ItemType} +{itemList.items[i].Value}   | {itemList.items[i].Explanation}   | {itemList.items[i].Price}G");
                }
                Console.WriteLine();
                Console.WriteLine("1. 아이템 구매");
                Console.WriteLine("0. 나가기");
                Console.WriteLine();

                int o = select(0, 1);
                if (o == 1)
                {
                    Console.Clear();
                    Console.WriteLine("상점 - 아이템 구매");
                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                    Console.WriteLine("[보유 골드]");
                    Console.WriteLine(Player.Instance.Gold);
                    Console.WriteLine();
                    Console.WriteLine("[아이템 목록]");
                    for (int i = 0; i < itemList.items.Count; i++)
                    {
                        Console.WriteLine($"-{i + 1} {itemList.items[i].equip}   | {itemList.items[i].ItemType} +{itemList.items[i].Value}   | {itemList.items[i].Explanation}   | {itemList.items[i].Price} G");
                    }
                    Console.WriteLine();
                    Console.WriteLine("0. 나가기");
                    Console.WriteLine();
                    bool roop = true;
                    while (roop)
                    {
                        int storeSelect = select(0, itemList.items.Count);
                        if (0 < storeSelect)
                        {
                            Item selectedItem = itemList.items[storeSelect-1];
                            int a;
                            bool t;
                            if (t = int.TryParse(selectedItem.Price, out a))
                            {
                                if (Player.Instance.Gold >= a)
                                {
                                    Player.Instance.Gold -= a;
                                    Player.Instance.Bag.AddItem(selectedItem);
                                    selectedItem.Price = "구매 완료";
                                    Console.Clear();
                                    Console.WriteLine("상점 - 아이템 구매");
                                    Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");
                                    Console.WriteLine("[보유 골드]");
                                    Console.WriteLine(Player.Instance.Gold);
                                    Console.WriteLine();
                                    Console.WriteLine("[아이템 목록]");
                                    for (int i = 0; i < itemList.items.Count; i++)
                                    {
                                        Console.WriteLine($"-{i + 1} {itemList.items[i].equip}   | {itemList.items[i].ItemType} +{itemList.items[i].Value}   | {itemList.items[i].Explanation}   | {itemList.items[i].Price}");
                                    }
                                    Console.WriteLine();
                                    Console.WriteLine("0. 나가기");
                                    Console.WriteLine();
                                    Console.WriteLine("구매를 완료 했습니다.\n");
                                }
                                else
                                {
                                    Console.WriteLine("골드가 부족 합니다.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("구매 한 아이템 입니다.");
                            }
                        }
                        else
                        {
                            roop = false;
                        }
                    }
                }
            }
        }


        public static void Main(string[] args)
        {

            Home home = new Home();
            state state = new state();
            inventory inventory = new inventory();
            Store store = new Store();

            while (true)
            {
                int select = home.screenSelect();
                if (select == 1)
                {
                    state.screenSelect();
                }
                else if (select == 2)
                {
                    inventory.screenSelect();
                }
                else if (select == 3)
                {
                    store.screenSelect();
                }
            }
        }
    }
}
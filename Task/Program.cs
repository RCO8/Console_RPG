using System.Diagnostics;

namespace Task
{
    internal class Program
    {
        //캐릭터 속성
        string yourName = "Chad";
        string yourWork = "전사";
        int yourLevel = 1;
        int yourHP = 100;
        int yourAttack = 10;
        int yourDefend = 8;
        int yourGold = 1500;

        //장비착용으로 추가 속성
        int addonAttack = 0;
        int addonDefend = 0;

        struct Item
        {
            string name;    //이름
            int addAtk;     //공격력
            int addDef;     //방어력
            string info;    //설명

            public int gold = 0;   //값어치(판매용)

            public Item(string n, int a, int d, string i)
            {
                //생성자에 매개변수를 받아서 각 맞는 속성에 넣는다.
                name = n;
                addAtk = a;
                addDef = d;
                info = i;
            }

            public void ShowItem()
            {

                string addProps = "";    //추가 공격 및 방어
                if (addAtk > 0)
                    addProps = $"공격력 +{addAtk}";
                else if (addDef > 0)
                    addProps = $"방어력 +{addDef}";

                Console.Write($"{name} \t | {addProps} | {info}");
            }

            public Item Giving() { return this; }

            public int GetAttack() { return addAtk; }
            public int GetDefend() { return addDef; }
        };

        class Invent
        {
            Item item;
            bool equip = false;
            int price;

            public Invent(string n, int a, int d, string i)
            {
                item = new Item(n, a, d, i);
            }

            public Invent(Item i)
            {
                item = i;
            }

            //아이템 장비
            public void EquipItem() => equip = !equip;

            public void ShowItem()
            {
                string equipMessage = "";
                if (equip) equipMessage = "[E]";
                Console.Write( equipMessage );
                item.ShowItem();
            }

            //공격 방어 정보 가져오기
            public int GetAttack() => item.GetAttack();
            public int GetDefend() => item.GetDefend();

            //아이템 가격 감별하기
            public void SetPrice(int g)
            {
                float pricing = (g / 100f) * 85f;  //85%계산할인
                price = (int)pricing;
            }
            public int GetPrice() { return price; }
        }

        class Product
        {
            Item item;  //아이템 정보
            bool soldout = false;   //판매완료
            int gold;   //가격

            public Product(string n, int a, int d, string i, int g)
            {
                item = new Item(n, a, d, i);
                gold = g;
            }

            public void ShowProduct()
            {
                string isSold = "";
                if(soldout)
                {
                    //판매완료 표시
                    isSold = "구매완료";
                }
                else
                {
                    //가격 표시
                    isSold = $"{gold} G";
                }
                item.ShowItem();
                Console.WriteLine($"\t| {isSold}");
            }

            public int BuyProduct(int g)   //상품 구매
            {
                if (soldout)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    return g;
                }
                else
                {
                    if (gold <= g)   //지불할 g가 gold이상이면
                    {
                        Console.WriteLine("구매를 완료했습니다.");
                        soldout = true;
                        return g - gold;
                    }
                    else    //가격이 부족해 환불
                    {
                        Console.WriteLine("Gold가 부족합니다.");
                        return g;
                    }
                }
            }

            public Item GiveItem()
            {
                return item;
            }
        }

        List<Invent> invents = new List<Invent>(); //현재 갖고있는 아이템들
        List<Product> products = new List<Product>();   //현재 상점에 있는 아이템들

        static void Main(string[] args)
        {
            Program pg = new Program();


            //기본 인벤토리 설정
            pg.invents.Add(new Invent("무쇠갑옷", 0, 5, "무쇠로 만들어져 튼튼한 갑옷입니다."));
            pg.invents.Add(new Invent("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다."));
            pg.invents.Add(new Invent("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다."));
            pg.invents[0].EquipItem();
            pg.invents[1].EquipItem();
            pg.invents[0].SetPrice(2000);
            pg.invents[1].SetPrice(3000);
            pg.invents[2].SetPrice(600);

            //기본 상정 재고 설정
            pg.products.Add(new Product("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", 1000));
            pg.products.Add(new Product("무쇠값옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷입니다.", 2000));
            pg.products.Add(new Product("스파르타 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500));
            pg.products.Add(new Product("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", 600));
            pg.products.Add(new Product("청동 도끼", 5, 0, "어디선가 사용됐던거 같은 도끼입니다.", 1500));
            pg.products.Add(new Product("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", 3000));
            
            pg.Title();
        }

        void Title()
        {
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.\n");
            
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            Console.Clear();//다시 돌아가게 하거나 할때 줄을 생성하지 않고 새로 갱신하기 위해
            switch (select)
            {
                case 1: //상태 보기
                    Status();
                    break;
                case 2: //인벤토리
                    Inventory();
                    break;
                case 3: //상점
                    Shop();
                    break;
                case 4: //던전 입장
                    EnterDungeon();
                    break;
                case 5: //휴식하기
                    RestCenter();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Title();
                    break;
            }
        }

        void Status()   //상태 확인
        {
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");

            addonAttack = 0;
            addonDefend = 0;
            for(int i=0;i<invents.Count;i++)
            {
                addonAttack += invents[i].GetAttack();
                addonDefend += invents[i].GetDefend();
            }

            string printAttack = "";
            if (addonAttack > 0) printAttack = $"(+{addonAttack})";
            string printDefend = "";
            if (addonDefend > 0) printDefend = $"(+{addonDefend})";

            Console.WriteLine($"Lv. {yourLevel}");
            Console.WriteLine($"{yourName} ( {yourWork} )");
            Console.WriteLine($"공격력 : {yourAttack+addonAttack} {printAttack}");
            Console.WriteLine($"방어력 : {yourDefend+addonDefend} {printDefend}");
            Console.WriteLine($"체  력 : {yourHP}");
            Console.WriteLine($"Gold : {yourGold} G");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            if (select == 0)
                Title();
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Status();   //다른 값을 입력하면 현재 창에서 리셋
            }
        }

        void Inventory()    //인벤토리
        {
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다. \n");

            Console.WriteLine("[아이템 목록]");
            foreach (Invent i in invents)
            {
                Console.Write("- ");
                i.ShowItem();
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            if (select == 1)
            {
                EquipManage();  //장착 관리
            }
            else if (select == 0)
            {
                Title();
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Inventory();
            }
        }

        void EquipManage()  //장착 관리
        {
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다. \n");

            Console.WriteLine("[아이템 목록]");
            int tmp = 1;
            foreach (Invent i in invents)
            {
                Console.Write($"- {tmp++} ");
                i.ShowItem();
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            if (0 < select && select <= invents.Count)
            {
                //해당 아이템 장착
                --select;
                invents[select].EquipItem();
                EquipManage();
            }
            else if (select == 0)
                Inventory();
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                EquipManage();
            }
        }

        void Shop() //상점
        {
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{yourGold} G");

            Console.WriteLine("\n[아이템 목록]");
            foreach(Product p in products)
            {
                Console.Write("- ");
                p.ShowProduct();
            }

            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            switch (select)
            {
                case 1:
                    //아이템 구매
                    BuyItem();
                    break;
                case 2:
                    //아이템 판매
                    SellItem();
                    break;
                case 0:
                    Title();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    Shop();
                    break;
            }
        }

        void BuyItem()  //아이템 구매
        {
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{yourGold} G");

            Console.WriteLine("\n[아이템 목록]");
            int tmp = 1;
            foreach (Product p in products)
            {
                Console.Write($"- {tmp++} ");
                p.ShowProduct();
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            if (0 < select && select <= products.Count)
            {
                //해당 아이템 구매
                --select;
                yourGold = products[select].BuyProduct(yourGold);
                //인벤토리에 추가
                invents.Add(new Invent(products[select].GiveItem()));
                invents[invents.Count-1].SetPrice(products[select].GiveItem().gold);  //가격 추가
                BuyItem();
            }
            else if (select == 0)
                Shop();
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                BuyItem();
            }
        }

        void SellItem() //아이템 판매
        {
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{yourGold} G");

            Console.WriteLine("\n[아이템 목록]");
            int tmp = 1;
            foreach(Invent i in invents)
            {
                Console.Write($"- {tmp++} ");
                i.ShowItem();
                //판매가격 출력
                Console.WriteLine($"| {i.GetPrice()} G");
            }

            Console.WriteLine();
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            if (0 < select && select <= invents.Count)
            {
                //아이템 판매
                ++select;
                yourGold += invents[select].GetPrice();
                invents.RemoveAt(select);
                SellItem();
            }
            else if(select == 0)
                Shop();
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                SellItem();
            }
        }

        void EnterDungeon() //던전 입장
        {
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");

            Console.WriteLine();
            Console.WriteLine("1. 쉬운 던전 \t | 방어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전 \t | 방어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전 \t | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
            switch (select)
            {
                case 1: //쉬운 던전
                case 2: //일반 던전
                case 3: //어려운 던전
                    ActionDungeon(select);
                    break;
                case 0: //돌아가기
                    Title();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    EnterDungeon();
                    break;
            }
        }

        void ActionDungeon(int diff)
        {
            // 1 : 쉬움
            // 2 : 일반
            // 3 : 어려움
            int safeDefend = 0; //권장 방어력
            Random actPlay = new Random();
            int yourNum = actPlay.Next(0, 100); //던전 성공확률
            int reward = 0; //보상

            switch(diff)    //난이도에 따라 권장 방어력 및 보상 설정
            {   
                case 1:
                    safeDefend = 5;
                    reward = 1000;
                    break;
                case 2:
                    safeDefend = 11;
                    reward = 1700;
                    break;
                case 3:
                    safeDefend = 17;
                    reward = 2500;
                    break;
            }
            //체력 감소량 (현재 방어력 - 권장방어력)
            // ex) 감소량이 4이면 -4~4까지 랜덤으로
            int decHP = Math.Abs(yourDefend - safeDefend);
            int usedHP = actPlay.Next(20 - decHP, 35 - decHP);

            //권장 방어력보다 낮으면 40%확률로 실패
            if (yourDefend < safeDefend && yourNum < 40)
            {
                //실패
                
                Console.WriteLine("던전 실패....");

                Console.WriteLine("[탐험 결과]");
                Console.Write($"체력 {yourHP} ->");
                yourHP /= 2;
                Console.WriteLine(yourHP + "\n");
            }
            else
            {
                //던전 클리어

                Console.WriteLine("던전 클리어");
                Console.WriteLine("축하합니다!!!\n");
                
                Console.WriteLine("[탐험 결과]");
                Console.Write($"체력 {yourHP} -> ");
                yourHP -= usedHP;
                Console.WriteLine(yourHP);

                Console.Write($"Gold {yourGold} -> ");
                yourGold += reward;
                Console.WriteLine(yourGold + "\n");
            }

            Console.WriteLine("0. 나가기");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            while(select != 0)  //입력범위를 벗어나면
            {
                Console.WriteLine("다시 입력하세요.");
                Console.Write(">>");
                select = Convert.ToInt32(Console.ReadLine());
            }

            Console.Clear();
            EnterDungeon();
        }   //던전 클리어

        void RestCenter()   //휴식하기
        {
            Console.WriteLine("휴식하기");
            Console.WriteLine($"500 G를 내면 체력을 회복할 수 있습니다. (보유골드 : {yourGold} G)");

            Console.WriteLine();
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");

            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            int select = Convert.ToInt32(Console.ReadLine());

            if(select == 1)
            {
                Console.Clear();
                if (yourGold >= 500)
                {
                    //회복
                    yourHP = 100;
                    yourGold -= 500;
                    Console.WriteLine("휴식을 완료했습니다.");
                }
                else 
                {
                    Console.WriteLine("Gold가 부족합니다.");
                }
                RestCenter();
            }
            else if(select == 0)
            {
                Console.Clear();
                Title();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("잘못된 입력입니다.");
                RestCenter();
            }
        }
    }
}

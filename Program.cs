using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SomeCSharpReminder
{
    enum FuelType
    {
        NineTwo,
        NineFive,
        NineEight,
        Disel
    }

    interface IBensinShop
    {
        bool checkShopStatus();

        bool getClientFuel(Client c);

        void WriteShopStatus();
    }

    class Manager
    {
        private IBensinShop shop;
        private bool fuel_status = false;
        public bool FuelStatus
        {
            get => fuel_status;
        }

        public Manager(IBensinShop shop)
        {
            this.shop = shop;
        }

        public bool checkShopStatus() //weird 
        {
            return shop.checkShopStatus();
        }
    }

    class AZS : IBensinShop
    {
        private int nine_two_max = 30000;
        private int nine_five_max = 16000;
        private int nine_eight_max = 16000;
        private int disel_max = 30000;

        private int nine_two;
        private int nine_five;
        private int nine_eight;
        private int disel;

        private string name;

        public string Name
        {
            get => name;
        }

        private Manager man;

        public AZS(string name, int nine_two = 100, int nine_five = 100, int nine_eight = 100, int disel = 100)
        {
            this.nine_two = nine_two;
            this.nine_five = nine_five;
            this.nine_eight = nine_eight;
            this.disel = disel;

            this.name = name;

            man = new Manager(this);
        }

        public void changeDisel(int val)
        {
            if(disel + val < disel_max)
                disel += val;
        }
        public void changeNineFive(int val)
        {
            if(nine_five + val < nine_five_max)
                nine_five += val;
        }
        public void changeNineEight(int val)
        {
            if(nine_eight + val < nine_eight_max)
                nine_eight += val;
        }
        public void changeNineTwo(int val)
        {
            if(nine_two + val < nine_two_max)   
                nine_two += val;
        }

        public bool checkIfThisFuelValueIsOut(int fuelValue)
        {
            return fuelValue < 0;
        }

        public bool checkIfFuelSoldOut()
        {
            return checkIfThisFuelValueIsOut(nine_two) || checkIfThisFuelValueIsOut(nine_five) || checkIfThisFuelValueIsOut(nine_eight) || checkIfThisFuelValueIsOut(disel);
        }

        public bool checkIfFuelToMuch()
        {
            return nine_two < nine_two_max && nine_five < nine_five_max && nine_eight < nine_eight_max && disel < disel_max;
        }

        public bool checkShopStatus()
        {
            return checkIfFuelSoldOut();
        }

        public bool getClientFuel(Client c)
        {
            if (c.query.QueryType == FuelType.NineTwo)
            {
                //weird?
                if (checkIfThisFuelValueIsOut(nine_two - c.query.QueryAmount))
                    return false;
                changeNineTwo(-c.query.QueryAmount);
            }

            else if (c.query.QueryType == FuelType.NineFive)
            {
                if (checkIfThisFuelValueIsOut(nine_five - c.query.QueryAmount))
                    return false;
                changeNineFive(-c.query.QueryAmount);
            }

            else if (c.query.QueryType == FuelType.NineEight)
            {
                if (checkIfThisFuelValueIsOut(nine_eight - c.query.QueryAmount))
                    return false;

                changeNineEight(-c.query.QueryAmount);
            }

            else if (c.query.QueryType == FuelType.Disel)
            {
                if (checkIfThisFuelValueIsOut(disel - c.query.QueryAmount))
                    return false;

                changeDisel(-c.query.QueryAmount);
            }

            else
            {
                return false;
            }

            return true;
        }

        public void WriteShopStatus()
        {
            void Write(object var)
            {
                Console.Write(var);
                Console.Write(" ");
            }

            Write(name);
            Write("\n");
            
            Write("nine two is -> ");
            Write(nine_two);
            
            Write("\n");

            Write("nine five is -> ");
            Write(nine_five);

            Write("\n");

            Write("nine eight is -> ");
            Write(nine_eight);

            Write("\n");

            Write("disel is -> ");
            Write(disel);

            Write("\n\n");
        }
    }

    class AZZS : IBensinShop
    {
        private int nine_two_max = 16000;
        private int nine_five_max = 15000;
        
        private int nine_two;
        private int nine_five;

        private string name;

        public string Name
        {
            get => name;
        }

        private Manager man;
        
        public AZZS(string name, int nine_two, int nine_five)
        {
            this.nine_five = nine_five;
            this.nine_two = nine_two;

            this.name = name;

            man = new Manager(this);
        }

        public void changeNineFive(int val)
        {
            nine_five += val;
        }

        public void changeNineTwo(int val)
        {
            nine_two += val;
        }

        public bool checkIfThisFuelValueIsOut(int fuelValue)
        {
            return fuelValue < 0;
        }

        public bool checkIfFuelSoldOut()
        {
            return checkIfThisFuelValueIsOut(nine_two) || checkIfThisFuelValueIsOut(nine_five);
        }

        public bool checkIfFuelToMuch()
        {
            return nine_two < nine_two_max && nine_five < nine_five_max;
        }

        public bool checkShopStatus()
        {
            return checkIfFuelSoldOut();
        }

        public bool getClientFuel(Client c)
        {
            if (c.query.QueryType == FuelType.NineTwo)
            {
                //weird?
                if (checkIfThisFuelValueIsOut(nine_two - c.query.QueryAmount))
                    return false;
                changeNineTwo(-c.query.QueryAmount);
            }

            else if (c.query.QueryType == FuelType.NineFive)
            {
                if (checkIfThisFuelValueIsOut(nine_five - c.query.QueryAmount))
                    return false;
                changeNineFive(-c.query.QueryAmount);
            }

            else
            {
                return false;
            }

            return true;
        }

        public void WriteShopStatus()
        {
            void Write(object var)
            {
                Console.Write(var);
                Console.Write(" ");
            }

            Write(name);
            Write("\n");
            Write("nine two is -> ");
            Write(nine_two);

            Write("\n");

            Write("nine five is -> ");
            Write(nine_five);

            Write("\n\n");
        }
    }

    class BensinTanker
    {
        private List<int> sections;
        private int section_capasity;
        private FuelType tanker_fuel;

        public FuelType TankerFuel
        {
            get => tanker_fuel;
        }

        public BensinTanker(FuelType tanker_fuel, int sections_amount, int section_capasity = 6000)
        {
            this.section_capasity = section_capasity;
            sections = new List<int>();
            for(int i = 0; i < sections_amount; ++i)
            {
                sections.Add(6000);
            }

            this.tanker_fuel = tanker_fuel;
        }

        public void RefuelBensinShop(IBensinShop shop, int sections_count_to_use)
        {
            if(tanker_fuel == FuelType.Disel)
            {
                if(shop is AZS)
                {
                    AZS shop_azs = (AZS)shop;

                    for (int i = 0; i < sections.Count(); ++i)
                    {
                        Console.WriteLine("-----");
                        shop_azs.changeDisel(section_capasity);
                        sections[i] -= section_capasity;
                    }
                }
            }
        }
    }

    class Client
    {
        public ClientQuery query;
        public Client(ClientQuery q)
        {
            query = q;
        }
    }

    class ClientQuery
    {
        private FuelType query_type;
        private int query_amount;

        public FuelType QueryType
        {
            get => query_type;
        }

        public int QueryAmount
        {
            get => query_amount;
        }

        public ClientQuery(FuelType query_type, int query_amount)
        {
            this.query_type = query_type;
            this.query_amount = query_amount;
        }
    }

    class LifeTime
    {
        AZS tanker1 = new AZS("Станция Коммунистическая", 100, 100);
        AZS tanker2 = new AZS("Станция Техно-Коммунистическая", 100, 100);
        AZS tanker3 = new AZS("Станция Трансгуманическая", 100, 100);

        BensinTanker btank1 = new BensinTanker(FuelType.Disel, 3);
        BensinTanker btank2 = new BensinTanker(FuelType.NineFive, 3);
        BensinTanker btank3 = new BensinTanker(FuelType.NineTwo, 3);
        BensinTanker btank4 = new BensinTanker(FuelType.NineEight, 3);

        private FuelType getFuelType(int type)
        {
            if (type == 1)
                return FuelType.Disel;
            if (type == 2)
                return FuelType.NineFive;
            if (type == 3)
                return FuelType.NineEight;

            return FuelType.NineTwo;
        }

        private BensinTanker getAppropriateTanker(FuelType type)
        {
            if (type == FuelType.Disel)
                return btank1;
            if (type == FuelType.NineFive)
                return btank2;
            if (type == FuelType.NineTwo)
                return btank3;
            return btank4;
        }

        private IBensinShop getAppropriateShop(int num)
        {
            if (num == 1)
                return tanker1;
            
            if (num == 2)
                return tanker2;
            
            return tanker3;
        }

        public void processClients()
        {
            var rnd = new Random();
            System.Threading.Thread.Sleep(rnd.Next(1000, 5000));
            var volume = rnd.Next(5, 40) + 10;
            var type = rnd.Next(1,4);
            var fuelType = getFuelType(type);

            ClientQuery cq = new ClientQuery(fuelType, volume);
            Client c = new Client(cq);

            var tanker = getAppropriateShop(rnd.Next(0, 4));

            var stat = tanker.getClientFuel(c);

            Console.Write("Клиент закупает ");
            Console.Write(fuelType.ToString());
            Console.Write(" размером ");
            Console.Write(volume);
            Console.Write(" ");

            tanker.WriteShopStatus();
            
            if (!stat)
                getAppropriateTanker(fuelType).RefuelBensinShop(tanker, 3);
        }


        public void LifeLoop()
        { 
            while (true)
            {
                //var stat = tanker1.getClientFuel(c1);

                //tanker1.WriteShopStatus();
                //if (!stat)
                //    btank1.RefuelBensinShop(tanker1, 3);

                processClients();

                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LifeTime life = new LifeTime();
            life.LifeLoop();
        }
    }
}

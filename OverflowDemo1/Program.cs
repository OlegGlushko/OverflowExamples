using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OverflowDemo1
{
    public class Program
    {
        public static List<AuctionItem> AuctionItems = new List<AuctionItem>();

        public static void Main(string[] args)
        {
            var buyer = new Person();
            buyer.Money = 100; // 10000 gold

            Console.WriteLine($"Person has {buyer.Money} gold");

            //uint.MaxValue = 0 .... 4,294,967,295

            AddBuyOrder(
                buyer: buyer,
                itemId: 1,
                amount: 11,
                price: uint.MaxValue / 10);

            Console.WriteLine($"Auction created.");

            // Print current auction prices
            foreach (var auctionItem in AuctionItems)
            {
                Console.WriteLine($"Auction with id: {auctionItem.ItemId}, amount: {auctionItem.Amount} price: {auctionItem.Price}");
            }

            // Move money to stash
            buyer.Stash = new Stash();
            buyer.Stash.StashMoney = buyer.Money;
            buyer.Money = 0;


            // Cancel Auction House Item
            CancelItemAuction(buyer: buyer, itemId: 1);
            Console.WriteLine($"Auction cancelled.");

            // Print out the money that person has
            Console.WriteLine($"Persons Stash has {buyer.Stash.StashMoney} gold");
            Console.WriteLine($"Person has {buyer.Money} gold");
            Console.ReadLine();
        }


        public static void AddBuyOrder(Person buyer, int itemId, uint amount, uint price)
        {
            var auctionItem = new AuctionItem();
            auctionItem.ItemId = itemId;
            auctionItem.Price = price;
            auctionItem.Seller = buyer;
            auctionItem.Amount = amount;
            AuctionItems.Add(auctionItem);

            // Remove money from buyer
            buyer.Money -= price * amount;
        }

        public static void CancelItemAuction(Person buyer, uint itemId)
        {
            var auctionItem = AuctionItems
                .Where(x => x.ItemId == itemId)
                .FirstOrDefault();

            // return gold to the buyer
            buyer.Money += auctionItem.Price * auctionItem.Amount;

            // remove auction item
            AuctionItems.Remove(auctionItem);
        }
    }

    public class Stash
    {
        public ulong StashMoney { get; set; }
    }

    public class Person
    {
        public Stash Stash { get; set; }
        public ulong Money { get; set; }
    }

    public class AuctionItem
    {
        public uint Amount { get; set; }

        public Person Seller { get; set; }

        public uint Price { get; set; }

        public int ItemId { get; set; }
    }
}

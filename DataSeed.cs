using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System;
using System.Linq;
using NgApi.Models;

namespace NgApi
{
    public class DataSeed
    {
        private readonly ApiContext apiContext;
        public DataSeed(ApiContext ctx)
        {
            apiContext = ctx;
        }

        public void SeedData(int nCustomers, int nOrders)
        {
            if (!apiContext.Customers.Any())
            {
                SeedCustomers(nCustomers);
                apiContext.SaveChanges();

            }
            if (!apiContext.Orders.Any())
            {
                SeedOrders(nOrders);
                apiContext.SaveChanges();

            }
            if (!apiContext.Servers.Any())
            {
                SeedServers();
                apiContext.SaveChanges();

            }

        }

        private void SeedOrders(int nOrders)
        {
            var orders = BuildOrderList(nOrders);
            foreach (var order in orders)
            {
                apiContext.Orders.Add(order);
            }
        }

        private void SeedServers()
        {
            List<Server> servers = BuildServerList();
            foreach (var server in servers)
            {
                apiContext.Servers.Add(server);
            }
        }



        private void SeedCustomers(int nCustomers)
        {
            var customers = BuildCustomerList(nCustomers);
            foreach (var customer in customers)
            {
                apiContext.Customers.Add(customer);
            }
        }

        private List<Customer> BuildCustomerList(int nCustomers)
        {
            var customers = new List<Customer>();
            var names = new List<string>();
            for (int i = 1; i < nCustomers; i++)
            {
                var name = Helpers.MakeUniqueCustomerName(names);
                names.Add(name);
                customers.Add(new Customer
                {
                    Id = i,
                    Name = name,
                    Email = Helpers.MakeCustomerEmail(name),
                    State = Helpers.GetRandomState()
                });
            }
            return customers;
        }

        private List<Order> BuildOrderList(int nOrders)
        {
            var orders = new List<Order>();
            var rand = new Random();
            for (int i = 1; i < nOrders; i++)
            {
                int randCustomerId = rand.Next(1, apiContext.Customers.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var complited = Helpers.GetRandomOrderComplited(placed);
                var customers = apiContext.Customers.ToList();
                orders.Add(new Order
                {
                    Id = i,
                    Customer = customers.First(c => c.Id == randCustomerId),
                    OrderTotal = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Complited = complited
                });
            }
            return orders;
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>()
            {
                new Server
                {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = false
                },
                new Server
                {
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true
                },
                new Server
                {
                    Id = 4,
                    Name = "QA-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 5,
                    Name = "QA-Mail",
                    IsOnline = false
                },
                new Server
                {
                    Id = 6,
                    Name = "QA-Services",
                    IsOnline = true
                },
                new Server
                {
                    Id = 7,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                new Server
                {
                    Id = 8,
                    Name = "Prod-Mail",
                    IsOnline = false
                },
                new Server
                {
                    Id = 9,
                    Name = "Prod-Services",
                    IsOnline = true
                },
            };
        }
    }
}
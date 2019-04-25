using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RailWayCorporationApp
{
    class Program
    {
        public static int IntParser(int from, int to = 2147483647)
        {
            int result;
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out result) && result >= from && result <= to)
                {
                    return result;
                }
            }
        }

        static void Main(string[] args)
        {
            // AddData();
            using (var context = new AppContext())
            {
                int wayId = 1;
                foreach (var way in context.Ways)
                {
                    bool hasFreePlaces = false;
                    foreach (var carriage in context.Carriages.Where(carriage => carriage.Train.Id == way.Train.Id))
                    {
                        var places = context.Places.Where(place => place.Carriage.Id == carriage.Id && place.IsRent == false);
                        hasFreePlaces = true;
                        break;
                    }
                    if (hasFreePlaces)
                    {
                        Console.WriteLine("Id - " + wayId);
                        Console.WriteLine("Из " + way.DepartureCity);
                        Console.WriteLine("В " + way.ArrivalCity);
                        Console.WriteLine("Отправление " + way.DepartureDate);
                        Console.WriteLine("Приезд " + way.ArrivalDate);
                        Console.WriteLine("Номер поезда " + way.Train.TrainNumber);
                        Console.WriteLine();
                    }
                    wayId++;
                }
                Console.WriteLine("Введите Id маршрута для бронирования");

                int chouse = IntParser(1, context.Ways.Count());

                var ticket = new Ticket();

                wayId = 1;
                foreach (var way in context.Ways)
                {
                    if (wayId == chouse)
                    {
                        foreach (var carriage in context.Carriages.Where(carriage => carriage.Train.Id == way.Train.Id))
                        {
                            var places = context.Places.Where(place => place.Carriage.Id == carriage.Id && place.IsRent == false).ToList();
                            
                            places[0].IsRent = true;

                            ticket.Place = places[0];

                            ticket.Way = way;

                            break;
                        }
                    }
                    wayId++;
                }

                User user = new User();
                
                Console.WriteLine("Введите номер телефона");
                user.PhoneNumber = Console.ReadLine();

                bool isExists = false;

                foreach (var existedUser in context.Users)
                {
                    if (existedUser.PhoneNumber == user.PhoneNumber)
                    {
                        ticket.User = existedUser;
                        isExists = true;
                    }
                }

                if (!isExists)
                {
                    Console.WriteLine("Данный номер телефона не зарегистрирован!");
                    Console.WriteLine("Введите Ваше имя");
                    user.Name = Console.ReadLine();

                    ticket.User = user;

                    context.Users.Add(user);
                }

                context.Tickets.Add(ticket);

                context.SaveChanges();

                Console.WriteLine("Билет успешно забронирован!");
                Console.WriteLine("Для выхода нажмите на Enter");
                Console.ReadLine();
            }
        }


        static void AddData()
        {
            using (var context = new AppContext())
            {
                List<Train> trains = new List<Train>
                {
                    new Train
                    {
                        TrainNumber = "145A"
                    },

                    new Train
                    {
                        TrainNumber = "9"
                    },

                    new Train
                    {
                        TrainNumber = "785"
                    }
                };

                foreach (var train in trains)
                {
                    context.Trains.Add(train);
                }

                List<Carriage> carriagesOfFirstTrain = new List<Carriage>();
                for (int i = 0; i < 5; i++)
                {
                    carriagesOfFirstTrain.Add(new Carriage { Train = trains[0] });
                }

                foreach (var carriage in carriagesOfFirstTrain)
                {
                    context.Carriages.Add(carriage);
                }

                List<Carriage> carriagesOfSecondTrain = new List<Carriage>();
                for (int i = 0; i < 6; i++)
                {
                    carriagesOfFirstTrain.Add(new Carriage { Train = trains[1] });
                }

                foreach (var carriage in carriagesOfSecondTrain)
                {
                    context.Carriages.Add(carriage);
                }

                List<Carriage> carriagesOfThirdTrain = new List<Carriage>();
                for (int i = 0; i < 3; i++)
                {
                    carriagesOfFirstTrain.Add(new Carriage { Train = trains[2] });
                }

                foreach (var carriage in carriagesOfThirdTrain)
                {
                    context.Carriages.Add(carriage);
                }


                foreach (var carriage in carriagesOfFirstTrain)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        context.Places.Add(new Place { Carriage = carriage });
                    }
                }

                foreach (var carriage in carriagesOfSecondTrain)
                {
                    for (int i = 0; i < 30; i++)
                    {
                        context.Places.Add(new Place { Carriage = carriage });
                    }
                }

                foreach (var carriage in carriagesOfThirdTrain)
                {
                    for (int i = 0; i < 15; i++)
                    {
                        context.Places.Add(new Place { Carriage = carriage });
                    }
                }


                Way almatyAstanaWay = new Way
                {
                    DepartureCity = "Алматы",
                    ArrivalCity = "Нур-Султан",

                    DepartureDate = DateTime.Now,
                    ArrivalDate = DateTime.Now,

                    Train = trains[0]
                };

                Way MoskowAstanaWay = new Way
                {
                    DepartureCity = "Москва",
                    ArrivalCity = "Нур-Султан",

                    DepartureDate = DateTime.Now,
                    ArrivalDate = DateTime.Now,

                    Train = trains[1]
                };

                Way OmskKaragandaWay = new Way
                {
                    DepartureCity = "Омск",
                    ArrivalCity = "Караганда",

                    DepartureDate = DateTime.Now,
                    ArrivalDate = DateTime.Now,

                    Train = trains[2]
                };

                context.Ways.AddRange(new Way[] { almatyAstanaWay, MoskowAstanaWay, OmskKaragandaWay });

                context.SaveChanges();

            }
        }
    }
}

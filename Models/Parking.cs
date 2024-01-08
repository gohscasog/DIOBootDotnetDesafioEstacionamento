using System;
using System.IO;
using System.Collections.Generic;

namespace Project.Models
{
    class Parking
    {
        public Parking(){}
        public Parking(double duty, double hour)
        {
            this.duty = duty;
            this.hour = hour;
        }

        public double DutyPrice
        {
            get => duty; 
            set
            {
                if(value < 0)
                {
                    throw new InvalidDataException();
                }

                duty = value;
            }
        }
        public double HourPrice
        {
            get => hour; 
            set
            {
                if(value < 0)
                {
                    throw new InvalidDataException();
                }

                hour = value;
            }
        }

        public string ReadPlate()
        {
            Console.WriteLine("Digite a placa (AAA-0000|AAA-0A00):");
            Console.Write(">");

            var plate = Console.ReadLine();

            Console.WriteLine();

            if(plate == null)
            {
                return string.Empty;
            }

            if(plate.Length != 8)
            {
                return string.Empty;
            }

            plate = plate.ToUpper();

            // Converte placa antiga para Mercosul
            if(plate[5] >= '0' && plate[5] <= '9')
            {
                char[] c = plate.ToCharArray();
                c[5] = (char)(c[5] + 17);
                plate = new string(c);
            }

            if(
                plate[3] != '-' ||
                plate[0] < 'A' || plate[0] > 'Z' || 
                plate[1] < 'A' || plate[1] > 'Z' || 
                plate[2] < 'A' || plate[2] > 'Z' || 
                plate[5] < 'A' || plate[5] > 'J' || 
                plate[6] < '0' || plate[6] > '9' ||
                plate[7] < '0' || plate[7] > '9')
            {
                return string.Empty;
            }

            return plate;
        }
        public string CheckPlate()
        {
            string plate = ReadPlate();

            if(vehicles.TryGetValue(plate, out var v))
            {
                return plate;
            }

            return string.Empty;
        }
        public void AddVehicle(string plate, DateTime time)
        {
            if(plate == string.Empty)
            {
                throw new InvalidDataException();
            }

            vehicles.Add(plate, time);
        }
        public void DelVehicle(string plate)
        {
            if(plate == string.Empty)
            {
                throw new KeyNotFoundException();
            }

            vehicles.Remove(plate);
        }
        public void GetVehicle(string plate)
        {
            if(plate == string.Empty)
            {
                throw new KeyNotFoundException();
            }

            PrintHeader();

            Console.WriteLine(
                $"{plate}\t{vehicles[plate].ToString("dd/MMM HH:mm")}");
        }
        public void ListVehicles()
        {
            PrintHeader();

            foreach(var v in vehicles)
            {
                Console.WriteLine(
                    $"{v.Key}\t{v.Value.ToString("dd/MMM HH:mm")}");
            }
        }
        public double GetPeriod(string plate)
        {
            return (DateTime.UtcNow - vehicles[plate]).TotalHours;
        }
        public double Checkout(string plate, double period)
        {
            return period * hour + duty;
        }

        void PrintHeader()
        {
            Console.WriteLine("Placa   \tEntrada");
        }

        double duty = 5.0;
        double hour = 10.0;
        Dictionary<string, DateTime> vehicles = [];
    }
}

using Microsoft.VisualBasic;

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

        public string VerifyPlate()
        {
            Console.Write("Digite a placa (AAA-0000|AAA-0A00):\n>");

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

            // Converte placa Mercosul para antiga
            if(plate[5] >= 'A' && plate[5] <= 'J')
            {
                char[] c = plate.ToCharArray();
                c[5] = (char)(c[5] - 17);
                plate = new string(c);
            }

            for(int i = 0; i < 3; i++)
            {
                if(plate[i] < 'A' || plate[i] > 'Z')
                {
                    return string.Empty;
                }
            }

            if(plate[3] != '-')
            {
                return string.Empty;
            }

            for(int i = 4; i < plate.Length; i++)
            {
                if(plate[i] < '0' || plate[i] > '9')
                {
                    return string.Empty;
                }
            }

            return plate;
        }

        public string GetPlate()
        {
            string plate = VerifyPlate();

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

            vehicles.Add(plate.ToUpper(), time);
        }

        public void DelVehicle(string plate)
        {
            if(plate == string.Empty)
            {
                throw new KeyNotFoundException();
            }

            vehicles.Remove(plate.ToUpper());
        }

        public void GetVehicle(string plate)
        {
            if(plate == string.Empty)
            {
                throw new KeyNotFoundException();
            }

            PrintHead();

            Console.WriteLine(
                $"{plate}\t{vehicles[plate].ToString("dd/MMM HH:mm")}");
        }

        public void ListVehicles()
        {
            PrintHead();

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

        private void PrintHead()
        {
            Console.WriteLine("Placa   \tEntrada");
        }

        double duty = 5.0;
        double hour = 10.0;
        Dictionary<string, DateTime> vehicles = [];
    }
}

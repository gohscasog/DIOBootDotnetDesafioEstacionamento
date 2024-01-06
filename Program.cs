using System.Globalization;
using Project.Models;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Parking lot = new();

        // Amostra para teste
        lot.AddVehicle("abc-1234", DateTime.UtcNow.AddHours(-1.0));
        lot.AddVehicle("MAC-0420", DateTime.UtcNow.AddHours(-12.0));
        lot.AddVehicle("SEX-6969", DateTime.UtcNow.AddHours(-24.0));
        lot.AddVehicle("Cap-0666", DateTime.UtcNow.AddHours(-48.0));

        Console.WriteLine();

        int opcode = 0;
        while(true)
        {
            Console.Clear();

            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1. Check in");
            Console.WriteLine("2. Remover entrada");
            Console.WriteLine("3. Check out");
            Console.WriteLine("4. Status");
            Console.WriteLine("5. Listar entradas");
            Console.WriteLine("6. Sair");
            Console.Write(">");

            opcode = int.TryParse(Console.ReadLine(), out int i) ? i : 0;
            Console.Clear();

            try
            {
                switch(opcode)
                {
                    case 1:
                        lot.AddVehicle(lot.VerifyPlate(), DateTime.UtcNow);
                        Console.WriteLine("Veículo adicionado com sucesso");
                        break;
                    case 2:
                        lot.DelVehicle(lot.GetPlate()); 
                        Console.WriteLine("Veículo removido com sucesso");
                        break;
                    case 3:
                        {
                            string plate = lot.GetPlate();
                            double time = Math.Ceiling(lot.GetPeriod(plate));
                            double total = lot.Checkout(plate, time);

                            Console.WriteLine($"Período: {time:N0} hora(s)");
                            Console.WriteLine(
                                $"Valor a pagar: {total.ToString("C2", 
                                CultureInfo.CreateSpecificCulture("pt-BR"))}"
                            );
                            lot.DelVehicle(plate);
                        }
                        break;
                    case 4:
                        lot.GetVehicle(lot.GetPlate());
                        break;
                    case 5:
                        lot.ListVehicles();
                        break;
                    case 6:
                        Console.WriteLine("Volte sempre!\n");
                        return;
                    default:
                        Console.WriteLine("Operação inválida");
                        break;
                }
            }
            catch(InvalidDataException)
            {
                Console.WriteLine("Formato inválido");
            }
            catch(KeyNotFoundException)
            {
                Console.WriteLine("Veículo não encontrado");
            }
            catch
            {
                Console.WriteLine("Um erro inesperado aconteceu");
            }

            Console.WriteLine("\nPressione ENTER para voltar ao menu");
            Console.ReadLine();
        }
    }
}
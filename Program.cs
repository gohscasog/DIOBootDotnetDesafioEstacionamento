using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using Project.Models;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Parking lot = new();

        // Amostra para teste
        lot.AddVehicle("ABC-1D34", DateTime.UtcNow.AddHours(-1.69));
        lot.AddVehicle("MAC-4A20", DateTime.UtcNow.AddHours(-12.666));
        lot.AddVehicle("SEX-2A69", DateTime.UtcNow.AddHours(-24.20));
        lot.AddVehicle("CAP-6C66", DateTime.UtcNow.AddHours(-45.13));

        Console.WriteLine();

        int opcode = 0;
        while(true)
        {
            Console.Clear();

            Console.WriteLine("Digite a sua opção:");
            Console.WriteLine("1. Check in");
            Console.WriteLine("2. Remover entrada");
            Console.WriteLine("3. Check out");
            Console.WriteLine("4. Exibir status");
            Console.WriteLine("5. Listar entradas");
            Console.WriteLine("6. Configurar");
            Console.WriteLine("7. Sair");
            Console.Write(">");

            opcode = int.TryParse(Console.ReadLine(), out int i) ? i : 0;
            Console.Clear();

            try
            {
                switch(opcode)
                {
                    case 1:
                        lot.AddVehicle(lot.ReadPlate(), DateTime.UtcNow);
                        Console.WriteLine("Veículo adicionado com sucesso");
                        break;
                    case 2:
                        lot.DelVehicle(lot.CheckPlate()); 
                        Console.WriteLine("Veículo removido com sucesso");
                        break;
                    case 3:
                        {
                            string plate = lot.CheckPlate();
                            double time = Math.Ceiling(lot.GetPeriod(plate));
                            double total = lot.Checkout(plate, time);

                            Console.WriteLine($"Período: {time:N0} hora(s)");
                            Console.WriteLine
                            (
                                $"Valor a pagar: {total.ToString("C2", 
                                CultureInfo.CreateSpecificCulture("pt-BR"))}"
                            );
                            lot.DelVehicle(plate);
                        }
                        break;
                    case 4:
                        lot.GetVehicle(lot.CheckPlate());
                        break;
                    case 5:
                        lot.ListVehicles();
                        break;
                    case 6:
                        Console.WriteLine
                        (
                            $"Taxa: {lot.DutyPrice.ToString("C2", 
                            CultureInfo.CreateSpecificCulture("pt-BR"))}"
                        );
                        Console.WriteLine
                        (
                            $"Hora: {lot.HourPrice.ToString("C2", 
                            CultureInfo.CreateSpecificCulture("pt-BR"))}"
                        );
                        Console.WriteLine();
                        Console.WriteLine("Digite o valor e/ou ENTER para continuar");
                        Console.Write("Taxa: ");

                        if(double.TryParse(Console.ReadLine(), out double duty))
                        {
                            lot.DutyPrice = duty;
                            Console.WriteLine("Valor alterado com sucesso\n");
                        }

                        Console.Write("Hora: ");

                        if(double.TryParse(Console.ReadLine(), out double hour))
                        {
                            lot.HourPrice = hour;
                            Console.WriteLine("Valor alterado com sucesso\n");
                        }
                        break;
                    case 7:
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

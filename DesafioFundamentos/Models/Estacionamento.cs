using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        private bool ValidarPlaca(string placa)
        {
            // L representa uma letra maiúscula de A a Z
            // formato antigo: LLL-DDDD
            string regexAntiga = @"^[A-Z]{3}-[0-9]{4}$";
            // formato Mercosul: LLLDLDD
            // Não permite letras I, O e Q nas 3 primeiras posições
            string regexMercosul = @"^[A-HJ-NPR-Z]{3}[0-9][A-Z][0-9]{2}$";

            if (Regex.IsMatch(placa, regexAntiga) || Regex.IsMatch(placa, regexMercosul))
            { 
                return true;
            }

            return false;
        }

        public void AdicionarVeiculo()
        {
            Console.Write("Digite a placa do veículo para estacionar: ");
            string placa = Console.ReadLine().Trim().ToUpper();
            if(veiculos.Any(x => x == placa))
            {
                Console.WriteLine("Veículo já está estacionado!");
                return;
            }

            bool placaValida = ValidarPlaca(placa);
            if (placaValida)
            {
                Console.WriteLine("Placa válida! Veículo estacionado.");
                veiculos.Add(placa.ToUpper());
            }
            else { 
                Console.WriteLine("Placa inválida! Entrada Bloqueada.");
            }
        }

        public void RemoverVeiculo()
        {
            if(veiculos.Count == 0)
            {
                Console.WriteLine("Não há veículos estacionados para remover.");
                return;
            }

            Console.Write("Digite a placa do veículo para remover: ");
            string placa = Console.ReadLine().ToUpper();

            // Verifica se o veículo existe
            if (veiculos.Any(x => x == placa))
            {
                string horasInformadas = "";
                while (horasInformadas == string.Empty || !horasInformadas.All(c => char.IsDigit(c)))
                {
                    Console.Write("Digite a quantidade de horas que o veículo permaneceu estacionado: ");
                    horasInformadas = Console.ReadLine().Trim();
                    if (horasInformadas == string.Empty || !horasInformadas.All(c => char.IsDigit(c)))
                    {
                        Console.WriteLine("Entrada inválida! Por favor, digite um número válido de horas.\n");
                    }
                }

                int horas = int.Parse(horasInformadas);
                decimal valorTotal = precoInicial + (precoPorHora * horas); 

                veiculos.Remove(placa);

                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal:F2}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach (string veiculo in veiculos)
                {
                    Console.WriteLine(veiculo);
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
